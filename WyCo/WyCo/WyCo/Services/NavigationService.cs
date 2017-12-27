using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WyCo.Pages;
using WyCo.Sqlite;
using WyCo.Tabbed;

namespace WyCo.Services
{
    public class NavigationService
    {
        public async Task Navigate(string pageName)
        {
            App.Master.IsPresented = false; 
            switch (pageName)
            {
                case "Restaurantes":
                    if (new ComprobarConexion().Conexion())
                    {
                        await App.Navigator.PopAsync();
                        await App.Navigator.PushAsync(new Restaurantes());
                    }
                    break;
                case "Promociones":
                    if (new ComprobarConexion().Conexion())
                    {
                        await App.Navigator.PopAsync();
                        await App.Navigator.PushAsync(new Promociones());
                    }
                    break;
                case "Login":
                    var datos = new DataAccess();
                    int num = datos.getCountUsuario();
                    if (num > 0)
                    {
                        await App.Navigator.PushAsync(new Datos());
                    }
                    else
                         if (new ComprobarConexion().Conexion())
                         {
                            await App.Navigator.PushAsync(new Login());
                         }
                    break;
                case "Carta":
                    if (new ComprobarConexion().Conexion())
                    {
                        await App.Navigator.PopAsync();
                        await App.Navigator.PushAsync(new Menu());
                    }
                    break;
            }
        }
    }
}
