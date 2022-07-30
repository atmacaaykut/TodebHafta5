using System;


namespace Bussines.Configuration.Auth
{
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
