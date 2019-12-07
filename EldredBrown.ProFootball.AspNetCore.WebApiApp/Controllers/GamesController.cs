using System;
using System.Threading.Tasks;
using AutoMapper;
using EldredBrown.ProFootball.AspNetCore.WebApiApp.Models;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace EldredBrown.ProFootball.AspNetCore.WebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameRepository _gameRepository;
        private readonly ISharedRepository _sharedRepository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public GamesController(IGameRepository gameRepository, ISharedRepository sharedRepository, IMapper mapper,
            LinkGenerator linkGenerator)
        {
            _gameRepository = gameRepository;
            _sharedRepository = sharedRepository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<ActionResult<GameModel[]>> Get()
        {
            try
            {
                var games = await _gameRepository.GetGames();

                return _mapper.Map<GameModel[]>(games);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameModel>> Get(int id)
        {
            try
            {
                var game = await _gameRepository.GetGame(id);
                if (game == null)
                {
                    return NotFound();
                }

                return _mapper.Map<GameModel>(game);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<GameModel>> Post(GameModel model)
        {
            try
            {
                var location = _linkGenerator.GetPathByAction("GetGame", "Games", new { id = -1 });
                if (string.IsNullOrWhiteSpace(location))
                {
                    return BadRequest("Could not use ID");
                }

                var game = _mapper.Map<Game>(model);

                await _gameRepository.Add(game);

                if (await _sharedRepository.SaveChanges() > 0)
                {
                    return Created(location, _mapper.Map<GameModel>(game));
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GameModel>> Put(int id, GameModel model)
        {
            try
            {
                var game = await _gameRepository.GetGame(id);
                if (game == null)
                {
                    return NotFound($"Could not find game with ID of {id}");
                }

                _mapper.Map(model, game);

                if (await _sharedRepository.SaveChanges() > 0)
                {
                    return _mapper.Map<GameModel>(game);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var game = await _gameRepository.GetGame(id);
                if (game == null)
                {
                    return NotFound($"Could not find game with ID of {id}");
                }

                await _gameRepository.Delete(id);

                if (await _sharedRepository.SaveChanges() > 0)
                {
                    return Ok();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }

            return BadRequest();
        }
    }
}
