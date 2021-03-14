using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

public struct SmsSendReq
{
    public string to { get; set; }
    public string msg_text { get; set; }
}

public struct SmsSendAck
{
    public SmsSendReq request { get; set; }
    public string result { get; set; }
}
namespace WebApplication1.Controllers
{
    [Route("api/sms")]
    [ApiController]
    public class SMSController : ControllerBase
    {
        

        
    

        // POST api/<SMSController1>
        [HttpPost]
        public SmsSendAck Post([FromBody] SmsSendReq request)
        {
            SmsSendAck result = new SmsSendAck();
            result.request = request;
            var accountSid = "AC0a4933cd7f64745cf40c99b6282009d1";
            var authToken = "dea929407b77c0196a46a05b380f4ab6";
            TwilioClient.Init(accountSid, authToken);

            var messageOptions = new CreateMessageOptions(
                new PhoneNumber(request.to));
            messageOptions.From = new PhoneNumber("+19087683791");
            messageOptions.MessagingServiceSid = "MG0879e4ab4a8f5dfff4c017f93e7c624c";
            messageOptions.Body = request.msg_text;

            var message = MessageResource.Create(messageOptions);
            Console.WriteLine(message.Body); 
            return result;
        }

        
       
    }
}
