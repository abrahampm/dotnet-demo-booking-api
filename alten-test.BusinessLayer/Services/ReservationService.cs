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
        private readonly IReservationRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReservationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.Reservations;
            _mapper = mapper;
        }

        public async Task<ReservationDto> Create(ReservationDtoInput reservationDtoInput)
        {
            var reservation = _mapper.Map<Reservation>(reservationDtoInput);
            reservation.Contact = null;
            reservation.Room = null;
            await _repository.Insert(reservation);
            await _unitOfWork.Save();
            var reservationDto = _mapper.Map<ReservationDto>(reservation);
            return reservationDto;
        }

        public async Task<ReservationDto> FindById(int id)
        {
            var reservation = await _repository.GetById(id);
            var reservationDto = _mapper.Map<ReservationDto>(reservation);
            return reservationDto;
        }

        public async Task<ReservationDto> Update(ReservationDto reservationDto)
        {
            var reservation = _mapper.Map<Reservation>(reservationDto);
            reservation.Contact = null;
            reservation.Room = null;
            _repository.Update(reservation);
            await _unitOfWork.Save();
            return reservationDto;
        }

        public async Task Delete(int id)
        {
            var reservation = await _repository.GetById(id);

            if (reservation != null)
            {
                _repository.Delete(reservation);
                await _unitOfWork.Save();
            }
        }

        public async Task<PaginationResultDto<ReservationDto>> List(IPaginationInfo pageInfo)
        {
            
            var pageData = await _repository.GetAllPaginated(pageInfo);
            pageInfo.Total = await _repository.GetAll().CountAsync();
            
            var pageInfoDto = _mapper.Map<PaginationInfoDto>(pageInfo);
            var pageDataDto = _mapper.Map<IEnumerable<ReservationDto>>(pageData);

            return new PaginationResultDto<ReservationDto>(pageInfoDto, pageDataDto);
        }

        public bool ReservationExists(int id)
        {
            return _repository.Exists(id);
        }
    }
}