
using SQLite.Net;
using Xamarin.Forms;
using System.IO;
using System;

namespace WyCo.Sqlite
{
    public class DataAccess: IDisposable
    {
        private SQLiteConnection connection;

        public DataAccess()
        {
            var config = DependencyService.Get<IConfig>();
            connection = new SQLiteConnection(config.Plataforma, Path.Combine(config.DirectorioDB, "Usuario.db3"));
            connection.CreateTable<Usuarios>();
        }
        public void InserUsuario(Usuarios user)
        {
            connection.Insert(user);
        }
        public void UpdateUsuario(Usuarios user)
        {
            connection.Update(user);
        }
        public Usuarios GetUsuario()
        {
            return connection.Table<Usuarios>().FirstOrDefault();
        }

        public int getCountUsuario()
        {
            return connection.Table<Usuarios>().Count();
        }
        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
