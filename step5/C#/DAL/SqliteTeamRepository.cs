using System.Collections.Generic;
using BasketballLeague.Models;
using Microsoft.Data.Sqlite;

namespace BasketballLeague.DAL
{
    public class SqliteTeamRepository : ITeamRepository
    {
        private readonly string _connectionString;
        public SqliteTeamRepository(string connectionString) => _connectionString = connectionString;

        public void Insert(Team team)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO team (name, league) VALUES ($name, $league);";
            cmd.Parameters.AddWithValue("$name", team.Name);
            cmd.Parameters.AddWithValue("$league", team.League);
            cmd.ExecuteNonQuery();
        }

        public IEnumerable<Team> GetAll()
        {
            var list = new List<Team>();
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id, name, league FROM team;";
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Team
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    League = reader.GetString(2)
                });
            }
            return list;
        }

        public Team GetById(int id)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id, name, league FROM team WHERE id = $id;";
            cmd.Parameters.AddWithValue("$id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Team
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    League = reader.GetString(2)
                };
            }
            return null;
        }
    }
}
