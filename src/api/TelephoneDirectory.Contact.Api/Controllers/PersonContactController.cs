using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TelephoneDirectory.Contact.Contracts.Dto;
using TelephoneDirectory.Service.Abstractions;

namespace TelephoneDirectory.Contact.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonContactController : ControllerBase
    {
        private readonly IPersonContactService personContactService;
        public PersonContactController(IPersonContactService personContactService)
        {
            this.personContactService = personContactService;

        }
        /// <summary>
        /// Rehberdeki kişiye iletişim bilgisi ekleme
        /// </summary>
        /// <param name="addPersonContactRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddPersonContact([FromBody] AddPersonContactRequest addPersonContactRequest, CancellationToken cancellationToken)
        {
            await personContactService.AddPersonContact(addPersonContactRequest, cancellationToken);
            return Ok();
        }
        /// <summary>
        /// Rehberdeki kişiden iletişim bilgisi kaldırma
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> RemovePersonContact([FromBody] Guid id, CancellationToken cancellationToken)
        {
            await personContactService.RemovePersonContact(id, cancellationToken);
            return Ok();
        }
    }
}
