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

    public class PracticesController : ApiController
    {
        [CacheControl(MaxAge = 300)]
        [Route("api/Practices")]
        public dynamic Get()
        {
            var cacheResults =
               HttpRuntime.Cache["Practices"];

            if (null == cacheResults)
            {
                Models.MedsEntities m = new Models.MedsEntities();

                var results = m.GPPractices.ToList();

                HttpRuntime.Cache["Practices"] = results;

                return results;
            }

            return cacheResults;
        }
    }
    public class DrugsController : ApiController
    {
        [CacheControl(MaxAge = 300)]
        [Route("api/Drugs")]
        public dynamic Get()
        {
            var cacheResults =
               HttpRuntime.Cache["Drugs"];

            if (null == cacheResults)
            {
                Models.MedsEntities m =
                    new Models.MedsEntities();

                var results =
                    m.DrugPracticeAndChapterSpends
                    .Select(x=> new { bnfChapter = x.BNFChapter, drugName = x.DrugName.Replace(" + ","-and-") })
                    .Distinct() 
                    .ToList();

                HttpRuntime.Cache["Drugs"] = results;

                return results;
            }

            return cacheResults;
        }
    }

    public class PrescriptionsController : ApiController
    {
        [CacheControl(MaxAge = 300)]
        [Route("api/Prescriptions/{practiceId}")]
        public dynamic Get(string practiceId)
        {
            var cacheResults = 
                HttpRuntime.Cache[practiceId];

            if (null == cacheResults)
            {
                Models.MedsEntities m = new Models.MedsEntities();

                var results = m.vwPracticePartitions
                    .Where( x=> x.PracticeNo == practiceId)
                    .ToList();

                HttpRuntime.Cache[practiceId] = results;

                return results;
            }

            return cacheResults;
        }

        [CacheControl(MaxAge = 300)]
        [Route("api/Prescriptions/{practiceId}/Drug/{drugName}")]
        public dynamic GetByDrug(string practiceId, string drugName)
        {
            drugName = drugName.Replace("-and-", " + ");
            var cacheResults =
                HttpRuntime.Cache[practiceId+drugName];

            if (null == cacheResults)
            {
                Models.MedsEntities m = new Models.MedsEntities();

                var results = m.vwPracticePartitions
                    .Where(x => x.PracticeNo == practiceId && x.DrugName.ToLower() == drugName.ToLower())
                    .ToList();

                HttpRuntime.Cache[practiceId + drugName] = results;

                return results;
            }

            return cacheResults;
        }

        [CacheControl(MaxAge = 300)]
        [Route("api/Prescriptions/{practiceId}/BNFChapter/{bnfChapter}")]
        public dynamic GetByChapter(string practiceId, string bnfChapter)
        {
            var cacheResults =
                HttpRuntime.Cache[practiceId + bnfChapter];

            if (null == cacheResults)
            {
                Models.MedsEntities m = new Models.MedsEntities();

                var results = m.vwPracticePartitions
                    .Where(x => x.PracticeNo == practiceId && x.BNFChapter == bnfChapter)
                    .ToList();

                HttpRuntime.Cache[practiceId + bnfChapter] = results;

                return results;
            }

            return cacheResults;
        }
    }
}
