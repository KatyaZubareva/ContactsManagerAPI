using System;
using System.IO;
using Newtonsoft.Json;
using System.Data.SQLite;
using System.Xml.Serialization;

class Commands {
    private ConnectionToDB db;

    public Commands() {
        db = new ConnectionToDB();
    }

    // Добавьте метод для сохранения контактов в JSON
    public void SaveToJson() {
        var contacts = db.GetAllContacts(); // Метод, который нужно реализовать в ConnectionToDB
        string json = JsonConvert.SerializeObject(contacts, Formatting.Indented);
        File.WriteAllText("contacts.json", json);
        Console.WriteLine("Контакты успешно сохранены в JSON.");
    }

    // Добавьте метод для загрузки контактов из JSON
    public void LoadFromJson() {
        if (!File.Exists("contacts.json")) {
            Console.WriteLine("Файл contacts.json не найден.");
            return;
        }

        string json = File.ReadAllText("contacts.json");
        var contacts = JsonConvert.DeserializeObject<List<User>>(json);
        foreach (var contact in contacts) {
            db.SaveUser(contact.Name, contact.Surname, contact.Phone, contact.Email);
        }
        Console.WriteLine("Контакты успешно загружены из JSON.");
    }

    // Добавьте метод для сохранения контактов в XML
    public void SaveToXml() {
        var contacts = db.GetAllContacts(); // Метод, который нужно реализовать в ConnectionToDB
        XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
        using (FileStream stream = new FileStream("contacts.xml", FileMode.Create)) {
            serializer.Serialize(stream, contacts);
        }
        Console.WriteLine("Контакты успешно сохранены в XML.");
    }

    // Добавьте метод для загрузки контактов из XML
    public void LoadFromXml() {
        if (!File.Exists("contacts.xml")) {
            Console.WriteLine("Файл contacts.xml не найден.");
            return;
        }

        XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
        using (FileStream stream = new FileStream("contacts.xml", FileMode.Open)) {
            var contacts = (List<User>)serializer.Deserialize(stream);
            foreach (var contact in contacts) {
                db.SaveUser(contact.Name, contact.Surname, contact.Phone, contact.Email);
            }
        }
        Console.WriteLine("Контакты успешно загружены из XML.");
    }

    // Обновите метод ShowMenu
    public void ShowMenu() {
        while (true) {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Показать все контакты");
            Console.WriteLine("2. Поиск контактов");
            Console.WriteLine("3. Добавить новый контакт");
            Console.WriteLine("4. Сохранить контакты в JSON");
            Console.WriteLine("5. Загрузить контакты из JSON");
            Console.WriteLine("6. Сохранить контакты в XML");
            Console.WriteLine("7. Загрузить контакты из XML");
            Console.WriteLine("8. Выход");
            Console.Write("> ");
            string choice = Console.ReadLine();

            switch (choice) {
                case "1":
                    ShowAllContacts();
                    break;
                case "2":
                    Search();
                    break;
                case "3":
                    NewContact();
                    break;
                case "4":
                    SaveToJson();
                    break;
                case "5":
                    LoadFromJson();
                    break;
                case "6":
                    SaveToXml();
                    break;
                case "7":
                    LoadFromXml();
                    break;
                case "8":
                    Exit();
                    break;
                default:
                    Console.WriteLine("Неверный выбор, попробуйте еще раз.");
                    break;
            }
        }
    }
}
