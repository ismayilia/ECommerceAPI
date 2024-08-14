using ECommerceAPI.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ECommerceAPI.Infrastructure.Services
{
	public class MailService : IMailService
	{

		readonly IConfiguration _configuration;

		public MailService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		
		public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
		{
			await SendMailAsync(new[] {to}, subject, body, isBodyHtml); ;
		}

		public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
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

		public async Task SendPasswordResetMailAsync(string to, string userId, string resetToken)
		{
			// Mail içeriğini oluştur
			var mail = new StringBuilder();

			mail.AppendLine("Hello,<br><br>");
			mail.AppendLine("If you want to reste your password you can do it through link:<br>");
			mail.AppendLine($"<strong><a target=\"_blank\" href=\"{_configuration["AngularClientUrl"]}/update-password/{userId}/{resetToken}\">Click for reset password...</a></strong><br><br>");
			mail.AppendLine("<span style=\"font-size:12px;\">If you do not know about this request please skip this mail.</span><br>");
			mail.AppendLine("<br><br><br>IA-Mini|E-commerce");

			// E-postayı gönder
			await SendMailAsync(to, "Reset Password", mail.ToString());
		}

		public async Task SendCompletedOrderMailAsync(string to, string orderCode, DateTime orderDate, 
													  string nameSurname, string userName)
		{
			string mail = $"Hello Dear {nameSurname}  {userName}, <br>" +
				$"{orderCode} order at {orderDate} date has been completed and sent to the third party cargo company.<br>" +
				$"Best regards!";

			await SendMailAsync(to, $"{orderCode}-Order Completed", mail);
		}
	}
}
