using AutoMapper;
using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DrinkItUpWebApp.Controllers
{
    public class MainAlcoholController : Controller
    {
        private readonly IMainAlcoholService _mainAlcoholService;
        private readonly IMapper _mapper;
        private readonly IUnitService _unitService;

        public MainAlcoholController(IMainAlcoholService mainAlcoholService, IMapper mapper)
        {
            _mapper = mapper;
            _mainAlcoholService = mainAlcoholService;
        }

        public async Task<IActionResult> Index()
        {
            var mainAlcoholsDto = await _mainAlcoholService.GetAll();
            var mainAlcoholsModel = new List<MainAlcoholModel>();
            
            foreach(var mainAlcohol in  mainAlcoholsDto)
            {
                var mainAlcoholModel = _mapper.Map<MainAlcoholModel>(mainAlcohol);
                mainAlcoholsModel.Add(mainAlcoholModel);
            }
            var model = new MainAlcoholModel();
            model.MainAlcohols = mainAlcoholsModel;

            return View(model);
        }
    }
}
