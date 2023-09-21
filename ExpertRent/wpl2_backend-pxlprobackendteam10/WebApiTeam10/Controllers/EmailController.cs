﻿using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Text.Json.Nodes;
using ClassLibTeam06.Business.Entities;
using ClassLibTeam06.Data;
using ClassLibTeam10.Business;
using ClassLibTeam10.Business.Entities;
using ClassLibTeam10.Data;
using ClassLibTeam10.Data.Framework;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebApiTeam10.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        [HttpPost]
        [Route("ReceiveEmailCode")]
        public void ReceiveEmailCode([FromBody] JsonObject json)
        {
            Console.Write(json.ToString());
            EmailCode emailCode = JsonConvert.DeserializeObject<EmailCode>(json.ToString());
            var smtpClient = new SmtpClient("smtp.strato.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("expertrent@wplpxl.be", "WerkpleklerenTeam10"),
                EnableSsl = true,
            };
            string recipientsEmail = emailCode.GetEmail();
            Random random = new Random();
            int code = random.Next(100000,999999);
            UserData userData = new UserData();
            userData.EmailCodeToDB(code.ToString(), recipientsEmail);
            //smtpClient.Send("expertrent@wplpxl.be", recipientsEmail, "Expertrent change password code", code.ToString());
            //////////////////////////////////////////////////////
            ///
            string base64Image = "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";
            string htmlBody = $@"
        <div style=""background-color: white; width: 100%"">
        <img src=""data:image/png;base64,{base64Image}"">
        </div>
        <div style=""text-align: center; display: flex; justify-content: center; flex-direction: column;"">
        <p style=""margin-bottom: 1px""><span style=""font-size: x-large"">Hi there! </span>You received this e-mail because you requested a password change!</p>
        <p style=""margin-top: 2px; margin-bottom: 80px"">If you think this is a mistake, be sure to contact us through the contact page!</p>
        <h4>Expertrent Change Password Code</h4>
        <p>Your code is:</p>
        <div style=""background-color: dodgerblue; width: 300px; display: flex; align-self: center; justify-content: center; border-radius: 10px"">
        <p style=""color: white; text-align: center""><strong>{code}</strong></p>
        </div>
        </div>";

            // Create the email message and set the HTML body
            MailMessage message = new MailMessage();
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(htmlBody, null, "text/html");
            message.AlternateViews.Add(htmlView);

            // Attach the image file
           /* Attachment logoAttachment = new Attachment("D:\\JASPER\\WORK\\Opleidingen\\PXL\\WPL\\WPL2\\ExpertRent\\wpl2_backend-pxlprobackendteam10\\WebApiTeam10\\bin\\Debug\\expertrent.png");
            logoAttachment.ContentId = "expertrent_logo";
            message.Attachments.Add(logoAttachment);*/

            MailMessage mailMessage = new MailMessage("expertrent@wplpxl.be", recipientsEmail, "Expertrent change password code", htmlBody);
            mailMessage.IsBodyHtml = true;

            smtpClient.Send(mailMessage);
        }
        
        [HttpGet]
        [Route("SendEmailCode")]
        public IActionResult sendEmailCode(string email)
        {
            UserData userData = new UserData();
            return Ok(userData.GetEmailCodeFromDB(email).ToString());
        }
        
    }
}