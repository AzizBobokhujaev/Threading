using System;
using System.Threading;
using System.Collections.Generic;

namespace Threading
{
    class Program {
        public static int newID = 0;
        public static List<Client> ClientsList = new List<Client>();
        public static List<Client> CheckClientList = new List<Client>();
        static void Main(string[] args)
        {
            TimerCallback TimerCallback = new TimerCallback(FindUpdateBalance);
            Timer tm = new Timer(TimerCallback, ClientsList, 0, 1000);
            var a = true;
            while (a)
            {
                Console.WriteLine("Выберите команду: \n\t\t\t1. Добавить клиента \n\t\t\t2. Обновить данные клиента по ID \n\t\t\t3. Удалить клиента по ID \n\t\t\t4. Посмотреть всех \n\t\t\t5. Посмотр клиента по ID \n\t\t\t6. Любая другая команда для выхода");
                var b = (Console.ReadLine());
                switch (b)
                {
                    case "1":
                        {
                            Thread InsertThread = new Thread(Insert);
                            InsertThread.Start();
                            InsertThread.Join();
                        }break;
                    case "2":
                        {
                            Thread UpdateThread = new Thread(UpdateById);
                            UpdateThread.Start();
                            UpdateThread.Join();
                        }break;
                    case "3":
                        {
                            Thread DeleteThread = new Thread(DeleteById);
                            DeleteThread.Start();
                            DeleteThread.Join();
                        }break;
                    case "4":
                        {
                            Thread SelectAllThread = new Thread(SelectAll);
                            SelectAllThread.Start();
                            SelectAllThread.Join();
                        }break;
                    case "5":
                        {
                            Thread SelectByIDThread = new Thread(SelectById);
                            SelectByIDThread.Start();
                            SelectByIDThread.Join();
                        }break;
                    default:a = false;
                        break;
                }
            }
        }
        public static void FindUpdateBalance(object obj)
        {
            List<Client> list = obj as List<Client>;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Balance > CheckClientList[i].Balance)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"ID:{list[i].Id}\nБаланс до изменения:{CheckClientList[i].Balance}\nБаланс после изменения:{list[i].Balance}\nРазница: +{list[i].Balance - CheckClientList[i].Balance}");
                    Console.ForegroundColor = ConsoleColor.White;
                    CheckClientList[i].Balance = list[i].Balance;
                }
                else if (list[i].Balance < CheckClientList[i].Balance)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"ID:{list[i].Id}\nБаланс до изменения:{CheckClientList[i].Balance}\nБаланс после изменения:{list[i].Balance}\nРазница: {list[i].Balance - CheckClientList[i].Balance}");
                    Console.ForegroundColor = ConsoleColor.White;
                    CheckClientList[i].Balance = list[i].Balance;
                }
            }
        }
        public static void Insert()
        {
            Console.Clear();
            Console.Write($"FirstName: "); var firstName = Console.ReadLine();
            Console.Write($"LastName: "); var lastName = Console.ReadLine();
            Console.Write("Age: "); var age = int.Parse(Console.ReadLine());
            newID++;
            Console.Write("Balance: "); var balance = decimal.Parse(Console.ReadLine());
            Client ClientInsert = new Client(newID, balance, firstName, lastName, age);
            ClientsList.Add(ClientInsert);
            CheckClientList.Add(ClientInsert);
        }
        public static void UpdateById()
        {
            Console.Clear();
            Console.Write("ID: "); int SetId = int.Parse(Console.ReadLine());
            Console.Write($"FirstName: "); var firstName = Console.ReadLine();
            Console.Write($"LastName: "); var lastName = Console.ReadLine();
            Console.Write("Age: "); var age = int.Parse(Console.ReadLine());
            Console.Write("Balance: "); var balance = decimal.Parse(Console.ReadLine());
            Client ClientUpdate = new Client(SetId, balance, firstName, lastName, age);
            foreach (var item in ClientsList)
            {
                if (SetId == item.Id)
                {
                    int index = ClientsList.IndexOf(item);
                    ClientsList[index] = ClientUpdate;
                    break;
                }
                else
                {
                    Console.WriteLine("Client with this Id not Found");
                }
            }
        }
        public static void DeleteById()
        {
            Console.Clear(); 
            Console.Write("ID: "); var ClientId = Convert.ToInt32(Console.ReadLine());
            foreach (var item in ClientsList)
            {
                if (ClientId == item.Id)
                {
                    ClientsList.Remove(item);
                    Console.WriteLine("Элемент удален из списка");
                    break;
                }
                else
                {
                    Console.WriteLine("Client by this Id not found");
                    break;
                }
            }
        }
        public static void SelectAll()
        {
            Console.Clear();
            foreach (var item in ClientsList)
            {
                Console.WriteLine($"ID: {item.Id}, FirstName: {item.FirstName}, LastName: {item.Lastname}, Age: {item.Age}, Balance: {item.Balance}");
                Console.WriteLine("_____________________________________________________________________________________________________");
            }
        }
        public static void SelectById()
        {
            Console.Clear();
            Console.Write("ID: "); var ClientId = Convert.ToInt32(Console.ReadLine());
            foreach (var item in ClientsList)
            {
                if (ClientId == item.Id)
                {
                    Console.WriteLine($"ID: {item.Id}, FirstName: {item.FirstName}, LastName: {item.Lastname}, Age: {item.Age}, Balance: {item.Balance}");
                }
                else
                {
                    Console.WriteLine("Client by this Id not found");
                }
            }
        }
        
    }
    class Client
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public int Age { get; set; }
        public Client() { }
        public Client(int id,decimal balance,string firstName,string lastName,int age)
        {
            Id = id;
            Balance = balance;
            FirstName = firstName;
            Lastname = lastName;
            Age = age;
        }
    }
}
