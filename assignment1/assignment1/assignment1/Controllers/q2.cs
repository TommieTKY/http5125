using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace assignment1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class q2 : ControllerBase
    {
        //q2
        //<summary> This method receives a name and outputs the welcome string </summary>
        //<param name = "name" > The name of the user</param> // param = argument
        //<returns> return Hi name! </returns>
        //<example>
        //GET : https://localhost:7214/api/q2/greeting?name=Tommie -> Hi Tommie!
        //GET : https://localhost:7214/api/q2/greeting?name=%E6%B9%AF%E7%B1%B3 -> Hi 湯米!
        //</example>

        [HttpGet(template: "greeting")]
        public string Greeting(string name)
        { return "Hi " + name + "!"; }
    }
}
