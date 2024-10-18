using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace assignment2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class q3b : ControllerBase
    {
        /// <summary>
        /// To determine the total spiciness of Ron’s chili after he has finished adding peppers.
        /// </summary>
        /// <param name="Ingredients">the names of peppers Ron has added.</param>
        /// <returns>output the total spiciness of Ron’s chili.</returns>
        /// <example> 
        /// GET: https://localhost:7276/api/q3/ChiliPeppers?Ingredients=Poblano%2CCayenne%2CThai%2CPoblano
        /// -> 118000
        /// curl https://localhost:7276/api/q3/ChiliPeppers?Ingredients=Poblano%2CMirasol%2CSerrano%2CCayenne%2CThai%2CHabanero%2CSerrano
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

            Dictionary<string, int> peperlist = new Dictionary<string, int>()
            {
                {"Poblano",1500 },
                {"Mirasol",6000 },
                {"Serrano",15500},
                {"Cayenne", 40000},
                {"Thai", 75000},
                {"Habanero", 125000},
            };

            foreach (string peper in ingredientsList)
            {
                foreach (KeyValuePair<string,int> item in peperlist)
                {
                    if (peper == item.Key) {
                        shu += item.Value;
                }   else
                    {
                        return "Invalid input";
                    }
                }
            }
            return shu.ToString();
        }
    }
}