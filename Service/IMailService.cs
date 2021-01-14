using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopBanHang.Models;

namespace ShopBanHang.Service
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);

    }
}
