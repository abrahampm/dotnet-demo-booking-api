using System;

using alten_test.Core.Interfaces;


namespace alten_test.Core.Utilities
{
    public class PaginationInfo : IPaginationInfo
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortPropertyName { get; set; }
        public PageDirection SortDirection { get; set; }
        public string FilterPropertyName { get; set; }
        public string FilterTerm { get; set; }
        public int Total { get; set; }

        public PaginationInfo(int pageNumber = 1, int pageSize = 10,
            string sortPropertyName = "", PageDirection sortDirection = PageDirection.Ascending,
            string filterPropertyName = "", string filterTerm = "") {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            PageNumber = pageNumber;
            PageSize = pageSize;
            SortPropertyName = sortPropertyName;
            SortDirection = sortDirection;
            FilterPropertyName = filterPropertyName;
            FilterTerm = filterTerm;
            Total = 0;
        }

        public bool HasFiltering() {
            return !String.IsNullOrEmpty(FilterPropertyName) && !String.IsNullOrEmpty(FilterTerm);
        }

        public bool HasSorting() {
            return !String.IsNullOrEmpty(SortPropertyName);
        }
    }

    public enum PageDirection {
        Ascending,
        Descending
    }
}