using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace assignment2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class q1 : ControllerBase
    {
        /// <summary>
        /// to determine the final score at the end of a game.
        /// </summary>
        /// <param name="Collisions">the number of collisions with obstacles.</param>
        /// <param name="Deliveries">the number of packages delivered.</param>
        /// <returns>output the final score</returns>
        /// <example> 
        /// POST : https://localhost:7276/api/q1/delivedroid
        /// HEADER: Content-Type: application/x-www-form-urlencoded
        /// BODY: Collisions=2&Deliveries=5
        /// ->730
        /// curl -H "Content-Type: application/x-www-form-urlencoded" -d "Collisions=2&Deliveries=5" https://localhost:7276/api/q1/delivedroid
        /// -> 730
        /// curl -H "Content-Type: application/x-www-form-urlencoded" -d "Collisions=-1&Deliveries=0" https://localhost:7276/api/q1/delivedroid
        /// -> Invalid input
        /// curl -H "Content-Type: application/x-www-form-urlencoded" -d "Collisions=2&Deliveries=0" https://localhost:7276/api/q1/delivedroid
        /// -> -20
        /// </example>

        [HttpPost(template: "delivedroid")]
        [Consumes("application/x-www-form-urlencoded")]
        public string Delivedroid([FromForm] int Collisions, [FromForm] int Deliveries)
        {
            if (Collisions < 0 || Deliveries < 0)
            {
                return "Invalid input";
            }
            int deliveries_point = Deliveries * 50;
            int collisions_point = Collisions * 10;
            if (Deliveries > Collisions) return (deliveries_point - collisions_point + 500).ToString();
            else return (deliveries_point - collisions_point).ToString();
        }
    }
}
