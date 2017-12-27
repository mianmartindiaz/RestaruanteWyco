using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.App;
using Xamarin.Auth;
using WyCo;
using Newtonsoft.Json;
using WyCo.Sqlite;
using WyCo.BD;
using Newtonsoft.Json.Linq;
using Firebase.Iid;

[assembly: ExportRenderer(typeof(Login), typeof(WyCo.Droid.Services.LoginPageRenderer))]
namespace WyCo.Droid.Services
{
    class LoginPageRenderer : PageRenderer
    {
        public LoginPageRenderer()
        {
            var activity = Context as Activity;
            var auth = new OAuth2Authenticator(
                clientId: "168703917051703",
                //clientId: "525812214440244",
                scope: "",
                authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"),
                redirectUrl: new Uri("http://www.facebook.com/connect/login_success.html"));
            auth.Completed += async (sender, eventArgs) => {
                if (eventArgs.IsAuthenticated)
                {
                    var accessToken = eventArgs.Account.Properties["access_token"].ToString();
                    var expiresIn = Convert.ToDouble(eventArgs.Account.Properties["expires_in"]);
                    var expiryDate = DateTime.Now + TimeSpan.FromSeconds(expiresIn);
                    var request = new OAuth2Request("GET", new Uri("https://graph.facebook.com/me?fields=id,name,email,picture"), null, eventArgs.Account);
                    var response = await request.GetResponseAsync();
                    var obj = JObject.Parse(response.GetResponseText());
                    var objs= JsonConvert.DeserializeObject<FacebookProfile>(response.GetResponseText());

                    Usuarios usuario = new Usuarios(objs);
                    Usuario usuariobd = new Usuario(objs);
                    var token = FirebaseInstanceId.Instance.Token;
                    ObtenerResul obtenerResul = new ObtenerResul();
                    string nombreuser = usuario.Name;
                    var contador = await obtenerResul.GetList<ClasesBd>("http://tools.agorafranquicias.com/APP/Consultas/getUsuarios.php?Name='" + nombreuser + "'");
                    int total=0;
                    foreach (Total t in contador.Total)
                    {
                        total = t.Contador;
                    }

                    if (total == 0)
                    {
                        obtenerResul.SaveUser(usuariobd, token);
                    }
                    else
                    {
                        obtenerResul.UpdateUser(usuariobd, token);
                    }
                   

                    using (var datos = new DataAccess())
                    {
                        datos.InserUsuario(usuario);
                    }
                    await App.NavigateToProfile(objs);
                }
                else
                {
                    await App.VueltaAtras();
                }
            };
            activity.StartActivity(auth.GetUI(activity));
        }
    }
}