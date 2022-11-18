using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.Contact.Contracts.Dto;
using TelephoneDirectory.Data.Entities;

namespace TelephoneDirectory.Service.Mappers
{
    public class PersonMapperProfile : Profile
    {
        public PersonMapperProfile() : base()
        {
            CreateMap<Person, AddPersonRequest>().ReverseMap();
            CreateMap<Person, SearchPersonResponse>().ReverseMap();
            CreateMap< PersonDetailResponse,Person>()
               .ForMember(x => x.PersonContacts, m => m.MapFrom(f => f.PersonContacts.Select(s => s.ContactDetail).ToList()))
               .ReverseMap();
        }
    }
}
