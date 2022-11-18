using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.Contact.Contracts.Dto;
using TelephoneDirectory.Data.Access.Context;
using TelephoneDirectory.Data.Entities;
using TelephoneDirectory.Report.Contracts.Dto;
using TelephoneDirectory.Service.Abstractions;

namespace TelephoneDirectory.Service.Concretes
{
    public class PersonService : IPersonService
    {
        private readonly TelephoneDirectoryDbContext dbContext;
        private readonly IMapper mapper;
        public PersonService(TelephoneDirectoryDbContext dbContext,
                             IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task AddPerson(AddPersonRequest addPersonRequest, CancellationToken cancellationToken)
        {
            var person = mapper.Map<Person>(addPersonRequest);
            await dbContext.Persons.AddAsync(person, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

        }

        public async Task RemovePerson(Guid id, CancellationToken cancellationToken)
        {
            var person = await dbContext.Persons.SingleAsync(x => x.Id == id, cancellationToken);
            person.IsDeleted = true;
            dbContext.Persons.Update(person);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<SearchPersonResponse>> SearchPerson(SearchPersonRequest searchPerson, CancellationToken cancellationToken)
        {
            var personList = dbContext.Persons.Where(x => x.IsDeleted == false);
            if (searchPerson.Id != null && searchPerson.Id != Guid.Empty)
            {
                personList = personList.Where(x => x.Id == searchPerson.Id);
            }
            else
            {
                if (!string.IsNullOrEmpty(searchPerson.Name))
                {
                    personList = personList.Where(x => x.Name == searchPerson.Name);
                }
                if (!string.IsNullOrEmpty(searchPerson.Surname))
                {
                    personList = personList.Where(x => x.Surname == searchPerson.Name);
                }
                if (!string.IsNullOrEmpty(searchPerson.Company))
                {
                    personList = personList.Where(x => x.Company == searchPerson.Company);
                }
            }

            return await personList.Select(usr => mapper.Map<SearchPersonResponse>(usr)).ToListAsync(cancellationToken);

        }
        public async Task<PersonDetailResponse> PersonDetail(Guid id, CancellationToken cancellationToken)
        {
            var person =await  dbContext.Persons
                .Include(x => x.PersonContacts.Where(x => x.IsDeleted == false))
                .Where(x => x.Id == id && x.IsDeleted == false).SingleAsync(cancellationToken);
            return mapper.Map<PersonDetailResponse>(person);

        }
        public async Task<List<LocationDetailResponse>> LocationDetail( CancellationToken cancellationToken)
        {
            var query = dbContext.
                   PersonContacts.
                   Where(x => x.IsDeleted == false && x.ContactType == Data.Entities.Enum.ContactType.Location).
                   GroupBy(x => x.ContactDetail).
                   Select(g => new LocationDetailResponse
                   {
                       LocationName = g.Key,

                   });
            return await query.ToListAsync(cancellationToken);
        }
    }
}
