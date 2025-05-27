using System;
using Microsoft.Data.Sqlite;

namespace BasketballLeague
{
    class Program
    {
        static string connectionString = "Data Source=basketball_legue.db;";

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Меню баскетбольной лиги ===");
                Console.WriteLine("1. Добавить команду");
                Console.WriteLine("2. Добавить игрока");
                Console.WriteLine("3. Добавить тренера");
                Console.WriteLine("4. Добавить игру");
                Console.WriteLine("5. Добавить тренировку");
                Console.WriteLine("6. Просмотреть все команды");
                Console.WriteLine("7. Просмотреть всех игроков");
                Console.WriteLine("8. Просмотреть всех тренеров");
                Console.WriteLine("9. Просмотреть все игры");
                Console.WriteLine("10. Просмотреть все тренировки");
                Console.WriteLine("11. Просмотреть статистику игрока");
                Console.WriteLine("12. Выход");
                Console.Write("Выберите пункт меню: ");
                string input = Console.ReadLine();

                Console.Clear();

                switch (input)
                {
                    case "1": AddTeam(); break;
                    case "2": AddPlayer(); break;
                    case "3": AddCoach(); break;
                    case "4": AddGame(); break;
                    case "5": AddTrainingSession(); break;
                    case "6": ViewAllTeams(); break;
                    case "7": ViewAllPlayers(); break;
                    case "8": ViewAllCoaches(); break;
                    case "9": ViewAllGames(); break;
                    case "10": ViewAllTrainingSessions(); break;
                    case "11": ViewPlayerStats(); break;
                    case "12":
                        Console.WriteLine("Выход из программы.");
                        return;
                    default:
                        Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                        break;
                }

                Console.WriteLine("\nНажмите Enter для продолжения...");
                Console.ReadLine();
            }
        }

        static void AddTeam()
        {
            Console.Write("Введите название команды: ");
            string name = Console.ReadLine();
            Console.Write("Введите лигу: ");
            string league = Console.ReadLine();

            using (var conn = new SqliteConnection(connectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO team (name, league) VALUES ($name, $league);";
                cmd.Parameters.AddWithValue("$name", name);
                cmd.Parameters.AddWithValue("$league", league);

                try
                {
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Команда успешно добавлена!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка при добавлении команды: " + ex.Message);
                }
            }
        }

        static void AddPlayer()
        {
            try
            {
                var teams = new System.Collections.Generic.List<(int Id, string Name, string League)>();
                using (var conn = new SqliteConnection(connectionString))
                {
                    conn.Open();
                    var cmd = conn.CreateCommand();
                    cmd.CommandText = "SELECT id, name, league FROM team;";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            teams.Add((reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                        }
                    }
                }

                if (teams.Count == 0)
                {
                    Console.WriteLine("Сначала создайте хотя бы одну команду!");
                    return;
                }

                Console.WriteLine("Выберите команду для игрока:");
                for (int i = 0; i < teams.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {teams[i].Name} ({teams[i].League}) [ID: {teams[i].Id}]");
                }

                int teamIndex = -1;
                while (true)
                {
                    Console.Write("Введите номер команды: ");
                    string input = Console.ReadLine();
                    if (int.TryParse(input, out teamIndex) && teamIndex >= 1 && teamIndex <= teams.Count)
                    {
                        teamIndex--;
                        break;
                    }
                    Console.WriteLine("Некорректный номер. Попробуйте снова.");
                }
                int teamId = teams[teamIndex].Id;

                Console.Write("Имя игрока: ");
                string name = Console.ReadLine();

                Console.Write("Фамилия игрока: ");
                string surname = Console.ReadLine();

                Console.Write("Дата рождения (ГГГГ-ММ-ДД): ");
                string birthDate = Console.ReadLine();

                Console.Write("Номер игрока: ");
                int number = int.Parse(Console.ReadLine());

                Console.Write("Рост (см): ");
                float height = float.Parse(Console.ReadLine());

                Console.Write("Вес (кг): ");
                float weight = float.Parse(Console.ReadLine());

                Console.Write("Позиция: ");
                string position = Console.ReadLine();

                using (var conn = new SqliteConnection(connectionString))
                {
                    conn.Open();
                    var cmd = conn.CreateCommand();
                    cmd.CommandText = @"INSERT INTO player 
                        (name, surname, birth_date, player_number, height, weight, position)
                        VALUES ($name, $surname, $birth_date, $number, $height, $weight, $position);";
                    cmd.Parameters.AddWithValue("$name", name);
                    cmd.Parameters.AddWithValue("$surname", surname);
                    cmd.Parameters.AddWithValue("$birth_date", birthDate);
                    cmd.Parameters.AddWithValue("$number", number);
                    cmd.Parameters.AddWithValue("$height", height);
                    cmd.Parameters.AddWithValue("$weight", weight);
                    cmd.Parameters.AddWithValue("$position", position);

                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "SELECT last_insert_rowid();";
                    int playerId = Convert.ToInt32(cmd.ExecuteScalar());

                    Console.WriteLine("Игрок успешно добавлен в команду " + teams[teamIndex].Name + "!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка ввода: " + ex.Message);
            }
        }

        static void AddCoach()
        {
            try
            {
                Console.Write("ID игрока (coach связан с player): ");
                int playerId = int.Parse(Console.ReadLine());
                Console.Write("Роль тренера: ");
                string role = Console.ReadLine();

                using (var conn = new SqliteConnection(connectionString))
                {
                    conn.Open();
                    var cmd = conn.CreateCommand();
                    cmd.CommandText = @"INSERT INTO coach (player_id, role) VALUES ($playerId, $role);";
                    cmd.Parameters.AddWithValue("$playerId", playerId);
                    cmd.Parameters.AddWithValue("$role", role);

                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Тренер успешно добавлен!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при добавлении тренера: " + ex.Message);
            }
        }

        static void AddGame()
        {
            try
            {
                Console.Write("ID команды: ");
                int teamId = int.Parse(Console.ReadLine());
                Console.Write("Дата игры (ГГГГ-ММ-ДД): ");
                string date = Console.ReadLine();
                Console.Write("Соперники: ");
                string opponents = Console.ReadLine();
                Console.Write("Результат (например, Win/Loss): ");
                string result = Console.ReadLine();

                using (var conn = new SqliteConnection(connectionString))
                {
                    conn.Open();
                    var cmd = conn.CreateCommand();
                    cmd.CommandText = @"INSERT INTO game (team_id, date, opponents, result)
                        VALUES ($teamId, $date, $opponents, $result);";
                    cmd.Parameters.AddWithValue("$teamId", teamId);
                    cmd.Parameters.AddWithValue("$date", date);
                    cmd.Parameters.AddWithValue("$opponents", opponents);
                    cmd.Parameters.AddWithValue("$result", result);

                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Игра успешно добавлена!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при добавлении игры: " + ex.Message);
            }
        }

        static void AddTrainingSession()
        {
            try
            {
                Console.Write("ID команды: ");
                int teamId = int.Parse(Console.ReadLine());
                Console.Write("Дата и время тренировки (ГГГГ-ММ-ДД ЧЧ:ММ:СС): ");
                string datetime = Console.ReadLine();
                Console.Write("Место проведения: ");
                string location = Console.ReadLine();

                using (var conn = new SqliteConnection(connectionString))
                {
                    conn.Open();
                    var cmd = conn.CreateCommand();
                    cmd.CommandText = @"INSERT INTO training_session (team_id, datetime, location)
                        VALUES ($teamId, $datetime, $location);";
                    cmd.Parameters.AddWithValue("$teamId", teamId);
                    cmd.Parameters.AddWithValue("$datetime", datetime);
                    cmd.Parameters.AddWithValue("$location", location);

                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Тренировка успешно добавлена!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при добавлении тренировки: " + ex.Message);
            }
        }

        static void ViewAllTeams()
        {
            using (var conn = new SqliteConnection(connectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT id, name, league FROM team;";
                using (var reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("Список всех команд:");
                    bool hasRows = false;
                    while (reader.Read())
                    {
                        hasRows = true;
                        Console.WriteLine($"ID: {reader.GetInt32(0)}\nНазвание: {reader.GetString(1)}\nЛига: {reader.GetString(2)}\n");
                    }
                    if (!hasRows)
                        Console.WriteLine("Нет команд в базе данных.");
                }
            }
        }

        static void ViewAllPlayers()
        {
            using (var conn = new SqliteConnection(connectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT id, name, surname, player_number, birth_date, height, weight, position FROM player;";
                using (var reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("Список всех игроков:");
                    bool hasRows = false;
                    while (reader.Read())
                    {
                        hasRows = true;
                        Console.WriteLine(
                            $"ID: {reader.GetInt32(0)}\nИмя: {reader.GetString(1)} {reader.GetString(2)}\nНомер: {reader.GetInt32(3)}\nДата рождения: {reader.GetString(4)}\nРост: {reader.GetFloat(5)} см\nВес: {reader.GetFloat(6)} кг\nПозиция: {reader.GetString(7)}\n"
                        );
                    }
                    if (!hasRows)
                        Console.WriteLine("Нет игроков в базе данных.");
                }
            }
        }

        static void ViewAllCoaches()
        {
            using (var conn = new SqliteConnection(connectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = @"SELECT coach.id, player.name, player.surname, coach.role 
                                    FROM coach 
                                    JOIN player ON coach.player_id = player.id;";
                using (var reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("Список всех тренеров:");
                    bool hasRows = false;
                    while (reader.Read())
                    {
                        hasRows = true;
                        Console.WriteLine(
                            $"ID: {reader.GetInt32(0)}\nИмя: {reader.GetString(1)} {reader.GetString(2)}\nРоль: {reader.GetString(3)}\n"
                        );
                    }
                    if (!hasRows)
                        Console.WriteLine("Нет тренеров в базе данных.");
                }
            }
        }

        static void ViewAllGames()
        {
            using (var conn = new SqliteConnection(connectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT id, team_id, date, opponents, result FROM game;";
                using (var reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("Список всех игр:");
                    bool hasRows = false;
                    while (reader.Read())
                    {
                        hasRows = true;
                        Console.WriteLine(
                            $"ID: {reader.GetInt32(0)}\nID команды: {reader.GetInt32(1)}\nДата: {reader.GetString(2)}\nСоперники: {reader.GetString(3)}\nРезультат: {reader.GetString(4)}\n"
                        );
                    }
                    if (!hasRows)
                        Console.WriteLine("Нет игр в базе данных.");
                }
            }
        }

        static void ViewAllTrainingSessions()
        {
            using (var conn = new SqliteConnection(connectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT id, team_id, datetime, location FROM training_session;";
                using (var reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("Список всех тренировок:");
                    bool hasRows = false;
                    while (reader.Read())
                    {
                        hasRows = true;
                        Console.WriteLine(
                            $"ID: {reader.GetInt32(0)}\nID команды: {reader.GetInt32(1)}\nДата и время: {reader.GetString(2)}\nМесто: {reader.GetString(3)}\n"
                        );
                    }
                    if (!hasRows)
                        Console.WriteLine("Нет тренировок в базе данных.");
                }
            }
        }

        static void ViewPlayerStats()
        {
            Console.Write("Введите ID игрока для просмотра статистики: ");
            if (!int.TryParse(Console.ReadLine(), out int playerId))
            {
                Console.WriteLine("Некорректный ID.");
                return;
            }

            using (var conn = new SqliteConnection(connectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = @"SELECT player.name, player.surname, stats_record.points, stats_record.rebounds, 
                                           stats_record.assists, stats_record.blocks, stats_record.steals
                                    FROM stats_record
                                    JOIN player ON stats_record.player_id = player.id
                                    WHERE player.id = $playerId;";
                cmd.Parameters.AddWithValue("$playerId", playerId);
                using (var reader = cmd.ExecuteReader())
                {
                    bool hasRows = false;
                    while (reader.Read())
                    {
                        hasRows = true;
                        Console.WriteLine($"Игрок: {reader.GetString(0)} {reader.GetString(1)}");
                        Console.WriteLine($"Очки: {reader.GetInt32(2)}, Подборы: {reader.GetInt32(3)}, Передачи: {reader.GetInt32(4)}, Блоки: {reader.GetInt32(5)}, Перехваты: {reader.GetInt32(6)}");
                    }
                    if (!hasRows)
                        Console.WriteLine("Нет статистики для этого игрока.");
                }
            }
        }
    }
}
