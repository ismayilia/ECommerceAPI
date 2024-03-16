using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence
{
	static class Configuration
	{
       static public string ConnectionString 
		{
			get
			{
				//Configruation package
				ConfigurationManager configurationManager = new();
				//Configruation.json package
				configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/ECommerceAPI.API"));
				configurationManager.AddJsonFile("appsettings.Development.json");

				return configurationManager.GetConnectionString("DefaultConnection");
			}
		}
    }
}
