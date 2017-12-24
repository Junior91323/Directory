namespace Directory.Common
{
    public class PageSortInfo
    {
        private const string DefaultOrderField = "CreateDate";

        public PageSortInfo(int pageIndex, int pageSize) : this(null, null, pageIndex, pageSize) { }

        public PageSortInfo(string sortField, SortOrderEnum? sortOrder, int pageIndex, int pageSize)
        {
            SortField = string.IsNullOrWhiteSpace(sortField) ? DefaultOrderField : sortField;
            SortOrder = sortOrder;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }


        public string SortField { get; }

        public SortOrderEnum? SortOrder { get; }

        public int PageIndex { get; }

        public int PageSize { get; }

        public static PageSortInfo All => new PageSortInfo(0, int.MaxValue - 1);
    }
}
