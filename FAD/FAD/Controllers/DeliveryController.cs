using FAD.Domain.Services;
using FAD.Models;
using FAD.Resources;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FAD.Controllers
{
    [Route("api/fad/deliveries")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService  _deliveryService;

        public  DeliveryController(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }


        // GET api/deliveries?from=from&to=to&date=date
        [HttpGet]
        public IActionResult Get(string from, string to, string date)
        {
            float price;
            float distance;

            //Check if IATA exists
            var checkIATA = _deliveryService.FindAirport(from);
            if (checkIATA)
            {
                checkIATA = _deliveryService.FindAirport(to);
                if (!checkIATA) return BadRequest("Unknown IATA airport code");
            }
            else
            {
                return BadRequest("Unknown IATA airport code");
            }


            //Check if the delivery is possible
            var checkFlight = _deliveryService.FindFlight(from, to);
            if (checkFlight)
            {
                distance = CalculateDistance(_deliveryService.GetAirport(from), _deliveryService.GetAirport(to));
                price = CalculatePrice(distance, CheckDate(date));

                DeliveryResource message = new DeliveryResource();
                message.Status = "AVAILABLE";
                message.Message = "Thank you for using FAD services";
                message.Departure = from;
                message.Arrival = to;
                message.TotalPrice = price;

                return Ok(message);
            }
            else {
                DeliveryResource message = new DeliveryResource();
                message.Status = "UNAVAILABLE";
                message.Message = "We're unable to fulfill your request :(";
                message.Departure = from;
                message.Arrival = to;
                message.TotalPrice = 0f;
                return Ok(message);
            }
            
        }

        private float CalculateDistance(Airport from, Airport to) {
            float distance = 0f;

            distance =  (float) Math.Sqrt(Math.Pow((to.Latitude - from.Latitude), 2) + Math.Pow((to.Longitude - from.Longitude), 2));

            return distance;
        }

        private string CheckDate(string value) {
            //value YYYY-MM-DD
            string DOW;
            string[] date = value.Split();

            DateTime dateValue = new DateTime(Int32.Parse(date[0]), Int32.Parse(date[1]), Int32.Parse(date[2]));
            DOW = dateValue.ToString("ddd");
            return DOW;
        }
        private float CalculatePrice(float distance, string date) {
            /*
             * Price = price per unit x distance
             * MON-FRI price per unit = 0.01
             * SAT price per unit = 0.02
             * SUN price per unit = 0.04
             */
            float pricePerUnit = 0f;

            switch (date) {
                case "MON": case "TUE": case "WED": case "THR": case "FRI":
                    pricePerUnit = 0.01f;
                    break;
                case "SAT":
                    pricePerUnit = 0.02f;
                    break;
                case "SUN":
                    pricePerUnit = 0.04f;
                    break;
                default:
                    break;
            }

            return distance * pricePerUnit;
        }
    }
}
