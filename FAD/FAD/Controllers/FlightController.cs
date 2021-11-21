using FAD.Domain.Repository;
using FAD.Domain.Services;
using FAD.Models;
using FAD.Repository;
using FAD.Resources;
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

        // POST api/fad/flights
        [HttpPost]
        public IActionResult Post([FromBody] Flight flight)
        {
            //Check if IATA exists
            var checkIATA = _flightService.FindAirport(flight.From);
            if (checkIATA)
            {
                checkIATA = _flightService.FindAirport(flight.To);
                if (!checkIATA) {
                    FlightResource message = new FlightResource();
                    message.Status = "ERROR";
                    message.Message = "Unknown IATA airport code";
                    message.NewFlights = 0;
                    return BadRequest(message);
                }
                
            }
            else {
                FlightResource message = new FlightResource();
                message.Status = "ERROR";
                message.Message = "Unknown IATA airport code";
                message.NewFlights = 0;
                return BadRequest("Unknown IATA airport code");
            }

            //Check if there's a flight already 
            var exisitingFlight = _flightService.FindFlight(flight);

            if (!exisitingFlight)
            {
                var addedFlight = _flightService.AddFlight(flight);
                FlightResource message = new FlightResource();
                message.Status = "OK";
                message.Message = "Thank you for using the FAD Services!";
                message.NewFlights = 1;

                return Ok(message);
            }
            else {
                return Ok();
            } 
            
           
            
        }

        // DELETE api/fad/flights/from
        [HttpDelete()]
        public IActionResult Delete(string from, string to)
        {
            // Check if IATA exists
             var checkIATA = _flightService.FindAirport(from);
            if (checkIATA)
            {
                checkIATA = _flightService.FindAirport(to);
                if (!checkIATA) return BadRequest("Unknown IATA airport code");
            }
            else
            {
                return BadRequest("Unknown IATA airport code");
            }

            return Ok();
        }
    }
}
