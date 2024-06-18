using ECommerceAPI.Application.Abstractions.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Services.Token
{
	public class TokenHandler : ITokenHandler
	{
		IConfiguration _configuration;

		public TokenHandler(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public Application.DTOs.Token CreateAccessToken(int second)
		{
			Application.DTOs.Token token = new();

			// Security Key-in simmetriyini aliriq
			SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

			// Shifrelenmish adami yaradiriq
			SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

			// Yaradilacaq token settingini veririk
			// tokenin omru-addminute 
			token.Expiration = DateTime.Now.AddSeconds(second);
			//yaradilacaq tokenin hansi deyerlerde olacaqin veririk
			JwtSecurityToken securityToken = new(
				audience : _configuration["Token:Audience"],
				issuer : _configuration["Token:Issuer"],
				expires : token.Expiration,
				notBefore : DateTime.Now, //token yaradilandan ne qeder sonra ishlesin -> .addminute(1) bunuda olar 
				signingCredentials : signingCredentials //security keye gore
				);

			// Token yaradici class-indan bir numune alaq

			JwtSecurityTokenHandler tokenHandler = new();	
			token.AccessToken = tokenHandler.WriteToken(securityToken);

			//string refreshToken = CreateRefreshToken();
			token.RefreshToken = CreateRefreshToken();
			return token;
		}

		public string CreateRefreshToken()
		{
			byte[] number = new byte[32];
			using RandomNumberGenerator random = RandomNumberGenerator.Create();
			random.GetBytes(number);
			return Convert.ToBase64String(number);
		}
	}
}
