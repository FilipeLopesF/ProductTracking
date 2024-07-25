using System;
using _3DTrackingProducts.Api.Core.Repositories;
using _3DTrackingProducts.Api.Data;
using _3DTrackingProducts.Api.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace _3DTrackingProducts.Api.Persistence.Repositories
{
    public class UsersRepository : Repository<User>, IUsersRepository
    {
        public UsersRepository(ApplicationDbContext context)
            : base(context)
        { }
    }
}

