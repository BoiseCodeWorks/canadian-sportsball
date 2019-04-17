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
    public class PlayersController : ControllerBase
    {
        private readonly PlayersRepository _pr;
        public PlayersController(PlayersRepository pr)
        {
            _pr = pr;
        }

        //GETALL
        [HttpGet]
        public ActionResult<IEnumerable<Player>> Get()
        {
            IEnumerable<Player> results = _pr.GetALL();
            if (results == null)
            {
                return BadRequest();
            }
            return Ok(results);
        }

        //GETBYID
        [HttpGet("{id}")]
        public ActionResult<Player> Get(int id)
        {
            Player found = _pr.GetById(id);
            if (found == null) { return BadRequest(); }
            return Ok(found);
        }

        //CREATE
        [HttpPost]
        public ActionResult<Player> Create([FromBody] Player playa)
        {
            Player newPlaya = _pr.CreatePlayer(playa);
            if (newPlaya == null) { return BadRequest("haters gonna hate"); }
            return Ok(newPlaya);
        }

        //EDIT
        [HttpPut("{id}")]
        public ActionResult<Player> Edit(int id, [FromBody] Player editedPlayer)
        {
            editedPlayer.Id = id;
            Player updatedPlayer = _pr.EditPlayer(id, editedPlayer);
            if (updatedPlayer == null) { return BadRequest("haters gonna hate"); }
            return Ok(updatedPlayer);
        }

        //DELETE
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            bool successful = _pr.Delete(id);
            if (!successful) { return BadRequest(); }
            return Ok();
        }

    }
}
