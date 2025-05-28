using System;
using System.Collections.Generic;
using BasketballLeague.Models;
using Microsoft.Data.Sqlite;

namespace BasketballLeague.DAL
{
    public class SqliteCoachRepository : ICoachRepository
    {
        private readonly string _connectionString;
        public SqliteCoachRepository(string connectionString) => _connectionString = connectionString;

        public void Insert(Coach coach)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO coach (name, surname, birth_date, experienceyears, specialization, teamid)
                                VALUES ($name, $surname, $birth_date, $experience, $specialization, $teamid);";
            cmd.Parameters.AddWithValue("$name", coach.Name);
            cmd.Parameters.AddWithValue("$surname", coach.Surname);
            cmd.Parameters.AddWithValue("$birth_date", coach.BirthDate.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("$experience", coach.ExperienceYears);
            cmd.Parameters.AddWithValue("$specialization", coach.Specialization);
            cmd.Parameters.AddWithValue("$teamid", coach.TeamId);
            cmd.ExecuteNonQuery();
        }

        public IEnumerable<Coach> GetAll()
        {
            var list = new List<Coach>();
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id, name, surname, birth_date, experienceyears, specialization, teamid FROM coach;";
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Coach
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Surname = reader.GetString(2),
                    BirthDate = DateTime.Parse(reader.GetString(3)),
                    ExperienceYears = reader.GetInt32(4),
                    Specialization = reader.GetString(5),
                    TeamId = reader.GetInt32(6)
                });
            }
            return list;
        }

        public Coach GetById(int id)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id, name, surname, birth_date, experienceyears, specialization, teamid FROM coach WHERE id = $id;";
            cmd.Parameters.AddWithValue("$id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Coach
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Surname = reader.GetString(2),
                    BirthDate = DateTime.Parse(reader.GetString(3)),
                    ExperienceYears = reader.GetInt32(4),
                    Specialization = reader.GetString(5),
                    TeamId = reader.GetInt32(6)
                };
            }
            return null;
        }
    }
}
