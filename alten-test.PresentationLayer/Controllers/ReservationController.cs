using System;
using System.Threading.Tasks;
using System.Collections.Generic;
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
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IContactService _contactService;
        private readonly IRoomService _roomService;

        public ReservationController(IReservationService reservationService, 
                                     IContactService contactService,
                                     IRoomService roomService)
        {
            _reservationService = reservationService;
            _contactService = contactService;
            _roomService = roomService;
        }
        
        // GET: api/Reservation
        [HttpGet]
        [Produces("application/json")]
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

            return await _reservationService.List(pageInfo);
        }
        
        // GET: api/Reservation/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReservationDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReservationDto>> GetReservation(int id)
        {
            var reservationDto = await _reservationService.FindById(id);

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
            var contactId = reservationDto.Contact.Id;
            if (contactId <= 0 || roomId <= 0)
            {
                return BadRequest();
            }

            try
            {
                await _reservationService.Update(reservationDto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_reservationService.ReservationExists(id) || !_contactService.ContactExists(contactId) || !_roomService.RoomExists(roomId))
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
        public async Task<ActionResult<ReservationDto>> PostReservation(ReservationDtoInput reservationDtoInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var contactId = reservationDtoInput.Contact.Id;
            if (!_contactService.ContactExists(contactId))
            {
                var newContactDto = await _contactService.Create(reservationDtoInput.Contact);
                reservationDtoInput.Contact.Id = newContactDto.Id;
            }
            
            var roomId = reservationDtoInput.Room.Id;
            if (!_roomService.RoomExists(roomId))
            {
                var newRoomDto = await _roomService.Create(reservationDtoInput.Room);
                reservationDtoInput.Room.Id = newRoomDto.Id;
            }

            var reservationDto = await _reservationService.Create(reservationDtoInput);
            
            return CreatedAtAction(nameof(GetReservation), new { id = reservationDto.Id }, reservationDto);
        }

        // DELETE: api/Reservation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            
            if (!_reservationService.ReservationExists(id))
            {
                return NotFound();
            }

            await _reservationService.Delete(id);

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