﻿using CMDB.API.Interfaces;
using CMDB.Domain.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CMDB.API.Controllers
{
    /// <summary>
    /// Controller for managing applications
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly string site = "Application";
        private HasAdminAccessRequest request;
        private ApplicationController()
        {
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="uow"></param>
        public ApplicationController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        /// <summary>
        /// Will get all applications
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation. Returns <see
        /// cref="UnauthorizedResult"/> if the user is not authorized, or <see cref="OkObjectResult"/></returns>
        [HttpGet, Authorize]
        public async Task<IActionResult> GetAll()
        {
            // Retrieve userId from the claims
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid)?.Value;
            if (userIdClaim == null)
                return Unauthorized();
            request = new()
            {
                AdminId = Int32.Parse(userIdClaim),
                Site = site,
                Permission = Permission.Read,
            };
            var hasAdminAcces = await _uow.AdminRepository.HasAdminAccess(request);
            if(!hasAdminAcces)
                return Unauthorized();
            return Ok(await _uow.ApplicationRepository.GetAll());
        }
        /// <summary>
        /// Will get all applications with a search string
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation. Returns <see
        /// cref="UnauthorizedResult"/> if the user is not authorized, or <see cref="OkObjectResult"/></returns>
        [HttpGet("{id:int}"), Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            // Retrieve userId from the claims
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid)?.Value;
            if (userIdClaim == null)
                return Unauthorized();
            request = new()
            {
                AdminId = Int32.Parse(userIdClaim),
                Site = site,
                Permission = Permission.Read
            };
            var hasAdminAcces = await _uow.AdminRepository.HasAdminAccess(request);
            if (!hasAdminAcces)
                return Unauthorized();
            return Ok(await _uow.ApplicationRepository.GetById(id));
        }
    }
}
