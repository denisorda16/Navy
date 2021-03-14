using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
public struct EmailSpammerReq
{
    public string to        {get; set;}
    public string subject   {get; set;}
    public string msg_text { get; set; } 
}

public struct EmailSpammerAck
{
    public EmailSpammerReq request { get; set; }
    public string result { get; set; }
}

namespace WebApplication1.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public string Get()
        {
            return "Hello World" ;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
            

        
        // POST api/<ValuesController>
        [Route("send_spam")]
        [HttpPost]
        public EmailSpammerAck Post([FromBody] EmailSpammerReq value)
        {
            EmailSpammerAck emailSpammerAck = new EmailSpammerAck();
            emailSpammerAck.request = value;
            emailSpammerAck.result = "OK";
            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress("andreyondatr@gmail.com", "Tom");
            // кому отправляем
            MailAddress to = new MailAddress(value.to);
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = value.subject;
            // текст письма
            m.Body = value.msg_text;
            // письмо представляет код html
            m.IsBodyHtml = false;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            // логин и пароль
            smtp.Credentials = new NetworkCredential("andreyondatr@gmail.com", "ztmybwadsatqecma");
            smtp.EnableSsl = true;
            smtp.Send(m);
            return emailSpammerAck;
        }
    }
}
