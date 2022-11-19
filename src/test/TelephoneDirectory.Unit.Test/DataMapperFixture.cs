using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.Contact.Contracts.Dto;
using TelephoneDirectory.Data.Entities;
using TelephoneDirectory.Report.Contracts.Dto;
using TelephoneDirectory.Service.Mappers;

namespace TelephoneDirectory.Unit.Test
{
    public class DataMapperFixture
    {
        private static IMapper mapper;
        public DataMapperFixture()
        {
            if (mapper == null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<PersonMapperProfile>();
                    cfg.AddProfile<PersonContactMapperProfile>();
                    cfg.AddProfile<ReportMapperProfile>();

                });
                var _mapper = config.CreateMapper();
                mapper = _mapper;
            }
        }
        [Fact]
        public void AddPersonRequestMapping()
        {
            var person = new AddPersonRequest()
            {
                Name = "Cemil",
                Surname = "GÜLER",
                Company = "Test"
            };
            var entity = mapper.Map<Person>(person);
            var dto = mapper.Map<AddPersonRequest>(entity);

            Assert.NotNull(dto);
            Assert.NotNull(entity);
            Assert.Equal(person.Name, dto.Name);
            Assert.Equal(person.Surname, dto.Surname);
            Assert.Equal(person.Company, dto.Company);

        }
        [Fact]
        public void PersonDetailResponseMapping()
        {
            var person = new Person()
            {
                Id = new Guid("52a5d7a3-5100-4e1b-be3a-a65eb227f2be"),
                Name = "Cemil",
                Surname = "GÜLER",
                Company = "Test",

            };
            var personContact = new PersonContact()
            {
                PersonId = new Guid("52a5d7a3-5100-4e1b-be3a-a65eb227f2be"),
                ContactType = Data.Entities.Enum.ContactType.PhoneNumber,
                ContactDetail = "5555555555"
            };
            person.PersonContacts = new List<PersonContact>();
            person.PersonContacts.Add(personContact);
            var dto = mapper.Map<PersonDetailResponse>(person);
            Assert.NotNull(dto);
            Assert.Equal(person.Id, dto.Id);
            Assert.Equal(person.Name, dto.Name);
            Assert.Equal(person.Surname, dto.Surname);
            Assert.Equal(person.Company, dto.Company);
            Assert.Equal(person.PersonContacts.FirstOrDefault().PersonId, dto.PersonContacts.FirstOrDefault().PersonId);
            Assert.Equal(person.PersonContacts.FirstOrDefault().ContactDetail, dto.PersonContacts.FirstOrDefault().ContactDetail);

        }

        [Fact]
        public void AddPersonContactRequestMapping()
        {
            var personContact = new AddPersonContactRequest()
            {
                PersonId = new Guid("52a5d7a3-5100-4e1b-be3a-a65eb227f2be"),
                ContactType = Contact.Contracts.Enum.ContactType.PhoneNumber,
                ContactDetail = "5555555555"
            };
            var entity = mapper.Map<PersonContact>(personContact);
            var dto = mapper.Map<AddPersonContactRequest>(entity);

            Assert.NotNull(dto);
            Assert.NotNull(entity);
            Assert.Equal(personContact.PersonId, dto.PersonId);
            Assert.Equal(personContact.ContactType, dto.ContactType);
            Assert.Equal(personContact.ContactDetail, dto.ContactDetail);

        }
        [Fact]
        public void ReportMapping()
        {
            var report = new TelephoneDirectory.Data.Entities.Report()
            {
                ReportStatus = true,
                Id= new Guid("52a5d7a3-5100-4e1b-be3a-a65eb227f2be"),
                CreatedAt =DateTime.UtcNow,
            };
            var dto = mapper.Map<ReportListResponse>(report);            
            Assert.NotNull(dto);
            
            Assert.Equal(report.Id, dto.Id);
            Assert.Equal(report.CreatedAt, dto.CreatedAt);
            
        }
    }
}
