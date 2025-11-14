using WatchList.Core.Tools.Concrete.Dto.User.Request;

namespace WatchList.Core.Tools.Concrete.Dto.User.Response;

public class LoginResponseDto
{
    public string Token { get; set; } = string.Empty;
    public UserInfoDto User { get; set; } = new();
}