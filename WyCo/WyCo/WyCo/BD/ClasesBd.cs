using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WyCo.BD
{
    public class Total
    {
        public int Contador { get; set; }
    }

    public class Locales
    {
        public String Direccion { get; set; }
        public double Lat { get; set; }
        public double Log { get; set; }
        public String Ciudad { get; set; }

    }

    public class DisenioGeneral
    {
        public int Id { get; set; }
        public String Nombre { get; set; }
        public String Codigo { get; set; }
        public int Baja { get; set; }

    }

    public class UserGestion
    {
        public int Id { get; set; }
        public String Nombre { get; set; }
        public String Password { get; set; }
        public int Baja { get; set; }

    }

    public class Imagen
    {
        public int Id { get; set; }
        public String URL { get; set; }
        public String Tipo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime Fecha_Fin { get; set; }

    }



    public class Categoria
    {
        public int Id { get; set; }
        public int Id_Imagen { get; set; }
        public String Nombre { get; set; }
    }

    public class ShoppingCart
    {
        public int Id_Categoria { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public string Descripcion { get; set; }
    }

    public class GetProductos
    {
        public int Id_Categoria { get; set; }
        public String Nombre { get; set; } 
        public String Descripcion { get; set; }
        public double Precio { get; set; }


    }
    public class ProductoPromo
    {
       // public int Id { get; set; }
        //public int Id_Imagen { get; set; }
       // public int Id_Categoria { get; set; }
       // public String Nombre { get; set; }
       // public String Descripcion { get; set; }
       // public String Datos { get; set; }
       // public int Baja { get; set; }
        public String URL { get; set; }
    }

    public class Promocion
    {
        public int Id { get; set; }
        public int Id_Imagen { get; set; }
        public String Descripcion { get; set; }
        public float Precio { get; set; }
        public DateTime Fecha_Inicio { get; set; }
        public DateTime Fecha_Fin { get; set; }
    }

    public class Promociones
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int Premium { get; set; }
        public string URL { get; set; } 
        public float Precio { get; set; }
        public string Dia { get; set; }
        public string imagenBloqueada { get; set; }
    }

    public class Cliente
    {
        public int Id { get; set; }
        public String Nombre { get; set; }
        public String Password { get; set; }
        public String Apellidos { get; set; }
        public String Direccion { get; set; }
        public String Correo { get; set; }
        public String Telefono { get; set; }
        public int Baja { get; set; }
        public string email { get; set; }
    }

    public class Geolocalizacion
    {
        public DateTime Fecha { get; set; }
        public DateTime Hora { get; set; }
        public int Id_Cliente { get; set; }
        public String Coordenadas { get; set; }
    }

    public class Notificacion
    {
        public int Id { get; set; }
        public int Id_Cliente { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime Hora { get; set; }
        public String Texto { get; set; }
        public float Precio { get; set; }
    }

    public class Mensaje
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public int Id_Producto { get; set; }
        public int Id_Notificacion { get; set; }
    }

    public class NombreImagenCategoria
    {
        public int Id { get; set; }
        public String URL { get; set; }
    }

    public class NombreImagenProducto
    {
        public int Id { get; set; }
        public String URL { get; set; }
    }

    public class ClasesBd
    {
        public List<Categoria> Categoria { get; set; }
        public List<Imagen> Imagen { get; set; }
        public List<NombreImagenCategoria> Nombreimagencategoria { get; set; } 
        public List<GetProductos> GetProductos { get; set; }
        public List<Promociones> Promociones { get; set; }
        public List<Promocion> Promocion { get; set; }
        public List<ProductoPromo> ProductoPromo { get; set; }
        public List<Locales> Local { get; set; }
        public List<ShoppingCart> ShoppingCart { get; set; }
        public List<Total> Total { get; set; }
    }
}
