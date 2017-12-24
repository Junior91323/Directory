namespace Directory.Common
{
    using System;

    public static class SortOrderHelper
    {
        private const string Desc = "DESC";

        private const string Asc = "ASC";

        public static SortOrderEnum Convert(this string sortOrder)
        {
            if (string.IsNullOrWhiteSpace(sortOrder))
            {
                throw new ArgumentNullException(nameof(sortOrder));
            }

            switch (sortOrder.ToUpper())
            {
                case Desc:
                    return SortOrderEnum.Desc;
                case Asc:
                    return SortOrderEnum.Asc;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sortOrder));
            }
        }
    }
}
