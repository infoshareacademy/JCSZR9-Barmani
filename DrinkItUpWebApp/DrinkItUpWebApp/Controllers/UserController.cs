using AutoMapper;
using DrinkItUpBusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;


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

    }
}
