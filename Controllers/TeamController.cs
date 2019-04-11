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
    public class TeamsController : ControllerBase
    {
        private readonly TeamsRepository _tr;
        public TeamsController(TeamsRepository tr)
        {
            _tr = tr;
        }

        //GETALL
        [HttpGet]
        public ActionResult<IEnumerable<Team>> Get()
        {
            IEnumerable<Team> results = _tr.GetALL();
            if (results == null)
            {
                return BadRequest();
            }
            return Ok(results);
        }

        //GETBYID
        [HttpGet("{id}")]
        public ActionResult<Team> Get(int id)
        {
            Team found = _tr.GetById(id);
            if (found == null) { return BadRequest(); }
            return Ok(found);
        }

        //GetPlayersByTeamId
        [HttpGet("{id}/players")]
        public ActionResult<IEnumerable<Player>> GetPlayers(int id)
        {
            return Ok(_tr.GetPlayers(id));
        }

        //CREATE
        [HttpPost]
        public ActionResult<Team> Create([FromBody] Team playa)
        {
            Team newPlaya = _tr.CreateTeam(playa);
            if (newPlaya == null) { return BadRequest("haters gonna hate"); }
            return Ok(newPlaya);
        }

        //EDIT
        [HttpPut("{id}")]
        public ActionResult<Team> Edit(int id, [FromBody] Team editedTeam)
        {
            Team updatedTeam = _tr.EditTeam(id, editedTeam);
            if (updatedTeam == null) { return BadRequest("haters gonna hate"); }
            return Ok(updatedTeam);
        }

        //DELETE
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            bool successful = _tr.Delete(id);
            if (!successful) { return BadRequest(); }
            return Ok();
        }

    }
}
