using Xamarin.Forms;
using WyCo.Sqlite;
using SQLite.Net.Interop;

[assembly: Dependency(typeof(WyCo.Droid.Services.Config))]

namespace WyCo.Droid.Services
{
    class Config : IConfig
    {
        private string directorioDB;
        private ISQLitePlatform plataforma;
        public string DirectorioDB
        {
            get
            {
                if (string.IsNullOrEmpty(directorioDB))
                {
                    directorioDB = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);


                }
                return directorioDB;
            }
        }
        public ISQLitePlatform Plataforma
        {
            get
            {
                if (plataforma == null)
                {
                    plataforma = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
                }
                return plataforma;
            }
        }
    }
}