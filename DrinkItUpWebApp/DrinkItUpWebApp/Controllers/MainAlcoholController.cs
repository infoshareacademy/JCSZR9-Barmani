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
            var mainAlcohol = _mapper.Map<MainAlcoholModel>(mainAlcoholDtoFromSubmit);


            var mainAlcoholModels = new List<MainAlcoholModel>();
            var mainAlcoholDtos = await _mainAlcoholService.GetAll();
            foreach (var mainAlcoholDto in mainAlcoholDtos)
            {
                var mainAlcoholModel = _mapper.Map<MainAlcoholModel>(mainAlcoholDto);
                mainAlcoholModel.IsUsed = await _mainAlcoholService.IsMainAlcoholUsed(mainAlcoholModel.MainAlcoholId);
                if (mainAlcoholModel.MainAlcoholId == id)
                    mainAlcohol.IsUsed = mainAlcoholModel.IsUsed;

                mainAlcoholModels.Add(mainAlcoholModel);
            }



            mainAlcohol.MainAlcohols = mainAlcoholModels;

            return View(mainAlcohol);
        }
    }
}
