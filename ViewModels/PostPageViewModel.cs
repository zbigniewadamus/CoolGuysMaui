using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CoolGuys.UseCases._contracts;
using CoolGuys.UseCases.Post;
using CoolGuys.Views;
using Mopups.Services;
using CommunityToolkit.Maui.Alerts;
using System.Collections.ObjectModel;

namespace CoolGuys.ViewModels;

public partial class PostPageViewModel: ObservableObject
{
    private readonly CreatePost createPost;
    private int? index = 1;
    private readonly AddPostImage addPostImage;
    private readonly ShowPost showPost;
    private readonly VotePost votePost;
    private MemoryStream imageStream;
    [ObservableProperty] private string imagePath; 

    [ObservableProperty]
    private bool isRefreshing;

    [ObservableProperty]
    private string footerText = "Brak postów";

    [ObservableProperty] private ObservableCollection<Post> postList = new ObservableCollection<Post>();

    [ObservableProperty]
    private bool isCreating;

    [ObservableProperty] private string description;
    private bool blockAppend = false;

    public PostPageViewModel(CreatePost createPost, AddPostImage addPostImage, ShowPost showPost, VotePost votePost)
    {
        this.createPost = createPost;
        this.addPostImage = addPostImage;
        this.showPost = showPost;
        this.votePost = votePost;

        

        LoadPosts();
    }

    

    [RelayCommand]
    private async void LoadPosts(string generateNextPage = "0")
    {
        try
        {
            if (generateNextPage == "1")
            {
                if (blockAppend) return;
                IsRefreshing = true;
                index += 1;
                var tempList = new ObservableCollection<Post>(await showPost.Exec(index)) ?? new ObservableCollection<Post>();
                var tempCount = PostList.Count;
                foreach (Post post in tempList)
                {
                    PostList.Add(post);
                }
                blockAppend = tempCount == PostList.Count;

            }
            else
            {
                IsRefreshing = true;
                blockAppend = false;
                index = 1;
                PostList = new ObservableCollection<Post>(await showPost.Exec(index)) ?? new ObservableCollection<Post>();
                if (PostList.Count > 0)
                {
                    FooterText = "Wiêcej postów nie widaæ";
                }

                if (PostList.Count < 10) blockAppend = true;
            }

            IsRefreshing = false;
        }
        catch (Exception err)
        {
            await Toast.Make(err.Message, CommunityToolkit.Maui.Core.ToastDuration.Short, 14).Show();
            IsRefreshing = false;
            return;
        }
    }

    [RelayCommand]
    async void Settings()
    {        
        await Shell.Current.GoToAsync("settingsPage",true);
    }

    [RelayCommand]
    async void AddPost ()       
    {        
        await MopupService.Instance.PushAsync(new CreatePostPop(this));
    }

    [RelayCommand]
    async void SubmitPost()
    {
        if(string.IsNullOrEmpty(ImagePath))
        {
            await Toast.Make("Obrazek jest wymagany").Show();
            return;
        }
        try
        {
            IsCreating = true;
            int id = await createPost.Exec(new PostDto { Description = Description });
            await addPostImage.Exec(ImagePath, id);
            LoadPosts();
            IsCreating = false;
            await MopupService.Instance.PopAllAsync();
        }
        catch (Exception err)
        {
            IsCreating = false;
            await Toast.Make("B³¹d: " + err.Message).Show();
        }
    }

    [RelayCommand]
    async void PlusVote(int postId)
    {
        try
        {
            await votePost.Exec(postId, true);
            PostList.First(p => p.Id == postId).Score += 1;
            OnPropertyChanged(nameof(PostList));
        }
        catch (Exception err)
        {
            await Toast.Make("B³¹d" + err.Message).Show();
        }
    }
    
    [RelayCommand]
    async void MinusVote(int postId)
    {
        try
        {
            await votePost.Exec(postId, false);
            PostList.First(p => p.Id == postId).Score -= 1;
        }
        catch (Exception err)
        {
            await Toast.Make("B³¹d" + err.Message).Show();
        }
    }
    
    [RelayCommand]
    async void AddPhoto()
    {
        IsCreating = true;
        var image = await MediaPicker.PickPhotoAsync();
        if (image == null) return;
        ImagePath = image.FullPath; 
        IsCreating = false;
    }
}