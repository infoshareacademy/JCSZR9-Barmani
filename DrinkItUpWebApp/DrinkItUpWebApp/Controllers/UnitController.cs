using AutoMapper;
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

        public async Task<IActionResult> Index()
        {
            var unitModels = new List<UnitModel>();
            var unitDtos = await _unitService.GetAll();
            foreach (var unitDto in unitDtos)
            {
                var unitModel = _mapper.Map<UnitModel>(unitDto);
                unitModel.IsUsed = await _unitService.UnitIsUsed(unitModel.UnitId);
                unitModels.Add(unitModel);
            }

            var unit = new UnitModel();
            unit.Units= unitModels;
            return View(unit);
        }
    }
}
