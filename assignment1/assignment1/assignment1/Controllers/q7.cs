using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace assignment1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class q7 : ControllerBase
    {
        //q7
        /// <summary>
        /// receives a number of day change and output the changed date starting from the current date
        /// </summary>
        /// <param name="days">how many days the user want to adjust</param>
        /// <returns>a string representation of the current date, adjusted by {days}</returns>
        ///<example>
        ///GET : https://localhost:7214/api/q7/timemachine?days=-3 -> 2024-09-23
        ///GET : https://localhost:7214/api/q7/timemachine?days=7 -> 2024-10-03
        ///</example>

        [HttpGet(template: "timemachine")]
        public string Timemachine(int days) // DateTime: Gets the current date
        {
            DateTime today = DateTime.Now;
            // DateTime: a built-in struct in C# that represents dates and times.
            //DateTime.Now: a property(~function) of the DateTime struct that returns the current local date and time on the system.
            DateTime daychange = today.AddDays(days);
            string date = daychange.ToString("yyyy-MM-dd"); // define the string format
            return date; }
    }
}

//property vs function: Both properties and functions allow you to encapsulate logic
//    but properties are designed specifically for reading and writing data (i.e., getting and setting values) 
//    in an object-oriented way. // modification in a controlled way.

//property: a member of a class or struct
//that provides a flexible mechanism to read, write, or compute the value of a private field.

//A struct: a value type that is used to encapsulate small groups of related variables.
//Structs can contain data and methods,
//but they are typically used for objects that are smaller and more efficient than classes. 

// Value Types:
//Primitive types: int, double, bool, char, float, etc.
//Structs: Custom - defined structures(struct) like DateTime, Point, etc.
//Enumerations(enum): Like enum Day { Sunday, Monday, ... }
