using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace Webproject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MarvelousController : Controller
    {
        public marvlousClass Avengers; 
        public marvlousClass AntiHeros;
        public marvlousClass Mutants;
        List<character> LimitedCharacters = new List<character>();
        public MarvelousController()
        {
            using var client = new HttpClient();
            var response = client.GetAsync("http://www.mocky.io/v2/5ecfd5dc3200006200e3d64b").Result;
            Avengers = JsonConvert.DeserializeObject<marvlousClass>(response.Content.ReadAsStringAsync().Result);
            foreach (var item in Avengers.character)
            {
               LimitedCharacters.Add(item);
            }

            response = client.GetAsync("http://www.mocky.io/v2/5ecfd630320000f1aee3d64d").Result;
            AntiHeros = JsonConvert.DeserializeObject<marvlousClass>(response.Content.ReadAsStringAsync().Result);
            foreach (var item in AntiHeros.character)
            {
                LimitedCharacters.Add(item);
            }
            response = client.GetAsync("http://www.mocky.io/v2/5ecfd6473200009dc1e3d64e").Result;
            Mutants = JsonConvert.DeserializeObject<marvlousClass>(response.Content.ReadAsStringAsync().Result);

            foreach (var item in Mutants.character)
            {
                LimitedCharacters.Add(item);
            }

            LimitedCharacters = LimitedCharacters.OrderByDescending(n => n.max_power).ToList().Take(15).ToList();


        }


        [HttpGet(Name = "GetMarvelouspower")]
        public int  Get(String name)
        {
            if (name == null)
            {
                return 0;
            }
            foreach (var item in LimitedCharacters)
            {
                if (item.name == name)
                    return item.max_power;
            }

            return 0;
        }

       
    }
}
