using System;
using System.Collections.Generic;
using System.Data;

namespace GlobalCollege.Entity.DTO
{
    public class PagedResult<T>
    {
        public PagedResult()
        {
            Results = new List<T>();
        }
        public List<T> Results { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }
    }

    public class PagedResultDataTable
    {
        public Guid parentId { get; set; }
        public DataTable Results { get; set; }
        public int PageNumber { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }
        
    }

}
