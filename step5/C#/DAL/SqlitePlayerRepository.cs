using System.Collections.Generic;
using BasketballLeague.Models;
using Microsoft.Data.Sqlite;
using System;

namespace BasketballLeague.DAL
{
    public class SqlitePlayerRepository : IPlayerRepository
    {
        private readonly string _connectionString;
        public SqlitePlayerRepository(string connectionString) => _connectionString = connectionString;

        public void Insert(Player player)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO player 
                (name, surname, birth_date, player_number, height, weight, position)
                VALUES ($name, $surname, $birth_date, $number, $height, $weight, $position);";
            cmd.Parameters.AddWithValue("$name", player.Name);
            cmd.Parameters.AddWithValue("$surname", player.Surname);
            cmd.Parameters.AddWithValue("$birth_date", player.BirthDate.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("$number", player.Number);
            cmd.Parameters.AddWithValue("$height", player.Height);
            cmd.Parameters.AddWithValue("$weight", player.Weight);
            cmd.Parameters.AddWithValue("$position", player.Position);
            cmd.ExecuteNonQuery();
        }

        public IEnumerable<Player> GetAll()
        {
            var list = new List<Player>();
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id, name, surname, birth_date, player_number, height, weight, position FROM player;";
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Player
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Surname = reader.GetString(2),
                    BirthDate = DateTime.Parse(reader.GetString(3)),
                    Number = reader.GetInt32(4),
                    Height = reader.GetFloat(5),
                    Weight = reader.GetFloat(6),
                    Position = reader.GetString(7)
                });
            }
            return list;
        }

        public Player GetById(int id)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id, name, surname, birth_date, player_number, height, weight, position FROM player WHERE id = $id;";
            cmd.Parameters.AddWithValue("$id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Player
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Surname = reader.GetString(2),
                    BirthDate = DateTime.Parse(reader.GetString(3)),
                    Number = reader.GetInt32(4),
                    Height = reader.GetFloat(5),
                    Weight = reader.GetFloat(6),
                    Position = reader.GetString(7)
                };
            }
            return null;
        }
        public Player GetByName(string name, string surname)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id, name, surname, birth_date, player_number, height, weight, position, teamid, points, assists, rebounds FROM player WHERE name = $name AND surname = $surname;";
            cmd.Parameters.AddWithValue("$name", name);
            cmd.Parameters.AddWithValue("$surname", surname);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Player
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Surname = reader.GetString(2),
                    BirthDate = DateTime.Parse(reader.GetString(3)),
                    Number = reader.GetInt32(4),
                    Height = reader.GetFloat(5),
                    Weight = reader.GetFloat(6),
                    Position = reader.GetString(7),
                    TeamId = reader.GetInt32(8),
                    Points = reader.GetInt32(9),
                    Assists = reader.GetInt32(10),
                    Rebounds = reader.GetInt32(11)
                };
            }
            return null;
        }
    }
}
