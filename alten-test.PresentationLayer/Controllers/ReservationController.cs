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
        [Authorize]
        public async Task<ActionResult<PaginationResultDto<ReservationDto>>> GetReservations(
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
            
            return await _reservationService.List(pageInfo, user, roles);
        }
        
        // GET: api/Reservation/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReservationDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<ActionResult<ReservationDto>> GetReservation(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);
            
            var reservationDto = await _reservationService.FindById(id, user, roles);

            if (reservationDto == null)
            {
                return NotFound();
            }

            return reservationDto;
        }
        
        // PUT: api/Reservation/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
            var roomId = reservationDto.Room.Id;
            if (!_reservationService.ReservationExists(id) || !_roomService.RoomExists(roomId))
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);
            
            try
            {
                await _reservationService.Update(reservationDto, user, roles);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_reservationService.ReservationExists(id) || !_roomService.RoomExists(roomId))
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
        
        // POST: api/Reservation
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<ActionResult<ReservationDto>> PostReservation(ReservationDtoInput reservationDtoInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (!_roomService.RoomExists(reservationDtoInput.Room.Id))
            {
                return BadRequest();
            }
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);

            var reservationDto = await _reservationService.Create(reservationDtoInput, user, roles);
            
            return CreatedAtAction(nameof(GetReservation), new { id = reservationDto.Id }, reservationDto);
        }

        // DELETE: api/Reservation/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            
            if (!_reservationService.ReservationExists(id))
            {
                return NotFound();
            }
            
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);

            await _reservationService.Delete(id, user, roles);

            return NoContent();
        }
        
        

        // GET: api/Reservation/Availability
        [HttpGet("Availability")]
        [Produces("application/json")]
        public async Task<ActionResult<List<RoomDto>>> GetAvailability(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate
        )
        {
            var availableRooms = await _reservationService.GetAvailability(startDate, endDate);

            if (availableRooms.Count == 0)
            {
                return NotFound();
            }

            return availableRooms;
        }
        
    }
}