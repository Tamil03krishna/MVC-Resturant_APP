using System.Security.Cryptography;
using System.Text;

namespace RestaurantWebSite.Helper
{

    //scaffold command
    //Scaffold-DbContext "server=localhost;port=3306;user=root;password=root;database=RestaurantDB;" Pomelo.EntityFrameworkCore.MySql -outputDir Context -Context DataContext -Project RestaurantWebApi.DataAccess -StartupProject RestaurantWebSite -force
    public class helper
    {

        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);

                var sb = new StringBuilder();
                foreach (var b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }

        }
    }
}
 



