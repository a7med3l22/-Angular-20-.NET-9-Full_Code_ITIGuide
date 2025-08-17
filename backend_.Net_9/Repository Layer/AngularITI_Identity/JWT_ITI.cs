using Core_Layer.Interfaces;
using Core_Layer.Models.AngularITI_Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.AngularITI_Identity
{
	public class JWT_ITI: IJWT_ITI
	{
		private readonly IConfiguration configuration;
		private readonly UserManager<RegisterUser> user;

		//token 
		public JWT_ITI(IConfiguration configuration,UserManager<RegisterUser> user)
		{
			this.configuration = configuration;
			this.user = user;
		}


		public async Task<string> MakeToken(RegisterUser registerUser)
		{

			//JwtSecurityToken(string issuer = null, string audience = null, IEnumerable<Claim> claims = null, DateTime? notBefore = null, DateTime? expires = null, SigningCredentials signingCredentials = null)

			string issuer = configuration["Jwt:Issuer"]!; // string? this[string key] { get; set; }
			string audience = configuration["Jwt:Audience"]!;
			List<Claim> claims = new();
			// ال اي دي ف الكليمز والرولز اللي ف ال يوزر	

			var claimRole =await user.GetRolesAsync(registerUser);
			foreach (var claim in claimRole)
			{
			claims.Add(new Claim(ClaimTypes.Role, claim)); // ضفت ال رول 
			}
			claims.Add(new Claim(ClaimTypes.NameIdentifier, registerUser.Id)); //ضفت ف ال كليمز ال اي دي 
			claims.Add(new Claim(ClaimTypes.Email, registerUser.Email!));
			claims.Add(new Claim(ClaimTypes.Name, registerUser.UserName!));

			foreach (var phone in registerUser.PhoneNumbers)
			{
				claims.Add(new Claim(ClaimTypes.MobilePhone, phone.PhoneNumber)); 
			}
			//   expires:
			//     If expires.HasValue a { exp, 'value' } claim is added, overwriting any 'exp'
			//     claim in 'claims' if present.
			//
			//   notBefore:
			//     If notbefore.HasValue a { nbf, 'value' } claim is added, overwriting any 'nbf'
			//     claim in 'claims' if present.
			DateTime notBefore =DateTime.UtcNow; // علشان لما يبقي اكسبير ميدلوش وقت اضافي 
			bool Result= int.TryParse(configuration["Jwt:ExpiresInDays"]!,out int exp);
			if (!Result)
			{
				exp = 7;
			}
			DateTime? expires = DateTime.UtcNow.AddDays(exp);

			//	public SigningCredentials(SecurityKey key, string algorithm)
			//	public SymmetricSecurityKey(byte[] key)
			string secretkey = configuration["Jwt:Key"]!;
			byte[] secretkeyByte= UTF8Encoding.UTF8.GetBytes(secretkey);
			SecurityKey key = new SymmetricSecurityKey(secretkeyByte);
			SigningCredentials signingCredentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256Signature);
			var Token =new JwtSecurityToken(issuer,audience,claims,notBefore,expires,signingCredentials);
			var stringToken=new JwtSecurityTokenHandler().WriteToken(Token);
			return stringToken;
		}
	}
}
