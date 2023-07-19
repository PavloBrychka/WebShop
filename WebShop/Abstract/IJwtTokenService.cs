using WebShop.Data.Entites.Identity;

namespace WebShop.Abstract
{
    public interface IJwtTokenService
    {
        Task<string> CreateToken(UserEntity user);
    }
}
