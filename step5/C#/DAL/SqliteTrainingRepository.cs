using System;
using System.Collections.Generic;
using BasketballLeague.Models;
using Microsoft.Data.Sqlite;

namespace BasketballLeague.DAL
{
    public class SqliteTrainingRepository : ITrainingRepository
    {
        private readonly string _connectionString;
        public SqliteTrainingRepository(string connectionString) => _connectionString = connectionString;

        public void Insert(Training training)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO training (teamid, date, place, description)
                                VALUES ($teamid, $date, $place, $description);";
            cmd.Parameters.AddWithValue("$teamid", training.TeamId);
            cmd.Parameters.AddWithValue("$date", training.Date.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("$place", training.Place);
            cmd.Parameters.AddWithValue("$description", training.Description);
            cmd.ExecuteNonQuery();
        }

        public IEnumerable<Training> GetAll()
        {
            var list = new List<Training>();
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id, teamid, date, place, description FROM training;";
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Training
                {
                    Id = reader.GetInt32(0),
                    TeamId = reader.GetInt32(1),
                    Date = DateTime.Parse(reader.GetString(2)),
                    Place = reader.GetString(3),
                    Description = reader.GetString(4)
                });
            }
            return list;
        }

        public Training GetById(int id)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id, teamid, date, place, description FROM training WHERE id = $id;";
            cmd.Parameters.AddWithValue("$id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Training
                {
                    Id = reader.GetInt32(0),
                    TeamId = reader.GetInt32(1),
                    Date = DateTime.Parse(reader.GetString(2)),
                    Place = reader.GetString(3),
                    Description = reader.GetString(4)
                };
            }
            return null;
        }
    }
}
