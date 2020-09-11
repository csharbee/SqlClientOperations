using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace SqlClientOperations
{
    class Program
    {
        private static IConfiguration _iconfiguration;

        static void Main(string[] args)
        {
            GetAppSettingsFile();
            UserOperation();
            Console.WriteLine("Hello World!");
        }
        static void GetAppSettingsFile()
        {
            var builder = new ConfigurationBuilder()
                                 .SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _iconfiguration = builder.Build();
        }
        static void UserOperation()
        {
            var userQueryProcessors = new UserQueryProcessors(_iconfiguration);
            // SELECT
            var users = userQueryProcessors.Select();
            foreach (var user in users)
            {
                Console.WriteLine("Name: " + user.Name + ", Age: " + user.Age);
            }

            // INSERT
            var userModel = new User() { Id = 2, Name = "Huseyin", Age = 26 };
            userQueryProcessors.Insert(userModel);

            // UPDATE
            var userUpdateModel = new User() { Name = "Hüseyin", Age = 27 };
            userQueryProcessors.Update(2, userUpdateModel);

            //DELETE
            userQueryProcessors.Delete(2);
        }
    }
}
