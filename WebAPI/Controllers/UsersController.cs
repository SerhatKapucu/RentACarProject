using Business.Abstract;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserService _userService;

        public UsersController(IUserService usersService)
        {
            _userService = usersService;
        }

        [HttpGet("get")]
        public ActionResult Get(int id)
        {
            var result = _userService.GetById(id);
            if (result.Success)
            {
                return Ok(result);

            }

            return BadRequest(result.Message);
        }
        [HttpGet("getclaims")]
        public ActionResult GetClaims(User user)
        {
            var result = _userService.GetClaims(user);
            if (result.Success)
            {
                return Ok(result);

            }

            return BadRequest(result.Message);
        }

    }
}
