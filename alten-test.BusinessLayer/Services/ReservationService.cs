using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

using alten_test.Core.Dto;
using alten_test.Core.Models;
using alten_test.Core.Interfaces;
using alten_test.BusinessLayer.Interfaces;
using alten_test.Core.Models.Authentication;
using alten_test.Core.Utilities;
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

        public async Task<ServiceResult> Create(ReservationDtoInput reservationDtoInput, ApplicationUser user, IList<string> roles)
        {
            var reservation = _mapper.Map<Reservation>(reservationDtoInput);

            var validate = await _validateReservation(reservation);

            if (validate.ResultType == ServiceResultType.Error)
            {
                return validate;
            }
            
            // Unset related entity before inserting in the database
            reservation.Room = null;
            // Set current user id to the reservation
            reservation.ApplicationUserId = user.Id;
            
            await _reservationRepository.Insert(reservation);
            await _unitOfWork.Save();
            
            var reservationDto = _mapper.Map<ReservationDto>(reservation);
            return new SuccessResult<ReservationDto>(reservationDto);
        }

        public async Task<ServiceResult> FindById(int id, ApplicationUser user, IList<string> roles)
        {
            var reservation = await _reservationRepository.GetById(id);
            if (reservation != null)
            {
                if (reservation.ApplicationUserId == user.Id || roles.Contains(ApplicationUserRoles.Admin))
                {
                    var reservationDto = _mapper.Map<ReservationDto>(reservation);
                    return new SuccessResult<ReservationDto>(reservationDto);    
                }
                else
                {
                    return new NoPermissionResult();
                }
            }
            else
            {
                return new NotFoundResult();
            }
            
            
        }

        public async Task<ServiceResult> Update(ReservationDto reservationDto, ApplicationUser user, IList<string> roles)
        {
            var reservation = await _reservationRepository.GetById(reservationDto.Id);
            var room = _roomRepository.Exists(reservationDto.Room.Id);

            if (reservation != null || !room)
            {
                if (reservation.ApplicationUserId == user.Id || roles.Contains(ApplicationUserRoles.Admin))
                {
                    reservation = _mapper.Map<Reservation>(reservationDto);
                    
                    var validate = await _validateReservation(reservation);

                    if (validate.ResultType == ServiceResultType.Error)
                    {
                        return validate;
                    }
                    
                    reservation.Room = null;
                    
                    _reservationRepository.Update(reservation);
                    await _unitOfWork.Save();
                    
                    reservationDto = _mapper.Map<ReservationDto>(reservation);
                    return new SuccessResult<ReservationDto>(reservationDto);
                }
                else
                {
                    return new NoPermissionResult();
                }
            }
            else
            {
                return new NotFoundResult();
            }
        }

        public async Task<ServiceResult> Delete(int id, ApplicationUser user, IList<string> roles)
        {
            var reservation = await _reservationRepository.GetById(id);

            if (reservation != null)
            {
                if (reservation.ApplicationUserId == user.Id || roles.Contains(ApplicationUserRoles.Admin))
                {
                    _reservationRepository.Delete(reservation);
                    await _unitOfWork.Save();
                    return new SuccessResult();
                }
                else
                {
                    return new NoPermissionResult();
                }
            }
            else
            {
                return new NotFoundResult();
            }
        }

        public async Task<ServiceResult> List(IPaginationInfo pageInfo, ApplicationUser user, IList<string> roles)
        {
            List<Reservation> pageData;
            if (roles.Contains(ApplicationUserRoles.Admin))
            {
                pageData = await _reservationRepository.GetAllPaginated(pageInfo);
                pageInfo.Total = await _reservationRepository.GetTotal();
            } else {
                pageData = await _reservationRepository.GetByUserPaginated(pageInfo, user);
                pageInfo.Total = await _reservationRepository.GetTotalByUser(user);
            }
            
            var pageInfoDto = _mapper.Map<PaginationInfoDto>(pageInfo);
            var pageDataDto = _mapper.Map<IEnumerable<ReservationDto>>(pageData);

            var page = new PaginationResultDto<ReservationDto>(pageInfoDto, pageDataDto);
            
            return new SuccessResult<PaginationResultDto<ReservationDto>>(page);
        }


        public async Task<ServiceResult> GetAvailability(DateTime startDate, DateTime endDate)
        {
            var availableRooms = await _roomRepository.GetAvailableWithStoredProcedure(startDate, endDate);

            if (availableRooms.Count == 0)
            {
                return new NotFoundResult();
            }
            else
            {
                var availableRoomsDto = _mapper.Map<List<RoomDto>>(availableRooms);
                return new SuccessResult<List<RoomDto>>(availableRoomsDto);    
            }
            
        }

        public bool ReservationExists(int id)
        {
            return _reservationRepository.Exists(id);
        }

        private async Task<ServiceResult> _validateReservation(Reservation reservation)
        {
            // Check reservation start and end dates
            if (DateTime.Compare(reservation.StartDate, reservation.EndDate) > 0)
            {
                return new ErrorResult("Reservation start date is later than end date!");
            }
            
            if (DateTime.Compare(reservation.StartDate, DateTime.Today.AddDays(1)) < 0)
            {
                return new ErrorResult("Reservation must start at least the next day of booking!");
            }
            
            if (DateTime.Compare(reservation.EndDate, DateTime.Today.AddDays(30)) > 0)
            {
                return new ErrorResult("Reservation can't be done with more than 30 days in advance!");
            }
            
            if (DateTime.Compare(reservation.StartDate.AddDays(3), reservation.EndDate) < 0)
            {
                return new ErrorResult("Reservation can't be longer than 3 days!");
            }
            
            // Check if room is available during reservation dates 
            var availableRooms =
                await _roomRepository.GetAvailableWithStoredProcedure(reservation.StartDate, reservation.EndDate);
            if (!availableRooms.Exists(r => r.Id == reservation.RoomId))
            {
                return new ErrorResult("Room unavailable during that dates!");
            }

            return new SuccessResult();
        }
    }
}