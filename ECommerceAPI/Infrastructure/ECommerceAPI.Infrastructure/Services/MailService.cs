﻿using ECommerceAPI.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace ECommerceAPI.Infrastructure.Services
{
	public class MailService : IMailService
	{

		readonly IConfiguration _configuration;

		public MailService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task SendMessageAsync(string to, string subject, string body, bool isBodyHtml = true)
		{
			await SendMessageAsync(new[] {to}, subject, body, isBodyHtml); ;
		}

		public async Task SendMessageAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
		{
			MailMessage mail = new();
			mail.IsBodyHtml = isBodyHtml;
			foreach (var to in tos)
				mail.To.Add(to);
			mail.Subject = subject;
			mail.Body = body;
			mail.From = new(_configuration["Mail:Username"], "E-Commerce", System.Text.Encoding.UTF8);

			// mail gondermek ucun
			SmtpClient smtp = new();
			smtp.Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
			smtp.Port = int.Parse(_configuration["Mail:Port"]);
			smtp.EnableSsl = true;
			smtp.Host = _configuration["Mail:Host"];
			await smtp.SendMailAsync(mail);
		}
	}
}
