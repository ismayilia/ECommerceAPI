using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.DTOs.Facebook
{
    public class FacebookAccessTokenResponse

    {
        // json atributu olmasa  donushu neye gore edecek bilmeyecek, ishlemeyecek
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }
    }
}
