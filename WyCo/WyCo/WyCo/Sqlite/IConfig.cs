using SQLite.Net.Interop;
using System;


namespace WyCo.Sqlite
{
    public interface IConfig
    {
        String DirectorioDB { get; }
        ISQLitePlatform Plataforma { get; }
    }
}
