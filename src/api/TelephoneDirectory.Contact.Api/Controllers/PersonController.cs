using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TelephoneDirectory.Contact.Contracts.Dto;
using TelephoneDirectory.Service.Abstractions;

namespace TelephoneDirectory.Contact.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService personService;
        public PersonController(IPersonService personService)
        {
            this.personService = personService;

        }
        /// <summary>
        /// Rehberde kişi oluşturma
        /// </summary>
        /// <param name="addPersonRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddPerson([FromBody] AddPersonRequest addPersonRequest, CancellationToken cancellationToken)
        {
            await personService.AddPerson(addPersonRequest, cancellationToken);
            return Ok();
        }
        /// <summary>
        /// Rehberde kişi kaldırma
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> RemovePerson([FromBody] Guid id, CancellationToken cancellationToken)
        {
            await personService.RemovePerson(id, cancellationToken);
            return Ok();
        }
        /// <summary>
        /// Rehberdeki kişilerin listelenmesi
        /// </summary>
        /// <param name="searchPerson"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> SearchPerson([FromQuery] SearchPersonRequest searchPerson, CancellationToken cancellationToken)
        {
            return Ok(await personService.SearchPerson(searchPerson, cancellationToken));

        }
        [HttpGet("person-detail")]
        public async Task<IActionResult> PersonDetail([FromQuery] Guid id, CancellationToken cancellationToken)
        {
            return Ok(await personService.PersonDetail(id, cancellationToken));

        }
    }
}
