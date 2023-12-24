using QDiet.Domain.DataBase;
using QDiet.Domain.Models.DataBase;
using System.Security.Cryptography;
using System.Text;

Db db = new Db();
SHA256 sha = SHA256.Create();

var roles = db.Roles.ToList();

db.Users.Add(new User
{
    Username = "dahuk",
    Password = Convert.ToHexString(sha.ComputeHash(Encoding.ASCII.GetBytes("12345"))),
    Email = "danik_1235@mail.ru",
    Roles = new List<Role> { roles.First() }
});
db.SaveChanges();

Console.WriteLine("Done");