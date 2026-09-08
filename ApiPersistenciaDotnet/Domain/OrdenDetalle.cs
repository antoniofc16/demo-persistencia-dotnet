using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPersistenciaDotnet.Domain
{
    public class OrdenDetalle
    {
        [Key]
        [Column("idOrdenDetalle")]
        public int IdOrdenDetalle { get; set; }
        [Column("idOrden")]
        public int IdOrden { get; set; }
        [Column("idProducto")]
        public int IdProducto { get; set; }
        [Column("cantidad")]
        public int Cantidad { get; set; }
        [Column("precioUnitario")]
        public decimal PrecioUnitario { get; set; }

        [ForeignKey("IdOrden")]
        public virtual Ordenes? Orden { get; set; }
        [ForeignKey("IdProducto")]
        public virtual Productos? Producto { get; set; }
    }
}
