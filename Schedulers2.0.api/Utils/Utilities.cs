using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Schedulers2._0.api.Service.Utils
{
    public class Utilities
    {
        private readonly IConfiguration _configuration;

        public Utilities(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Utilities()
        {
            
        }

        public Dictionary<string, Uri> generateForPageURL(int pageNumber, int pageSize, string resource)
        {
            var baseResource = resource + "?pageNumber={0}&pageSize={1}";
            var resp = new Dictionary<string, Uri>();
            var mainPrevURL = String.Format(baseResource, pageNumber - 1 <= 0 ? 1 : pageNumber - 1, pageSize);
            var mainNextURL = String.Format(baseResource, pageNumber + 1, pageSize);
            var baseURL = _configuration["MicroServices:NgBackend"];
            var previousPage = new Uri(baseURL + mainPrevURL);
            var nextPage = new Uri(baseURL  + mainNextURL);
            resp.Add("nextPage", nextPage);
            resp.Add("prevPage", previousPage);
            return resp;
        }

        public object generateForPageURL(object pageNumber, object pageSize, object applicationPath)
        {
            throw new NotImplementedException();
        }
    }
}