using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WyCo.PopUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DialogRegistrado : PopupPage
    {
        public DialogRegistrado()
        {
            InitializeComponent();
            label.TextColor = Color.Black;
            Registrar.TextColor= Color.White;
            Cancelar.TextColor= Color.White;

            Registrar.BackgroundColor = Color.FromHex("#C52224");
            Cancelar.BackgroundColor = Color.FromHex("#C52224");

            Fondo.BackgroundColor = Color.FromHex("#E9DED0");
        }

        private void Registrar_Clicked(object sender, EventArgs e)
        {
            LoginPromos l = new LoginPromos();
            ((NavigationPage)this.Parent).PushAsync(l);
            Navigation.PopPopupAsync();
            Navigation.RemovePage(l);
        }

        private void Cancelar_Clicked(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
        }
    }
}