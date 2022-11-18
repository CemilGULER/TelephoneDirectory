using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.Report.Contracts.Dto;

namespace TelephoneDirectory.Service.Mappers
{
    public class ReportMapperProfile: Profile
    {
        public ReportMapperProfile() : base()
        {
            CreateMap<TelephoneDirectory.Data.Entities.Report, ReportListResponse>()
                .ForMember(x=>x.Status,opt => opt.MapFrom(x => x.ReportStatus == true ? "Tamamlandı" : "Hazırlanıyor"));
           
        }
    }
}
