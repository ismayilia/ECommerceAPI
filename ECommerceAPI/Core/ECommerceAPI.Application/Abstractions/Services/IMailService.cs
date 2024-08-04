﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstractions.Services
{
	public interface IMailService
	{
		Task SendMessageAsync(string to, string subject, string body, bool isBodyHtml = true);
		Task SendMessageAsync(string[] toa, string subject, string body, bool isBodyHtml = true);
	}
}
