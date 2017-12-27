using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WyCo.PopUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DialogoConexion : PopupPage
    {
        public DialogoConexion()
        {
            InitializeComponent();

            Ok.TextColor = Color.White;
            Ok.BackgroundColor = Color.FromHex("#C52224");
            Fondo.BackgroundColor = Color.FromHex("#E9DED0");
        }

        private void Ok_Clicked(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
        }
    }
}