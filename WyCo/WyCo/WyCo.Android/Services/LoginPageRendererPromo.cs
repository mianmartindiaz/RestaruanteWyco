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

[assembly: ExportRenderer(typeof(LoginPromos), typeof(WyCo.Droid.Services.LoginPageRendererPromo))]
namespace WyCo.Droid.Services
{
    class LoginPageRendererPromo:PageRenderer
    {
        public LoginPageRendererPromo()
        {
            var activity = this.Context as Activity;
            var auth = new OAuth2Authenticator(
                clientId: "168703917051703",
                scope: "",
                authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"),
                redirectUrl: new Uri("http://www.facebook.com/connect/login_success.html"));
            auth.Completed += async (sender, eventArgs) => {
                if (eventArgs.IsAuthenticated)
                {
                    var accessToken = eventArgs.Account.Properties["access_token"].ToString();
                    var expiresIn = Convert.ToDouble(eventArgs.Account.Properties["expires_in"]);
                    var expiryDate = DateTime.Now + TimeSpan.FromSeconds(expiresIn);
                    var request = new OAuth2Request("GET", new Uri("https://graph.facebook.com/me?fields=id,name,cover,age_range,picture"), null, eventArgs.Account);
                    var response = await request.GetResponseAsync();
                    var obj = JObject.Parse(response.GetResponseText());
                    var objs = JsonConvert.DeserializeObject<FacebookProfile>(response.GetResponseText());
                    var nombre = obj["name"].ToString().Replace("\"", "");
                    var Imagen = obj["picture"].ToString().Replace("\"", "");

                    Usuarios usuario = new Usuarios(objs);
                    Usuario usuariobd = new Usuario(objs);

                    var token = FirebaseInstanceId.Instance.Token;
                    ObtenerResul obtenerResul = new ObtenerResul();

                    string nombreuser = usuario.Name;
                    var contador = await obtenerResul.GetList<ClasesBd>("http://tools.agorafranquicias.com/APP/Consultas/getUsuarios.php?Name='" + nombreuser +"'");
                    int total = 0;
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
                    obtenerResul.SaveUser(usuariobd, token);

                    using (var datos = new DataAccess())
                    {
                        datos.InserUsuario(usuario);
                    }
                    await App.NavigateToPromos();
                }
                else
                {
                    await App.VolverPromociones();
                }
            };
            activity.StartActivity(auth.GetUI(activity));
        }
    }
}