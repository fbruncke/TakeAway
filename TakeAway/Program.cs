using Microsoft.AspNetCore.Http.HttpResults;
using System.Runtime.CompilerServices;
using System.Text.Json;
using TakeAway.Model;

public class Program
{ 
    private static List<Item> menu = new List<Item>();

    private static List<Order> orders = new List<Order>();

    private static List<Customer> customers = new List<Customer>();

    public static void Main(String[] args) { 
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.MapGet("/", () => "Hello World!");

        BuildMenu();
        BuildFakeCustomers();
        BuildFakeOrders();

        app.MapGet("/menu", () => {
            //Order order =new Order { Id = 1, Description = "Large Fries" };
            string jsonString = JsonSerializer.Serialize(menu);
            return jsonString;

        });

        app.MapGet("/order/{id}", (int id) => {
            if (id > orders.Count - 1)
                return JsonSerializer.Serialize(StatusCodes.Status204NoContent);

            string jsonString = JsonSerializer.Serialize(orders[id]);
            return jsonString;

        });

        app.Run();
    }

    private static void BuildMenu()
    {
        int id = 1;
        menu.Add(new Item{ Id = id++, Description = "Large Fries", Price=25 } );
        menu.Add(new Item { Id = id++, Description = "Small Fries", Price = 15 });
        menu.Add(new Item { Id = id++, Description = "Hamburger", Price = 35 });
        menu.Add(new Item { Id = id++, Description = "Coca cola", Price = 20 });
    }

    private static void BuildFakeCustomers()
    {
        int id = 1;
        customers.Add(new Customer { CustomerId = id++, Name = "Irmgard Von Neugarden", Adress = "NyVej" });
        customers.Add(new Customer { CustomerId = id++, Name = "Valtraud Hintergeld", Adress = "GammelVej" });
        customers.Add(new Customer { CustomerId = id++, Name = "Valiant Von Grussgod", Adress = "HalvgammelVej" });
    }

    private static void BuildFakeOrders()
    {
        int id = 1;
        List<Item> it = new List<Item>();
        it.Add(menu[1]);
        it.Add(menu[2]);
        orders.Add(new Order { Id = id++, Items=it, Price=it.Sum(x=>x.Price), OrderCustomer= customers[1] });


        List<Item> it2 = new List<Item>();
        it2.Add(menu[0]);
        it2.Add(menu[3]);
        orders.Add(new Order { Id = id++, Items = it2, Price = it2.Sum(x => x.Price), OrderCustomer = customers[0] });
    }
}