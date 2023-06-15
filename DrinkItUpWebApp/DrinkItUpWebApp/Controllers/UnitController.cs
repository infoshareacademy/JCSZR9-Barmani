using AutoMapper;
using DrinkItUpBusinessLogic;
using DrinkItUpBusinessLogic.DTOs;
using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DrinkItUpWebApp.Controllers
{
    public class UnitController : Controller
    {
        private readonly IUnitService _unitService;
        private readonly IMapper _mapper;

        public UnitController(IUnitService unitService, IMapper mapper)
        {
            _unitService = unitService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var unitModels = new List<UnitModel>();
            var unitDtos = await _unitService.GetAll();
            foreach (var unitDto in unitDtos)
            {
                var unitModel = _mapper.Map<UnitModel>(unitDto);
                unitModel.IsUsed = await _unitService.IsUnitUsed(unitModel.UnitId);
                unitModels.Add(unitModel);
            }

            var unit = new UnitModel();
            unit.Units= unitModels;
            return View(unit);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UnitModel model)
        {
            if(!await _unitService.IsUnitUnique(model.Name))
                return RedirectToAction(nameof(Index));

            var unitDto = _mapper.Map<UnitDto>(model);
            await _unitService.AddUnit(unitDto);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if(id == 0)
            {
                return RedirectToAction(nameof(Index));
            }

            var unitDtoFromSubmit = await _unitService.GetById(id);
            if(unitDtoFromSubmit == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var unit = _mapper.Map<UnitModel>(unitDtoFromSubmit) ;


            var unitModels = new List<UnitModel>();
            var unitDtos = await _unitService.GetAll();
            foreach (var unitDto in unitDtos)
            {
                var unitModel = _mapper.Map<UnitModel>(unitDto);
                unitModel.IsUsed = await _unitService.IsUnitUsed(unitModel.UnitId);
                if (unitModel.UnitId == id)
                    unit.IsUsed = unitModel.IsUsed;

                unitModels.Add(unitModel);
            }



            unit.Units = unitModels;

            return View(unit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UnitModel model)
        {
            var unitDto = _mapper.Map<UnitDto>(model);
            if (!await _unitService.IsUnitUnique(unitDto.Name))
            {
                return RedirectToAction(nameof(Edit), new { id = unitDto.UnitId });
            }
            await _unitService.Update(unitDto);
            return RedirectToAction(nameof(Edit), new { id = unitDto.UnitId });

        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return RedirectToAction(nameof(Index));
            }

            var unitDtoFromSubmit = await _unitService.GetById(id);
            if (unitDtoFromSubmit == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var unit = _mapper.Map<UnitModel>(unitDtoFromSubmit);


            var unitModels = new List<UnitModel>();
            var unitDtos = await _unitService.GetAll();
            foreach (var unitDto in unitDtos)
            {
                var unitModel = _mapper.Map<UnitModel>(unitDto);
                unitModel.IsUsed = await _unitService.IsUnitUsed(unitModel.UnitId);
                if(unitModel.UnitId== id)
                    unit.IsUsed = unitModel.IsUsed;

                unitModels.Add(unitModel);
            }



            unit.Units = unitModels;

            return View(unit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, UnitModel model)
        {
            if (await _unitService.IsUnitUsed(id))
            {
                return RedirectToAction(nameof(Delete), new { id = id });
            }


            if(await _unitService.Remove(id))
                return RedirectToAction(nameof(Index));
            else
                return RedirectToAction(nameof(Delete), new { id = id });

        }


        public ActionResult Drink()
        { 
            return View();
        }
    }
}
