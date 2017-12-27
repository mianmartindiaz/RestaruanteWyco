using Plugin.Geolocator;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WyCo.PopUp;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace WyCo.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Restaurantes : ContentPage
    {
        public Restaurantes()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            MostrarMenu();
            ImgAtras();
        }

        public void MostrarMenu()
        {
            var Rect = new Rectangle(Width, Y, Width, Height);
            this.LayoutTo(Rect, 250, Easing.CubicIn);
        }


        protected override void OnAppearing()
        {
            var Latitud = 0.0;
            var Longitud = 0.0;
            var direccion = "";
            var localidad = "";

            Device.BeginInvokeOnMainThread(async () =>
            {
                BD.ObtenerResul obtenerResul = new BD.ObtenerResul();
                var img = await obtenerResul.GetList<BD.ClasesBd>("http://tools.agorafranquicias.com/APP/Consultas/getLocales.php");
                if (img != null)
                {
                    foreach (BD.Locales locales in img.Local)
                    {
                        Latitud = locales.Lat;
                        Longitud = locales.Log;
                        direccion = locales.Direccion;
                        localidad = locales.Ciudad;
                        LlenarPins(Latitud, Longitud, direccion, localidad);
                    }
                    Locator();
                }
                else
                {
                    await Navigation.PushPopupAsync(new DialogoServidor());
                    Navigation.RemovePage(this);
                }
            });
        }


        //El método LlenarPins es el encargado de hacer que cuando muestres el mapa
        //Salgan los globos con información sobre ese mapa y así poder saber cual de 
        //es la dirección etc... de los restaurantes que hay.
        public void LlenarPins(double latitud, double longitud, string direccion, string localidad)
        {
            var position1 = new Position(latitud, longitud);
            var pin1 = new Pin
            {
                Type = PinType.Place,
                Position = position1,
                Label = "Restaurante WyCo " + localidad,
                Address = direccion
            };
            MyMap.Pins.Add(pin1);
        }
        //El método Locator es el encargado de hacer la geolocalización
        //es decir, saber el punto exacto donde se encuantra la persona
        //con la aplicación en ese momento.
        public async void Locator()
        {
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                var location = await locator.GetPositionAsync(timeoutMilliseconds: 10000);
                var position = new Position(location.Latitude, location.Longitude);
                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(.3)));
            }
            catch (Exception e)
            {
                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(39.470018, -6.374965), Distance.FromKilometers(.1)));
                await Navigation.PushPopupAsync(new DialogoUbicacion());
            }
        }

        private void ImgAtras()
        {
            var TapAtras = new TapGestureRecognizer();
            TapAtras.Tapped += TapVolverAtras;
            atras.GestureRecognizers.Add(TapAtras);
        }

        private void TapVolverAtras(object sender, EventArgs e)
        {
            Navigation.RemovePage(this);
        }
    }
}
