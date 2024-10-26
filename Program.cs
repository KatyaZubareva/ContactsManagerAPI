using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        var dataHandler = new DataHandler();
        List<User> contacts = new List<User>(); 

        Console.WriteLine("Выберите вариант сохранения/загрузки данных:");
        Console.WriteLine("1. Сохранить в JSON");
        Console.WriteLine("2. Загрузить из JSON");
        Console.WriteLine("3. Сохранить в XML");
        Console.WriteLine("4. Загрузить из XML");
        Console.WriteLine("5. Сохранить в SQLite");
        Console.WriteLine("6. Загрузить из SQLite");

        string choice = Console.ReadLine();
        string filePath = "contacts.json"; 
        string connectionString = "Data Source=contacts.db;Version=3;";

        switch (choice)
        {
            case "1":
                dataHandler.SaveToJson(contacts, filePath);
                break;
            case "2":
                contacts = dataHandler.LoadFromJson(filePath);
                break;
            case "3":
                dataHandler.SaveToXml(contacts, "contacts.xml");
                break;
            case "4":
                contacts = dataHandler.LoadFromXml("contacts.xml");
                break;
            case "5":
                dataHandler.SaveToSQLite(contacts, connectionString);
                break;
            case "6":
                contacts = dataHandler.LoadFromSQLite(connectionString);
                break;
            default:
                Console.WriteLine("Неверный выбор.");
                break;
        }

        foreach (var contact in contacts)
        {
            Console.WriteLine($"{contact.Name} {contact.Surname} ({contact.Phone}, {contact.Email})");
        }
    }
}
