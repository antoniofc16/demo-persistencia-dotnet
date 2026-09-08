using AutoMapper;

namespace ApiPersistenciaDotnet.Data
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Domain.Categorias, DTOs.CategoriaDTO>();

            CreateMap<Domain.Productos, DTOs.ProductoDTO>()
                .ForMember(dest => dest.Categoria, opt => opt.MapFrom(src => src.Categoria));

            CreateMap<Domain.Clientes, DTOs.ClienteDTO>();

            CreateMap<Domain.Categorias, DTOs.CategoriaDTO>();

            CreateMap<Domain.Ordenes, DTOs.OrdenDTO>()
                .ForMember(dest => dest.Cliente, opt => opt.MapFrom(src => src.Cliente));

            CreateMap<Domain.OrdenDetalle, DTOs.OrdenDetalleDTO>()
                .ForMember(dest => dest.Producto, opt => opt.MapFrom(src => src.Producto));
        }
    }
}
