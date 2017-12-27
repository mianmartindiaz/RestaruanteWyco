using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using WyCo.Sqlite;

namespace WyCo.BD
{
    public class ObtenerResul
    {
        public async Task<T> GetList<T>(string url)
        {



            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(url);


                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonstring = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<T>(jsonstring);
                }
                else { return default(T); }
            }
            catch(NullReferenceException e)
            {
                return default(T);
            }
            catch (HttpRequestException r)
            {
                return default(T);
            }

        }
        public void SaveUser(Usuario item,string token)
        {
            item.Token = token;
            var Json = JsonConvert.SerializeObject(item);
            HttpClient cliente = new HttpClient();
            var URI = new Uri("http://tools.agorafranquicias.com/APP/Consultas/postCliente.php?json=" + Json);
            cliente.GetAsync(URI);
        }

        public void UpdateUser(Usuario item, string token)
        {
            item.Token = token;
            var Json = JsonConvert.SerializeObject(item);
            HttpClient cliente = new HttpClient();
            var URI = new Uri("http://tools.agorafranquicias.com/APP/Consultas/UpdateToken.php?Token='" +token+"'&Name='"+item.Name+"'");
            cliente.GetAsync(URI);
        }


    }
}
