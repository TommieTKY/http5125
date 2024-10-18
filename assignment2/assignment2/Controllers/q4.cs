using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace assignment2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class q4 : ControllerBase
    {
        /// <summary>
        /// Question: 2024 CCC J2
        /// to determine Dusa’s size when Dusa encounters a Yobi that causes it to run away.
        /// </summary>
        /// <param name="D">representing Dusa’s starting size.</param>
        /// <param name="Y">representing the sizes of the Yobis.</param>
        /// <returns>output the Dusa’s size when it eventually runs away.</returns>
        /// <example>
        /// GET:
        /// curl "https://localhost:7276/api/q4/Dusa?D=5&Y=3&Y=2&Y=9&Y=20&Y=22&Y=14"
        /// ->19
        /// curl "https://localhost:7276/api/q4/Dusa?D=10&Y=10&Y=3&Y=5"
        /// ->10
        /// curl "https://localhost:7276/api/q4/Dusa?D=0&Y=10&Y=3&Y=5"
        /// ->Invalid input.
        /// curl "https://localhost:7276/api/q4/Dusa?D=-3&Y=10&Y=3&Y=5"
        /// ->Invalid input.
        /// curl "https://localhost:7276/api/q4/Dusa?D=10&Y=-10&Y=3&Y=5"
        /// ->Invalid input.
        /// curl "https://localhost:7276/api/q4/Dusa?D=10&Y=5&Y=20&Y=1"
        /// -> 15
        /// </example>
        [HttpGet(template: "Dusa")]        
        public string Dusa (int D, [FromQuery] int[] Y)
        {
            if (D <= 0) {
                return "Invalid input."; 
            }

            foreach (int i in Y)
            {
                if (i <= 0)
                {
                    return "Invalid input.";
                }
            }

            foreach (int yobis in Y)
            {
                if (D > yobis) D += yobis;
                else return D.ToString();
            }
            return D.ToString();
        }
    }
}
