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
        private readonly IMainAlcoholService _mainAlcoholService;
        private readonly IDifficultyService _difficultyService;
        private readonly IMapper _mapper;

        public UserController(IUnitService unitService, IMainAlcoholService mainAlcoholService, IDifficultyService difficultyService, IMapper mapper)
        {
            _mapper = mapper;
            _unitService = unitService;
            _difficultyService = difficultyService;
            _mainAlcoholService = mainAlcoholService;
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

        [HttpGet]
        public IActionResult AddMainAlcohol()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddMainAlcohol(MainAlcoholModel model)
        {
            var mainAlcoholDto = _mapper.Map<MainAlcoholDto>(model);
            await _mainAlcoholService.AddMainAlcohol(mainAlcoholDto);

            return RedirectToAction(nameof(Admin));
        }


        [HttpGet]
        public IActionResult AddDifficulty()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddDifficulty(DifficultyModel model)
        {
            var difficultyDto = _mapper.Map<DifficultyDto>(model);
            await _difficultyService.AddDifficulty(difficultyDto);

            return RedirectToAction(nameof(Admin));
        }
    }
}
