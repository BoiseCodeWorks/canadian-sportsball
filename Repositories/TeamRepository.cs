using System;
using System.Collections.Generic;
using System.Data;
using canadian_sportsball.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace canadian_sportsball.Repositories
{
    public class TeamsRepository
    {
        private readonly IDbConnection _db;
        public TeamsRepository(IDbConnection db)
        {
            _db = db;
        }

        public IEnumerable<Team> GetALL()
        {
            return _db.Query<Team>("SELECT * FROM teams");
        }

        public Team GetById(int Id)
        {
            return _db.QueryFirstOrDefault<Team>("SELECT * FROM teams WHERE id = @Id", new { Id });
        }

        public Team CreateTeam(Team team)
        {
            try
            {
                int id = _db.ExecuteScalar<int>(@"
                INSERT INTO teams (name, mascot)
                    VALUES (@Name, @Mascot);
                    SELECT LAST_INSERT_ID();
                ", team);
                team.Id = id;
                return team;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public Team EditTeam(Team editedTeam)
        {
            try
            {
                string query = @"
                UPDATE teams SET
                    name = @Name,
                    mascot = @Mascot
                WHERE id = @id;
                SELECT * FROM teams WHERE id = @id;
                ";
                return _db.QueryFirstOrDefault<Team>(query, editedTeam );
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

        }

        public IEnumerable<Player> GetPlayers(int id)
        {
            return _db.Query<Player>("SELECT * FROM players WHERE teamId = @id", new { id });
        }

        public bool Delete(int id)
        {
            int success = _db.Execute("DELETE FROM teams WHERE id = @id", new { id });
            return success > 0;
        }
    }
}
