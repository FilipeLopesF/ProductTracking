using System;
using System.ComponentModel.DataAnnotations;

namespace _3DTrackingProducts.Api.Resources
{
    public class UserSignUpResource
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }
    }
}

