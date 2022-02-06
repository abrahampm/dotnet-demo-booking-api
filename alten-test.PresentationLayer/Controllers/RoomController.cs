using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using alten_test.Core.Dto;
using alten_test.Core.Models;
using alten_test.BusinessLayer.Interfaces;
using alten_test.Core.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace alten_test.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }
            
        // GET: api/Room
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<PaginationResultDto<RoomDto>>> GetRooms(
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
                case "number":
                case "number_asc":
                    sortProperty = nameof(RoomDto.Number);
                    sortDirection = PageDirection.Ascending;
                    break;
                case "number_desc":
                    sortProperty = nameof(RoomDto.Number);
                    sortDirection = PageDirection.Descending;
                    break;
                case "type":
                case "type_asc":
                    sortProperty = nameof(RoomDto.Type);
                    sortDirection = PageDirection.Ascending;
                    break;
                case "type_desc":
                    sortProperty = nameof(RoomDto.Type);
                    sortDirection = PageDirection.Descending;
                    break;
                default:
                    sortProperty = nameof(RoomDto.Number);
                    sortDirection = PageDirection.Ascending;
                    break;
            }
            string filterProperty;
            switch (filterBy)
            {
                case "type":
                    filterProperty = nameof(RoomDto.Type);
                    RoomType roomType;
                    if (Enum.TryParse(searchTerm, out roomType))
                    {
                        searchTerm = ((int) roomType).ToString();
                    } 
                    else
                    {
                        return BadRequest();
                    }
                    break;
                default:
                    filterProperty = "";
                    break;
            }

            var pageInfo = new PaginationInfo(pageNumber, pageSize, sortProperty, sortDirection, filterProperty, searchTerm);

            return await _roomService.List(pageInfo);
        }
        
        // GET: api/Room/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoomDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RoomDto>> GetRoom(int id)
        {
            var roomDto = await _roomService.FindById(id);

            if (roomDto == null)
            {
                return NotFound();
            }

            return roomDto;
        }
        
        // PUT: api/Room/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutRoom(int id, RoomDto room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (id != room.Id)
            {
                return BadRequest();
            }

            try
            {
                await _roomService.Update(room);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_roomService.RoomExists(id))
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
        
        // POST: api/Room
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RoomDto>> PostRoom(RoomDtoInput roomDtoInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var roomDto = await _roomService.Create(roomDtoInput);
            
            return CreatedAtAction(nameof(GetRoom), new { id = roomDto.Id }, roomDto);
        }

        // DELETE: api/Room/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            
            if (!_roomService.RoomExists(id))
            {
                return NotFound();
            }

            await _roomService.Delete(id);

            return NoContent();
        }
    }
}