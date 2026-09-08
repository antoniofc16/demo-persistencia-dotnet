using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPersistenciaDotnet.DTOs
{
    public class CategoriaDTO
    {
        public int? IdCategoria { get; set; }
        public string NombreCategoria { get; set; } = string.Empty;
        public bool Activo { get; set; }
    }
}
