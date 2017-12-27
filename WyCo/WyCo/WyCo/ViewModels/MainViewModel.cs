using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WyCo.ViewModels
{
    public class MainViewModel
    {
        #region properties
        public ObservableCollection<MenuItemViewModels> Menu { get; set; }
        #endregion

        public MainViewModel()
        {
            Menu = new ObservableCollection<MenuItemViewModels>();
            LoadMenu();
        }

        private void LoadMenu()
        {
            Menu.Add(new MenuItemViewModels
            {
                PageName = "Promociones",
                Title = "Promociones",
                Color = "#94939F"

            });

            Menu.Add(new MenuItemViewModels
            {
                PageName = "Carta",
                Title = "Carta",
                Color= "#93CDCF"
            });

            Menu.Add(new MenuItemViewModels
            {
                PageName = "Restaurantes",
                Title = "Restaurantes",
                Color = "#EC99B1"
            });

            Menu.Add(new MenuItemViewModels
            {
                PageName = "Login",
                Title = "Mi Perfil",
                Color= "#EFAC9C"
            });
        }
    }
}
