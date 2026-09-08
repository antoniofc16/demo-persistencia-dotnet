using ApiPersistenciaDotnet.Data.Services;
using ApiPersistenciaDotnet.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPersistenciaDotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdoNetController : ControllerBase
    {
        private readonly IAdoNetService _adoNetService;


        public AdoNetController(IAdoNetService adoNetService)
        {
            _adoNetService = adoNetService;
        }

        [HttpGet("categorias")]
        public async Task<IActionResult> GetCategorias()
        {
            var result = await _adoNetService.GetCategorias();
            return Ok(result);
        }

        [HttpPost("categorias/new")]
        public async Task<IActionResult> NewCategoria([FromBody] CategoriaDTO categoria)
        {
            var result = await _adoNetService.NewCategoria(categoria);
            return Ok(result);
        }

        [HttpPut("categorias/update")]
        public async Task<IActionResult> UpdateCategoria([FromBody] CategoriaDTO categoria)
        {
            var result = await _adoNetService.UpdateCategoria(categoria);
            return Ok(result);
        }

        [HttpDelete("categorias/delete/{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var result = await _adoNetService.DeleteCategoria(id);
            return Ok(result);
        }

        [HttpGet("productos")]
        public async Task<IActionResult> GetProductos()
        {
            var result = await _adoNetService.GetProductos();
            return Ok(result);
        }

        [HttpPost("productos/new")]
        public async Task<IActionResult> NewProducto([FromBody] ProductoDTO producto)
        {
            var result = await _adoNetService.NewProducto(producto);
            return Ok(result);
        }

        [HttpPut("productos/update")]
        public async Task<IActionResult> UpdateProducto([FromBody] ProductoDTO producto)
        {
            var result = await _adoNetService.UpdateProducto(producto);
            return Ok(result);
        }

        [HttpDelete("productos/delete/{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var result = await _adoNetService.DeleteProducto(id);
            return Ok(result);
        }

        [HttpGet("clientes")]
        public async Task<IActionResult> GetClientes()
        {
            var result = await _adoNetService.GetClientes();
            return Ok(result);
        }

        [HttpPost("clientes/new")]
        public async Task<IActionResult> NewCliente([FromBody] ClienteDTO cliente)
        {
            var result = await _adoNetService.NewCliente(cliente);
            return Ok(result);
        }

        [HttpPut("clientes/update")]
        public async Task<IActionResult> UpdateCliente([FromBody] ClienteDTO cliente)
        {
            var result = await _adoNetService.UpdateCliente(cliente);
            return Ok(result);
        }

        [HttpDelete("clientes/delete/{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var result = await _adoNetService.DeleteCliente(id);
            return Ok(result);
        }

        [HttpPost("ordenes/new")]
        public async Task<IActionResult> NewOrden([FromBody] OrdenDTO orden)
        {
            var result = await _adoNetService.NewOrden(orden);
            return Ok(result);
        }

        [HttpGet("ordenes/{id}")]
        public async Task<IActionResult> GetOrden(int id)
        {
            var result = await _adoNetService.GetOrden(id);
            return Ok(result);
        }
    }
}
