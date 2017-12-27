using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using WyCo.BD;
using WyCo.PopUp;
using WyCo.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WyCo.Tabbed
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Domicilio : ContentPage
    {
        public Domicilio()
        {
            InitializeComponent();
            Proximamente();
            //Si queremos mostrar ya la carta de a domicilio,
            //lo único que hay que hacer es descomentar
            //esta parte de código, es decir, el método
            //domicilio(); y borrar el método Proximamente();

            //método
            //domicilio();
        }

        //Método que muestra la imagen proximamente de domicilio
        private void Proximamente(){
            
            Image proximamente = new Image()
            {
                Source = "proximamente.png",
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Rotation = 330
            };
           
            AbsoluteLayout prox = new AbsoluteLayout();
            AbsoluteLayout.SetLayoutBounds(proximamente, new Rectangle(-10, 100, 380, 400));
            prox.Children.Add(proximamente);


            this.Content = prox;
        }

        //Método que muestra toda la carta que hay para domicilio
        private void domicilio()
        {
            var template = new DataTemplate(typeof(DefaultTemplate));
            var view = new AccordionView(template);
            List<Section> ListaCategoria = new List<Section>();
            List<ShoppingCart> ListaProductos = null;

            view.SetBinding(AccordionView.ItemsSourceProperty, "List");
            view.Template.SetBinding(AccordionSectionView.TitleProperty, "Title");
            view.Template.SetBinding(AccordionSectionView.ItemsSourceProperty, "List");
            Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
            {
                ObtenerResul obtenerResul = new ObtenerResul();
                var img = await obtenerResul.GetList<ClasesBd>("http://tools.agorafranquicias.com/APP/Consultas/nombreImagenCategoriaDomicilio.php");
                var prod = await obtenerResul.GetList<ClasesBd>("http://tools.agorafranquicias.com/APP/Consultas/ShoppingCart.php");
                if (img != null)
                {
                    foreach (BD.NombreImagenCategoria Cat in img.Nombreimagencategoria)
                    {
                        ListaProductos = new List<ShoppingCart>();
                        foreach (ShoppingCart productos in prod.ShoppingCart)
                        {
                            if (Cat.Id == productos.Id_Categoria)
                            {
                                ListaProductos.Add(productos);
                            }
                        }
                        ListaCategoria.Add(new Section
                        {
                            Title = Cat.URL,
                            List = ListaProductos
                        });
                    }
                    view.BindingContext = new ViewModel
                    {
                        List = ListaCategoria
                    };
                    this.Content = view;
                }
                else
                {
                    await Navigation.PushPopupAsync(new DialogoError());
                }

                Image Llamar = new Image()
                {
                    Source = "http://tools.agorafranquicias.com/APP/Images/Otros/Llamar.jpg",
                    WidthRequest = 500,
                };


                var TapedLlamar = new TapGestureRecognizer();
                TapedLlamar.Tapped += TapImgLlamar;
                Llamar.GestureRecognizers.Add(TapedLlamar);

                AbsoluteLayout AbsoluteLayout = new AbsoluteLayout();

                AbsoluteLayout.SetLayoutBounds(Llamar, new Rectangle(55, 330, 250, 300));
                AbsoluteLayout.SetLayoutBounds(view, new Rectangle(0, 0, 380, 430));

                AbsoluteLayout.Children.Add(view);
                AbsoluteLayout.Children.Add(Llamar);


                this.Content = AbsoluteLayout;
            });
        }

        private void TapImgLlamar(object sender, EventArgs e)
        {
            ICallServices callServices = DependencyService.Get<ICallServices>();
            callServices.MakeCall();
        }
    }
}