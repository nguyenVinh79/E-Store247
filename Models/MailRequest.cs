using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace ECommerce.Project.Models
{
    public class MailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<IFormFile> Attachments { get; set; }

        //install NUGET :  Install-Package MailKit
        //install NUGET :  Install-Package MimeKit
    }
}