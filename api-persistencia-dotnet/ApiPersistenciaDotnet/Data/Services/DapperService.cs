using ApiPersistenciaDotnet.Data.Persistence;
using ApiPersistenciaDotnet.Domain;
using ApiPersistenciaDotnet.DTOs;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.PortableExecutable;

namespace ApiPersistenciaDotnet.Data.Services
{
    public interface IDapperService
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
        public Task<OrdenDapperDTO> GetOrden(int id);
    }

    public class DapperService : IDapperService
    {

        public async Task<int> NewCategoria(CategoriaDTO categoria)
        {
            var conn = AdoNetConnectionFactory.Create();
            await conn.OpenAsync();

            var result = await conn.QuerySingleAsync<int>(
                "SP_RegistrarCategoria",
                new { nombreCategoria = categoria.NombreCategoria },
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<int> UpdateCategoria(CategoriaDTO categoria)
        {
            var conn = AdoNetConnectionFactory.Create();
            await conn.OpenAsync();

            var result = await conn.QuerySingleAsync<int>(
                "SP_ActualizarCategoria",
                new { idCategoria = categoria.IdCategoria, nombreCategoria = categoria.NombreCategoria, activo = categoria.Activo },
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<int> DeleteCategoria(int id)
        {
            var conn = AdoNetConnectionFactory.Create();
            await conn.OpenAsync();

            var result = await conn.QuerySingleAsync<int>(
                "SP_EliminarCategoria",
                new { idCategoria = id },
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<List<CategoriaDTO>> GetCategorias()
        {
            var conn = AdoNetConnectionFactory.Create();
            await conn.OpenAsync();

            var result = await conn.QueryAsync<CategoriaDTO>(
                "SP_ConsultarCategorias",
                commandType: CommandType.StoredProcedure);

            return result.ToList();
        }

        public async Task<int> NewProducto(ProductoDTO producto)
        {
            var conn = AdoNetConnectionFactory.Create();
            await conn.OpenAsync();

            var result = await conn.QuerySingleAsync<int>(
                "SP_RegistrarProducto",
                new { nombreProducto = producto.NombreProducto, idCategoria = producto.IdCategoria, precioProducto = producto.PrecioProducto, stock = producto.Stock },
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<int> UpdateProducto(ProductoDTO producto)
        {
            var conn = AdoNetConnectionFactory.Create();
            await conn.OpenAsync();

            var result = await conn.QuerySingleAsync<int>(
                "SP_ActualizarProducto",
                new { idProducto = producto.IdProducto, nombreProducto = producto.NombreProducto, precioProducto = producto.PrecioProducto, stock = producto.Stock, idCategoria = producto.IdCategoria, activo = producto.Activo },
                commandType: CommandType.StoredProcedure
            );
            
            return result;
        }

        public async Task<int> DeleteProducto(int id)
        {
            var conn = AdoNetConnectionFactory.Create();
            await conn.OpenAsync();

            var result = await conn.QuerySingleAsync<int>(
                "SP_EliminarProducto",
                new { idProducto = id },
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<List<ProductoDTO>> GetProductos()
        {
            var conn = AdoNetConnectionFactory.Create();
            await conn.OpenAsync();

            var result = await conn.QueryAsync<ProductoDTO>(
                "SP_ConsultarProductos",
                commandType: CommandType.StoredProcedure
            );

            return result.ToList();
        }

        public async Task<int> NewCliente(ClienteDTO cliente)
        {
            var conn = AdoNetConnectionFactory.Create();
            await conn.OpenAsync();

            var result = await conn.QuerySingleAsync<int>(
                "SP_RegistraCliente",
                new
                {
                    nombres = cliente.Nombres,
                    apellidoPaterno = cliente.ApellidoPaterno,
                    apellidoMaterno = cliente.ApellidoMaterno,
                    correoElectronico = cliente.CorreoElectronico,
                    telefono = cliente.Telefono,
                    direccion = cliente.Direccion,
                    fechaNacimiento = cliente.FechaNacimiento?.ToString("yyyy-MM-dd")
                },
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<int> UpdateCliente(ClienteDTO cliente)
        {
            var conn = AdoNetConnectionFactory.Create();
            await conn.OpenAsync();

            var result = await conn.QuerySingleAsync<int>(
                "SP_ActualizaCliente",
                new
                {
                    idCliente = cliente.IdCliente,
                    nombres = cliente.Nombres,
                    apellidoPaterno = cliente.ApellidoPaterno,
                    apellidoMaterno = cliente.ApellidoMaterno,
                    correoElectronico = cliente.CorreoElectronico,
                    telefono = cliente.Telefono,
                    direccion = cliente.Direccion,
                    fechaNacimiento = cliente.FechaNacimiento?.ToString("yyyy-MM-dd"),
                    activo = cliente.Activo
                },
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<int> DeleteCliente(int id)
        {
            var conn = AdoNetConnectionFactory.Create();
            await conn.OpenAsync();

            var result = await conn.QuerySingleAsync<int>(
                "SP_EliminarCliente",
                new { idCliente = id },
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<List<ClienteDTO>> GetClientes()
        {
            var conn = AdoNetConnectionFactory.Create();
            await conn.OpenAsync();

            var result = await conn.QueryAsync<ClienteDTO>(
                "SP_ConsultaClientes",
                commandType: CommandType.StoredProcedure
            );

            return result.ToList();
        }

        public async Task<int> NewOrden(OrdenDTO orden)
        {
            var conn = AdoNetConnectionFactory.Create();
            await conn.OpenAsync();

            var transaction = await conn.BeginTransactionAsync();

            var result = await conn.QuerySingleAsync<int>(
                "SP_RegistrarOrden",
                new { idCliente = orden.IdCliente },
                commandType: CommandType.StoredProcedure,
                transaction: transaction
            );

            int idOrden = result;


            foreach (var detalle in orden.Detalle)
            {
                var resultDetalle = await conn.QuerySingleAsync<int>(
                    "SP_RegistrarOrdenDetalle",
                    new
                    {
                        idOrden = idOrden,
                        idProducto = detalle.IdProducto,
                        precioUnitario = detalle.PrecioUnitario,
                        cantidad = detalle.Cantidad
                    },
                    commandType: CommandType.StoredProcedure,
                    transaction: transaction
                );
            }

            transaction.Commit();

            return idOrden;
        }

        public async Task<OrdenDapperDTO> GetOrden(int id)
        {
            var conn = AdoNetConnectionFactory.Create();
            await conn.OpenAsync();

            var result = await conn.QueryFirstAsync<OrdenDapperDTO>(
                "SP_ObtenerOrden",
                new { idOrden = id },
                commandType: CommandType.StoredProcedure
            );

            var resultDetalle = await conn.QueryAsync<OrdenDetalleDapperDTO>(
                "SP_ObtenerOrdenDetalle",
                new { idOrden = id },
                commandType: CommandType.StoredProcedure
            );

            result.Detalle = resultDetalle.ToList();

            return result;
        }
    }
}
