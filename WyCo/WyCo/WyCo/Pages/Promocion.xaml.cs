using Rg.Plugins.Popup.Extensions;
using System;
using System.Linq;
using WyCo.PopUp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WyCo.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Promocion : ContentPage
	{
        private int Id;
        public Promocion ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);

		}
        public Promocion(int id)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.Id = id;
            BtnCruz();

        }
        protected void BtnCruz()
        {
            var TapImage = new TapGestureRecognizer();
            TapImage.Tapped += TapImgCruz;
            Cruz.GestureRecognizers.Add(TapImage); 
        }

        private void TapImgCruz(object sender, EventArgs e)
        {
            Navigation.RemovePage(this);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Device.BeginInvokeOnMainThread(async () => {
                BD.ObtenerResul obtenerResul = new BD.ObtenerResul();
                var producto = await obtenerResul.GetList<BD.ClasesBd>("http://tools.agorafranquicias.com/APP/Consultas/getPromocion.php?id=" + Id);
                if (producto != null)
                {
                    promo.Source = producto.ProductoPromo.ElementAtOrDefault(0).URL;
                }
                else
                {
                    await Navigation.PushPopupAsync(new DialogoServidor());
                }

            });
        }

       
    }
}