using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization; // to use CultureInfo.CreateSpecificCulture("en-CA")

namespace assignment1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class q8 : ControllerBase
    {
        //q8

        /// <summary>
        /// receives a POST request with how many small and large SquashFellows plushies
        /// outputs the online summary order
        /// </summary>
        /// <param name="Small">how many the small plushies ordered</param>
        /// <param name="Large">how many the large plushies ordered</param>
        /// <returns>return the checkout summary for an order </returns>
        ///<example>
        /// POST : https://localhost:7214/api/q8/squashfellows
        /// HEADER: Content-Type: application/x-www-form-urlencoded
        /// BODY: Small=100&Large=100
        ///->100 Small @ $25.50 = $2,550.00; 100 Large @ $40.50 = $4,050.00; Subtotal = $6,600.00; Tax = $858.00 HST; Total = $7,458.00
        ///curl -H "Content-Type: application/x-www-form-urlencoded" -d "Small=10&Large=5" https://localhost:7214/api/q8/squashfellows
        ///->10 Small @ $25.50 = $255.00; 5 Large @ $40.50 = $202.50; Subtotal = $457.50; Tax = $59.48 HST; Total = $516.98
        ///curl -H "Content-Type: application/x-www-form-urlencoded" -d "Small=5&Large=10" https://localhost:7214/api/q8/squashfellows
        ///->5 Small @ $25.50 = $127.50; 10 Large @ $40.50 = $405.00; Subtotal = $532.50; Tax = $69.23 HST; Total = $601.73
        ///</example>

        [HttpPost(template: "squashfellows")]
        [Consumes("application/x-www-form-urlencoded")]
        public string Squashfellows([FromForm] int Small, [FromForm] int Large)
        {
            // float require an f suffix e.g. 25.5f
            // int * 25.5 will become double, cannot convert to float
            double small_total = Small * 25.50;
            double large_total = Large * 40.50;
            double subtotal = small_total + large_total;
            double tax = Math.Round(subtotal * 0.13,2);
            double total = subtotal+tax;
            // subtotal*1.13 = 516.9749999999999, no use for Math.Round, may due to floating-point precision problem when using the double data type

            string small_string = small_total.ToString("C", CultureInfo.CreateSpecificCulture("en-CA"));
            string large_string = large_total.ToString("C", CultureInfo.CreateSpecificCulture("en-CA"));
            string subtotal_string = subtotal.ToString("C", CultureInfo.CreateSpecificCulture("en-CA"));
            string tax_string = tax.ToString("C", CultureInfo.CreateSpecificCulture("en-CA"));
            string total_string = total.ToString("C", CultureInfo.CreateSpecificCulture("en-CA"));
            //return Small + " Small @ $25.50 = " + small_total.ToString("C", CultureInfo.CreateSpecificCulture("en-CA")) + "; " + Large + " Large @ $40.50 = " + large_total.ToString("C", CultureInfo.CreateSpecificCulture("en-CA")) + "; Subtotal = " + subtotal.ToString("C", CultureInfo.CreateSpecificCulture("en-CA")) + "; Tax = " + tax.ToString("C", CultureInfo.CreateSpecificCulture("en-CA")) + " HST; Total = " + total.ToString("C", CultureInfo.CreateSpecificCulture("en-CA"));
            //.ToString("C") will return HK$25.5;
            //return $"{Small} Small @ $25.50 = {small_total}; {Large} Large @ $40.50 = {large_total}; Subtotal = {subtotal}; Tax = {tax} HST; Total = {total}";
            return $"{Small} Small @ $25.50 = {small_string}; {Large} Large @ $40.50 = {large_string}; Subtotal = {subtotal_string}; Tax = {tax_string} HST; Total = {total_string}";
        }
    }
}
