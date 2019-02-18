using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Upope.Identity.Entities;
using Upope.Loyalty.Services.Interfaces;
using Upope.Loyalty.ViewModels;

namespace Upope.Loyalty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoyaltyController : ControllerBase
    {
        private readonly ILoyaltyService _loyaltyService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public LoyaltyController(ILoyaltyService loyaltyService, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _loyaltyService = loyaltyService;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        [Route("GetPoint")]
        public async Task<IActionResult> GetPoint()
        {
            var user = await GetCurrentUserAsync();
            var point = _loyaltyService.GetPointByUserId(user.Id);
            var pointViewModel = _mapper.Map<GetPointViewModel>(point);

            return Ok(pointViewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetSufficientPoints(GetSufficientPointViewModel model)
        {
            var user = await GetCurrentUserAsync();
            var sufficientPoints = _loyaltyService.GetSufficientPoints(model.Point);
            var pointsViewModel = _mapper.Map<List<PointViewModel>>(sufficientPoints);

            return Ok(pointsViewModel);
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(User);
    }
}