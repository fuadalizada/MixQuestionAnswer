using AutoMapper;
using MixQuestionAnswer.Domains;
namespace MixQuestionAnswer.BLL.DTOs
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            AllowNullCollections = true;
            AllowNullDestinationValues = true;

            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<UserRoleDTO, UserRole>().ReverseMap();
            CreateMap<RoleDTO, Role>().ReverseMap();
            CreateMap<BlogDTO, Blog>().ReverseMap();
        }
    }
}
