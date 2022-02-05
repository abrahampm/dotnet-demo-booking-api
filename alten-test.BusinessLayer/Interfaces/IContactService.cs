using System.Threading.Tasks;

using alten_test.Core.Dto;
using alten_test.Core.Interfaces;


namespace alten_test.BusinessLayer.Interfaces
{
    public interface IContactService
    {
        Task<ContactDto> Create(ContactDtoInput contact);

        Task<ContactDto> FindById(int id);

        Task<ContactDto> Update(ContactDto contact);

        Task Delete(int id);

        Task<PaginationResultDto<ContactDto>> List(IPaginationInfo pageInfo);

        bool ContactExists(int id);
    }
}