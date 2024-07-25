using System;
using _3DTrackingProducts.Api.Core;
using _3DTrackingProducts.Api.Models.Auth;
using _3DTrackingProducts.Api.Persistence;
using _3DTrackingProducts.Api.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _3DTrackingProducts.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UsersController(IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _unitOfWork.Users.GetAll();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string email)
        {
            var user = await _unitOfWork.Users.Find(u=>u.Email.Equals(email));

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string email)
        {
            var user = await _unitOfWork.Users.SingleOrDefault(u => u.Email == email);

            if(user == null)
            {
                return NotFound("The user does not exist");
            }

            await _unitOfWork.Users.Remove(user);
            await _unitOfWork.Users.Save();

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateById(Guid id, UserUpdateResource userUpdateResource)
        {
            var user = await _unitOfWork.Users.SingleOrDefault(u => u.Id == id);

            user.Email = userUpdateResource.Email;
            user.FirstName = userUpdateResource.FirstName;
            user.LastName = userUpdateResource.LastName;
            user.NormalizedEmail = userUpdateResource.Email.Normalize();
            user.UserName = userUpdateResource.Email;

            if(user == null)
            {
                return NotFound("The user does not exist");
            }

            await _userManager.UpdateAsync(user);

            return Ok(user);
        }
    }
}

