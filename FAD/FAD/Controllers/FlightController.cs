using FAD.Domain.Repository;
using FAD.Domain.Services;
using FAD.Models;
using FAD.Repository;
using FAD.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FAD.Controllers
{
    [Route("api/fad/flights")]
    [Produces("application/json")]
    [ApiController]
    public class FlightController : ControllerBase
    {

        private readonly IFlightService _flightService;

        public FlightController(IFlightService flightService) {
            _flightService = flightService;
        }

        // GET: api/<FlightController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<FlightController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/fad/flights
        [HttpPost]
        public ActionResult Post([FromBody] Flight flight)
        {
            //Check if IATA exists
            var checkIATA = _flightService.FindAirport(flight.From);
            if (checkIATA)
            {
                checkIATA = _flightService.FindAirport(flight.To);
                if (!checkIATA) return Problem("Unknown IATA airport code");
            }
            else {
                return Problem("Unknown IATA airport code");
            }

            //Check if there's a flight already 
            var exisitingFlight = _flightService.FindFlight(flight);

            if (!exisitingFlight)
            {
                var addedFlight = _flightService.AddFlight(flight);
                return Ok("Thank you for using the FAD Services!");
            }
            else {
                return Ok("Thank you for using the FAD Services!");
            } 
            
           
            
        }

        // PUT api/<FlightController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FlightController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
