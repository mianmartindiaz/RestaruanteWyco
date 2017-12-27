using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using WyCo;
using WyCo.BD;
using WyCo.Sqlite;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(LoginPromos), typeof(WyCo.iOS.LoginPageRendererPromo))]

namespace WyCo.iOS
{
    class LoginPageRendererPromo: PageRenderer
    {
        bool done = false;
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if (done)
                return;

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

                    Usuarios usuario = new Usuarios(objs);
                    Usuario usuariobd = new Usuario(objs);

                    ObtenerResul obtenerResul = new ObtenerResul();
                    string nombreuser = usuario.Name;
                    var contador = await obtenerResul.GetList<ClasesBd>("http://tools.agorafranquicias.com/APP/Consultas/getUsuarios.php?Name='" + nombreuser + "'");
                    int total;
                    foreach (Total t in contador.Total)
                    {
                        total = t.Contador;
                    }
                 /*   if (total == 0)
                    {
                        obtenerResul.SaveUser(usuariobd, token);
                    }
                    else
                    {
                        obtenerResul.UpdateUser(usuariobd, token);
                    }*/
                    obtenerResul.SaveUser(usuariobd,null);

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

            done = true;
            PresentViewController(auth.GetUI(), true, null);
        }
    }
}
