using Core_Layer.Interfaces;
using Core_Layer.Models.ErrorsHandle;
using Core_Layer.Models.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Service_Layer.JwtToken
{
	public class JwtToken: IJwtToken
	{
		private readonly IConfiguration _config;
		private readonly UserManager<ApplicationUser> _userManager;

		public JwtToken(IConfiguration config, UserManager<ApplicationUser> userManager)
		{
			_config = config;
			_userManager = userManager;
		
		}
		public  async Task<string> CreateTokenAsync(ApplicationUser user)
		{
			//create claim
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Email, user.Email!),
				new Claim(ClaimTypes.NameIdentifier, user.Id),
				new Claim(ClaimTypes.Name, user.DisplayName)
			};
			var roles =await _userManager.GetRolesAsync(user);
			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}
			//create Key
			var issuer = _config["Jwt:Issuer"];
			var audience = _config["Jwt:audience"];

			double Days = 7;
			double.TryParse(_config["Jwt:ExpiresInDays"], out Days); // لو حول صح هيغير قيمة ال days ولو محولش هتفضل ب 7 ايام
			var expires =DateTime.UtcNow.AddDays(Days);

			var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);
			var SecurityKey = new SymmetricSecurityKey(key);
			var signingCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
			var notBefore = DateTime.UtcNow;
			//public JwtSecurityToken(string issuer = null, string audience = null, IEnumerable<Claim> claims = null, DateTime? notBefore = null, DateTime? expires = null, SigningCredentials signingCredentials = null)

			var jwtToken = new JwtSecurityToken(issuer,audience,claims, notBefore, expires,signingCredentials);
			return new JwtSecurityTokenHandler().WriteToken(jwtToken);

		}
		
	}
}
