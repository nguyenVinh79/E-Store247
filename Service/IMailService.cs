using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Project.Models;

namespace ECommerce.Project.Service
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);

    }
}
