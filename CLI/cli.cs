using System;
using System.Collections.Generic;

namespace BasketballLeague
{
    class Program
    {
        static List<Team> teams = new List<Team>();
        static List<Player> players = new List<Player>();
        static List<Coach> coaches = new List<Coach>();
        static List<Game> games = new List<Game>();
        static List<TrainingSession> trainingSessions = new List<TrainingSession>();

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

                string input = "";
                while (string.IsNullOrWhiteSpace(input))
                {
                    input = Console.ReadLine();
                }

                Console.Clear();

                switch (input)
                {
                    case "1":
                        AddTeam();
                        break;
                    case "2":
                        AddPlayer();
                        break;
                    case "3":
                        AddCoach();
                        break;
                    case "4":
                        AddGame();
                        break;
                    case "5":
                        AddTrainingSession();
                        break;
                    case "6":
                        ViewAllTeams();
                        break;
                    case "7":
                        ViewAllPlayers();
                        break;
                    case "8":
                        ViewAllCoaches();
                        break;
                    case "9":
                        ViewAllGames();
                        break;
                    case "10":
                        ViewAllTrainingSessions();
                        break;
                    case "11":
                        ViewPlayerStats();
                        break;
                    case "12":
                        Console.WriteLine("Выход из программы.");
                        return;
                    default:
                        Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                        break;
                }

                Console.WriteLine("\nНажмите Enter для продолжения...");
                while (Console.ReadLine() != "") { }
            }
        }

        static void AddTeam()
        {
            Console.Write("Введите название команды: ");
            string name = Console.ReadLine();

            Console.Write("Введите лигу: ");
            string league = Console.ReadLine();

            var exists = teams.Exists(t => t.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && t.League.Equals(league, StringComparison.OrdinalIgnoreCase));
            if (exists)
            {
                Console.WriteLine("Такая команда уже существует!");
                return;
            }

            Team team = new Team
            {
                Id = Guid.NewGuid(),
                Name = name,
                League = league,
                Roster = new Roster { Season = "2025", Players = new List<Player>() }
            };

            teams.Add(team);
            Console.WriteLine("Команда успешно добавлена!");
        }

        static void AddPlayer()
        {
            try
            {
                Console.Write("Имя игрока: ");
                string name = Console.ReadLine();

                Console.Write("Фамилия игрока: ");
                string surname = Console.ReadLine();

                Console.Write("Номер игрока: ");
                int number = int.Parse(Console.ReadLine());

                var exists = players.Exists(p =>
                    p.Name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
                    p.Surname.Equals(surname, StringComparison.OrdinalIgnoreCase) &&
                    p.PlayerNumber == number
                );
                if (exists)
                {
                    Console.WriteLine("Игрок с такими данными уже существует!");
                    return;
                }

                Console.Write("Дата рождения (ГГГГ-ММ-ДД): ");
                DateTime birthDate = DateTime.Parse(Console.ReadLine());

                Console.Write("Рост (см): ");
                float height = float.Parse(Console.ReadLine());

                Console.Write("Вес (кг): ");
                float weight = float.Parse(Console.ReadLine());

                Console.Write("Позиция: ");
                string position = Console.ReadLine();

                Player player = new Player
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Surname = surname,
                    PlayerNumber = number,
                    BirthDate = birthDate,
                    Height = height,
                    Weight = weight,
                    Position = position
                };

                players.Add(player);
                Console.WriteLine("Игрок успешно добавлен!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка ввода: " + ex.Message);
            }
        }

        static void AddCoach()
        {
            Console.Write("Введите роль тренера: ");
            string role = Console.ReadLine();

            var exists = coaches.Exists(c => c.Role.Equals(role, StringComparison.OrdinalIgnoreCase));
            if (exists)
            {
                Console.WriteLine("Тренер с такой ролью уже существует!");
                return;
            }

            Coach coach = new Coach { Role = role };
            coaches.Add(coach);

            Console.WriteLine("Тренер успешно добавлен!");
        }

        static void AddGame()
        {
            try
            {
                Console.Write("Дата игры (ГГГГ-ММ-ДД): ");
                DateTime date = DateTime.Parse(Console.ReadLine());

                Console.Write("Соперники: ");
                string opponents = Console.ReadLine();

                var exists = games.Exists(g => g.Date.Date == date.Date && g.Opponents.Equals(opponents, StringComparison.OrdinalIgnoreCase));
                if (exists)
                {
                    Console.WriteLine("Игра с такими параметрами уже существует!");
                    return;
                }

                Console.Write("Результат (например, Win/Loss): ");
                string result = Console.ReadLine();

                Game game = new Game
                {
                    Id = Guid.NewGuid(),
                    Date = date,
                    Opponents = opponents,
                    Result = result,
                    StatsRecords = new List<StatsRecord>()
                };

                games.Add(game);
                Console.WriteLine("Игра успешно добавлена!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка ввода: " + ex.Message);
            }
        }

        static void AddTrainingSession()
        {
            try
            {
                Console.Write("Дата и время тренировки (ГГГГ-ММ-ДД ЧЧ:ММ): ");
                DateTime dateTime = DateTime.Parse(Console.ReadLine());

                Console.Write("Место проведения: ");
                string location = Console.ReadLine();

                var exists = trainingSessions.Exists(ts => ts.DateTime == dateTime && ts.Location.Equals(location, StringComparison.OrdinalIgnoreCase));
                if (exists)
                {
                    Console.WriteLine("Тренировка с такими параметрами уже существует!");
                    return;
                }

                TrainingSession session = new TrainingSession
                {
                    Id = Guid.NewGuid(),
                    DateTime = dateTime,
                    Location = location
                };

                trainingSessions.Add(session);
                Console.WriteLine("Тренировка успешно добавлена!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка ввода: " + ex.Message);
            }
        }

        static void ViewAllTeams()
        {
            Console.WriteLine("Список всех команд:");
            if (teams.Count == 0)
            {
                Console.WriteLine("Нет команд в базе данных.");
                return;
            }
            foreach (var team in teams)
            {
                Console.WriteLine($"ID: {team.Id}\nНазвание: {team.Name}\nЛига: {team.League}\n");
            }
        }

        static void ViewAllPlayers()
        {
            Console.WriteLine("Список всех игроков:");
            if (players.Count == 0)
            {
                Console.WriteLine("Нет игроков в базе данных.");
                return;
            }
            foreach (var player in players)
            {
                Console.WriteLine($"ID: {player.Id}\nИмя: {player.Name} {player.Surname}\nНомер: {player.PlayerNumber}\nДата рождения: {player.BirthDate.ToShortDateString()}\nРост: {player.Height} см\nВес: {player.Weight} кг\nПозиция: {player.Position}\n");
            }
        }

        static void ViewAllCoaches()
        {
            Console.WriteLine("Список всех тренеров:");
            if (coaches.Count == 0)
            {
                Console.WriteLine("Нет тренеров в базе данных.");
                return;
            }
            int i = 1;
            foreach (var coach in coaches)
            {
                Console.WriteLine($"{i++}. Роль: {coach.Role}");
            }
        }

        static void ViewAllGames()
        {
            Console.WriteLine("Список всех игр:");
            if (games.Count == 0)
            {
                Console.WriteLine("Нет игр в базе данных.");
                return;
            }
            foreach (var game in games)
            {
                Console.WriteLine($"ID: {game.Id}\nДата: {game.Date.ToShortDateString()}\nСоперники: {game.Opponents}\nРезультат: {game.Result}\n");
            }
        }

        static void ViewAllTrainingSessions()
        {
            Console.WriteLine("Список всех тренировок:");
            if (trainingSessions.Count == 0)
            {
                Console.WriteLine("Нет тренировок в базе данных.");
                return;
            }
            foreach (var session in trainingSessions)
            {
                Console.WriteLine($"ID: {session.Id}\nДата и время: {session.DateTime}\nМесто: {session.Location}\n");
            }
        }

        static void ViewPlayerStats()
        {
            Console.Write("Введите имя игрока для просмотра статистики: ");
            string name = Console.ReadLine();

            var found = players.Find(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (found != null)
            {
                Console.WriteLine($"Статистика игрока {found.Name} {found.Surname}:");
                Console.WriteLine("Пока статистика не реализована.");
            }
            else
            {
                Console.WriteLine("Игрок не найден.");
            }
        }
    }

    public class Team
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string League { get; set; }
        public Roster Roster { get; set; }
    }

    public class Roster
    {
        public string Season { get; set; }
        public List<Player> Players { get; set; }
    }

    public class Player
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int PlayerNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public string Position { get; set; }
    }

    public class Coach
    {
        public string Role { get; set; }
    }

    public class TrainingSession
    {
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Location { get; set; }
    }

    public class Game
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Opponents { get; set; }
        public string Result { get; set; }
        public List<StatsRecord> StatsRecords { get; set; }
    }

    public class StatsRecord
    {
        public int Points { get; set; }
        public int Rebounds { get; set; }
        public int Assists { get; set; }
        public int Blocks { get; set; }
        public int Steals { get; set; }
        public string Attribute { get; set; }
    }
}
