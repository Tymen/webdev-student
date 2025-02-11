using Microsoft.AspNetCore.Mvc;
using ShowcaseAPI.Models;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using ShowcaseAPI.Models.config;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShowcaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly MailtrapConfig _mailtrapConfig;

        public MailController(IOptions<MailtrapConfig> config)
        {
            _mailtrapConfig = config.Value;
        }
        // POST api/<MailController>
        [HttpPost]
        public ActionResult Post([Bind("FirstName, LastName, Email, Phone")] Contactform form)
        {
            try
            {
                var client = new SmtpClient(_mailtrapConfig.SMTPUrl, _mailtrapConfig.Port)
                {
                    Credentials = new NetworkCredential("api", _mailtrapConfig.Password),
                    EnableSsl = true
                };
                client.Send("hello@demomailtrap.com", "tymenvis@gmail.com", "Hello world", "testbody");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

            return Ok();
        }
    }
}
