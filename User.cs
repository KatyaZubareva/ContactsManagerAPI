using System;

class User {
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

    // Метод для создания нового контакта
    public void Create() {
        Console.Write("Введите имя: ");
        Name = Console.ReadLine();

        Console.Write("Введите фамилию: ");
        Surname = Console.ReadLine();

        Console.Write("Введите номер телефона: ");
        Phone = Console.ReadLine();

        Console.Write("Введите email: ");
        Email = Console.ReadLine();
    }
}
