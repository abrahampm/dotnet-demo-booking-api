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
    public class ContactService : IContactService
    {
        private readonly IRepository<Contact> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ContactService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.Contacts;
            _mapper = mapper;
        }

        public async Task<ContactDto> Create(ContactDtoInput contactDtoInput)
        {
            var contact = _mapper.Map<Contact>(contactDtoInput);
            await _repository.Insert(contact);
            await _unitOfWork.Save();
            var contactDto = _mapper.Map<ContactDto>(contact);
            return contactDto;
        }

        public async Task<ContactDto> FindById(int id)
        {
            var contact = await _repository.GetById(id);
            var contactDto = _mapper.Map<ContactDto>(contact);
            return contactDto;
        }

        public async Task<ContactDto> Update(ContactDto contactDto)
        {
            var contact = _mapper.Map<Contact>(contactDto);
            _repository.Update(contact);
            await _unitOfWork.Save();
            return contactDto;
        }

        public async Task Delete(int id)
        {
            var contact = await _repository.GetById(id);

            if (contact != null)
            {
                _repository.Delete(contact);
                await _unitOfWork.Save();
            }
        }

        public async Task<PaginationResultDto<ContactDto>> List(IPaginationInfo pageInfo)
        {
            var pageData = await _repository.GetAllPaginated(pageInfo);
            pageInfo.Total = await _repository.GetAll().CountAsync();
            
            var pageInfoDto = _mapper.Map<PaginationInfoDto>(pageInfo);
            var pageDataDto = _mapper.Map<IEnumerable<ContactDto>>(pageData);

            return new PaginationResultDto<ContactDto>(pageInfoDto, pageDataDto);
        }
        
        public bool ContactExists(int id)
        {
            return _repository.Exists(id);
        }
    }
}