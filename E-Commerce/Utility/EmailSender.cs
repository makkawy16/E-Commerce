﻿using Microsoft.AspNetCore.Identity.UI.Services;

namespace E_Commerce.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.CompletedTask;  
        }
    }
}
