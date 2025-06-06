﻿using CMDB.API.Interfaces;
using CMDB.Domain.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CMDB.API.Controllers
{
    /// <summary>
    /// Controller for managing identity accounts
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class IdenAccountController : ControllerBase
    {
        private IdenAccountController()
        {
        }
        private readonly IUnitOfWork _uow;
        /// <summary>
        /// Constructor for the IdenAccountController
        /// </summary>
        /// <param name="uow"></param>
        public IdenAccountController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        /// <summary>
        /// This will return all the identity accounts
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}"), Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            // Retrieve userId from the claims
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid)?.Value;
            if (userIdClaim == null)
                return Unauthorized();
            return Ok(await _uow.IdenAccountRepository.GetById(id));
        }
        /// <summary>
        /// This will check if the period is overlapping
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("IsPeriodOverlapping"), Authorize]
        public async Task<IActionResult> IsPeriodOverlapping(IsPeriodOverlappingRequest request)
        {
            // Retrieve userId from the claims
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid)?.Value;
            if (userIdClaim == null)
                return Unauthorized();
            return Ok(await _uow.IdenAccountRepository.IsPeriodOverlapping(request));
        }
    }
}
