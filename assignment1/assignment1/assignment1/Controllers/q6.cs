using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace assignment1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class q6 : ControllerBase
    {
        //q6
        /// <summary>
        /// receives a side length and outputs the area of the regular hexagon
        /// </summary>
        /// <param name="side">The side length</param>
        /// <returns>return the area of a regular hexagon with side length</returns>
        ///<example>
        ///GET : https://localhost:7214/api/q6/hexagon?side=9.5 -> 234.47637807463676
        ///GET : https://localhost:7214/api/q6/hexagon?side=15 -> 584.567147554496
        ///</example>

        [HttpGet(template: "hexagon")]
        public double Hexagon(double side)
        {
            double area = Math.Pow(side, 2) * Math.Sqrt(3) * 3 / 2;
            return area;
        }
    }
}
// order: Parentheses() > Exponentiation > */ > +-
