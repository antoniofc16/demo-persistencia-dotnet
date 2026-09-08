using ApiPersistenciaDotnet.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPersistenciaDotnet.DTOs
{
    public class OrdenDTO
    {
        public int? IdOrden { get; set; }
        public int? IdCliente { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public ClienteDTO? Cliente { get; set; }
        public List<OrdenDetalleDTO> Detalle { get; set; } = [];
    }

    public class OrdenDetalleDTO
    {
        public int? IdOrdenDetalle { get; set; }
        public int? IdOrden { get; set; }
        public int? IdProducto { get; set; }
        public int Cantidad { get; set; } = 0;
        public decimal PrecioUnitario { get; set; }
        public ProductoDTO? Producto { get; set; }
    }


    public class OrdenDapperDTO
    {
        public int? IdOrden { get; set; }
        public int? IdCliente { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public string Nombres { get; set; } = string.Empty;
        public string ApellidoPaterno { get; set; } = string.Empty;
        public string ApellidoMaterno { get; set; } = string.Empty;
        public List<OrdenDetalleDapperDTO> Detalle { get; set; } = [];
    }

    public class OrdenDetalleDapperDTO
    {
        public int? IdOrdenDetalle { get; set; }
        public int? IdProducto { get; set; }
        public int Cantidad { get; set; } = 0;
        public decimal PrecioUnitario { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public string NombreCategoria { get; set; } = string.Empty;
    }
}
