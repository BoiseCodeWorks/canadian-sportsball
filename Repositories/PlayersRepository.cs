using System;
using System.Collections.Generic;
using System.Data;
using canadian_sportsball.Models;
using Dapper;

namespace canadian_sportsball.Repositories
{
    public class PlayersRepository
    {
        private readonly IDbConnection _db;
        public PlayersRepository(IDbConnection db)
        {
            _db = db;
        }

        public IEnumerable<Player> GetALL()
        {
            return _db.Query<Player>("SELECT * FROM players");
        }

        public Player GetById(int Id)
        {
            return _db.QueryFirstOrDefault<Player>("SELECT * FROM players WHERE id = @Id", new { Id });
        }

        public Player CreatePlayer(Player playa)
        {
            try
            {
                int id = _db.ExecuteScalar<int>(@"
                INSERT INTO players (name, teamId)
                    VALUES (@Name, @TeamId);
                    SELECT LAST_INSERT_ID();
                ", playa);
                playa.Id = id;
                return playa;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public Player EditPlayer(int id, Player editedPlayer)
        {
            try
            {
                string query = @"
                UPDATE players SET
                    name = @editedPlayer.Name,
                    teamId = @editedPlayer.TeamId
                WHERE id = @id;
                SELECT * FROM players WHERE id = @id;
                ";
                return _db.QueryFirstOrDefault<Player>(query, new { id, editedPlayer });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

        }

        public bool Delete(int id)
        {
            int success = _db.Execute("DELETE FROM players WHERE id = @id", new { id });
            return success > 0;
        }
    }
}