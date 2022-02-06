using System;
using System.Threading.Tasks;

using alten_test.Core.Dto;
using alten_test.Core.Interfaces;
using alten_test.Core.Models;
using alten_test.Core.Utilities;


namespace alten_test.BusinessLayer.Interfaces
{
    public interface IRoomService
    {
        Task<ServiceResult> Create(RoomDtoInput room);

        Task<ServiceResult> FindById(int id);

        Task<ServiceResult> Update(RoomDto room);

        Task<ServiceResult> Delete(int id);

        Task<ServiceResult> List(IPaginationInfo pageInfo);

        bool RoomExists(int id);
    }
}