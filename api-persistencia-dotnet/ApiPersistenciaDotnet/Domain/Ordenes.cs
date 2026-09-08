using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPersistenciaDotnet.Domain
{
    public class Ordenes
    {
        [Key]
        [Column("idOrden")]
        public int IdOrden { get; set; }
        [Column("idCliente")]
        public int IdCliente { get; set; }
        [Column("fechaRegistro")]
        public required DateTime FechaRegistro { get; set; }

        [ForeignKey("IdCliente")]
        public virtual Clientes? Cliente { get; set; }

        public virtual ICollection<OrdenDetalle> Detalle { get; set; } = new List<OrdenDetalle>();
    }
}
