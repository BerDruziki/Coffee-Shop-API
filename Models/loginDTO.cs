using System.ComponentModel.DataAnnotations;
public class loginDTO
    {
        [Required (ErrorMessage = "Email Required")]
        public string email { get; set; }

        [Required (ErrorMessage = "Password Required")]
        public string password { get; set; }
        public loginDTO(string Email, string Password)
        {
            this.email = Email;
            this.password = Password;
        }
    }