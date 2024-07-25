using System;
using System.ComponentModel.DataAnnotations;

namespace _3DTrackingProducts.Api.Resources
{
    public class UserSignInResource
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}

