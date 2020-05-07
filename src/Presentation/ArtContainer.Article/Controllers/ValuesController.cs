using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ArtContainer.Services.Article;
using System.Collections;
using Microsoft.AspNetCore.Authorization;

namespace ArtContainer.Article.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly IArticleService _articleService;

        public ValuesController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values
        [HttpGet("GetTest/{id}")]
        public ActionResult<IEnumerable<string>> GetTest(int id)
        {
            try
            {
                TestTryCatch(id);
                return new string[] { "value1", "value2" };

            }
            catch (Exception e)
            {
                ResponseErrorObject errorObject = new ResponseErrorObject
                {
                    ErrorMessage = e.Message,
                    HelpLink = e.HelpLink,
                    Datas = new List<DictionaryEntry>()
                };
                foreach (DictionaryEntry dictionary in e.Data)
                {
                    errorObject.Datas.Add(dictionary);
                }
                return BadRequest(errorObject);
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return _articleService.GetAllArticle();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private void TestTryCatch(int a)
        {
            if(a < 0)
            {
                Exception e =  new Exception("a less than 0");
                e.Data.Add("value a", $"{a}");
                e.HelpLink = "www.html.css";
                throw e;
            }
        }
    }

    public class ResponseErrorObject
    {
        public List<DictionaryEntry> Datas { get; set; }

        public string HelpLink { get; set; }

        public string ErrorMessage { get; set; }
    }
}
