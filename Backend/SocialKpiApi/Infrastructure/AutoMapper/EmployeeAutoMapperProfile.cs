using AutoMapper;
using SocialKpiApi.Models;

namespace SocialKpiApi.Infrastructure.AutoMapper
{
    public class EmployeeAutoMapperProfile : Profile
    {
        public EmployeeAutoMapperProfile() : base(nameof(EmployeeAutoMapperProfile))
        {
            CreateMap<EmployeeInput, Employee>();
            CreateMap<Employee, EmployeeOutput>();
        }
    }
}
