using ApiPersistenciaDotnet.Data.Services;
using ApiPersistenciaDotnet.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPersistenciaDotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EFCoreController : ControllerBase
    {
        private readonly IEFCoreService _EFCoreService;


        public EFCoreController(IEFCoreService eFCoreService)
        {
            _EFCoreService = eFCoreService;
        }

        [HttpGet("categorias")]
        public async Task<IActionResult> GetCategorias()
        {
            var result = await _EFCoreService.GetCategorias();
            return Ok(result);
        }

        [HttpPost("categorias/new")]
        public async Task<IActionResult> NewCategoria([FromBody] CategoriaDTO categoria)
        {
            var result = await _EFCoreService.NewCategoria(categoria);
            return Ok(result);
        }

        [HttpPut("categorias/update")]
        public async Task<IActionResult> UpdateCategoria([FromBody] CategoriaDTO categoria)
        {
            var result = await _EFCoreService.UpdateCategoria(categoria);
            return Ok(result);
        }

        [HttpDelete("categorias/delete/{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var result = await _EFCoreService.DeleteCategoria(id);
            return Ok(result);
        }

        [HttpGet("productos")]
        public async Task<IActionResult> GetProductos()
        {
            var result = await _EFCoreService.GetProductos();
            return Ok(result);
        }

        [HttpPost("productos/new")]
        public async Task<IActionResult> NewProducto([FromBody] ProductoDTO producto)
        {
            var result = await _EFCoreService.NewProducto(producto);
            return Ok(result);
        }

        [HttpPut("productos/update")]
        public async Task<IActionResult> UpdateProducto([FromBody] ProductoDTO producto)
        {
            var result = await _EFCoreService.UpdateProducto(producto);
            return Ok(result);
        }

        [HttpDelete("productos/delete/{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var result = await _EFCoreService.DeleteProducto(id);
            return Ok(result);
        }

        [HttpGet("clientes")]
        public async Task<IActionResult> GetClientes()
        {
            var result = await _EFCoreService.GetClientes();
            return Ok(result);
        }

        [HttpPost("clientes/new")]
        public async Task<IActionResult> NewCliente([FromBody] ClienteDTO cliente)
        {
            var result = await _EFCoreService.NewCliente(cliente);
            return Ok(result);
        }

        [HttpPut("clientes/update")]
        public async Task<IActionResult> UpdateCliente([FromBody] ClienteDTO cliente)
        {
            var result = await _EFCoreService.UpdateCliente(cliente);
            return Ok(result);
        }

        [HttpDelete("clientes/delete/{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var result = await _EFCoreService.DeleteCliente(id);
            return Ok(result);
        }

        [HttpPost("ordenes/new")]
        public async Task<IActionResult> NewOrden([FromBody] OrdenDTO orden)
        {
            var result = await _EFCoreService.NewOrden(orden);
            return Ok(result);
        }

        [HttpGet("ordenes/{id}")]
        public async Task<IActionResult> GetOrden(int id)
        {
            var result = await _EFCoreService.GetOrden(id);
            return Ok(result);
        }
    }
}
