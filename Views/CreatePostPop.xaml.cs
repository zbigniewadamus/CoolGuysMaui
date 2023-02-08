using CoolGuys.ViewModels;
using Mopups.Services;

namespace CoolGuys.Views;

public partial class CreatePostPop
{
	private string ImagePath;

	public CreatePostPop(PostPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext= viewModel;
	}

	protected override void OnDisappearing()
	{
		base.OnDisappearing();
		(BindingContext as PostPageViewModel).ImagePath = "";
		(BindingContext as PostPageViewModel).Description = "";
		
	}
}