﻿using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using ThesisRestaurant.Application.Common.Services;
using ThesisRestaurant.Application.Foods.Commands.Create;
using ThesisRestaurant.Application.Foods.Commands.Delete;
using ThesisRestaurant.Application.Foods.Commands.DeleteDiscount;
using ThesisRestaurant.Application.Foods.Commands.DeleteFile;
using ThesisRestaurant.Application.Foods.Commands.Discount;
using ThesisRestaurant.Application.Foods.Commands.Update;
using ThesisRestaurant.Application.Foods.Commands.UploadFile;
using ThesisRestaurant.Application.Foods.Queries.GetAll;
using ThesisRestaurant.Application.Foods.Queries.GetById;
using ThesisRestaurant.Contracts.Food;

namespace ThesisRestaurant.Api.Controllers
{
    [Route("food")]
    [Authorize]
    public class FoodController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly ISender _meaditor;
        private readonly IUserService _userService;

        public FoodController(IMapper mapper, ISender meaditor, IUserService userService)
        {
            _mapper = mapper;
            _meaditor = meaditor;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFoods()
        {
            //var userName = _userService.GetMyName();
            //return Ok(userName);

            var query = new GetAllFoodQuery();
            var result = await _meaditor.Send(query);
            return result.Match(
                    foods => Ok(foods.Adapt<FoodResponse[]>()),
                    errors => Problem(errors)
                );
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetFoodById(int id)
        {
            var query = new GetFoodByIdQuery(id);
            var result = await _meaditor.Send(query);
            return result.Match(
               food => Ok(_mapper.Map<FoodResponse>(food)),
               errors => Problem(errors)
           );
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> CreateFood(CreateFoodRequest request)
        {
            var command = _mapper.Map<CreateFoodCommand>(request);
            var result = await _meaditor.Send(command);
            return result.Match(
                    food => Ok(_mapper.Map<FoodResponse>(food)),
                    errors => Problem(errors)
                );
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> EditFood(CreateFoodRequest request, int id)
        {
            var command = _mapper.Map<UpdateFoodCommand>((request, id));
            var result = await _meaditor.Send(command);
            return result.Match(
                    food => Ok(_mapper.Map<FoodResponse>(food)),
                    errors => Problem(errors)
                );
        }

        [HttpPut("discount/{id:int}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> AddDiscount(DiscountRequest request, int id)
        {

            var command = _mapper.Map<AddDiscountCommand>((request, id));
            var result = await _meaditor.Send(command);
            return result.Match(
                    food => Ok(_mapper.Map<FoodResponse>(food)),
                    errors => Problem(errors)
                );
        }

        [HttpDelete("discount={id:int}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> DeleteDiscount(int id)
        {
            var command = new DeleteDiscountCommad(id);
            var result = await _meaditor.Send(command);
            return result.Match(
                    deleted => NoContent(),
                    errors => Problem(errors)
                );
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> DeleteFood(int id)
        {
            var command = new DeleteFoodCommand(id);
            var result = await _meaditor.Send(command);
            return result.Match(
                    deleted => Ok(NoContent()),
                    errors => Problem(errors)
                );
        }

        [HttpPost("file/{id:int}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> UploadFile(IFormFile file, int id)
        {
            var command = new UploadFileCommand(id, file);
            var result = await _meaditor.Send(command);
            return result.Match(
                    uploaded => Ok(NoContent()),
                    errors => Problem(errors)
                );
        }

        [HttpDelete("file/{id:int}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            var command = new DeleteFileCommand(id);
            var result = await _meaditor.Send(command);
            return result.Match(
                    uploaded => Ok(NoContent()),
                    errors => Problem(errors)
                );
        }
    }
}
