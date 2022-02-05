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
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoomService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.Rooms;
            _mapper = mapper;
        }

        public async Task<RoomDto> Create(RoomDtoInput roomDtoInput)
        {
            var room = _mapper.Map<Room>(roomDtoInput);
            await _repository.Insert(room);
            await _unitOfWork.Save();
            var roomDto = _mapper.Map<RoomDto>(room);
            return roomDto;
        }

        public async Task<RoomDto> FindById(int id)
        {
            var room = await _repository.GetById(id);
            var roomDto = _mapper.Map<RoomDto>(room);
            return roomDto;
        }

        public async Task<RoomDto> Update(RoomDto roomDto)
        {
            var room = _mapper.Map<Room>(roomDto);
            _repository.Update(room);
            await _unitOfWork.Save();
            return roomDto;
        }

        public async Task Delete(int id)
        {
            var room = await _repository.GetById(id);

            if (room != null)
            {
                _repository.Delete(room);
                await _unitOfWork.Save();
            }
        }

        public async Task<PaginationResultDto<RoomDto>> List(IPaginationInfo pageInfo)
        {
            var pageData = await _repository.GetAllPaginated(pageInfo);
            pageInfo.Total = await _repository.GetAll().CountAsync();
            
            var pageInfoDto = _mapper.Map<PaginationInfoDto>(pageInfo);
            var pageDataDto = _mapper.Map<IEnumerable<RoomDto>>(pageData);

            return new PaginationResultDto<RoomDto>(pageInfoDto, pageDataDto);
        }


        public async Task<PaginationResultDto<RoomDto>> GetAvailable(IPaginationInfo pageInfo, DateTime startDate, DateTime endDate)
        {
            RoomType roomType = RoomType.Single;
            if (pageInfo.HasFiltering() && pageInfo.FilterPropertyName == nameof(Room.Type))
            {
                Enum.TryParse(pageInfo.FilterTerm, out roomType);    
            }
            var rooms = await _repository.GetAvailableWithStoredProcedure(startDate, endDate, roomType);
            
            var pageInfoDto = _mapper.Map<PaginationInfoDto>(pageInfo);
            var pageDataDto = _mapper.Map<IEnumerable<RoomDto>>(rooms);

            return new PaginationResultDto<RoomDto>(pageInfoDto, pageDataDto);
        }
        
        public bool RoomExists(int id)
        {
            return _repository.Exists(id);
        }
    }
}