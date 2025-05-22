import psycopg2

# Данные для подключения — замени на свои!
DB_NAME = "basketball_db"
DB_USER = "postgres"
DB_PASSWORD = "yourpassword"
DB_HOST = "localhost"
DB_PORT = "5432"

# Все SQL-запросы для создания таблиц
CREATE_TABLES_SQL = [
    """
    CREATE TABLE IF NOT EXISTS player (
        id SERIAL PRIMARY KEY,
        name VARCHAR(100) NOT NULL,
        surname VARCHAR(100) NOT NULL,
        birth_date DATE NOT NULL,
        player_number INT,
        height FLOAT,
        weight FLOAT,
        position VARCHAR(30)
    );
    """,
    """
    CREATE TABLE IF NOT EXISTS coach (
        id SERIAL PRIMARY KEY,
        player_id INT UNIQUE REFERENCES player(id) ON DELETE CASCADE,
        role VARCHAR(50)
    );
    """,
    """
    CREATE TABLE IF NOT EXISTS team (
        id SERIAL PRIMARY KEY,
        name VARCHAR(100) NOT NULL,
        league VARCHAR(100)
    );
    """,
    """
    CREATE TABLE IF NOT EXISTS roster (
        id SERIAL PRIMARY KEY,
        team_id INT REFERENCES team(id) ON DELETE CASCADE,
        season VARCHAR(20) NOT NULL
    );
    """,
    """
    CREATE TABLE IF NOT EXISTS game (
        id SERIAL PRIMARY KEY,
        team_id INT REFERENCES team(id) ON DELETE CASCADE,
        date DATE NOT NULL,
        opponents VARCHAR(100),
        result VARCHAR(20)
    );
    """,
    """
    CREATE TABLE IF NOT EXISTS training_session (
        id SERIAL PRIMARY KEY,
        team_id INT REFERENCES team(id) ON DELETE CASCADE,
        datetime TIMESTAMP NOT NULL,
        location VARCHAR(100)
    );
    """,
    """
    CREATE TABLE IF NOT EXISTS player_roster (
        player_id INT REFERENCES player(id) ON DELETE CASCADE,
        roster_id INT REFERENCES roster(id) ON DELETE CASCADE,
        PRIMARY KEY (player_id, roster_id)
    );
    """,
    """
    CREATE TABLE IF NOT EXISTS stats_record (
        id SERIAL PRIMARY KEY,
        player_id INT REFERENCES player(id) ON DELETE CASCADE,
        game_id INT REFERENCES game(id) ON DELETE CASCADE,
        points INT DEFAULT 0,
        rebounds INT DEFAULT 0,
        assists INT DEFAULT 0,
        blocks INT DEFAULT 0,
        steals INT DEFAULT 0
    );
    """,
    """
    CREATE TABLE IF NOT EXISTS team_coach (
        id SERIAL PRIMARY KEY,
        team_id INT REFERENCES team(id) ON DELETE CASCADE,
        coach_id INT REFERENCES coach(id) ON DELETE CASCADE
    );
    """
]

def create_database():
    try:
        # Подключение к БД
        conn = psycopg2.connect(
            dbname=DB_NAME,
            user=DB_USER,
            password=DB_PASSWORD,
            host=DB_HOST,
            port=DB_PORT
        )
        conn.autocommit = True
        cur = conn.cursor()

        # Выполнение SQL-скриптов
        for sql in CREATE_TABLES_SQL:
            cur.execute(sql)
            print("Таблица создана (если ещё не была):", sql.split('(')[0].split()[-1])

        cur.close()
        conn.close()
        print("База данных успешно инициализирована!")
    except Exception as e:
        print("Ошибка при создании базы данных:", e)

if __name__ == "__main__":
    create_database()
