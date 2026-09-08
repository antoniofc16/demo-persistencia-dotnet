using ApiPersistenciaDotnet.Data.Persistence;
using ApiPersistenciaDotnet.Domain;
using ApiPersistenciaDotnet.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiPersistenciaDotnet.Data.Services
{
    public interface IEFCoreService
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

    public class EFCoreService : IEFCoreService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EFCoreService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> NewCategoria(CategoriaDTO categoria)
        {
            Categorias categoriaEntity = new Categorias
            {
                NombreCategoria = categoria.NombreCategoria,
                FechaRegistro = DateTime.UtcNow
            };

            _context.Categorias.Add(categoriaEntity);

            await _context.SaveChangesAsync();

            return categoriaEntity.IdCategoria;
        }

        public async Task<int> UpdateCategoria(CategoriaDTO categoria)
        {
            var categoriaEntity = await _context.Categorias.FindAsync(categoria.IdCategoria);

            if (categoriaEntity == null)
                throw new Exception("No existe la Categoria");

            categoriaEntity.NombreCategoria = categoria.NombreCategoria;
            categoriaEntity.Activo = categoria.Activo;
            categoriaEntity.FechaActualizacion = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return categoriaEntity.IdCategoria;
        }

        public async Task<int> DeleteCategoria(int id)
        {
            var categoriaEntity = await _context.Categorias.FindAsync(id);
            var hasProductos = await _context.Productos.AnyAsync(p => p.IdCategoria == id);

            if (hasProductos)
                throw new Exception("No se puede eliminar la Categoria porque tiene Productos asociados");

            if (categoriaEntity == null)
                throw new Exception("No existe la Categoria");

            _context.Categorias.Remove(categoriaEntity);
            var result = await _context.SaveChangesAsync();

            return result;
        }

        public async Task<List<CategoriaDTO>> GetCategorias()
        {
            var categorias = await _context.Categorias.ToListAsync();

            var result = _mapper.Map<List<CategoriaDTO>>(categorias);

            return result;
        }

        public async Task<int> NewProducto(ProductoDTO producto)
        {
            Productos productoEntity = new Productos
            {
                NombreProducto = producto.NombreProducto,
                IdCategoria = producto.IdCategoria!.Value,
                PrecioProducto = producto.PrecioProducto,
                Stock = producto.Stock,
                FechaRegistro = DateTime.UtcNow
            };

            _context.Productos.Add(productoEntity);

            await _context.SaveChangesAsync();

            return productoEntity.IdProducto;
        }

        public async Task<int> UpdateProducto(ProductoDTO producto)
        {
            var productoEntity = await _context.Productos.FindAsync(producto.IdProducto);

            if (productoEntity == null)
                throw new Exception("No existe el Producto");

            productoEntity.NombreProducto = producto.NombreProducto;
            productoEntity.IdCategoria = producto.IdCategoria ?? 0;
            productoEntity.PrecioProducto = producto.PrecioProducto;
            productoEntity.Stock = producto.Stock;
            productoEntity.Activo = producto.Activo;
            productoEntity.FechaActualizacion = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return productoEntity.IdProducto;
        }

        public async Task<int> DeleteProducto(int id)
        {
            var productoEntity = await _context.Productos.FindAsync(id);
            var hasOrdenes = await _context.OrdenDetalle.AnyAsync(o => o.IdProducto == id);

            if (hasOrdenes)
                throw new Exception("No se puede eliminar el Producto porque tiene Órdenes asociadas");

            if (productoEntity == null)
                throw new Exception("No existe el Producto");

            _context.Productos.Remove(productoEntity);

            var result = await _context.SaveChangesAsync();

            return result;
        }

        public async Task<List<ProductoDTO>> GetProductos()
        {
            var productos = await _context.Productos.Include(p => p.Categoria).ToListAsync();
            var result = _mapper.Map<List<ProductoDTO>>(productos);
            return result;
        }

        public async Task<int> NewCliente(ClienteDTO cliente)
        {
            Clientes clienteEntity = new()
            {
                Nombres = cliente.Nombres,
                ApellidoPaterno = cliente.ApellidoPaterno,
                ApellidoMaterno = cliente.ApellidoMaterno,
                CorreoElectronico = cliente.CorreoElectronico,
                Telefono = cliente.Telefono,
                FechaNacimiento = cliente.FechaNacimiento!.Value,
                FechaRegistro = DateTime.UtcNow
            };

            _context.Clientes.Add(clienteEntity);

            await _context.SaveChangesAsync();

            return clienteEntity.IdCliente;
        }

        public async Task<int> UpdateCliente(ClienteDTO cliente)
        {
            var clienteEntity = await _context.Clientes.FindAsync(cliente.IdCliente);

            if (clienteEntity == null)
                throw new Exception("No existe el Cliente");

            clienteEntity.Nombres = cliente.Nombres;
            clienteEntity.ApellidoPaterno = cliente.ApellidoPaterno;
            clienteEntity.ApellidoMaterno = cliente.ApellidoMaterno;
            clienteEntity.CorreoElectronico = cliente.CorreoElectronico;
            clienteEntity.Telefono = cliente.Telefono;
            clienteEntity.Direccion = cliente.Direccion;
            clienteEntity.FechaNacimiento = cliente.FechaNacimiento!.Value;
            clienteEntity.Activo = cliente.Activo;
            clienteEntity.FechaActualizacion = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return clienteEntity.IdCliente;
        }

        public async Task<int> DeleteCliente(int id)
        {
            var clienteEntity = await _context.Clientes.FindAsync(id);
            var hasOrdenes = await _context.Ordenes.AnyAsync(o => o.IdCliente == id);

            if (hasOrdenes)
                throw new Exception("No se puede eliminar el Cliente porque tiene Órdenes asociadas");

            if (clienteEntity == null)
                throw new Exception("No existe el Cliente");

            _context.Clientes.Remove(clienteEntity);

            var result = await _context.SaveChangesAsync();

            return result;
        }

        public async Task<List<ClienteDTO>> GetClientes()
        {
            var clientes = await _context.Clientes.ToListAsync();
            var result = _mapper.Map<List<ClienteDTO>>(clientes);
            return result;
        }

        public async Task<int> NewOrden(OrdenDTO orden)
        {
            Ordenes ordenEntity = new()
            {
                IdCliente = orden.IdCliente!.Value,
                FechaRegistro = DateTime.UtcNow
            };
            _context.Ordenes.Add(ordenEntity);
            await _context.SaveChangesAsync();

            foreach (var detalle in orden.Detalle)
            {
                OrdenDetalle detalleEntity = new()
                {
                    IdOrden = ordenEntity.IdOrden,
                    IdProducto = detalle.IdProducto!.Value,
                    Cantidad = detalle.Cantidad,
                    PrecioUnitario = detalle.PrecioUnitario
                };
                _context.OrdenDetalle.Add(detalleEntity);
            }

            await _context.SaveChangesAsync();

            return ordenEntity.IdOrden;
        }

        public async Task<OrdenDTO> GetOrden(int id)
        {
            var orden = await _context.Ordenes
                .Include(o => o.Cliente)
                .Include(o => o.Detalle)
                    .ThenInclude(d => d.Producto)
                        .ThenInclude(p => p.Categoria)
                .FirstOrDefaultAsync(o => o.IdOrden == id);

            if (orden == null)
                throw new Exception("No existe la Orden");

            var result = _mapper.Map<OrdenDTO>(orden);
            return result;
        }
    }
}
