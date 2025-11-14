using System.ComponentModel.DataAnnotations;

namespace WatchList.Core.Tools.Concrete.Dto.User.Request;

public class LoginRequestDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}