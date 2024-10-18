using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace assignment2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class q5 : ControllerBase
    {
        /// <summary>
        /// Question: 2024 CCC J3
        /// To determine the score required for bronze level and how many participants achieved this score.
        /// </summary>
        /// <param name="N">representing the number of participants.</param>
        /// <param name="L">representing the scores of participants.</param>
        /// <returns>output the score required for bronze level and how many participants achieved this score.</returns>
        /// <example>
        /// curl "https://localhost:7276/api/q5/Bronze?N=4&L=70&L=62&L=58&L=73"
        /// ->62 1
        /// curl "https://localhost:7276/api/q5/Bronze?N=8&L=75&L=70&L=60&L=70&L=70&L=60&L=75&L=70"
        /// ->60 2
        /// curl "https://localhost:7276/api/q5/Bronze?N=2&L=70&L=62"
        /// ->Invalid input: Please input at least three participants or scores.
        /// curl "https://localhost:7276/api/q5/Bronze?N=3&L=70&L=62"
        /// Invalid input: Please input at least three participants or scores.
        /// curl "https://localhost:7276/api/q5/Bronze?N=3&L=70&L=62&L=70&L=2"
        /// ->Invalid input: numbers of participants and scores are not match
        /// curl "https://localhost:7276/api/q5/Bronze?N=3&L=70&L=62&L=62"
        /// Invalid input: Please input at least three distinct scores.
        /// curl "https://localhost:7276/api/q5/Bronze?N=3&L=70&L=62&L=61"
        /// 61 1
        /// </example>
        [HttpGet(template: "Bronze")]
        public string Bronze(int N, [FromQuery] int[] L)
        {
            if (N <3 || L.Length<3)
            {
                return "Invalid input: Please input at least three participants or scores.";
            }

            if (N != L.Length)
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

            List<int> scoreList = L.ToList();
            int maxScore = scoreList.Max();
            scoreList.RemoveAll(n=> n == maxScore);
            maxScore = scoreList.Max();
            scoreList.RemoveAll(n => n == maxScore);
            int S = scoreList.Max();
            int P = scoreList.Count(n => n == S);
            return S + " " + P;
        }
    }
}

//	Array is fixed size; List is dynamic size;
// scoreList.Remove(scoreList.Max()); only remove one element not all;
// .Length for arrays and strings;
// .Count for List<T>, Dictionary<T1, T2>;
