using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using POOII_cibertec_demo.Models;

namespace POOII_cibertec_demo.Repositories
{
    public class ProductRepository
    {
        private readonly string _connectionString;

        // Constructor recibe la cadena de conexión (la pasaremos desde el controlador)
        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        // Llama al stored procedure y devuelve la lista + total (tarea async)
        public async Task<(List<Product> Items, int TotalRegistros)> FiltrarPaginadoAsync(
            string nombre,
            decimal? precioMin,
            int? cantidadMin,
            DateTime? fechaDesde,
            DateTime? fechaHasta,
            bool? isCompleted,
            int page = 1,
            int pageSize = 5)
        {
            var lista = new List<Product>();
            int totalRegistros = 0;

            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("dbo.sp_FiltrarProductosPaginados", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nombre", (object)nombre ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@precioMin", (object)precioMin ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@cantidadMin", (object)cantidadMin ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);

                await con.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        // obtener total desde la primera fila (viene igual en todas)
                        if (totalRegistros == 0 && !reader.IsDBNull(reader.GetOrdinal("TotalRegistros")))
                        {
                            totalRegistros = Convert.ToInt32(reader["TotalRegistros"]);
                        }

                        var p = new Product
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            nombre = reader["nombre"] as string,
                            precio = Convert.ToDecimal(reader["precio"]),
                            cantidad = reader["Cantidad"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Cantidad"]),
                            fechaRegistro = reader["FechaRegistro"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["FechaRegistro"]),
                            isCompleted = reader["IsCompleted"] == DBNull.Value ? false : Convert.ToBoolean(reader["IsCompleted"]),
                        };

                        lista.Add(p);
                    }
                }
            }

            return (lista, totalRegistros);
        }
    }
}
