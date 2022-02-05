using System;
using System.Threading.Tasks;

using alten_test.Core.Dto;
using alten_test.Core.Interfaces;
using alten_test.Core.Models;


namespace alten_test.BusinessLayer.Interfaces
{
    public interface IRoomService
    {
        Task<RoomDto> Create(RoomDtoInput room);

        Task<RoomDto> FindById(int id);

        Task<RoomDto> Update(RoomDto room);

        Task Delete(int id);

        Task<PaginationResultDto<RoomDto>> List(IPaginationInfo pageInfo);

        bool RoomExists(int id);
    }
}