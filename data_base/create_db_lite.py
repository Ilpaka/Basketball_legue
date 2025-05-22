import sqlite3

DB_NAME = "basketball_legue.db"

CREATE_TABLES_SQL = [
    """
    CREATE TABLE IF NOT EXISTS player (
        id INTEGER PRIMARY KEY,
        name TEXT NOT NULL,
        surname TEXT NOT NULL,
        birth_date TEXT NOT NULL,  -- формат даты 'YYYY-MM-DD'
        player_number INTEGER,
        height REAL,
        weight REAL,
        position TEXT
    );
    """,
    """
    CREATE TABLE IF NOT EXISTS coach (
        id INTEGER PRIMARY KEY,
        player_id INTEGER UNIQUE,
        role TEXT,
        FOREIGN KEY (player_id) REFERENCES player(id) ON DELETE CASCADE
    );
    """,
    """
    CREATE TABLE IF NOT EXISTS team (
        id INTEGER PRIMARY KEY,
        name TEXT NOT NULL,
        league TEXT
    );
    """,
    """
    CREATE TABLE IF NOT EXISTS roster (
        id INTEGER PRIMARY KEY,
        team_id INTEGER,
        season TEXT NOT NULL,
        FOREIGN KEY (team_id) REFERENCES team(id) ON DELETE CASCADE
    );
    """,
    """
    CREATE TABLE IF NOT EXISTS game (
        id INTEGER PRIMARY KEY,
        team_id INTEGER,
        date TEXT NOT NULL,  -- формат даты 'YYYY-MM-DD'
        opponents TEXT,
        result TEXT,
        FOREIGN KEY (team_id) REFERENCES team(id) ON DELETE CASCADE
    );
    """,
    """
    CREATE TABLE IF NOT EXISTS training_session (
        id INTEGER PRIMARY KEY,
        team_id INTEGER,
        datetime TEXT NOT NULL,  -- формат 'YYYY-MM-DD HH:MM:SS'
        location TEXT,
        FOREIGN KEY (team_id) REFERENCES team(id) ON DELETE CASCADE
    );
    """,
    """
    CREATE TABLE IF NOT EXISTS player_roster (
        player_id INTEGER,
        roster_id INTEGER,
        PRIMARY KEY (player_id, roster_id),
        FOREIGN KEY (player_id) REFERENCES player(id) ON DELETE CASCADE,
        FOREIGN KEY (roster_id) REFERENCES roster(id) ON DELETE CASCADE
    );
    """,
    """
    CREATE TABLE IF NOT EXISTS stats_record (
        id INTEGER PRIMARY KEY,
        player_id INTEGER,
        game_id INTEGER,
        points INTEGER DEFAULT 0,
        rebounds INTEGER DEFAULT 0,
        assists INTEGER DEFAULT 0,
        blocks INTEGER DEFAULT 0,
        steals INTEGER DEFAULT 0,
        FOREIGN KEY (player_id) REFERENCES player(id) ON DELETE CASCADE,
        FOREIGN KEY (game_id) REFERENCES game(id) ON DELETE CASCADE
    );
    """,
    """
    CREATE TABLE IF NOT EXISTS team_coach (
        id INTEGER PRIMARY KEY,
        team_id INTEGER,
        coach_id INTEGER,
        FOREIGN KEY (team_id) REFERENCES team(id) ON DELETE CASCADE,
        FOREIGN KEY (coach_id) REFERENCES coach(id) ON DELETE CASCADE
    );
    """
]

def create_database():
    try:
        conn = sqlite3.connect(DB_NAME)
        conn.execute("PRAGMA foreign_keys = ON;")  # Включаем поддержку внешних ключей
        cur = conn.cursor()

        for sql in CREATE_TABLES_SQL:
            cur.execute(sql)
            print("Таблица создана (если ещё не была):", sql.split('(')[0].split()[-1])

        conn.commit()
        cur.close()
        conn.close()
        print("База данных успешно инициализирована!")
    except Exception as e:
        print("Ошибка при создании базы данных:", e)

if __name__ == "__main__":
    create_database()
