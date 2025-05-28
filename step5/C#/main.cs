using System;
using BasketballLeague.BLL;
using BasketballLeague.DAL;
using BasketballLeague.Models;

namespace BasketballLeague
{
    class Program
    {
        static string connectionString = "Data Source=basketball_legue.db;";

        static ITeamRepository teamRepo = new SqliteTeamRepository(connectionString);
        static IPlayerRepository playerRepo = new SqlitePlayerRepository(connectionString);
        static ICoachRepository coachRepo = new SqliteCoachRepository(connectionString);
        static IGameRepository gameRepo = new SqliteGameRepository(connectionString);
        static ITrainingRepository trainingRepo = new SqliteTrainingRepository(connectionString);

        static TeamService teamService = new TeamService(teamRepo);
        static PlayerService playerService = new PlayerService(playerRepo);
        static CoachService coachService = new CoachService(coachRepo);
        static GameService gameService = new GameService(gameRepo);
        static TrainingService trainingService = new TrainingService(trainingRepo);

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
                Console.WriteLine("11. Выход");
                Console.Write("Выберите пункт меню: ");
                string input = Console.ReadLine();

                Console.Clear();

                switch (input)
                {
                    case "1": AddTeam(); break;
                    case "2": AddPlayer(); break;
                    case "3": AddCoach(); break;
                    case "4": AddGame(); break;
                    case "5": AddTraining(); break;
                    case "6": ViewAllTeams(); break;
                    case "7": ViewAllPlayers(); break;
                    case "8": ViewAllCoaches(); break;
                    case "9": ViewAllGames(); break;
                    case "10": ViewAllTrainings(); break;
                    case "11":
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

            try
            {
                teamService.Add(new Team { Name = name, League = league });
                Console.WriteLine("Команда успешно добавлена!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }

        static void AddPlayer()
        {
            var teams = teamService.GetAll();
            var teamList = new System.Collections.Generic.List<Team>(teams);

            if (teamList.Count == 0)
            {
                Console.WriteLine("Сначала создайте хотя бы одну команду!");
                return;
            }

            Console.WriteLine("Выберите команду для игрока:");
            for (int i = 0; i < teamList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {teamList[i].Name} ({teamList[i].League}) [ID: {teamList[i].Id}]");
            }

            int teamIndex = -1;
            while (true)
            {
                Console.Write("Введите номер команды: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out teamIndex) && teamIndex >= 1 && teamIndex <= teamList.Count)
                {
                    teamIndex--;
                    break;
                }
                Console.WriteLine("Некорректный номер. Попробуйте снова.");
            }
            int teamId = teamList[teamIndex].Id;

            Console.Write("Имя игрока: ");
            string name = Console.ReadLine();
            Console.Write("Фамилия игрока: ");
            string surname = Console.ReadLine();
            Console.Write("Дата рождения (ГГГГ-ММ-ДД): ");
            string birthDateStr = Console.ReadLine();
            Console.Write("Номер игрока: ");
            int number = int.Parse(Console.ReadLine());
            Console.Write("Рост (см): ");
            float height = float.Parse(Console.ReadLine());
            Console.Write("Вес (кг): ");
            float weight = float.Parse(Console.ReadLine());
            Console.Write("Позиция: ");
            string position = Console.ReadLine();

            try
            {
                playerService.Add(new Player
                {
                    Name = name,
                    Surname = surname,
                    BirthDate = DateTime.Parse(birthDateStr),
                    Number = number,
                    Height = height,
                    Weight = weight,
                    Position = position,
                    TeamId = teamId
                });
                Console.WriteLine("Игрок успешно добавлен в команду " + teamList[teamIndex].Name + "!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }

        static void AddCoach()
        {
            var teams = teamService.GetAll();
            var teamList = new System.Collections.Generic.List<Team>(teams);

            if (teamList.Count == 0)
            {
                Console.WriteLine("Сначала создайте хотя бы одну команду!");
                return;
            }

            Console.WriteLine("Выберите команду для тренера:");
            for (int i = 0; i < teamList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {teamList[i].Name} ({teamList[i].League}) [ID: {teamList[i].Id}]");
            }

            int teamIndex = -1;
            while (true)
            {
                Console.Write("Введите номер команды: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out teamIndex) && teamIndex >= 1 && teamIndex <= teamList.Count)
                {
                    teamIndex--;
                    break;
                }
                Console.WriteLine("Некорректный номер. Попробуйте снова.");
            }
            int teamId = teamList[teamIndex].Id;

            Console.Write("Имя тренера: ");
            string name = Console.ReadLine();
            Console.Write("Фамилия тренера: ");
            string surname = Console.ReadLine();
            Console.Write("Дата рождения (ГГГГ-ММ-ДД): ");
            string birthDateStr = Console.ReadLine();
            Console.Write("Стаж (лет): ");
            int exp = int.Parse(Console.ReadLine());
            Console.Write("Специализация: ");
            string spec = Console.ReadLine();

            try
            {
                coachService.Add(new Coach
                {
                    Name = name,
                    Surname = surname,
                    BirthDate = DateTime.Parse(birthDateStr),
                    ExperienceYears = exp,
                    Specialization = spec,
                    TeamId = teamId
                });
                Console.WriteLine("Тренер успешно добавлен в команду " + teamList[teamIndex].Name + "!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }

        static void AddGame()
        {
            var teams = teamService.GetAll();
            var teamList = new System.Collections.Generic.List<Team>(teams);

            if (teamList.Count < 2)
            {
                Console.WriteLine("Для создания игры нужно минимум две команды!");
                return;
            }

            Console.WriteLine("Выберите первую команду:");
            for (int i = 0; i < teamList.Count; i++)
                Console.WriteLine($"{i + 1}. {teamList[i].Name}");

            int team1Index = -1;
            while (true)
            {
                Console.Write("Введите номер команды: ");
                if (int.TryParse(Console.ReadLine(), out team1Index) && team1Index >= 1 && team1Index <= teamList.Count)
                {
                    team1Index--;
                    break;
                }
                Console.WriteLine("Некорректный номер. Попробуйте снова.");
            }

            Console.WriteLine("Выберите вторую команду:");
            for (int i = 0; i < teamList.Count; i++)
                if (i != team1Index) Console.WriteLine($"{i + 1}. {teamList[i].Name}");

            int team2Index = -1;
            while (true)
            {
                Console.Write("Введите номер команды: ");
                if (int.TryParse(Console.ReadLine(), out team2Index) && team2Index >= 1 && team2Index <= teamList.Count && team2Index - 1 != team1Index)
                {
                    team2Index--;
                    break;
                }
                Console.WriteLine("Некорректный номер. Попробуйте снова.");
            }

            Console.Write("Дата игры (ГГГГ-ММ-ДД): ");
            string dateStr = Console.ReadLine();
            Console.Write("Счёт первой команды: ");
            int score1 = int.Parse(Console.ReadLine());
            Console.Write("Счёт второй команды: ");
            int score2 = int.Parse(Console.ReadLine());

            try
            {
                gameService.Add(new Game
                {
                    Team1Id = teamList[team1Index].Id,
                    Team2Id = teamList[team2Index].Id,
                    Date = DateTime.Parse(dateStr),
                    Score1 = score1,
                    Score2 = score2
                });
                Console.WriteLine("Игра успешно добавлена!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }

        static void AddTraining()
        {
            var teams = teamService.GetAll();
            var teamList = new System.Collections.Generic.List<Team>(teams);

            if (teamList.Count == 0)
            {
                Console.WriteLine("Сначала создайте хотя бы одну команду!");
                return;
            }

            Console.WriteLine("Выберите команду для тренировки:");
            for (int i = 0; i < teamList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {teamList[i].Name}");
            }

            int teamIndex = -1;
            while (true)
            {
                Console.Write("Введите номер команды: ");
                if (int.TryParse(Console.ReadLine(), out teamIndex) && teamIndex >= 1 && teamIndex <= teamList.Count)
                {
                    teamIndex--;
                    break;
                }
                Console.WriteLine("Некорректный номер. Попробуйте снова.");
            }
            int teamId = teamList[teamIndex].Id;

            Console.Write("Дата тренировки (ГГГГ-ММ-ДД): ");
            string dateStr = Console.ReadLine();
            Console.Write("Место проведения: ");
            string place = Console.ReadLine();
            Console.Write("Описание: ");
            string desc = Console.ReadLine();

            try
            {
                trainingService.Add(new Training
                {
                    TeamId = teamId,
                    Date = DateTime.Parse(dateStr),
                    Place = place,
                    Description = desc
                });
                Console.WriteLine("Тренировка успешно добавлена!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }

        static void ViewAllTeams()
        {
            var teams = teamService.GetAll();
            Console.WriteLine("Список всех команд:");
            bool hasRows = false;
            foreach (var team in teams)
            {
                hasRows = true;
                Console.WriteLine($"ID: {team.Id}\nНазвание: {team.Name}\nЛига: {team.League}\n");
            }
            if (!hasRows)
                Console.WriteLine("Нет команд в базе данных.");
        }

        static void ViewAllPlayers()
        {
            var players = playerService.GetAll();
            Console.WriteLine("Список всех игроков:");
            bool hasRows = false;
            foreach (var player in players)
            {
                hasRows = true;
                Console.WriteLine(
                    $"ID: {player.Id}\nИмя: {player.Name} {player.Surname}\nНомер: {player.Number}\nДата рождения: {player.BirthDate:yyyy-MM-dd}\nРост: {player.Height} см\nВес: {player.Weight} кг\nПозиция: {player.Position}\n"
                );
            }
            if (!hasRows)
                Console.WriteLine("Нет игроков в базе данных.");
        }

        static void ViewAllCoaches()
        {
            var coaches = coachService.GetAll();
            Console.WriteLine("Список всех тренеров:");
            bool hasRows = false;
            foreach (var coach in coaches)
            {
                hasRows = true;
                Console.WriteLine(
                    $"ID: {coach.Id}\nИмя: {coach.Name} {coach.Surname}\nДата рождения: {coach.BirthDate:yyyy-MM-dd}\nСтаж: {coach.ExperienceYears} лет\nСпециализация: {coach.Specialization}\n"
                );
            }
            if (!hasRows)
                Console.WriteLine("Нет тренеров в базе данных.");
        }

        static void ViewAllGames()
        {
            var games = gameService.GetAll();
            var teams = new System.Collections.Generic.Dictionary<int, string>();
            foreach (var t in teamService.GetAll())
                teams[t.Id] = t.Name;

            Console.WriteLine("Список всех игр:");
            bool hasRows = false;
            foreach (var game in games)
            {
                hasRows = true;
                string t1 = teams.ContainsKey(game.Team1Id) ? teams[game.Team1Id] : $"ID {game.Team1Id}";
                string t2 = teams.ContainsKey(game.Team2Id) ? teams[game.Team2Id] : $"ID {game.Team2Id}";
                Console.WriteLine(
                    $"ID: {game.Id}\n{t1} vs {t2}\nДата: {game.Date:yyyy-MM-dd}\nСчёт: {game.Score1}:{game.Score2}\n"
                );
            }
            if (!hasRows)
                Console.WriteLine("Нет игр в базе данных.");
        }

        static void ViewAllTrainings()
        {
            var trainings = trainingService.GetAll();
            var teams = new System.Collections.Generic.Dictionary<int, string>();
            foreach (var t in teamService.GetAll())
                teams[t.Id] = t.Name;

            Console.WriteLine("Список всех тренировок:");
            bool hasRows = false;
            foreach (var tr in trainings)
            {
                hasRows = true;
                string tname = teams.ContainsKey(tr.TeamId) ? teams[tr.TeamId] : $"ID {tr.TeamId}";
                Console.WriteLine(
                    $"ID: {tr.Id}\nКоманда: {tname}\nДата: {tr.Date:yyyy-MM-dd}\nМесто: {tr.Place}\nОписание: {tr.Description}\n"
                );
            }
            if (!hasRows)
                Console.WriteLine("Нет тренировок в базе данных.");
        }

        static void ViewPlayerStats()
        {
            Console.Write("Введите имя игрока: ");
            string name = Console.ReadLine();
            Console.Write("Введите фамилию игрока: ");
            string surname = Console.ReadLine();
            var player = playerService.GetByName(name, surname);
            if (player == null)
            {
                Console.WriteLine("Игрок не найден.");
                return;
            }
            Console.WriteLine(
                $"ID: {player.Id}\nИмя: {player.Name} {player.Surname}\nНомер: {player.Number}\nДата рождения: {player.BirthDate:yyyy-MM-dd}\nРост: {player.Height} см\nВес: {player.Weight} кг\nПозиция: {player.Position}\n" +
                $"Статистика: Очки: {player.Points}, Передачи: {player.Assists}, Подборы: {player.Rebounds}\n"
            );
        }
    }
}
