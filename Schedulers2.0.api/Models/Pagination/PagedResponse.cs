
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace  Schedulers2._0.api.Models.Pagination
{
    public class PagedResponse<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        // public Uri FirstPage { get; set; }
        // public Uri LastPage { get; set; }
        // public int TotalPages { get; set; }
         public int TotalRecords { get; set; }
        public Uri NextPage { get; set; }
        public Uri PreviousPage { get; set; }
        public T Data { get; set; }
        public PagedResponse(T data, int pageNumber, int pageSize, Uri previousPage, Uri nextPage)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Data = data;
            this.PreviousPage = previousPage;
            this.NextPage = nextPage;
            this.TotalRecords = TotalRecords;
            // this.Message = null;
            // this.Succeeded = true;
            // this.Errors = null;
        }
         public PagedResponse(T data, int pageNumber, int pageSize, Uri previousPage, Uri nextPage,int totalRecord)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Data = data;
            this.PreviousPage = previousPage;
            this.NextPage = nextPage;
            this.TotalRecords=totalRecord;
            // this.Message = null;
            // this.Succeeded = true;
            // this.Errors = null;
        }
    }
}