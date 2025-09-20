using System.ComponentModel.DataAnnotations;

namespace RestaurantWebApi.Model
{
    public class UserInputModel
    {
       
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;

        [Required, DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]

        public string Password { get; set; } = null!;
    }
}
