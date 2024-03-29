﻿using AutoMapper;
using DrinkItUpBusinessLogic.DTOs;
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
                difficultyModel.IsUsed = await _difficultyService.IsDifficultyUsed(difficultyModel.DifficultyId);
                difficultiesModel.Add(difficultyModel);
            }
            var model = new DifficultyModel();
            model.Difficulties = difficultiesModel;

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return RedirectToAction(nameof(Index));
            }

            var difficultyDtoFromSubmit = await _difficultyService.GetById(id);
            if (difficultyDtoFromSubmit == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var difficulty = _mapper.Map<DifficultyModel>(difficultyDtoFromSubmit);


            var difficultyModels = new List<DifficultyModel>();
            var difficultyDtos = await _difficultyService.GetAll();
            foreach (var difficultyDto in difficultyDtos)
            {
                var difficultyModel = _mapper.Map<DifficultyModel>(difficultyDto);
                difficultyModel.IsUsed = await _difficultyService.IsDifficultyUsed(difficultyModel.DifficultyId);
                if (difficultyModel.DifficultyId == id)
                    difficulty.IsUsed = difficultyModel.IsUsed;

                difficultyModels.Add(difficultyModel);
            }



            difficulty.Difficulties = difficultyModels;

            return View(difficulty);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return RedirectToAction(nameof(Index));
            }

            var difficultyDtoFromSubmit = await _difficultyService.GetById(id);
            if (difficultyDtoFromSubmit == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var difficulty = _mapper.Map<DifficultyModel>(difficultyDtoFromSubmit);


            var difficultyModels = new List<DifficultyModel>();
            var difficultyDtos = await _difficultyService.GetAll();
            foreach (var difficultyDto in difficultyDtos)
            {
                var difficultyModel = _mapper.Map<DifficultyModel>(difficultyDto);
                difficultyModel.IsUsed = await _difficultyService.IsDifficultyUsed(difficultyModel.DifficultyId);
                if (difficultyModel.DifficultyId == id)
                    difficulty.IsUsed = difficultyModel.IsUsed;

                difficultyModels.Add(difficultyModel);
            }



            difficulty.Difficulties = difficultyModels;

            return View(difficulty);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DifficultyModel model)
        {
            if (!await _difficultyService.IsDifficultyUnique(model.Name))
                return RedirectToAction(nameof(Index));

            var difficultyDto = _mapper.Map<DifficultyDto>(model);
            await _difficultyService.AddDifficulty(difficultyDto);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, DifficultyModel model)
        {
            if (await _difficultyService.IsDifficultyUsed(id))
            {
                return RedirectToAction(nameof(Delete), new { id = id });
            }


            if (await _difficultyService.Remove(id))
                return RedirectToAction(nameof(Index));
            else
                return RedirectToAction(nameof(Delete), new { id = id });


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DifficultyModel model)
        {
            var difficultyDto = _mapper.Map<DifficultyDto>(model);
            if (!await _difficultyService.IsDifficultyUnique(difficultyDto.Name))
            {
                return RedirectToAction(nameof(Edit), new { id = difficultyDto.DifficultyId });
            }
            await _difficultyService.Update(difficultyDto);
            return RedirectToAction(nameof(Edit), new { id = difficultyDto.DifficultyId });

        }
    }

}
