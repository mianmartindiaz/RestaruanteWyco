using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WyCo.Services;

namespace WyCo.ViewModels
{
    public class MenuItemViewModels
    {
        private NavigationService navigationService;

        public MenuItemViewModels()
        {
            navigationService = new NavigationService();
        }


        public string Title { get; set; }
        public string PageName { get; set; }
        public string Color { get; set; }
        public ICommand NavigateCommand { get { return new RelayCommand(Navigate); }}

        private async void Navigate()
        {
            await navigationService.Navigate(PageName);
        }
    }
}
