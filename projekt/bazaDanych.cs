using System.Data.SQLite;

namespace projekt
{
    internal class Database
    {
        public static void Connection()
        {
            SQLiteConnection connection;
            connection = CreateConnection();
            ReadData(connection);
        }

        public static void InsertUserConnection(User user)
        {
            SQLiteConnection connection;
            connection = CreateConnection();
            InsertUser(connection, user);
        }

        public static void DeleteUserConnetion(User user)
        {
            SQLiteConnection connection;
            connection = CreateConnection();
            DeleteUser(connection, user);

        }

        public static void UpdateUserConnetion(User user)
        {
            UpdateUser(CreateConnection(), user);
        }
        public static SQLiteConnection CreateConnection()
        {
            SQLiteConnection connection = new SQLiteConnection("data source=C:\\Users\\numer\\source\\repos\\projekt\\projekt\\Resources\\Database\\database.db; Version = 3;");
            try
            {
                connection.Open();
            }
            catch { }

            return connection;
        }

        static string? tekst;
        static void ReadData(SQLiteConnection connection)
        {
            SQLiteDataReader reader;
            SQLiteCommand command;
            command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM users";
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                string temp_name = (string)reader["Name"];
                string temp_surname = (string)reader["Surname"];
                string temp_email = (string)reader["Email"];
                string temp_password = (string)reader["Password"];
                string? temp_pass;

                if ((object)reader["admPass"] is System.DBNull)
                    temp_pass = "none";
                else
                    temp_pass = reader["admPass"].ToString();

                var existingUser = userManager.ListaUzytkownikow.FirstOrDefault(u => u.Email == temp_email);

                if (existingUser == null)
                {
                    User newUser = new User(temp_name, temp_surname, temp_email, temp_password, temp_pass);
                    userManager.AddUser(newUser);
                }
            }
            reader.Close();
        }

        static void InsertUser(SQLiteConnection connection, User user)
        {
            SQLiteCommand command;
            command = connection.CreateCommand();
            command.CommandText = "INSERT INTO users (Name, Surname, Email, Password, admPass) VALUES ('" + user.Imie + "', '" + user.Nazwisko + "', '" + user.Email + "', '" + user.Haslo + "', '" + user.Uprawnienia + "')";
            command.ExecuteNonQuery();
        }

        static void DeleteUser(SQLiteConnection connection, User user)
        {
            SQLiteCommand command;
            command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM users WHERE email = '{user.Email}'";
            command.ExecuteNonQuery();

            var userInList = userManager.ListaUzytkownikow.FirstOrDefault(u => u.Email == user.Email);

            if (userInList != null)
                userManager.ListaUzytkownikow.Remove(userInList);
        }

        static void UpdateUser(SQLiteConnection connection, User user)
        {
            SQLiteCommand command;
            command = connection.CreateCommand();
            command.CommandText = $"UPDATE users SET Name = '{user.Imie}', Surname = '{user.Nazwisko}', Email = '{user.Email}', Password = '{user.Haslo}', admPass = '{user.Uprawnienia}' WHERE Email = '{user.Email}'";
            command.ExecuteNonQuery();
        }
    }
}
