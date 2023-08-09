using Proyeto.Models;
using System.Data.SqlClient;
using System.Data;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;

namespace Proyeto.Datos
{
    public class CategoriaDatos
    {

       public List<Categorium> Listar()
        {
            List<Categorium> lista = new List<Categorium>();
            var cn = new ConexionCate();
            using(var Conexion = new SqlConnection(cn.getCadenaSql()))
            {
                Conexion.Open();
                SqlCommand cmd =  new SqlCommand("sp_ListarCategoria", Conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using(var dr=cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Categorium
                        {
                            IdCategoria = Convert.ToInt32(dr["IdCategoria"]),
                            Tipo = dr["Tipo"].ToString(),
                            Descripcion = dr["Descripcion"].ToString()
                        }); ;
                    }
                }

            }
            return lista;
        }

        public Categorium ObtenerCategoria (int IdCategoria)
        {
            Categorium _categoria = new Categorium();

            var cn = new ConexionCate();
            using(var conexion= new SqlConnection(cn.getCadenaSql()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ObtenerCategoria", conexion);
                cmd.Parameters.AddWithValue("IdCategoria", IdCategoria);
                cmd.CommandType= CommandType.StoredProcedure;
                using(var dr= cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        _categoria.IdCategoria = Convert.ToInt32(dr["IdCategoria"]);
                        _categoria.Tipo = dr["Tipo"].ToString();
                        _categoria.Descripcion = dr["Descripcion"].ToString(); 
                        
                    }
                }
            }
            return _categoria;
        }

        public bool GuardarCategoria(Categorium model)
        {
            bool respuesta;
            try
            {
                var cn = new ConexionCate();
                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    SqlCommand cmd = new SqlCommand("sp_GuardarCategoria", conexion);
                    cmd.Parameters.AddWithValue("Tipo", model.Tipo);
                    cmd.Parameters.AddWithValue("Descripcion", model.Descripcion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                respuesta = true;
            }

            catch(Exception ex)
            {
                string error = ex.Message;
                respuesta = false;
            }
            return respuesta;
        }

        public bool EditarCategoria(Categorium model)
        {
            bool respuesta;
            try
            {
                var cn = new ConexionCate();
                using(var conexion= new SqlConnection(cn.getCadenaSql()))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarCategoria", conexion);
                    cmd.Parameters.AddWithValue("Tipo", model.Tipo);
                    cmd.Parameters.AddWithValue("Descripcion", model.Descripcion);
                    cmd.Parameters.AddWithValue("IdCategoria", model.IdCategoria);
                }
                respuesta = true;
            }
            catch(Exception ex)
            {
                string error = ex.Message;
                respuesta = false;
            }
            return respuesta;
        }

    }
}
