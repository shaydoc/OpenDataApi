using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace OpenDataApi.Controllers
{
    public class CacheControlAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        public int MaxAge { get; set; }

        public CacheControlAttribute()
        {
            MaxAge = 3600;
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Response != null)
                actionExecutedContext.Response.Headers.CacheControl = new CacheControlHeaderValue()
                {
                    Public = true,
                    MaxAge = TimeSpan.FromSeconds(MaxAge)
                };
            base.OnActionExecuted(actionExecutedContext);
        }

         
    }

    public class PrescriptionsController : ApiController
    {
     
        [CacheControl(MaxAge = 300)]
        public dynamic Get()
        {
            var cacheResults = 
                HttpRuntime.Cache["BNFChapterDrugSpending"];

            if (null == cacheResults)
            {
                Models.MedsEntities m = new Models.MedsEntities();

                var results = m.BNFChapterDrugSpendings.ToList();

                HttpRuntime.Cache["BNFChapterDrugSpending"] = results;

                return results;
            }

            return cacheResults;
        }
    }
}
