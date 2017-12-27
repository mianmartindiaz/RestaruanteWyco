using DLToolkit.Forms.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WyCo.Pages;
using Xamarin.Forms;
using Plugin.FirebasePushNotification;

namespace WyCo
{
    public partial class App : Application
    {
        public static NavigationPage Navigator { get; internal set; }
        public static MasterPage Master { get; internal set; }
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new Animacion());
            FlowListView.Init();


            //Notificaciones al iniciar la aplicación

            /*CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Token:",$"TOKEN : {p.Token}");
            };*/
            /*CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {

                System.Diagnostics.Debug.WriteLine("Received");

            };*/
          /*  CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Opened");
                foreach (var data in p.Data)
                {
                    System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                }

                if (!string.IsNullOrEmpty(p.Identifier))
                {
                    System.Diagnostics.Debug.WriteLine($"ActionId: {p.Identifier}");
                }

            };*/

        }

       
        public async static Task NavigateToProfile(BD.FacebookProfile obj)
        {
            await Navigator.PopToRootAsync(false);
            await Navigator.PushAsync(new Datos(obj));
        }

        public async static Task NavigateToPromos()
        {
            await Navigator.PopToRootAsync(false);
            await Navigator.PushAsync(new Promociones());
        }
        public async static Task VueltaAtras()
        {
            await Navigator.PopToRootAsync(false);
        }
        public async static Task VolverPromociones()
        {
            await Navigator.PopToRootAsync(false);
            await Navigator.PushAsync(new Promociones());
        }
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}