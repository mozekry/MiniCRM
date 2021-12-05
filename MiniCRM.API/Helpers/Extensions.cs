using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MiniCRM.API.Helpers
{
    public static class Extensions
    {
        public static void AddApplicationErrors(this HttpResponse response, string message){

            response.Headers.Add("Application-Error",message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin","*");
        }
         public static void AddPagination(this HttpResponse response, int currentPage,
         int itemsPerPage, int totalItems, int totalPages)
        {
            var paginationHeader = new PaginationHeader(currentPage,itemsPerPage,totalItems,totalPages);
            var CamelCaseFormatter = new JsonSerializerSettings();
            CamelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            response.Headers.Add("Pagination",JsonConvert.SerializeObject(paginationHeader,CamelCaseFormatter));
            response.Headers.Add("Access-Control-Expose-Headers","Pagination");
        }
    }
    
}