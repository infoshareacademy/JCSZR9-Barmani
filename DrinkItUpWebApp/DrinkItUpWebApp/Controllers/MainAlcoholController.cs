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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return RedirectToAction(nameof(Index));
            }

            var mainAlcoholDtoFromSubmit = await _mainAlcoholService.GetById(id);
            if (mainAlcoholDtoFromSubmit == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var mainAlcohol = _mapper.Map<UnitModel>(mainAlcoholDtoFromSubmit);


            var unitModels = new List<UnitModel>();
            var unitDtos = await _unitService.GetAll();
            foreach (var unitDto in unitDtos)
            {
                var unitModel = _mapper.Map<UnitModel>(unitDto);
                unitModel.IsUsed = await _unitService.IsUnitUsed(unitModel.UnitId);
                if (unitModel.UnitId == id)
                    mainAlcohol.IsUsed = unitModel.IsUsed;

                unitModels.Add(unitModel);
            }



            mainAlcohol.Units = unitModels;

            return View(mainAlcohol);
        }
    }
}
