using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace assignment1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class q1 : ControllerBase
    {
        // q1

        /// <summary>
        /// output a welcome string.
        /// </summary>
        /// <returns>return "Welcome to 5125!"</returns>
        /// <example>
        /// GET : https://localhost:7214/api/q1/welcome -> Welcome to 5125!
        /// </example>

        [HttpGet(template: "welcome")] 
        // maps GET request with route "welcome" = access ../welcome
        public string Welcome() // welcome method to return a string
            // method name did not have to be same as route"welcome"
        { return "Welcome to 5125!"; }
    }
}

// GET: curl https://localhost:7214/api/q1/welcome
// curl = send HTTP requests
