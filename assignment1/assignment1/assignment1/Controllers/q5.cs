using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace assignment1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class q5 : ControllerBase
    {
        //q5

        /// <summary>
        /// receives a POST request with secret number and
        /// outputs a string with the secret number
        /// </summary>
        /// <param name="secret">The secret numbe</param>
        /// <returns>return an acknowledgement of the { secret } integer</returns>     
        ///<example>
        /// POST : https://localhost:7214/api/q4/knockknock
        /// HEADER: Content-Type: application/json
        /// BODY: -1000
        ///-> Shh.. the secret is -1000
        ///curl -H "Content-Type: application/json" -d "-1000" https://localhost:7214/api/q5/secret
        ///curl -H "Content-Type: application/json" -d "20" https://localhost:7214/api/q5/secret
        ///-> Shh.. the secret is 20
        ///</example>

        [HttpPost(template: "secret")] //"secret" route
        [Consumes("application/json")] 
        //only accept requests with Content-Type set to application/json
        public string Secret([FromBody]int secret) //secret method has int secret, method/class should be Abc
        //add the [FromBody] attribute to the parameter
        //To force Web API to read a simple type from the request body
        //to be sent as raw JSON as "application/json"
        {
            return "Shh.. the secret is " + secret;
        }
    }
}
// "application/x-www-form-urlencoded" <=> Fromform : -d "secret=-900"
// "application/json" <=> FromBody: -d "-900"