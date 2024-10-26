using System.Collections.Generic;
using System.IO;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using Newtonsoft.Json;

class DataHandler {
    // Сохранить контакты в JSON
    public void SaveToJson(List<User> contacts, string filePath) {
        string json = JsonConvert.SerializeObject(contacts, Formatting.Indented);
        File.WriteAllText(filePath, json);
        Console.WriteLine("Контакты успешно сохранены в JSON.");
    }

    // Загрузить контакты из JSON
    public List<User> LoadFromJson(string filePath) {
        if (!File.Exists(filePath)) {
            Console.WriteLine("Файл contacts.json не найден.");
            return new List<User>();
        }

        string json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<List<User>>(json);
    }

    // Сохранить контакты в XML
    public void SaveToXml(List<User> contacts, string filePath) {
        XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
        using (FileStream stream = new FileStream(filePath, FileMode.Create)) {
            serializer.Serialize(stream, contacts);
        }
        Console.WriteLine("Контакты успешно сохранены в XML.");
    }

    // Загрузить контакты из XML
    public List<User> LoadFromXml(string filePath) {
        if (!File.Exists(filePath)) {
            Console.WriteLine("Файл contacts.xml не найден.");
            return new List<User>();
        }

        XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
        using (FileStream stream = new FileStream(filePath, FileMode.Open)) {
            return (List<User>)serializer.Deserialize(stream);
        }
    }

    // Сохранить контакты в SQLite
    public void SaveToSQLite(List<User> contacts, string connectionString) {
        using (var connection = new SQLiteConnection(connectionString)) {
            connection.Open();
            foreach (var contact in contacts) {
                string query = "INSERT INTO contacts (name, surname, phone, email) VALUES (@Name, @Surname, @Phone, @Email);";
                using (var command = new SQLiteCommand(query, connection)) {
                    command.Parameters.AddWithValue("@Name", contact.Name);
                    command.Parameters.AddWithValue("@Surname", contact.Surname);
                    command.Parameters.AddWithValue("@Phone", contact.Phone);
                    command.Parameters.AddWithValue("@Email", contact.Email);
                    command.ExecuteNonQuery();
                }
            }
            Console.WriteLine("Контакты успешно сохранены в SQLite.");
        }
    }

    // Загрузить контакты из SQLite
    public List<User> LoadFromSQLite(string connectionString) {
        List<User> contacts = new List<User>();
        using (var connection = new SQLiteConnection(connectionString)) {
            connection.Open();
            string query = "SELECT * FROM contacts;";
            using (var command = new SQLiteCommand(query, connection)) {
                using (var reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        contacts.Add(new User {
                            Name = reader["name"].ToString(),
                            Surname = reader["surname"].ToString(),
                            Phone = reader["phone"].ToString(),
                            Email = reader["email"].ToString()
                        });
                    }
                }
            }
        }
        Console.WriteLine("Контакты успешно загружены из SQLite.");
        return contacts;
    }
}
