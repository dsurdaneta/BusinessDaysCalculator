using AutoMapper;
using DbEntites = DsuDev.BusinessDays.DataAccess.Entites;
using DomainEntities = DsuDev.BusinessDays.Domain.Entities;

namespace DsuDev.BusinessDays.Services.Profiles
{
    public class HolidayContractProfile : Profile
    {
        public HolidayContractProfile()
        {
            this.CreateHolidayMapping();
        }

        public void CreateHolidayMapping()
        {

            this.CreateMap<DbEntites.Holiday, DomainEntities.Holiday>()
                .ForMember(dest => dest.Id, op => op.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, op => op.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, op => op.MapFrom(src => src.Description))
                .ForMember(dest => dest.HolidayDate, op => op.MapFrom(src => src.HolidayDate))
                .ForMember(dest => dest.HolidayStringDate,
                        op => op.MapFrom(src => src.HolidayDate.ToString(DomainEntities.Holiday.DateFormat)))
                .ForAllOtherMembers(dest => dest.Ignore());


            this.CreateMap<DomainEntities.Holiday, DbEntites.Holiday>()
                .ForMember(dest => dest.Id, op => op.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, op => op.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, op => op.MapFrom(src => src.Description))
                .ForMember(dest => dest.HolidayDate, op => op.MapFrom(src => src.HolidayDate))
                .ForMember(dest => dest.Year, op => op.MapFrom(src => src.HolidayDate.Year))
                .ForAllOtherMembers(dest => dest.Ignore());
        }
    }
}
