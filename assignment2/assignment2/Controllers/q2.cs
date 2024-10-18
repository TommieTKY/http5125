using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace assignment2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class q2 : ControllerBase
    {
        /// <summary>
        /// Question: 2024 CCC J1
        /// to determine the cost of a meal, given the number of plates of each colour chosen by a customer.
        /// </summary>
        /// <param name="R"> the number of red plates chosen.</param>
        /// <param name="G">the number of green plates chosen.</param>
        /// <param name="B">the number of blue plates chosen.</param>
        /// <returns>output the cost of the meal in dollars.</returns>
        /// <example> 
        /// POST : https://localhost:7276/api/q2/Conveyor
        /// HEADER: Content-Type: application/x-www-form-urlencoded
        /// BODY: R=0&G=2&B=4
        /// ->28
        /// curl -H "Content-Type: application/x-www-form-urlencoded" -d "R=0&G=2&B=4" https://localhost:7276/api/q2/Conveyor
        /// -> 28
        /// curl -H "Content-Type: application/x-www-form-urlencoded" -d "R=0&G=2&B=-4" https://localhost:7276/api/q2/Conveyor
        /// -> Invalid input.
        /// curl -H "Content-Type: application/x-www-form-urlencoded" -d "R=3&G=2&B=1" https://localhost:7276/api/q2/Conveyor
        /// -> 22
        /// </example>

        [HttpPost(template: "Conveyor")]
        [Consumes("application/x-www-form-urlencoded")]
        public string Conveyor([FromForm] int R, [FromForm] int G, [FromForm] int B)
        {
            if (R < 0 || G < 0 || B < 0)
            {
                return "Invalid input.";
            }
            int red_total = R * 3;
            int green_total = G * 4;
            int blue_total = B * 5;
            int C = red_total + green_total + blue_total;
            return C.ToString();
        }
    }
}
