using alten_test.Core.Utilities;

namespace alten_test.Core.Interfaces
{
    public interface IPaginationInfo
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortPropertyName { get; set; }
        public PageDirection SortDirection { get; set; }
        public string FilterPropertyName { get; set; }
        public string FilterTerm { get; set; }
        public int Total { get; set; }

        public bool HasFiltering();
        public bool HasSorting();
    }
}