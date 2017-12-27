using Rg.Plugins.Popup.Extensions;
using System;
using WyCo.PopUp;
using WyCo.Tabbed;
using WyCo.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WyCo.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPage : ContentPage
    {
        private int Contador;
        private int Slide;

        public UserPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            MetodoBotones();
            Device.StartTimer(TimeSpan.FromSeconds(1), TimerCallBack);
        }
        private bool TimerCallBack()
        {
            Slide = Carousel.Position;
            return false;
        }

        protected void MetodoBotones()
        {
            var tapimage = new TapGestureRecognizer();
            tapimage.Tapped += tapimageCarta;
            Carta.GestureRecognizers.Add(tapimage);

            var tapimagePromo = new TapGestureRecognizer();
            tapimagePromo.Tapped += tapimagePromocion;
            Promociones.GestureRecognizers.Add(tapimagePromo);

            var tapimageRes = new TapGestureRecognizer();
            tapimageRes.Tapped += tapimageRestaurante;
            Restaurantes.GestureRecognizers.Add(tapimageRes);
        }

        private void OnPrev(object sender, EventArgs e)
        {

            if (Slide != 0)
            {
                Slide = Slide - 1;
                Carousel.Position = Slide;
            }
            else
            {
                Slide = Contador;
            }
        }

        private void OnNext(object sender, EventArgs e)
        {

            //System.Diagnostics.Debug.WriteLine("Contador: " + Contador);
            DefMenor.IsVisible = true;
            Slide++;
            if (Slide == Contador)
            {
                Slide = 0;
                Carousel.Position = Slide;
            }
            Carousel.Position = Slide;
        }

        void tapimageCarta(Object sender, EventArgs e)
        {
            if (new ComprobarConexion().Conexion())
            {
                ((NavigationPage)this.Parent).PushAsync(new Tabbed.Menu());
            }
        }
        void tapimagePromocion(Object sender, EventArgs e)
        {
            if (new ComprobarConexion().Conexion())
            {
                ((NavigationPage)this.Parent).PushAsync(new Promociones());
            }
        }
        void tapimageRestaurante(Object sender, EventArgs e)
        {
            if (new ComprobarConexion().Conexion())
            {
                ((NavigationPage)this.Parent).PushAsync(new Restaurantes());
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Device.BeginInvokeOnMainThread(async () =>
            {
                BD.ObtenerResul obtenerResul = new BD.ObtenerResul();
                var img = await obtenerResul.GetList<BD.ClasesBd>("http://tools.agorafranquicias.com/APP/Consultas/getCarousel.php");
                if (img != null)
                {

                    Carousel.IsVisible = true;
                    DefMayor.IsVisible = true;
                    Carousel.IsEnabled = true;

                    Carousel.ItemsSource = img.Imagen;
                    Contador = img.Imagen.Count;

                    var tapMayor = new TapGestureRecognizer();
                    tapMayor.Tapped += OnNext;
                    DefMayor.GestureRecognizers.Add(tapMayor);

                    var TapMenor = new TapGestureRecognizer();
                    TapMenor.Tapped += OnPrev;
                    DefMenor.GestureRecognizers.Add(TapMenor);
                }
                else
                {
                    await Navigation.PushPopupAsync(new DialogoServidor());
                }
            });
        }
    }
}