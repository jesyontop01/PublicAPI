using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PublicAPI.Dtos;
using PublicAPI.Utiities;
using System;
using System.Diagnostics;
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
            BasicIfo.GithubUrl = "https://github.com/jesyontop01/PublicAPI";
            BasicIfo.Email = "jesyontop01@gmail.com";
            DateTime currentDate = DateTime.Now;

            BasicIfo.CurrentDatetime = currentDate.ToUniversalTime().ToString("u").Replace(" ", "T");

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower };
            var json = System.Text.Json.JsonSerializer.Serialize(BasicIfo, options);

            return Ok(json);

        }

        //        {
        //    "number": 371,
        //    "is_prime": false,
        //    "is_perfect": false,
        //    "properties": ["armstrong", "odd"],
        //    "digit_sum": 11,  // sum of its digits
        //    "fun_fact": "371 is an Armstrong number because 3^3 + 7^3 + 1^3 = 371" //gotten from the numbers API
        //}

        [HttpGet("classify-number/{number}")]
        public async Task<IActionResult> NumbClassAsync(string number)
        {
            int numValue = 0;

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower };

            if (string.IsNullOrWhiteSpace(number))
            {
                NumbClassErrorDto numbClassErrorDto = new NumbClassErrorDto()
                {
                    Number = "Null",
                    Error = true
                };

                var jsonError = System.Text.Json.JsonSerializer.Serialize(numbClassErrorDto, options);


                return BadRequest(jsonError);
            }

            if ((int.TryParse(number, out numValue)))
            {
                NumbClassDto numbClassDto = new NumbClassDto();
                numbClassDto.Number = numValue;

                if (SubQueries.IsPrimeNumber(numValue))
                    numbClassDto.IsPrime = true;
                else
                    numbClassDto.IsPrime = false;

                int perfectNum = SubQueries.IsPerfect(numValue);

                if (numValue <= 1)
                {
                    numbClassDto.IsPerfect = false;
                    numbClassDto.DigitSum = perfectNum;
                }
                else if (numValue == perfectNum)
                {
                    numbClassDto.IsPerfect = true;
                    numbClassDto.DigitSum = perfectNum;

                }

                else
                {
                    numbClassDto.DigitSum = perfectNum;
                    numbClassDto.IsPerfect = false;

                }

                string[] numProperties = new string[2];
                numbClassDto.Properties = new string[2];// Check if the number is even or odd


                if (SubQueries.IsArmstrong(numValue))
                {
                    numbClassDto.Properties = new string[2];
                    numbClassDto.Properties[0] = "armstrong";

                }
                else
                {
                    numbClassDto.Properties = new string[1];

                }

                if (numValue % 2 == 0)
                {
                    if (numbClassDto.Properties[0] == "armstrong")
                    {
                        numbClassDto.Properties[1] = "even";
                    }
                    else
                    {
                        numbClassDto.Properties[0] = "even";

                    }
                }
                else
                {
                    if (numbClassDto.Properties[0] == "armstrong")
                    {
                        numbClassDto.Properties[1] = "odd";
                    }
                    else
                    {
                        numbClassDto.Properties[0] = "odd";

                    }
                }

                

                string apiURL = $"http://numbersapi.com/{numValue}/math";
                string numberFunFact = string.Empty;
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(apiURL))
                    {
                        //string apiResponse = await response.Content.ReadAsStringAsync();
                        //numberFunFact = JsonConvert.DeserializeObject<string>(apiResponse);
                        numbClassDto.FunFact = await response.Content.ReadAsStringAsync();

                    }
                }

                //numbClassDto.FunFact = apiResponse;
                var jsonOk = System.Text.Json.JsonSerializer.Serialize(numbClassDto, options);


                return Ok(jsonOk);
            }
            else
            {
                NumbClassErrorDto numbClassErrorDto = new NumbClassErrorDto()
                {
                    Number = "alphabet",
                    Error = true
                };

                var jsonError = System.Text.Json.JsonSerializer.Serialize(numbClassErrorDto, options);


                return BadRequest(jsonError);
            }


            //BasicInfoDto BasicIfo = new BasicInfoDto();
            //BasicIfo.GithubUrl = "https://github.com/jesyontop01/PublicAPI";
            //BasicIfo.Email = "jesyontop01@gmail.com";
            //DateTime currentDate = DateTime.Now;

            //BasicIfo.CurrentDatetime = currentDate.ToUniversalTime().ToString("u").Replace(" ", "T");

            //var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower };
            //var json = JsonSerializer.Serialize(BasicIfo, options);

            //return Ok(json);

        }
    }
}
