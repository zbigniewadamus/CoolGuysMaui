using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CoolGuys.UseCases._contracts
{
    public class Post:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public int Id { get; set; }
        public string NameSlug { get; set; }
        public string Description { get; set; }
        public int? Score { get; set; }
        public string ImageUrl { get; set; }
        public string AvatarUrl { get; set; }
    }
}
