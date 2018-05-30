using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CognitiveAI.WebAPI.BusinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CognitiveAI.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/TextAnalytics")]
    public class TextAnalyticsController : Controller
    {

        TextAnalyticsBL objTextAnalyticsBL = new TextAnalyticsBL();


        // GET: api/TextAnalytics
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/TextAnalytics/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/TextAnalytics
        [HttpPost]
        public dynamic Post([FromBody]string value)
        {
            return objTextAnalyticsBL.TextAnalysis(value);
        }
        
        // PUT: api/TextAnalytics/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
