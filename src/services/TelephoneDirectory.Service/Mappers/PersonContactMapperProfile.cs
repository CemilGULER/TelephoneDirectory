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
    public class PersonContactMapperProfile : Profile
    {
        public PersonContactMapperProfile() : base()
        {
            CreateMap<PersonContact, AddPersonContactRequest>().ReverseMap();
            CreateMap<PersonContact, PersonContactInfo>().ReverseMap();

        }
    }
}
