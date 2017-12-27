using System;
using WyCo.Sqlite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WyCo.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Datos : ContentPage
    {
        private DataAccess datos = new DataAccess();
        public Datos()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            nombreuser.Text = datos.GetUsuario().Name;
            imagenUser.Source = datos.GetUsuario().Picture;
            ImgAtras();
        }
        public Datos(BD.FacebookProfile facebook)
        {
            //este constructor está creado para los usuarios que meten Facebook
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            //el label (texto) nombre es igual a el nombre que nos da Facebook
            nombreuser.Text = facebook.Name;
            //y la imagen que nos da Facebook 
            imagenUser.Source = facebook.Picture.Data.Url;
            ImgAtras();
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

        //el metodo OnBackButtonPressed  es el encargado de el botón 
        //atrás del hardware del telefono que hará borrar la pagina datos
        //de la pila y así que haya una mejor fluidez entre ellas.
        protected override bool OnBackButtonPressed()
        {
            Navigation.PopAsync(false);
            return true;
        }
    }
}