using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeardScoolDem6.Database
{
    public static class ImportClass
    {
        public static void Clietnts() {
            string[] lines = File.ReadAllLines(@"C:\Users\AlexDev\Desktop\dem6task\Сессия 1\client_s_import.txt");
            foreach(string line  in lines.Skip(1))
            {
                string[] items = line.Split(',').Select(i=>i.Trim()).ToArray();
                string fio = items[0] + " " + items[1] + " " + items[2];
                string[] fioArr = fio.Split(' ');

                Client client = new Client
                {
                    FirstName = fioArr[0],
                    LastName = fioArr[1],
                    Patronymic = fioArr[2],
                    GenderCode = items[3],
                    Phone = items[4],
                    Birthday = DateTime.Parse(items[5]),
                    Email = items[6],
                    RegistrationDate = DateTime.Parse(items[7])
                };
                EfModel.Init().Clients.Add(client);
            }
            EfModel.Init().SaveChanges();
        }

        public static void Services()
        {
            string[] lines = File.ReadAllLines(@"C:\Users\AlexDev\Desktop\dem6task\Сессия 1\service_s_import.csv");
            foreach (string line in lines.Skip(1))
            {
                string[] items = line.Split(',').Select(i => i.Trim()).ToArray();

                Service service = new Service
                {
                    Title = items[0],
                    MainImagePath = items[1],
                    DurationInSeconds = Convert.ToInt32(items[2]),
                    Cost = Convert.ToDecimal(items[3]),
                    
                };

                try
                {
                    service.Discount = Convert.ToDouble(items[4]);
                }
                catch
                {

                }

                try
                {
                    service.MainImage = File.ReadAllBytes(@"C:\Users\AlexDev\Desktop\dem6task\Сессия 1\services_s_import\" + service.MainImagePath);
                }
                catch
                {
                    Console.WriteLine(service.MainImagePath);
                }
                EfModel.Init().Services.Add(service);
            }
            EfModel.Init().SaveChanges();
        }

        public static void ClientServiceImport()
        {
            string[] lines = File.ReadAllLines(@"C:\Users\AlexDev\Desktop\dem6task\Сессия 1\serviceclient_s_import.csv");
            foreach (string line in lines.Skip(1))
            {
                string[] items = line.Split(';').Select(i => i.Trim()).ToArray();

                string clientFirstName = items[2];
                string serviceTitle = items[0];
                ClientService clientService = new ClientService
                {
                    Client = EfModel.Init().Clients.FirstOrDefault(c => c.FirstName == clientFirstName),
                    Service = EfModel.Init().Services.FirstOrDefault(s => s.Title == serviceTitle),
                    StartTime = DateTime.Parse(items[1])
                };
                EfModel.Init().ClientServices.Add(clientService);
            }
            EfModel.Init().SaveChanges();
        }
    }
}
