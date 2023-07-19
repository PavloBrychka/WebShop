namespace WebShop.Models.Auth
{
    public class LoginViewModel
    {
        /// <summary>
        /// Електронна пошта
        /// </summary>
        /// <example>brychka.p@gmail.com</example>
        public string Email { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        /// <example>Password</example> 
        public string Password { get; set; }
    }
}