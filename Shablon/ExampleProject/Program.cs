using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Reflection;
using System.IO;

namespace ExampleProject
{
    class Program
    {
        static void Main(string[] args)
        {

            // Create a model with some sample data
            Models.OrderModel orderModel = new Models.OrderModel()
            {
                Name = "John Smith",
                Address = "12 High Street, London",
                ContactInfo = new NameValueCollection() {
                    { "Phone", "01234567889" },
                    { "Fax", "0987654321" },
                    { "Email", "Shablon@test.net" },
                    { "LinkedIn", "www.linkedin.com/myname" }
                },
                OrderLines = new List<Models.OrderLineModel>()
                {
                    { new Models.OrderLineModel() { ID = 1, Description = "Laptop", Price = 500.00, Quantity = 1, Total = 500.00 } },
                    { new Models.OrderLineModel() { ID = 2, Description = "Mouse", Price = 59.99, Quantity = 1, Total = 59.99 } },
                    { new Models.OrderLineModel() { ID = 3, Description = "Monitors", Price = 139.85, Quantity = 2, Total = 279.70 } }
                },
                Offices = new List<string>() { "London", "Paris", "Kyiv", "L'viv" }
            };

            // Read in the template teaxt file
            string template = ReadFile("ExampleProject.Template.txt");

            // Put the data into the template
            template = Shablon.TemplateProcessor.ProcessTemplate(template, orderModel, "[#", "#]");

            // output to the console
            Console.Write(template);

            Console.WriteLine("\nPress any key to exit..");
            Console.ReadKey(true);
        }


        /// <summary>
        /// Reads a resource and return its contents as a string.
        /// </summary>
        /// <param name="resourceName">The name of the resource to be read</param>
        /// <returns>The resource as a string or String.Empty</returns>
        private static string ReadFile(string resourceName)
        {
            if (resourceName == null) throw new ArgumentNullException("resourceName");
            var assembly = Assembly.GetExecutingAssembly();
            var result = String.Empty;
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null) return result;
                using (var reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }
            return result;
        }
    }
}
