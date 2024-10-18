using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace assignment2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class q3 : ControllerBase
    {
        /// <summary>
        /// To determine the total spiciness of Ron’s chili after he has finished adding peppers.
        /// </summary>
        /// <param name="Ingredients">the names of peppers Ron has added.</param>
        /// <returns>output the total spiciness of Ron’s chili.</returns>
        /// <example>
        /// GET:
        /// curl https://localhost:7276/api/q3/ChiliPeppers?Ingredients=Poblano%2CCayenne%2CThai%2CPoblano
        /// -> 118000
        /// https://localhost:7276/api/q3/ChiliPeppers?Ingredients=Poblano%2CMirasol%2CSerrano%2CCayenne%2CThai%2CHabanero%2CSerrano
        /// -> 278500
        /// curl https://localhost:7276/api/q3/ChiliPeppers?Ingredients=Poblano%2CThai%2CThai
        /// -> 151500
        /// curl https://localhost:7276/api/q3/ChiliPeppers?Ingredients=Poblano%2CThai%2CUuuu
        /// -> Invalid input
        /// </example>
        [HttpGet(template: "ChiliPeppers")]
        public string ChiliPeppers(string Ingredients)
        {
            int shu = 0;           
            string[] ingredientsList = Ingredients.Split(',');
            foreach (string peper in ingredientsList)
            {
                switch (peper)
                {
                    case "Poblano": shu += 1500;
                        break;
                    case "Mirasol": shu += 6000;
                        break;
                    case "Serrano": shu += 15500;
                        break;
                    case "Cayenne": shu += 40000;
                        break;
                    case "Thai": shu += 75000;
                        break;
                    case "Habanero": shu += 125000;
                        break;
                    default: return "Invalid input";
                }                
            }
            return shu.ToString();
        }
    }
}