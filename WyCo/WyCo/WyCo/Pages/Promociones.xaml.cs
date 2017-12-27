using System;
using WyCo.Sqlite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Extensions;
using WyCo.PopUp;
using WyCo.Services;

namespace WyCo.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Promociones : ContentPage
	{
        private DataAccess datos = new DataAccess();
        private DateTime diahora = DateTime.Now;

        public Promociones ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            ImgAtras();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (new ComprobarConexion().Conexion())
                {
                    var datos = new DataAccess();
                    int num = datos.getCountUsuario();
                    if (num > 0)
                    {
                        BD.ObtenerResul obtenerResul = new BD.ObtenerResul();
                        var img = await obtenerResul.GetList<BD.ClasesBd>("http://tools.agorafranquicias.com/APP/Consultas/getPromociones.php");
                        if (img != null)
                        {
                            foreach (BD.Promociones promos in img.Promociones)
                            {
                                if (!promos.Dia.Contains(diahora.DayOfWeek.ToString()))
                                {
                                    promos.imagenBloqueada = "Bloqueado.png";
                                }
                            }
                            myList.FlowItemsSource = img.Promociones;
                            Registrate.IsVisible = false;
                        }
                        else
                        {
                            await Navigation.PushPopupAsync(new DialogoServidor());
                        }
                    }
                    else
                    {
                        BD.ObtenerResul obtenerResul = new BD.ObtenerResul();
                        var img = await obtenerResul.GetList<BD.ClasesBd>("http://tools.agorafranquicias.com/APP/Consultas/getPromociones.php");
                        if (img != null)
                        {
                            foreach (BD.Promociones promos in img.Promociones)
                            {
                                if (promos.Premium == 1 || !promos.Dia.Contains(diahora.DayOfWeek.ToString()))
                                {
                                    promos.imagenBloqueada = "Bloqueado.png";
                                }


                            }
                            myList.FlowItemsSource = img.Promociones;
                            Registrate.Source = "http://tools.agorafranquicias.com/APP/Images/Promociones/BotonRegistrate.png";
                            BtnRegistrar();
                        }
                        else
                        {
                            await Navigation.PushPopupAsync(new DialogoServidor());
                        }
                    }
                }
            });
        
        }

        private void BtnRegistrar()
        {
            var TapRegistro = new TapGestureRecognizer();
            TapRegistro.Tapped += TapRegistrate;
            Registrate.GestureRecognizers.Add(TapRegistro);
        } 

        private void TapRegistrate(object sender, EventArgs e)
        {
            ((NavigationPage)this.Parent).PushAsync(new LoginPromos());
            Navigation.RemovePage(this);
        }

        private void FlowListView_OnFlowItemTapped(object sender, ItemTappedEventArgs e)
        {
            var prm = (BD.Promociones)e.Item;
            var datos = new DataAccess();
            int num = datos.getCountUsuario();
            if (num > 0)
            {
                if (!prm.Dia.Contains(diahora.DayOfWeek.ToString()))
                {
                     Navigation.PushPopupAsync(new DialogoNoDia());
                }
                else
                {
                    ((NavigationPage)this.Parent).PushAsync(new Promocion(prm.Id));
                }
            }
            else
            {
                if (prm.Premium == 1)
                {
                    Navigation.PushPopupAsync(new DialogRegistrado());
                }
                else
                {
                    if (!prm.Dia.Contains(diahora.DayOfWeek.ToString()))
                    {
                        Navigation.PushPopupAsync(new DialogoNoDia());
                    }
                    else
                    {
                        ((NavigationPage)this.Parent).PushAsync(new Promocion(prm.Id));
                    }
                }
            }
        }
        private void FlowListView_OnFlowItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            DisplayAlert("OnFlowItemAppearing", "OnFlowItemAppearing", "ok");
        }
        private void FlowListView_OnFlowItemDisappearing(object sender, ItemVisibilityEventArgs e)
        {
            DisplayAlert("OnFlowItemDisappearing", "OnFlowItemDisappearing", "ok");
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