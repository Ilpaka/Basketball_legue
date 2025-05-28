using System;
using System.Collections.Generic;
using BasketballLeague.Models;
using Microsoft.Data.Sqlite;

namespace BasketballLeague.DAL
{
    public class SqliteGameRepository : IGameRepository
    {
        private readonly string _connectionString;
        public SqliteGameRepository(string connectionString) => _connectionString = connectionString;

        public void Insert(Game game)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO game (team1id, team2id, date, score1, score2)
                                VALUES ($team1id, $team2id, $date, $score1, $score2);";
            cmd.Parameters.AddWithValue("$team1id", game.Team1Id);
            cmd.Parameters.AddWithValue("$team2id", game.Team2Id);
            cmd.Parameters.AddWithValue("$date", game.Date.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("$score1", game.Score1);
            cmd.Parameters.AddWithValue("$score2", game.Score2);
            cmd.ExecuteNonQuery();
        }

        public IEnumerable<Game> GetAll()
        {
            var list = new List<Game>();
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id, team1id, team2id, date, score1, score2 FROM game;";
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Game
                {
                    Id = reader.GetInt32(0),
                    Team1Id = reader.GetInt32(1),
                    Team2Id = reader.GetInt32(2),
                    Date = DateTime.Parse(reader.GetString(3)),
                    Score1 = reader.GetInt32(4),
                    Score2 = reader.GetInt32(5)
                });
            }
            return list;
        }

        public Game GetById(int id)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id, team1id, team2id, date, score1, score2 FROM game WHERE id = $id;";
            cmd.Parameters.AddWithValue("$id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Game
                {
                    Id = reader.GetInt32(0),
                    Team1Id = reader.GetInt32(1),
                    Team2Id = reader.GetInt32(2),
                    Date = DateTime.Parse(reader.GetString(3)),
                    Score1 = reader.GetInt32(4),
                    Score2 = reader.GetInt32(5)
                };
            }
            return null;
        }
    }
}
