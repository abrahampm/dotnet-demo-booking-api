using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

using alten_test.Core.Dto;
using alten_test.Core.Models;
using alten_test.Core.Interfaces;
using alten_test.BusinessLayer.Interfaces;
using alten_test.DataAccessLayer.Interfaces;

namespace alten_test.BusinessLayer.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReservationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _reservationRepository = unitOfWork.Reservations;
            _roomRepository = unitOfWork.Rooms;
            _mapper = mapper;
        }

        public async Task<ReservationDto> Create(ReservationDtoInput reservationDtoInput)
        {
            var reservation = _mapper.Map<Reservation>(reservationDtoInput);
            reservation.Contact = null;
            reservation.Room = null;
            await _reservationRepository.Insert(reservation);
            await _unitOfWork.Save();
            var reservationDto = _mapper.Map<ReservationDto>(reservation);
            return reservationDto;
        }

        public async Task<ReservationDto> FindById(int id)
        {
            var reservation = await _reservationRepository.GetById(id);
            var reservationDto = _mapper.Map<ReservationDto>(reservation);
            return reservationDto;
        }

        public async Task<ReservationDto> Update(ReservationDto reservationDto)
        {
            var reservation = _mapper.Map<Reservation>(reservationDto);
            reservation.Contact = null;
            reservation.Room = null;
            _reservationRepository.Update(reservation);
            await _unitOfWork.Save();
            return reservationDto;
        }

        public async Task Delete(int id)
        {
            var reservation = await _reservationRepository.GetById(id);

            if (reservation != null)
            {
                _reservationRepository.Delete(reservation);
                await _unitOfWork.Save();
            }
        }

        public async Task<PaginationResultDto<ReservationDto>> List(IPaginationInfo pageInfo)
        {
            
            var pageData = await _reservationRepository.GetAllPaginated(pageInfo);
            pageInfo.Total = await _reservationRepository.GetAll().CountAsync();
            
            var pageInfoDto = _mapper.Map<PaginationInfoDto>(pageInfo);
            var pageDataDto = _mapper.Map<IEnumerable<ReservationDto>>(pageData);

            return new PaginationResultDto<ReservationDto>(pageInfoDto, pageDataDto);
        }


        public async Task<List<RoomDto>> GetAvailability(DateTime startDate, DateTime endDate)
        {
            var availableRooms = await _roomRepository.GetAvailableWithStoredProcedure(startDate, endDate);
            
            var availableRoomsDto = _mapper.Map<List<RoomDto>>(availableRooms);

            return availableRoomsDto;
        }

        public bool ReservationExists(int id)
        {
            return _reservationRepository.Exists(id);
        }
    }
}