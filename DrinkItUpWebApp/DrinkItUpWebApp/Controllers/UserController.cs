using AutoMapper;
using DrinkItUpBusinessLogic.DTOs;
using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace DrinkItUpWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUnitService _unitService;
        private readonly IMapper _mapper;

        public UserController(IUnitService unitService, IMapper mapper)
        {
            _mapper = mapper;
            _unitService = unitService;
        }

        // GET: Home
        public IActionResult Index()
        {
                return View();
        }

        public IActionResult Admin()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddUnit()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUnit(UnitModel model)
        {
            var unitDto = _mapper.Map<UnitDto>(model);
            await _unitService.AddUnit(unitDto);

            return RedirectToAction(nameof(Admin));
        }

    }
}
