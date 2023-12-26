using System.Collections.Generic;

namespace alten_test.Core.Dto
{
    public class PaginationResultDto<T>
    {
        public PaginationInfoDto Pagination { get; }
        public IEnumerable<T> Data { get; }

        public PaginationResultDto(PaginationInfoDto pageInfo, IEnumerable<T> pageData)
        {
            Pagination = pageInfo;
            Data = pageData;
        }
    }
}