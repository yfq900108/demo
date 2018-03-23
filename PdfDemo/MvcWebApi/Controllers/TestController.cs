using MvcWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace MvcWebApi.Controllers
{
    public class TestController : ApiController
    {

        Test[] products = new Test[]

{

new Test { id = 1, name = "Tomato Soup"},

new Test { id = 2, name = "Yo-yo" },

new Test { id = 3, name = "Hammer" }

};



        public IEnumerable<Test> GetAllProducts()
        {

            return products;

        }



        public IHttpActionResult GetProduct(int id)
        {

            var product = products.FirstOrDefault((p) => p.id == id);

            if (product == null)
            {

                return NotFound();

            }

            return Ok(product);

        }



        [HttpPost]

        public IHttpActionResult PostTest([FromBody]Test t)
        {

            var product = products.FirstOrDefault((p) => p.id == t.id);

            if (product == null)
            {

                return NotFound();

            }

            return Ok(product);

        }
    }
}
