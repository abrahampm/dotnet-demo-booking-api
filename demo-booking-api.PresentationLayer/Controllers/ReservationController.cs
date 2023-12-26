using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using alten_test.Core.Dto;
using alten_test.Core.Models;
using alten_test.BusinessLayer.Interfaces;
using alten_test.Core.Models.Authentication;
using alten_test.Core.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace alten_test.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IReservationService _reservationService;
        private readonly IRoomService _roomService;

        public ReservationController(UserManager<ApplicationUser> userManager,
                                     IReservationService reservationService,
                                     IRoomService roomService)
        {
            _reservationService = reservationService;
            _roomService = roomService;
            _userManager = userManager;
        }
        
        // GET: api/Reservation
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationResultDto<ReservationDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> GetReservations(
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
                case "start-date":
                case "start-date_asc":
                    sortProperty = nameof(ReservationDto.StartDate);
                    sortDirection = PageDirection.Ascending;
                    break;
                case "start-date_desc":
                    sortProperty = nameof(ReservationDto.StartDate);
                    sortDirection = PageDirection.Descending;
                    break;
                case "end-date":
                case "end-date_asc":
                    sortProperty = nameof(ReservationDto.StartDate);
                    sortDirection = PageDirection.Ascending;
                    break;
                case "end-date_desc":
                    sortProperty = nameof(ReservationDto.StartDate);
                    sortDirection = PageDirection.Descending;
                    break;
                default:
                    sortProperty = nameof(ReservationDto.Id);
                    sortDirection = PageDirection.Ascending;
                    break;
            }

            var pageInfo = new PaginationInfo(pageNumber, pageSize, sortProperty, sortDirection);
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);
            
            var list = await _reservationService.List(pageInfo, user, roles);
            
            switch (list.ResultType)
            {
                case ServiceResultType.Success:
                    var pageReservationDto = ((SuccessResult<PaginationResultDto<ReservationDto>>) list).Result;
                    return Ok(pageReservationDto);
                    
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
        
        // GET: api/Reservation/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReservationDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> GetReservation(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);
            
            var getResult = await _reservationService.FindById(id, user, roles);

            switch (getResult.ResultType)
            {
                case ServiceResultType.Success:
                    var reservationDto = ((SuccessResult<ReservationDto>) getResult).Result;
                    return Ok(reservationDto);
                    
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
        
        // PUT: api/Reservation/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReservationDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> PutReservation(int id, ReservationDto reservationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (id != reservationDto.Id)
            {
                return BadRequest();
            }
            
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);
            
            try
            {
                var update = await _reservationService.Update(reservationDto, user, roles);
                
                switch (update.ResultType)
                {
                    case ServiceResultType.Success:
                        var updateReservationDto = ((SuccessResult<ReservationDto>) update).Result;
                        return Ok(updateReservationDto);
                        
                    case ServiceResultType.NoPermission:
                        return Unauthorized();
                        
                    case ServiceResultType.NotFound:
                        return NotFound();
                        
                    case ServiceResultType.Error:
                        var updateError = (ErrorResult) update;
                        return StatusCode(StatusCodes.Status500InternalServerError, new StatusResponseDto
                        {
                            Status = "Error",
                            Message = updateError.Error
                        });
                        
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }
        
        // POST: api/Reservation
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ReservationDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> PostReservation(ReservationDtoInput reservationDtoInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);

            var create = await _reservationService.Create(reservationDtoInput, user, roles);
            
            switch (create.ResultType)
            {
                case ServiceResultType.Success:
                    var reservationDto = ((SuccessResult<ReservationDto>) create).Result;
                    return CreatedAtAction(nameof(PostReservation), new { id = reservationDto.Id }, reservationDto);
                    
                case ServiceResultType.NoPermission:
                    return Unauthorized();

                case ServiceResultType.Error:
                    var createError = (ErrorResult) create;
                    return StatusCode(StatusCodes.Status500InternalServerError, new StatusResponseDto
                    {
                        Status = "Error",
                        Message = createError.Error
                    });
            }

            return NoContent();
        }

        // DELETE: api/Reservation/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);

            var delete = await _reservationService.Delete(id, user, roles);
            
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
        
        

        // GET: api/Reservation/Availability
        [HttpGet("Availability")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RoomDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAvailability(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate
        )
        {
            var available = await _reservationService.GetAvailability(startDate, endDate);
            
            switch (available.ResultType)
            {
                case ServiceResultType.Success:
                    var availableRooms = ((SuccessResult<List<RoomDto>>) available).Result;
                    return Ok(availableRooms);
                    
                case ServiceResultType.NotFound:
                    return NotFound();
                    
                case ServiceResultType.Error:
                    var createError = (ErrorResult) available;
                    return StatusCode(StatusCodes.Status500InternalServerError, createError.Error);
                        
            }

            return NoContent();
        }
        
    }
}