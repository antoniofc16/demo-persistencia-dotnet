using ApiPersistenciaDotnet.Data.Persistence;
using ApiPersistenciaDotnet.DTOs;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ApiPersistenciaDotnet.Data.Services
{
    public interface IAdoNetService
    {
        public Task<int> NewCategoria(CategoriaDTO categoria);
        public Task<int> UpdateCategoria(CategoriaDTO categoria);
        public Task<int> DeleteCategoria(int id);
        public Task<List<CategoriaDTO>> GetCategorias();

        public Task<int> NewProducto(ProductoDTO producto);
        public Task<int> UpdateProducto(ProductoDTO producto);
        public Task<int> DeleteProducto(int id);
        public Task<List<ProductoDTO>> GetProductos();

        public Task<int> NewCliente(ClienteDTO cliente);
        public Task<int> UpdateCliente(ClienteDTO cliente);
        public Task<int> DeleteCliente(int id);
        public Task<List<ClienteDTO>> GetClientes();
        
        public Task<int> NewOrden(OrdenDTO orden);
        public Task<OrdenDTO> GetOrden(int id);
    }

    public class AdoNetService : IAdoNetService
    {

        public async Task<int> NewCategoria(CategoriaDTO categoria)
        {
            var conn = AdoNetConnectionFactory.Create();
            await conn.OpenAsync();

            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_RegistrarCategoria";
                cmd.Parameters.AddWithValue("@nombreCategoria", categoria.NombreCategoria);
                var result = await cmd.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            }
        }

        public async Task<int> UpdateCategoria(CategoriaDTO categoria)
        {
            var conn = AdoNetConnectionFactory.Create();
            await conn.OpenAsync();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_ActualizarCategoria";
                cmd.Parameters.AddWithValue("@idCategoria", categoria.IdCategoria);
                cmd.Parameters.AddWithValue("@nombreCategoria", categoria.NombreCategoria);
                cmd.Parameters.AddWithValue("@activo", categoria.Activo);
                var result = await cmd.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            }
        }

        public async Task<int> DeleteCategoria(int id)
        {
            var conn = AdoNetConnectionFactory.Create();
            await conn.OpenAsync();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_EliminarCategoria";
                cmd.Parameters.AddWithValue("@idCategoria", id);
                var result = await cmd.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            }
        }

        public async Task<List<CategoriaDTO>> GetCategorias()
        {
            var conn = AdoNetConnectionFactory.Create();
            await conn.OpenAsync();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_ConsultarCategorias";
                var reader = await cmd.ExecuteReaderAsync();
                var categorias = new List<CategoriaDTO>();
                while (await reader.ReadAsync())
                {
                    categorias.Add(new CategoriaDTO
                    {
                        IdCategoria = reader.GetInt32(0),
                        NombreCategoria = reader.GetString(1),
                        Activo = reader.GetBoolean(2)
                    });
                }
                return categorias;
            }
        }

        public async Task<int> NewProducto(ProductoDTO producto)
        {
            var conn = AdoNetConnectionFactory.Create();
            await conn.OpenAsync();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_RegistrarProducto";
                cmd.Parameters.AddWithValue("@nombreProducto", producto.NombreProducto);
                cmd.Parameters.AddWithValue("@idCategoria", producto.IdCategoria);
                cmd.Parameters.AddWithValue("@precioProducto", producto.PrecioProducto);
                cmd.Parameters.AddWithValue("@stock", producto.Stock);
                var result = await cmd.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            }
        }

        public async Task<int> UpdateProducto(ProductoDTO producto)
        {
            var conn = AdoNetConnectionFactory.Create();
            await conn.OpenAsync();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_ActualizarProducto";
                cmd.Parameters.AddWithValue("@idProducto", producto.IdProducto);
                cmd.Parameters.AddWithValue("@nombreProducto", producto.NombreProducto);
                cmd.Parameters.AddWithValue("@precioProducto", producto.PrecioProducto);
                cmd.Parameters.AddWithValue("@stock", producto.Stock);
                cmd.Parameters.AddWithValue("@idCategoria", producto.IdCategoria);
                cmd.Parameters.AddWithValue("@activo", producto.Activo);
                var result = await cmd.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            }
        }

        public async Task<int> DeleteProducto(int id)
        {
            var conn = AdoNetConnectionFactory.Create();
            await conn.OpenAsync();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_EliminarProducto";
                cmd.Parameters.AddWithValue("@idProducto", id);
                var result = await cmd.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            }
        }

        public async Task<List<ProductoDTO>> GetProductos()
        {
            var conn = AdoNetConnectionFactory.Create();
            await conn.OpenAsync();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_ConsultarProductos";
                var reader = await cmd.ExecuteReaderAsync();
                var productos = new List<ProductoDTO>();
                while (await reader.ReadAsync())
                {
                    productos.Add(new ProductoDTO
                    {
                        IdProducto = reader.GetInt32(0),
                        NombreProducto = reader.GetString(1),
                        PrecioProducto = reader.GetDecimal(2),
                        Stock = reader.GetInt32(3),
                        IdCategoria = reader.GetInt32(4),
                        Activo = reader.GetBoolean(5)
                    });
                }
                return productos;
            }
        }

        public async Task<int> NewCliente(ClienteDTO cliente)
        {
            var conn = AdoNetConnectionFactory.Create();
            await conn.OpenAsync();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_RegistraCliente";
                cmd.Parameters.AddWithValue("@nombres", cliente.Nombres);
                cmd.Parameters.AddWithValue("@apellidoPaterno", cliente.ApellidoPaterno);
                cmd.Parameters.AddWithValue("@apellidoMaterno", cliente.ApellidoMaterno);
                cmd.Parameters.AddWithValue("@correoElectronico", cliente.CorreoElectronico);
                cmd.Parameters.AddWithValue("@telefono", cliente.Telefono);
                cmd.Parameters.AddWithValue("@direccion", cliente.Direccion);
                cmd.Parameters.AddWithValue("@fechaNacimiento", cliente.FechaNacimiento?.ToString("yyyy-MM-dd"));
                var result = await cmd.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            }
        }

        public async Task<int> UpdateCliente(ClienteDTO cliente)
        {
            var conn = AdoNetConnectionFactory.Create();
            await conn.OpenAsync();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_ActualizaCliente";
                cmd.Parameters.AddWithValue("@idCliente", cliente.IdCliente);
                cmd.Parameters.AddWithValue("@nombres", cliente.Nombres);
                cmd.Parameters.AddWithValue("@apellidoPaterno", cliente.ApellidoPaterno);
                cmd.Parameters.AddWithValue("@apellidoMaterno", cliente.ApellidoMaterno);
                cmd.Parameters.AddWithValue("@correoElectronico", cliente.CorreoElectronico);
                cmd.Parameters.AddWithValue("@telefono", cliente.Telefono);
                cmd.Parameters.AddWithValue("@direccion", cliente.Direccion);
                cmd.Parameters.AddWithValue("@fechaNacimiento", cliente.FechaNacimiento?.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@activo", cliente.Activo);
                var result = await cmd.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            }
        }

        public async Task<int> DeleteCliente(int id)
        {
            var conn = AdoNetConnectionFactory.Create();
            await conn.OpenAsync();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_EliminarCliente";
                cmd.Parameters.AddWithValue("@idCliente", id);
                var result = await cmd.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            }
        }

        public async Task<List<ClienteDTO>> GetClientes()
        {
            var conn = AdoNetConnectionFactory.Create();
            await conn.OpenAsync();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_ConsultaClientes";
                var reader = await cmd.ExecuteReaderAsync();
                var clientes = new List<ClienteDTO>();
                while (await reader.ReadAsync())
                {
                    clientes.Add(new ClienteDTO
                    {
                        IdCliente = reader.GetInt32(0),
                        Nombres = reader.GetString(1),
                        ApellidoPaterno = reader.GetString(2),
                        ApellidoMaterno = reader.GetString(3),
                        CorreoElectronico = reader.GetString(4),
                        Telefono = reader.GetString(5),
                        Direccion = reader.GetString(6),
                        FechaNacimiento = reader.GetDateTime(7),
                        Activo = reader.GetBoolean(8)
                    });
                }
                return clientes;
            }
        }

        public async Task<int> NewOrden(OrdenDTO orden)
        {
            var conn = AdoNetConnectionFactory.Create();
            await conn.OpenAsync();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_RegistrarOrden";
                cmd.Parameters.AddWithValue("@idCliente", orden.IdCliente);
                var result = await cmd.ExecuteScalarAsync();
                int idOrden = Convert.ToInt32(result);

                foreach (var detalle in orden.Detalle)
                {
                    using (var cmdDetalle = conn.CreateCommand())
                    {
                        cmdDetalle.CommandType = CommandType.StoredProcedure;
                        cmdDetalle.CommandText = "SP_RegistrarOrdenDetalle";
                        cmdDetalle.Parameters.AddWithValue("@idOrden", idOrden);
                        cmdDetalle.Parameters.AddWithValue("@idProducto", detalle.IdProducto);
                        cmdDetalle.Parameters.AddWithValue("@precioUnitario", detalle.PrecioUnitario);
                        cmdDetalle.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                        await cmdDetalle.ExecuteNonQueryAsync();
                    }
                }

                return idOrden;
            }
        }

        public async Task<OrdenDTO> GetOrden(int id)
        {
            var conn = AdoNetConnectionFactory.Create();
            await conn.OpenAsync();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_ObtenerOrden";
                cmd.Parameters.AddWithValue("@idOrden", id);
                var reader = await cmd.ExecuteReaderAsync();
                OrdenDTO orden = null;
                if (await reader.ReadAsync())
                {
                    orden = new OrdenDTO
                    {
                        IdOrden = reader.GetInt32(0),
                        IdCliente = reader.GetInt32(1),
                        FechaRegistro = reader.GetDateTime(2),
                        Cliente = new ClienteDTO
                        {
                            IdCliente = reader.GetInt32(1),
                            Nombres = reader.GetString(3),
                            ApellidoPaterno = reader.GetString(4),
                            ApellidoMaterno = reader.GetString(5),
                        }
                    };

                    await reader.CloseAsync();
                }

                if (orden != null)
                {
                    orden.Detalle = new List<OrdenDetalleDTO>();
                    using (var cmdDetalle = conn.CreateCommand())
                    {
                        cmdDetalle.CommandType = CommandType.StoredProcedure;
                        cmdDetalle.CommandText = "SP_ObtenerOrdenDetalle";
                        cmdDetalle.Parameters.AddWithValue("@idOrden", id);
                        var readerDetalle = await cmdDetalle.ExecuteReaderAsync();
                        while (await readerDetalle.ReadAsync())
                        {
                            orden.Detalle.Add(new OrdenDetalleDTO
                            {
                                IdOrdenDetalle = readerDetalle.GetInt32(0),
                                IdOrden = orden.IdOrden,
                                IdProducto = readerDetalle.GetInt32(1),
                                Cantidad = readerDetalle.GetInt32(2),
                                PrecioUnitario = readerDetalle.GetDecimal(3),
                                Producto = new ProductoDTO
                                {
                                    IdProducto = readerDetalle.GetInt32(1),
                                    NombreProducto = readerDetalle.GetString(4),
                                    Categoria = new CategoriaDTO
                                    {
                                        NombreCategoria = readerDetalle.GetString(5)
                                    }
                                }
                            });
                        }
                    }
                }

                return orden;
            }
        }
    }
}
