﻿using CMDB.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CMDB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        public MenuController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        [HttpGet("FirstLevel"), Authorize]
        public async Task<ActionResult> GetFirstLevelMenu()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid)?.Value;
            if (userIdClaim == null)
                return Unauthorized();
            return Ok(await _uow.MenuRepository.GetFirstLevel());
        }
        [HttpGet("SecondLevel/{id:int}"), Authorize]
        public async Task<ActionResult> GetSecondLevel(int id)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid)?.Value;
            if (userIdClaim == null)
                return Unauthorized();
            return Ok(await _uow.MenuRepository.GetSecondLevel(id));
        }
        [HttpGet("PersonalMenu/{menuId:int}"), Authorize]
        public async Task<IActionResult> GetPersonalMenu(int menuId)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid)?.Value;
            if (userIdClaim == null)
                return Unauthorized();
            int level = Int32.Parse(User.Claims.First(x => x.Type == ClaimTypes.Name).Value);
            return Ok(await _uow.MenuRepository.GetPestonalMenu(menuId,level));
        }
    }
}
