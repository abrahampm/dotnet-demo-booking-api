using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using alten_test.Core.Dto;
using alten_test.Core.Models;
using alten_test.BusinessLayer.Interfaces;
using alten_test.Core.Utilities;

namespace alten_test.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }
            
        // GET: api/Contact
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<PaginationResultDto<ContactDto>>> GetContacts(
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize,
            [FromQuery] string filterBy,
            [FromQuery] string searchTerm,
            [FromQuery] string sortBy)
        {   
            string sortProperty;
            PageDirection sortDirection;
            switch (sortBy)
            {
                case "first-name":
                case "first-name_asc":
                    sortProperty = nameof(ContactDto.FirstName);
                    sortDirection = PageDirection.Ascending;
                    break;
                case "first-name_desc":
                    sortProperty = nameof(ContactDto.FirstName);
                    sortDirection = PageDirection.Descending;
                    break;
                case "last-name":
                case "last-name_asc":
                    sortProperty = nameof(ContactDto.LastName);
                    sortDirection = PageDirection.Ascending;
                    break;
                case "last-name_desc":
                    sortProperty = nameof(ContactDto.LastName);
                    sortDirection = PageDirection.Descending;
                    break;
                case "birth-date":
                case "birth-date_asc":
                    sortProperty = nameof(ContactDto.BirthDate);
                    sortDirection = PageDirection.Ascending;
                    break;
                case "birth-date_desc":
                    sortProperty = nameof(ContactDto.BirthDate);
                    sortDirection = PageDirection.Descending;
                    break;
                default:
                    sortProperty = nameof(ContactDto.Id);
                    sortDirection = PageDirection.Ascending;
                    break;
            }
            string filterProperty;
            switch (filterBy)
            {
                case "first-name":
                    filterProperty = nameof(ContactDto.FirstName);
                    break;
                default:
                    filterProperty = "";
                    break;
            }

            var pageInfo = new PaginationInfo(pageNumber, pageSize, sortProperty, sortDirection, filterProperty, searchTerm);

            return await _contactService.List(pageInfo);
        }
        
        // GET: api/Contact/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContactDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ContactDto>> GetContact(int id)
        {
            var contactDto = await _contactService.FindById(id);

            if (contactDto == null)
            {
                return NotFound();
            }

            return contactDto;
        }
        
        // PUT: api/Contact/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutContact(int id, ContactDto contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (id != contact.Id)
            {
                return BadRequest();
            }

            try
            {
                await _contactService.Update(contact);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_contactService.ContactExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        
        // POST: api/Contact
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ContactDto>> PostContact(ContactDtoInput contactDtoInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var contactDto = await _contactService.Create(contactDtoInput);
            
            return CreatedAtAction(nameof(GetContact), new { id = contactDto.Id }, contactDto);
        }

        // DELETE: api/Contact/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            
            if (!_contactService.ContactExists(id))
            {
                return NotFound();
            }

            await _contactService.Delete(id);

            return NoContent();
        }
    }
}