using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace assignment2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class q5b : ControllerBase
    {
        /// <summary>
        /// Question: 2024 CCC J3
        /// To determine the score required for bronze level and how many participants achieved this score.
        /// </summary>
        /// <param name="N">representing the number of participants.</param>
        /// <param name="L">representing the scores of participants.</param>
        /// <returns>output the score required for bronze level and how many participants achieved this score.</returns>
        /// <example>
        /// POST : https://localhost:7276/api/q5b/Bronze
        /// HEADER: Content-Type: application/x-www-form-urlencoded
        /// BODY: N=4&L=70&L=62&L=58&L=73
        /// ->62 1
        /// curl -H "Content-Type: application/x-www-form-urlencoded" -d "N=4&L=70&L=62&L=58&L=73" https://localhost:7276/api/q5b/Bronze
        /// ->62 1
        /// curl -H "Content-Type: application/x-www-form-urlencoded" -d "N=8&L=75&L=70&L=60&L=70&L=70&L=60&L=75&L=70" https://localhost:7276/api/q5b/Bronze
        /// ->60 2
        /// curl -H "Content-Type: application/x-www-form-urlencoded" -d "N=2&L=70&L=62" https://localhost:7276/api/q5b/Bronze
        /// ->Invalid input: Please input at least three participants or scores.
        /// curl -H "Content-Type: application/x-www-form-urlencoded" -d "N=3&L=70&L=62" https://localhost:7276/api/q5b/Bronze
        /// -> Invalid input: Please input at least three participants or scores.
        /// curl -H "Content-Type: application/x-www-form-urlencoded" -d "N=3&L=70&L=62&L=70&L=2" https://localhost:7276/api/q5b/Bronze
        /// ->Invalid input: numbers of participants and scores are not match
        /// curl -H "Content-Type: application/x-www-form-urlencoded" -d "N=3&L=70&L=62&L=62" https://localhost:7276/api/q5b/Bronze
        /// ->Invalid input: Please input at least three distinct scores.
        /// </example>
        [HttpPost(template: "Bronze")]
        [Consumes("application/x-www-form-urlencoded")]
        // /json can only pass one parameter
        //if there are 2 parameters need to pass, use /x-www-form-urlencoded
        public string Bronze([FromForm] int N, [FromForm] List<int> L)
        {
            if (N < 3 || L.Count < 3)
            {
                return "Invalid input: Please input at least three participants or scores.";
            }

            if (N != L.Count)
            {
                return "Invalid input: numbers of participants and scores are not match";
            }

            foreach (int i in L)
            {
                if (i < 0 || i > 75)
                {
                    return "Invalid input: Each score must be between 0 and 75 (inclusive).";
                }
            }

            List<int> distinctScores = L.Distinct().ToList();
            if (distinctScores.Count < 3)
            {
                return "Invalid input: Please input at least three distinct scores.";
            }

            //List<int> scoreList = L.ToList();
            int maxScore = L.Max();
            L.RemoveAll(n => n == maxScore);
            maxScore = L.Max();
            L.RemoveAll(n => n == maxScore);
            int S = L.Max();
            int P = L.Count(n => n == S);
            return S + " " + P;
        }
    }
}
// .Length for arrays and strings;
// .Count for List<T>, Dictionary<T1, T2>;