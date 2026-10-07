using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.Models
{
    public class PaginationParams
    {
        private const int MaxPageSize = 100;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int pageSize
        {
            get => PageSize;
            set => PageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}
