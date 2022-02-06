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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationResultDto<RoomDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRooms(
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

            var list = await _roomService.List(pageInfo);
            
            switch (list.ResultType)
            {
                case ServiceResultType.Success:
                    var pageRoomDto = ((SuccessResult<PaginationResultDto<RoomDto>>) list).Result;
                    return Ok(pageRoomDto);
                    
                case ServiceResultType.NoPermission:
                    return Unauthorized();
                    
                case ServiceResultType.NotFound:
                    return NotFound();
                    
                case ServiceResultType.Error:
                    var listError = (ErrorResult) list;
                    return StatusCode(StatusCodes.Status500InternalServerError, listError.Error);
                        
            }

            return NoContent();
        }
        
        // GET: api/Room/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoomDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRoom(int id)
        {
            var getResult = await _roomService.FindById(id);

            switch (getResult.ResultType)
            {
                case ServiceResultType.Success:
                    var roomDto = ((SuccessResult<RoomDto>) getResult).Result;
                    return Ok(roomDto);
                    
                case ServiceResultType.NoPermission:
                    return Unauthorized();
                    
                case ServiceResultType.NotFound:
                    return NotFound();
                    
                case ServiceResultType.Error:
                    var getError = (ErrorResult) getResult;
                    return StatusCode(StatusCodes.Status500InternalServerError, getError.Error);
                        
            }

            return NoContent();
        }
        
        // PUT: api/Room/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoomDto))]
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
                var update = await _roomService.Update(room);
                
                switch (update.ResultType)
                {
                    case ServiceResultType.Success:
                        var updateRoomDto = ((SuccessResult<RoomDto>) update).Result;
                        return Ok(updateRoomDto);
                        
                    case ServiceResultType.NoPermission:
                        return Unauthorized();
                        
                    case ServiceResultType.NotFound:
                        return NotFound();
                        
                    case ServiceResultType.Error:
                        var updateError = (ErrorResult) update;
                        return StatusCode(StatusCodes.Status500InternalServerError, updateError.Error);
                        
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }
        
        // POST: api/Room
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostRoom(RoomDtoInput roomDtoInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var create = await _roomService.Create(roomDtoInput);
            
            switch (create.ResultType)
            {
                case ServiceResultType.Success:
                    var roomDto = ((SuccessResult<RoomDto>) create).Result;
                    return CreatedAtAction(nameof(PostRoom), new { id = roomDto.Id }, roomDto);
                    
                case ServiceResultType.NoPermission:
                    return Unauthorized();
                    
                case ServiceResultType.Error:
                    var createError = (ErrorResult) create;
                    return StatusCode(StatusCodes.Status500InternalServerError, createError.Error);
                        
            }

            return NoContent();
        }

        // DELETE: api/Room/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            
            var delete = await _roomService.Delete(id);

            switch (delete.ResultType)
            {
                case ServiceResultType.Success:
                    return Ok();
                    
                case ServiceResultType.NoPermission:
                    return Unauthorized();
                    
                case ServiceResultType.NotFound:
                    return NotFound();
                    
                case ServiceResultType.Error:
                    var deleteError = (ErrorResult) delete;
                    return StatusCode(StatusCodes.Status500InternalServerError, deleteError.Error);
                        
            }

            return NoContent();
        }
    }
}