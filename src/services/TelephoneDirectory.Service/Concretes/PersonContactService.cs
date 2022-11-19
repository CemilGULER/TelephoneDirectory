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
using TelephoneDirectory.Service.Abstractions;

namespace TelephoneDirectory.Service.Concretes
{
    public class PersonContactService : IPersonContactService
    {
        private readonly TelephoneDirectoryDbContext dbContext;
        private readonly IMapper mapper;
        public PersonContactService(TelephoneDirectoryDbContext dbContext,
                             IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<Guid> AddPersonContact(AddPersonContactRequest addPersonContactRequest, CancellationToken cancellationToken)
        {
            var personContact = mapper.Map<PersonContact>(addPersonContactRequest);
            await dbContext.PersonContacts.AddAsync(personContact, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            return personContact.Id;
        }

        public async Task RemovePersonContact(Guid id, CancellationToken cancellationToken)
        {
            var personContact = await dbContext.PersonContacts.SingleAsync(x => x.Id == id, cancellationToken);
            personContact.IsDeleted = true;
            dbContext.PersonContacts.Update(personContact);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
