namespace WatchList.Core.Tools.Concrete.JwtTool;

public class JwtInfo
{
    // JWT token'ları imzalamak için kullanılan gizli anahtar
    // En az 256 bit (32 karakter) olmalı
    public string SecurityKey { get; set; }
    // Token'ı kim oluşturdu? (Token'ı yaratan servis)
    public string Issuer { get; set; }
    // Token'ı kim kullanacak? (Hedef kitle)
    public string Audience { get; set; }
    // Token'ın kaç dakika geçerli olacağı
    public int Expires { get; set; }
}