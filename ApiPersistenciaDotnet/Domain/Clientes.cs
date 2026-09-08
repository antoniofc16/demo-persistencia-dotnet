using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPersistenciaDotnet.Domain
{
    public class Clientes
    {
        [Key]
        [Column("idCliente")]
        public int IdCliente { get; set; }
        [Column("nombres")]
        public required string Nombres { get; set; }
        [Column("apellidoPaterno")]
        public required string ApellidoPaterno { get; set; }
        [Column("apellidoMaterno")]
        public required string ApellidoMaterno { get; set; }
        [Column("correoElectronico")]
        public string CorreoElectronico { get; set; } = string.Empty;
        [Column("telefono")]
        public string Telefono { get; set; } = string.Empty;
        [Column("direccion")]
        public string Direccion { get; set; } = string.Empty;
        [Column("fechaNacimiento")]
        public required DateTime FechaNacimiento { get; set; }
        [Column("activo")]
        public bool Activo { get; set; }
        [Column("fechaRegistro")]
        public required DateTime FechaRegistro { get; set; }
        [Column("fechaActualizacion")]
        public DateTime? FechaActualizacion { get; set; }
    }
}
