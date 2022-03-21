using AutoMapper;
using SP.WebApi.Domain.DTO;
using SP.WepApi.Domain.Models.AWS;

namespace SP.WebApi.Domain.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeDTO, Employee>().ReverseMap();

            CreateMap<EmployeeCreateDTO, Employee>().ReverseMap();
            CreateMap<EmployeeCreateDTO, EmployeeDTO>();

            CreateMap<EmployeeUpdateDTO, Employee>().ReverseMap();
            CreateMap<EmployeeUpdateDTO, EmployeeDTO>();

        }
    }
}
