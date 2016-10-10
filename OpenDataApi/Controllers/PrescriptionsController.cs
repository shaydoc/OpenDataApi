using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using WebApi.OutputCache.V2;

namespace OpenDataApi.Controllers
{
    //public class CacheControlAttribute : System.Web.Http.Filters.ActionFilterAttribute
    //{
    //    public int MaxAge { get; set; }

    //    public CacheControlAttribute()
    //    {
    //        MaxAge = 3600;
    //    }

    //    public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
    //    {
    //        if (actionExecutedContext.Response != null)
    //            actionExecutedContext.Response.Headers.CacheControl = new CacheControlHeaderValue()
    //            {
    //                Public = false,
    //                MaxAge = TimeSpan.FromSeconds(MaxAge),
                   
    //            };

    //            actionExecutedContext.Response.Content.Headers.Expires = DateTimeOffset.Now.AddSeconds(MaxAge);
    //        base.OnActionExecuted(actionExecutedContext);
    //    }

         
    //}

    public class PracticesController : ApiController
    {
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
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
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
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
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
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

        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
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

        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
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


   public class BNFController: ApiController
    {
        [CacheOutput(ClientTimeSpan = 86400, ServerTimeSpan = 86400)]
        [Route("api/bnfsummary")]
        public IHttpActionResult GetBNFSumary()
        {
            Models.MedsEntities m =
                new Models.MedsEntities();

            var results =
                m.GetCacheQuery("api/bnfsummary");

            HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.OK);
            msg.Content = new StringContent(results.FirstOrDefault().Json, Encoding.UTF8, "aplication/json");
            return ResponseMessage(msg);

        }



        [CacheOutput(ClientTimeSpan = 86400, ServerTimeSpan = 86400)]
        [Route("api/diabetes")]
        public IHttpActionResult GetDiabetesStats()
        {
            Models.MedsEntities m =
                new Models.MedsEntities();

            var results = 
                m.GetCacheQuery("api/diabetes");

            HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.OK);
            msg.Content = new StringContent(results.FirstOrDefault().Json, Encoding.UTF8, "aplication/json");
            return ResponseMessage(msg);

        }





        [CacheOutput(ClientTimeSpan = 86400, ServerTimeSpan = 86400)]
        [Route("api/diabetes/{year}/{month}")]
        public dynamic GetDiabetesMonthlyStats(string year, string month)
        {
            Models.MedsEntities m =
                new Models.MedsEntities();

            var results =
                m.GetDrugUseByBNFChapterAndSectionByPractice("6", "1",year,month);


            return results;

        }
    }

    public class SOAController : ApiController
    {
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        [Route("api/soa/{drugName}/{year}/{month}")]
        public dynamic GetByDrug(string drugName, string year, string month)
        {    
            Models.MedsEntities m =
                new Models.MedsEntities();

            var results = m.GetDrugUseBySuperOutputArea(drugName, year, month);


            return results;
             
        }

        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        [Route("api/soa/bnfchapter/{bnfChapter}/{bnfSection}/{year}/{month}")]
        public dynamic GetByBNF(string bnfChapter, string bnfSection, string year, string month)
        {
            Models.MedsEntities m =
                new Models.MedsEntities();

            var results = m.GetDrugUseByBNFChapterAndSectionBySuperOutputArea(bnfChapter, bnfSection, year, month);


            return results;

        }

        [CacheOutput(ClientTimeSpan = 86400, ServerTimeSpan = 86400)]
        [Route("api/soa/patient/bnfchapter/{bnfChapter}/{bnfSection}/{year}/{month}")]
        public dynamic GetByBNFPatientRation(string bnfChapter, string bnfSection, string year, string month)
        {
            Models.MedsEntities m =
                new Models.MedsEntities();

            var results = m.GetDrugUseBySuperOutputAreaPatientRatio(bnfChapter, bnfSection, year, month);


            return results;

        }


    }
}
