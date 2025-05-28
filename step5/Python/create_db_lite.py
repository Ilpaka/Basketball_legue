import sqlite3

DB_NAME = "basketball_legue.db"

CREATE_TABLES_SQL = [
    """
    CREATE TABLE IF NOT EXISTS team (
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        name TEXT NOT NULL,
        league TEXT NOT NULL
    );
    """, """
    CREATE TABLE IF NOT EXISTS player (
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        name TEXT NOT NULL,
        surname TEXT NOT NULL,
        birth_date TEXT NOT NULL,
        player_number INTEGER NOT NULL,
        height REAL NOT NULL,
        weight REAL NOT NULL,
        position TEXT NOT NULL,
        teamid INTEGER,
        points INTEGER DEFAULT 0,
        assists INTEGER DEFAULT 0,
        rebounds INTEGER DEFAULT 0,
        FOREIGN KEY (teamid) REFERENCES team(id)
    );
    """, """
    CREATE TABLE IF NOT EXISTS coach (
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        name TEXT NOT NULL,
        surname TEXT NOT NULL,
        birth_date TEXT NOT NULL,
        experienceyears INTEGER NOT NULL,
        specialization TEXT NOT NULL,
        teamid INTEGER,
        FOREIGN KEY (teamid) REFERENCES team(id)
    );
    """, """
    CREATE TABLE IF NOT EXISTS game (
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        team1id INTEGER NOT NULL,
        team2id INTEGER NOT NULL,
        date TEXT NOT NULL,
        score1 INTEGER NOT NULL,
        score2 INTEGER NOT NULL,
        FOREIGN KEY (team1id) REFERENCES team(id),
        FOREIGN KEY (team2id) REFERENCES team(id)
    );
    """, """
    CREATE TABLE IF NOT EXISTS training (
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        teamid INTEGER NOT NULL,
        date TEXT NOT NULL,
        place TEXT NOT NULL,
        description TEXT,
        FOREIGN KEY (teamid) REFERENCES team(id)
    );
    """
]


def create_database():
    try:
        conn = sqlite3.connect(DB_NAME)
        conn.execute("PRAGMA foreign_keys = ON;")
        cur = conn.cursor()

        # Удаляем старые несовместимые таблицы (если они есть)
        cur.execute("DROP TABLE IF EXISTS player_roster")
        cur.execute("DROP TABLE IF EXISTS stats_record")
        cur.execute("DROP TABLE IF EXISTS roster")
        cur.execute("DROP TABLE IF EXISTS team_coach")

        # Создаём новые таблицы
        for sql in CREATE_TABLES_SQL:
            cur.execute(sql)
            print(f"Таблица создана: {sql.split('(')[0].split()[-1]}")

        conn.commit()
        print("\nБаза данных успешно пересоздана!")
    except Exception as e:
        print("Ошибка:", e)
    finally:
        if conn:
            conn.close()


if __name__ == "__main__":
    create_database()
