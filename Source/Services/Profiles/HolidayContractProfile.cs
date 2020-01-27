using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using DbModels = DsuDev.BusinessDays.DataAccess.Models;
using DomainEntities = DsuDev.BusinessDays.Domain.Entities;

namespace DsuDev.BusinessDays.Services.Profiles
{
    [ExcludeFromCodeCoverage]
    public class HolidayContractProfile : Profile
    {
        public HolidayContractProfile()
        {
            this.CreateHolidayMapping();
        }

        public void CreateHolidayMapping()
        {

            this.CreateMap<DbModels.Holiday, DomainEntities.Holiday>()
                .ForMember(dest => dest.Id, op => op.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, op => op.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, op => op.MapFrom(src => src.Description))
                .ForMember(dest => dest.HolidayDate, op => op.MapFrom(src => src.HolidayDate))
                .ForMember(dest => dest.HolidayStringDate,
                        op => op.MapFrom(src => src.HolidayDate.ToString(DomainEntities.Holiday.DateFormat)))
                .ForAllOtherMembers(dest => dest.Ignore());


            this.CreateMap<DomainEntities.Holiday, DbModels.Holiday>()
                .ForMember(dest => dest.Id, op => op.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, op => op.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, op => op.MapFrom(src => src.Description))
                .ForMember(dest => dest.HolidayDate, op => op.MapFrom(src => src.HolidayDate))
                .ForMember(dest => dest.Year, op => op.MapFrom(src => src.HolidayDate.Year))
                .ForAllOtherMembers(dest => dest.Ignore());
        }
    }
}
