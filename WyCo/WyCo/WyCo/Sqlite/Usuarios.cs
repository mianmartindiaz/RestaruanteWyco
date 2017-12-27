using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WyCo.BD;

namespace WyCo.Sqlite
{
    public class Usuarios
    {
        public Usuarios() { }

        public Usuarios(FacebookProfile obj)
        {
            Name = obj.Name;
            Picture = obj.Picture.Data.Url;
            Id = obj.Id;
            Email = obj.email;
        }
        [PrimaryKey, AutoIncrement]
        public int Id_User { get; set; }
        public String Name { get; set; }
        public String Picture { get; set; }
        public String Email { get; set; }
        public string Id { get; set; }
    }
}
