using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModelLib.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTDemoReal.Controllers
{
    

    [Route("api/Items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private static List<Item> _data = new List<Item>()
        {
            new Item(1, "dims", "Low", 16),
            new Item(2, "ding", "Middle", 13),
            new Item(3, "dums", "High", 12)
        };

        // GET: api/<ItemsController>
        [HttpGet]
        public IEnumerable<Item> Get()
        {
            return _data;
        }

        // GET api/<ItemsController>/5
        [HttpGet]
        [Route("{id}")]
        public Item Get(int id)
        {
            return _data.Find(d => d.Id == id);
        }

        // POST api/<ItemsController>
        [HttpPost]
        public void Post([FromBody] Item value)
        {
            _data.Add(value);
        }

        // PUT api/<ItemsController>/5
        [HttpPut]
        [Route("{id}")]
        public void Put(int id, [FromBody] Item value)
        {
            Item item = Get(id);
            if (item != value)
            {
                item.Id = value.Id;
                item.Name = value.Name;
                item.Quality = value.Quality;
                item.Quantity = value.Quantity;
            }
        }

        // DELETE api/<ItemsController>/5
        [HttpDelete]
        [Route("{id}")]
        public void Delete(int id)
        {
            Item item = Get(id);
            if (item != null)
            {
                _data.Remove(item);
            }
        }

        [HttpGet]
        [Route("Name/{substring}")]
        public IEnumerable<Item> GetFromSubstring(String substring)
        {
            return _data.FindAll(i => i.Name.Contains(substring));
        }

        [HttpGet]
        [Route("Quality/{substring}")]
        public IEnumerable<Item> GetFromQuality(String substring)
        {
            return _data.FindAll(i => i.Quality.Contains(substring));
        }
    }
}
