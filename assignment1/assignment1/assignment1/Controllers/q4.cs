using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace assignment1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class q4 : ControllerBase
    {
        //q4
        /// <summary>
        /// receives a POST request and outputs a string
        /// </summary>
        /// <returns>return the start of a knock knock joke</returns>
        ///<example>
        /// POST : https://localhost:7214/api/q4/knockknock
        /// HEADER: (None)
        /// BODY: (None)
        ///-> Who's there?
        ///curl -H "" -d "" https://localhost:7214/api/q4/knockknock
        ///</example>

        [HttpPost(template: "knockknock")]
        public string Knockknock()
        { return "Who's there?"; }
    }
}


// -d "" : request body = none; send an emprt string to the server
// -H "" : empty value in a header
