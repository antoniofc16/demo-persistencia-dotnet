using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPersistenciaDotnet.Domain
{
    public class Categorias
    {
        [Key]
        [Column("idCategoria")]
        public int IdCategoria { get; set; }
        [Column("nombreCategoria")]
        public string NombreCategoria { get; set; } = string.Empty;
        [Column("activo")]
        public bool Activo { get; set; }
        [Column("fechaRegistro")]
        public required DateTime FechaRegistro { get; set; }
        [Column("fechaActualizacion")]
        public DateTime? FechaActualizacion { get; set; }
    }
}
