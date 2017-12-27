using Plugin.Connectivity;
using Rg.Plugins.Popup.Extensions;
using WyCo.PopUp;
using Xamarin.Forms;

namespace WyCo.Services
{
    class ComprobarConexion : ContentPage
    {
        public ComprobarConexion()
        {

        }

        public bool Conexion()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                Navigation.PushPopupAsync(new DialogoConexion());
            }
            return CrossConnectivity.Current.IsConnected;
        }
    }
}
