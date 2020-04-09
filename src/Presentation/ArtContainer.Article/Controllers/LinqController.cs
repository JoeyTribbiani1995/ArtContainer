using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace ArtContainer.Article.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinqController : ControllerBase
    {
        public LinqController()
        {
        }

        [HttpGet]
        public IActionResult CompareTwoSequences()
        {
            var list = new List<int>{ 1, 2, 3, 4, 5, 6 };
            var otherList = new List<int> { 1, 2, 3, 4, 5};
            var result = list.SequenceEqual(otherList);
            if(!result)
            {
                var differentItem = list.Except(otherList);
                var differentList = new List<int>();
                Array.ForEach(differentItem.ToArray(), x => differentList.Add(x));
                return Ok($"Are they equal? {result} . Different item: {differentList.ToString()}");
            }

            return Ok();
        }
    }
}
