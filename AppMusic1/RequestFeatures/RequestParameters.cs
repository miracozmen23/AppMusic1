﻿namespace AppMusic1.RequestFeatures
{
    public class RequestParameters
    {
        const int maxPageSize = 50;

        public int PageNumber { get; set; }

        private int _pageSize;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > maxPageSize) ? maxPageSize : value; }
        }

        public String? OrderBy { get; set; } = "id";
        public bool OrderByDescending { get; set; }
    }
}
