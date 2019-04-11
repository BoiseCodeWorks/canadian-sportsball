using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using canadian_sportsball.Models;
using canadian_sportsball.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace canadian_sportsball.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly GamesRepository _gr;
        public GamesController(GamesRepository gr)
        {
            _gr = gr;
        }

        //GETALL
        [HttpGet]
        public ActionResult<IEnumerable<Game>> Get()
        {
            IEnumerable<Game> results = _gr.GetALL();
            if (results == null)
            {
                return BadRequest();
            }
            return Ok(results);
        }

        //GETBYID
        [HttpGet("{id}")]
        public ActionResult<Game> Get(int id)
        {
            Game found = _gr.GetById(id);
            if (found == null) { return BadRequest(); }
            return Ok(found);
        }

        //GetAllPlayersInGame
        [HttpGet("{id}/players")]
        public ActionResult<IEnumerable<Player>> GetPlayers(int id)
        {
            IEnumerable<Player> results = _gr.GetPlayers(id);
            return Ok(results);
        }

        //CREATE
        [HttpPost]
        public ActionResult<Game> Create([FromBody] Game game)
        {
            Game newGame = _gr.CreateGame(game);
            if (newGame == null) { return BadRequest("haters gonna hate"); }
            return Ok(newGame);
        }

        //EDIT
        [HttpPut("{id}")]
        public ActionResult<Game> Edit(int id, [FromBody] Game editedGame)
        {
            Game updatedGame = _gr.EditGame(id, editedGame);
            if (updatedGame == null) { return BadRequest("haters gonna hate"); }
            return Ok(updatedGame);
        }

        //DELETE
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            bool successful = _gr.Delete(id);
            if (!successful) { return BadRequest(); }
            return Ok();
        }

    }
}
