using JWT.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace JWT.Business {
    public class Manager : IManager {
        private List<User> userList = new List<User>();
        public Manager() {
            //Some dummy data 
            userList.Add(new User() {
                Id = 1, 
                Name = "Furkan",
                Password = "qwerty",
                UserName = "Lion",
                Surname = "Öztürk" });
            
            userList.Add(new User() { 
                Id = 2,
                Name = "Ali",
                Password = "asdasd1234",
                UserName = "asdsa34",
                Surname = "Ali" }); 
            
            userList.Add(new User() { 
                Id = 3,
                Name = "Veli",
                Password = "ads334",
                UserName = "df5gt23",
                Surname = "Veli" });
        }

        public string Authenticate(string userName, string password) {

            var user = userList.FirstOrDefault(x => x.UserName.Equals(userName) && x.Password.Equals(password));
            if(user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("THIS IS MY SECRET KEY DID YOU LIKE IT");
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(5), //Expire Date is 5 minutes
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var result = tokenHandler.WriteToken(token);
            return result;
        }
    }
}
