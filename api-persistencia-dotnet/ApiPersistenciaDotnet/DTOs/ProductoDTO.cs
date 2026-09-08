using ApiPersistenciaDotnet.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPersistenciaDotnet.DTOs
{
    public class ProductoDTO
    {
        public int? IdProducto { get; set; }
        public required string NombreProducto { get; set; }
        public int? IdCategoria { get; set; }
        public decimal PrecioProducto { get; set; } = 0;
        public int Stock { get; set; } = 0;
        public bool Activo { get; set; }
        public CategoriaDTO? Categoria { get; set; }
    }
}
