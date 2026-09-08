using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPersistenciaDotnet.Domain
{
    public class Productos
    {
        [Key]
        [Column("idProducto")]
        public int IdProducto { get; set; }
        [Column("nombreProducto")]
        public required string NombreProducto { get; set; }
        [Column("idCategoria")]
        public int IdCategoria { get; set; }
        [Column("precioProducto")]
        public decimal PrecioProducto { get; set; }
        [Column("stock")]
        public int Stock { get; set; }
        [Column("activo")]
        public bool Activo { get; set; }
        [Column("fechaRegistro")]
        public required DateTime FechaRegistro { get; set; }
        [Column("fechaActualizacion")]
        public DateTime? FechaActualizacion { get; set; }

        [ForeignKey("IdCategoria")]
        public virtual Categorias? Categoria { get; set; }
    }
}
