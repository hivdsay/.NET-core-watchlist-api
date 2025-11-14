using WatchList.Entities.Concrete;

namespace WatchList.Core.Tools.Abstract.JwtTool;

public interface IJwtService
{
    Task<string> GenerateJwt(User user, List<UserRole> role);
}