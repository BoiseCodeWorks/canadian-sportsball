using System;
using System.Collections.Generic;
using System.Data;
using canadian_sportsball.Models;
using Dapper;

namespace canadian_sportsball.Repositories
{
    public class GamesRepository
    {
        private readonly IDbConnection _db;
        public GamesRepository(IDbConnection db)
        {
            _db = db;
        }

        public IEnumerable<Game> GetALL()
        {
            return _db.Query<Game>("SELECT * FROM games");
        }

        public Game GetById(int Id)
        {
            return _db.QueryFirstOrDefault<Game>("SELECT * FROM games WHERE id = @Id", new { Id });
        }

        public Game CreateGame(Game game)
        {
            try
            {
                int id = _db.ExecuteScalar<int>(@"
                INSERT INTO games (team1Id, team2Id)
                    VALUES (@Team1Id, @Team2Id);
                    SELECT LAST_INSERT_ID();
                ", game);
                game.Id = id;
                return game;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public Game EditGame(int id, Game editedGame)
        {
            try
            {
                string query = @"
                UPDATE games SET
                    team1Id = @editedGame.Team1Id,
                    team2Id = @editedGame.Team2Id
                WHERE id = @id;
                SELECT * FROM games WHERE id = @id;
                ";
                return _db.QueryFirstOrDefault<Game>(query, new { id, editedGame });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

        }

        internal IEnumerable<Player> GetPlayers(int id)
        {
            return _db.Query<Player>(@"
            SELECT * 
	            FROM players p
	            JOIN games g 
		            ON p.teamId = g.team1Id 
		            OR p.teamId = g.team2Id
	            WHERE g.id = @id
            ", new { id });
        }

        public bool Delete(int id)
        {
            int success = _db.Execute("DELETE FROM games WHERE id = @id", new { id });
            return success > 0;
        }
    }
}