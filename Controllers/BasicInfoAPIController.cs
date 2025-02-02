using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicAPI.Dtos;
using System.Text.Json;

namespace PublicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasicInfoAPIController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            BasicInfoDto BasicIfo = new BasicInfoDto();
            BasicIfo.GithubUrl = "https://github.com/jesyontop01/HNGPublicAPI";
            BasicIfo.Email = "jesyontop01@gmail.com";
            DateTime currentDate = DateTime.Now;

            BasicIfo.CurrentDatetime = currentDate.ToUniversalTime().ToString("u").Replace(" ", "T");

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower };
            var json = JsonSerializer.Serialize(BasicIfo, options);

            return Ok(json);

        }
    }
}
