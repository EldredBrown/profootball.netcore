﻿using System;
using AutoMapper;
using EldredBrown.ProFootball.AspNetCore.WebApiApp.Models;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace EldredBrown.ProFootball.AspNetCore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeasonStandingsController : ControllerBase
    {
        private readonly ISeasonStandingsRepository _seasonStandingsRepository;
        private readonly IMapper _mapper;

        public SeasonStandingsController(ISeasonStandingsRepository seasonStandingsRepository, IMapper mapper)
        {
            _seasonStandingsRepository = seasonStandingsRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<SeasonStandingsModel[]> Get()
        {
            try
            {
                var seasonTeams = _seasonStandingsRepository.GetSeasonStandings();

                return _mapper.Map<SeasonStandingsModel[]>(seasonTeams);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
    }
}