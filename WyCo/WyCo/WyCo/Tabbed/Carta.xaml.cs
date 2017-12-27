using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using Rg.Plugins.Popup.Extensions;
using WyCo.PopUp;
using WyCo.BD;
using System.Linq;

namespace WyCo.Tabbed
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Carta : ContentPage
    {
        public Carta()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var template = new DataTemplate(typeof(DefaultTemplate));
            var view = new AccordionView(template);

            List<Section> ListaCategoria = new List<Section>();
            List<ShoppingCart> ListaProductos = null;

            view.SetBinding(AccordionView.ItemsSourceProperty, "List");
            view.Template.SetBinding(AccordionSectionView.TitleProperty, "Title");
            view.Template.SetBinding(AccordionSectionView.ItemsSourceProperty, "List");
            Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
            {
                BD.ObtenerResul obtenerResul = new BD.ObtenerResul();
                var img = await obtenerResul.GetList<BD.ClasesBd>("http://tools.agorafranquicias.com/APP/Consultas/nombreImagenCategoriaLocal.php");
                var prod = await obtenerResul.GetList<ClasesBd>("http://tools.agorafranquicias.com/APP/Consultas/ShoppingCart.php");
                if (img != null) {
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
            });
        }
    }
    public class Section
    {
        public string Title { get; set; }
        public IEnumerable<ShoppingCart> List { get; set; }
    }
    public class ViewModel
    {
        public IEnumerable<Section> List { get; set; }
    }
}