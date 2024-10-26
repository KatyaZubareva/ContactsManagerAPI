using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

class ConnectionToDB {
    private MySqlConnection connection;
    private string server;
    private string database;
    private string username;
    private string password;

    // Конструктор для установки параметров подключения
    public ConnectionToDB() {
        server = "localhost";
        database = "contact_book";
        username = "root";
        password = "root";

        string connectionString = $"SERVER={server};DATABASE={database};UID={username};PASSWORD={password};";
        connection = new MySqlConnection(connectionString);
    }

    // Открытие соединения с базой данных
    public void OpenConnection() {
        try {
            connection.Open();
        } catch (Exception ex) {
            Console.WriteLine("Ошибка подключения к базе данных: " + ex.Message);
        }
    }

    // Закрытие соединения с базой данных
    public void CloseConnection() {
        try {
            connection.Close();
        } catch (Exception ex) {
            Console.WriteLine("Ошибка закрытия подключения: " + ex.Message);
        }
    }

    // Метод для сохранения нового контакта
    public void SaveUser(string name, string surname, string phone, string email) {
        try {
            OpenConnection();
            string userData = "INSERT INTO contacts (name, surname, phone, email) VALUES (@Name, @Surname, @Phone, @Email);";
            MySqlCommand command = new MySqlCommand(userData, connection);
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Surname", surname);
            command.Parameters.AddWithValue("@Phone", phone);
            command.Parameters.AddWithValue("@Email", email);
            command.ExecuteNonQuery();
            Console.WriteLine("Контакт успешно сохранён.");
        } catch (Exception ex) {
            Console.WriteLine("Не удалось сохранить контакт. Ошибка: " + ex.Message);
        } finally {
            CloseConnection();
        }
    }

    // Метод для отображения всех контактов
    public void ShowAllContacts() {
        try {
            OpenConnection();
            string query = "SELECT * FROM contacts;";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows) {
                while (reader.Read()) {
                    Console.WriteLine($"ID: {reader["id"]}, Name: {reader["name"]}, Surname: {reader["surname"]}, Phone: {reader["phone"]}, Email: {reader["email"]}");
                }
            } else {
                Console.WriteLine("Нет контактов.");
            }

            reader.Close();
        } catch (Exception ex) {
            Console.WriteLine("Ошибка при получении контактов: " + ex.Message);
        } finally {
            CloseConnection();
        }
    }

    // Метод для поиска контактов
    public void SearchContact(string field, string query) {
        try {
            OpenConnection();
            string searchQuery = $"SELECT * FROM contacts WHERE {field} LIKE @Query;";
            MySqlCommand command = new MySqlCommand(searchQuery, connection);
            command.Parameters.AddWithValue("@Query", "%" + query + "%");
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows) {
                while (reader.Read()) {
                    Console.WriteLine($"ID: {reader["id"]}, Name: {reader["name"]}, Surname: {reader["surname"]}, Phone: {reader["phone"]}, Email: {reader["email"]}");
                }
            } else {
                Console.WriteLine("Контактов не найдено.");
            }

            reader.Close();
        } catch (Exception ex) {
            Console.WriteLine("Ошибка при поиске контактов: " + ex.Message);
        } finally {
            CloseConnection();
        }
    }

    public List<User> GetAllContacts() {
        List<User> users = new List<User>();
        try {
            OpenConnection();
            string query = "SELECT * FROM contacts;";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read()) {
                User user = new User {
                    Name = reader["name"].ToString(),
                    Surname = reader["surname"].ToString(),
                    Phone = reader["phone"].ToString(),
                    Email = reader["email"].ToString()
                };
                users.Add(user);
            }
        } catch (Exception ex) {
            Console.WriteLine("Ошибка при получении контактов: " + ex.Message);
        } finally {
            CloseConnection();
        }
        return users;
    }
}
