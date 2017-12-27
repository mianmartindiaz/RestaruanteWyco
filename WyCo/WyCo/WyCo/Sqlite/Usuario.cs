using System;
using SQLite.Net.Attributes;
using WyCo.BD;

namespace WyCo.Sqlite
{
    public class Usuario
    {
        public Usuario() { }
        public Usuario(FacebookProfile obj)
        {
            Name = obj.Name;
            Id = obj.Id;
            Email = obj.email;
        }

        [PrimaryKey, AutoIncrement]
        public int Id_User { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public String Token { get; set; }
        public string Id { get; set; }
    }
}
