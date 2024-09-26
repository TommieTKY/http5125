using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace assignment1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class q3 : ControllerBase
    {
        //q3
        //<summary> This method receives an integer and outputs the cube of the integer</summary>
        //<param name = "number" > The base number to raise to an cube</param>
        //<returns> return the cube of the integer {number} </returns>
        //<example>
        //GET : https://localhost:7214/api/q3/cube/6 -> 216
        //GET : https://localhost:7214/api/q3/cube/-6 -> -216
        //</example>

        [HttpGet(template: "cube/{number}")] // route has to be "route"
        public double Cube(int number) 
        // base is a reserved word, cannot use
        // Math.Pow() will return a double
        {
            double cubeNum = Math.Pow(number, 3);
            return cubeNum;
        }
    }
}
