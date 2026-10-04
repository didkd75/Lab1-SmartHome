using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab_1_Roshko
{
    internal class Program
    {
        private static List<SmartDevice> devices = new List<SmartDevice>();
        private static int maxCapacity = 0;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.Write("Введіть максимальну кількість пристроїв N (N > 0): ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out maxCapacity) && maxCapacity > 0)
                {
                    break;
                }
                Console.WriteLine("Error: має бути ціле додатне число!");
            }

            bool exitProgram = false;
            while (!exitProgram)
            {
                Console.WriteLine("\n=== МЕНЮ КЕРУВАННЯ РОЗУМНИМ ДОВОМ ===");
                Console.WriteLine("1 – Додати об'єкт");
                Console.WriteLine("2 – Переглянути всі об'єкти");
                Console.WriteLine("3 – Знайти об'єкт");
                Console.WriteLine("4 – Продемонструвати поведінку");
                Console.WriteLine("5 – Видалити об'єкт");
                Console.WriteLine("0 – Вийти з програми");
                Console.Write("Оберіть пункт меню: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": AddDeviceMenu(); break;
                    case "2": ShowAllDevices(); break;
                    case "3": SearchDeviceMenu(); break;
                    case "4": DemonstrateBehaviorMenu(); break;
                    case "5": DeleteDeviceMenu(); break;
                    case "0":
                        exitProgram = true;
                        Console.WriteLine("Програму успішно завершено. До побачення!");
                        break;
                    default:
                        Console.WriteLine("Некоректний вибір! Спробуйте ще раз.");
                        break;
                }
            }
        }

        private static string ReadValidSerialNumber()
        {
            while (true)
            {
                Console.Write("Введіть серійний номер (6 цифр): ");
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Error: відсутнє значення!");
                }
                else if (!input.All(char.IsDigit))
                {
                    Console.WriteLine("Error: не цифри!");
                }
                else if (input.Length != 6)
                {
                    Console.WriteLine("Error: має бути 6 цифр!");
                }
                else
                {
                    return input;
                }
            }
        }

        private static string ReadValidDeviceName()
        {
            while (true)
            {
                Console.Write("Введіть назву пристрою (3-15 символів, тільки букви): ");
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Error: відсутнє значення!");
                }
                else if (input.Length < 3 || input.Length > 15)
                {
                    Console.WriteLine("Error: довжина має бути від 3 до 15 символів!");
                }
                else if (!input.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
                {
                    Console.WriteLine("Error: мають бути тільки букви!");
                }
                else
                {
                    return input;
                }
            }
        }

        private static DeviceType ReadValidDeviceType()
        {
            while (true)
            {
                Console.WriteLine("Оберіть тип пристрою: 1 - Thermostat, 2 - SmartLight, 3 - SecurityCamera, 4 - SmartLock");
                Console.Write("Ваш вибір: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int val) && Enum.IsDefined(typeof(DeviceType), val))
                {
                    return (DeviceType)val;
                }
                Console.WriteLine("Error: некоректний тип пристрою!");
            }
        }

        private static double ReadValidPowerUsage()
        {
            while (true)
            {
                Console.Write("Введіть споживання потужності Вт (0..500): ");
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Error: відсутнє значення!");
                }
                else if (!double.TryParse(input, out double val))
                {
                    Console.WriteLine("Error: не число!");
                }
                else if (val < 0 || val > 500)
                {
                    Console.WriteLine("Error: значення має бути у діапазоні [0..500]!");
                }
                else
                {
                    return val;
                }
            }
        }

        // --- ПУНКТ 1: ДОДАВАННЯ ОБ'ЄКТА ---
        private static void AddDeviceMenu()
        {
            if (devices.Count >= maxCapacity)
            {
                Console.WriteLine($"Error: досягнуто ліміт пристроїв N = {maxCapacity}. Додавання неможливе!");
                return;
            }

            Console.WriteLine("1 - Ввести дані вручну");
            Console.WriteLine("2 - Згенерувати автоматично (тестовий об'єкт)");
            Console.Write("Ваш вибір: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                string sn = ReadValidSerialNumber();
                string name = ReadValidDeviceName();
                DeviceType type = ReadValidDeviceType();
                double power = ReadValidPowerUsage();

                Console.Write("Пристрій увімкнений? (1 - Так, 0 - Ні): ");
                bool isActive = Console.ReadLine() == "1";

                devices.Add(new SmartDevice(sn, name, type, power, isActive));
                Console.WriteLine("Об'єкт успішно додано!");
            }
            else if (choice == "2")
            {
                Random rnd = new Random();
                string sn = rnd.Next(100000, 999999).ToString();
                string name = "AutoDevice" + rnd.Next(10, 99);
                DeviceType type = (DeviceType)rnd.Next(1, 5);
                double power = Math.Round(rnd.NextDouble() * 150, 1);

                devices.Add(new SmartDevice(sn, name, type, power, true));
                Console.WriteLine("Автоматичний об'єкт успішно згенеровано та додано!");
            }
            else
            {
                Console.WriteLine("Некоректний вибір моду додавання.");
            }
        }

        // --- ПУНКТ 2: ТАБЛИЧНИЙ ВИВІД ---
        private static void ShowAllDevices()
        {
            PrintTable(devices);
        }

        private static void PrintTable(List<SmartDevice> list)
        {
            if (list == null || list.Count == 0)
            {
                Console.WriteLine("Жодного об'єкта не знайдено.");
                return;
            }

            Console.WriteLine(new string('-', 65));
            Console.WriteLine($"| {"№",-3} | {"Серійний номер",-14} | {"Назва пристрою",-15} | {"Тип",-14} | {"Потужність",-10} |");
            Console.WriteLine(new string('-', 65));

            for (int i = 0; i < list.Count; i++)
            {
                int indexInOriginal = devices.IndexOf(list[i]) + 1;
                Console.WriteLine($"| {indexInOriginal,-3} | {list[i].SerialNumber,-14} | {list[i].DeviceName,-15} | {list[i].Type,-14} | {list[i].PowerUsage + "W",-10} |");
            }
            Console.WriteLine(new string('-', 65));
        }

        // --- ПУНКТ 3: ПОШУК ОБ'ЄКТА ---
        private static void SearchDeviceMenu()
        {
            if (devices.Count == 0)
            {
                Console.WriteLine("Жодного об'єкта немає в системі.");
                return;
            }

            Console.WriteLine("Оберіть характеристику для пошуку:");
            Console.WriteLine("1 – За Серійним номерам");
            Console.WriteLine("2 – За Типом пристрою");
            Console.Write("Ваш вибір: ");
            string choice = Console.ReadLine();

            List<SmartDevice> results = new List<SmartDevice>();

            if (choice == "1")
            {
                Console.Write("Введіть серійний номер для пошуку: ");
                string sn = Console.ReadLine();
                results = devices.Where(d => d.SerialNumber.Equals(sn, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            else if (choice == "2")
            {
                Console.Write("Введіть назву типу (Thermostat, SmartLight, SecurityCamera, SmartLock): ");
                string typeStr = Console.ReadLine();
                results = devices.Where(d => d.Type.ToString().Equals(typeStr, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            else
            {
                Console.WriteLine("Некоректна характеристика для пошуку.");
                return;
            }

            PrintTable(results);
        }

        // --- ПУНКТ 4: ДЕМОНСТРАЦІЯ ПОВЕДІНКИ ---
        private static void DemonstrateBehaviorMenu()
        {
            if (devices.Count == 0)
            {
                Console.WriteLine("Жодного об'єкта немає для демонстрації поведінки.");
                return;
            }

            ShowAllDevices();
            Console.Write("Введіть № пристрою у таблиці: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= devices.Count)
            {
                SmartDevice dev = devices[index - 1];
                Console.WriteLine($"\n--- Демонстрація методів об'єкта '{dev.DeviceName}' ---");

                // Виклики всіх публічних методів класу
                dev.TogglePower();

                Console.Write("Введіть нове значення потужності для калібрування: ");
                if (double.TryParse(Console.ReadLine(), out double p))
                {
                    dev.CalibratePower(p);
                }

                Console.WriteLine(dev.GetDiagnosticsReport());
            }
            else
            {
                Console.WriteLine("Error: некоректний порядковий номер!");
            }
        }

        // --- ПУНКТ 5: ВИДАЛЕННЯ ОБ'ЄКТА ---
        private static void DeleteDeviceMenu()
        {
            if (devices.Count == 0)
            {
                Console.WriteLine("Жодного об'єкта немає в системі.");
                return;
            }

            Console.WriteLine("Оберіть спосіб видалення:");
            Console.WriteLine("1 – За порядковим номером");
            Console.WriteLine("2 – За характеристикою (Серійний номер)");
            Console.Write("Ваш вибір: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Введіть порядковий номер: ");
                if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= devices.Count)
                {
                    devices.RemoveAt(index - 1);
                    Console.WriteLine("Об'єкт успішно видалено!");
                }
                else
                {
                    Console.WriteLine("Error: неможливість видалення через некоректне значення порядкового номера!");
                }
            }
            else if (choice == "2")
            {
                Console.Write("Введіть серійний номер: ");
                string sn = Console.ReadLine();
                int removedCount = devices.RemoveAll(d => d.SerialNumber.Equals(sn, StringComparison.OrdinalIgnoreCase));

                if (removedCount > 0)
                {
                    Console.WriteLine($"Успішно видалено об'єктів: {removedCount}");
                }
                else
                {
                    Console.WriteLine("Відсутність об'єктів для видалення із заданою характеристикою.");
                }
            }
            else
            {
                Console.WriteLine("Некоректний вибір!");
            }
        }
    }
}
