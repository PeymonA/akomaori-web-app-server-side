using Microsoft.AspNetCore.Mvc;
using A2.Models;
using A2.Dtos;
using A2.Data;
using A2.Helper;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Globalization;

namespace A2.Controllers
{
    [Route("webapi")]
    [ApiController]
    public class A2Controller : Controller
    {
        private readonly IA2Repo _repository;
        public A2Controller(IA2Repo repository)
        {
            _repository = repository;
        }
        // GET webapi/Register
        [HttpPost("Register")] 
        public ActionResult<User> Register(User user)
        {
            if (_repository.GetCustomerByUsername(user.Username) == null)
            {
                _repository.AddUser(user);
                return Ok("User successfully registered.");
            }

            return Ok(String.Format("UserName {0} is not available.", user.Username));
        }
        // GET webapi/PurchaseItem/{id}
        [Authorize(AuthenticationSchemes = "Authentication")]
        [Authorize(Policy = "UserOnly")]
        [HttpGet("PurchaseItem/{id}")]
        public ActionResult<PurchaseOutput> PurchaseItem(int id)
        {
            Product product = _repository.GetProductById(id);
            if (product == null)
            {
                return BadRequest(String.Format("Product {0} not found.", id));
            }
            ClaimsIdentity ci = HttpContext.User.Identities.FirstOrDefault();
            Claim c = ci.FindFirst("normalUser");
            string userName = c.Value;
            PurchaseOutput p = new PurchaseOutput{ userName = userName, productID = product.Id };
            return Ok(p);  
        }
        [Authorize(AuthenticationSchemes = "Authentication")]
        [Authorize(Policy = "OrganizorOnly")]
        [HttpPost("AddEvent")]
        public ActionResult<Event> AddEvent(EventInput eventIn) {
            DateTime d1;
            DateTime d2;
            bool start = DateTime.TryParseExact(eventIn.Start, "yyyyMMddTHHmmssZ", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out d1);
            bool end = DateTime.TryParseExact(eventIn.End, "yyyyMMddTHHmmssZ", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out d2);

            if (!(start) & !(end))
            {
                var output = new
                {
                    message = "Bad Request",
                    errorCode = 400,
                    detail = "The format of Start and End should be yyyyMMddTHHmmssZ."
                };
                return BadRequest(output);
            }
            else if (!start)
            {
                var output = new
                {
                    message = "Bad Request",
                    errorCode = 400,
                    detail = "The format of Start should be yyyyMMddTHHmmssZ."
                };
                return BadRequest(output);
            }
            else if (!end)
            {
                var output = new
                {
                    message = "Bad Request",
                    errorCode = 400,
                    detail = "The format of End should be yyyyMMddTHHmmssZ."
                };
                return BadRequest(output);
            }
            Event e = new Event { Description = eventIn.Description, End = eventIn.End, Location = eventIn.Location,
                Start = eventIn.Start, Summary = eventIn.Summary};
            _repository.AddEvent(e);
            return Ok("Success");
        }
        [Authorize(AuthenticationSchemes = "Authentication")]
        [Authorize(Policy = "AuthOnly")]
        [HttpGet("EventCount")]
        public ActionResult EventCount()
        {
            int count = _repository.GetAllEvents().Count();
            return Ok(count);
        }
        [Authorize(AuthenticationSchemes = "Authentication")]
        [Authorize(Policy = "AuthOnly")]
        [HttpGet("Event/{id}")]
        public ActionResult Event(int id)
        {
            Event e = _repository.GetEventById(id);
            
            if (e == null)
            {
                return BadRequest(String.Format("Event {0} does not exist.", id));
            }
            Response.Headers.Add("Content-Type", "text/calendar");
            return Ok(e);
        }
    }
}