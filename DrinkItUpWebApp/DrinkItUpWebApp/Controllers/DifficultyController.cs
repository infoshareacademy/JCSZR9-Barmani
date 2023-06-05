using AutoMapper;
using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DrinkItUpWebApp.Controllers
{
    public class DifficultyController : Controller
    {
        private readonly IDifficultyService _difficultyService;
        private readonly IMapper _mapper;
        private readonly IUnitService _unitService;

        public DifficultyController(IDifficultyService difficultyService, IMapper mapper)
        {
            _mapper = mapper;
            _difficultyService = difficultyService;


        }



        public async Task<IActionResult> Index()
        {
            var difficultiesDto = await _difficultyService.GetAll();
            var difficultiesModel = new List<DifficultyModel>();

            foreach (var difficulty in difficultiesDto)
            { 
                var difficultyModel = _mapper.Map<DifficultyModel>(difficulty);
                difficultiesModel.Add(difficultyModel);
            }
            var model = new DifficultyModel();
            model.Difficulties = difficultiesModel;

            return View(model);
        }
    }
}
