﻿using EntityModels.Models;
using Microsoft.EntityFrameworkCore;
using Week3EntityFramework.Dtos;

var context = new IndustryConnectWeek2Context();

//Task-1 Start

// Query to retrieve customers who don't have any sales
var customersWithoutSales = context.Customers
    .Where(c => !c.Sales.Any())  // Ensure no sales exist for the customer
    .ToList();

// Display the customers
foreach (var customer in customersWithoutSales)
{
    Console.WriteLine($"Customer: {customer.FirstName} {customer.LastName}");
}

//Task - 1 END


//Task-2 Start

// Create a new customer
var newCustomer = new Customer
{
    FirstName = "kp",
    LastName = "desai",
    DateOfBirth = DateTime.Now.AddYears(-20) // Assume 25 years old
};

// Create a new sale for this customer
var newSale = new Sale
{
    DateSold = DateTime.Now, // Sale amount
    ProductId = 1,
    StoreId = 1,
    Customer = newCustomer // Link this sale to the new customer
};

// Add the customer and sale to the context
context.Customers.Add(newCustomer);
context.Sales.Add(newSale); // Alternatively, if using navigation properties, EF will automatically add the sale when you add the customer

// Save changes to the database
context.SaveChanges();

Console.WriteLine("New customer and sale record added successfully!");

//Task-2 End

//Task 3//

try
{
    // Create a new store
    var newStore = new Store
    {
        Name = "Store D",
        Location = "London",
    };

    // Add the store to the context
    context.Stores.Add(newStore);

    // Save changes to the database
    context.SaveChanges();
    Console.WriteLine("New store added successfully.");
}
catch (DbUpdateException ex)
{
    Console.WriteLine("An error occurred while saving changes to the database:");
    Console.WriteLine(ex.InnerException?.Message);
}
catch (Exception ex)
{
    Console.WriteLine("An unexpected error occurred:");
    Console.WriteLine(ex.Message);
}

//Task3 END



//var customer = new Customer
//{
//    DateOfBirth = DateTime.Now.AddYears(-20)
//};


//Console.WriteLine("Please enter the customer firstname?");

//customer.FirstName = Console.ReadLine();

//Console.WriteLine("Please enter the customer lastname?");

//customer.LastName = Console.ReadLine();


//var customers = context.Customers.ToList();

//foreach (Customer c in customers)
//{   
//    Console.WriteLine("Hello I'm " + c.FirstName);
//}

//Console.WriteLine($"Your new customer is {customer.FirstName} {customer.LastName}");

//Console.WriteLine("Do you want to save this customer to the database?");

//var response = Console.ReadLine();

//if (response?.ToLower() == "y")
//{
//    context.Customers.Add(customer);
//    context.SaveChanges();
//}



var sales = context.Sales.Include(c => c.Customer)
    .Include(p => p.Product).ToList();

var salesDto = new List<SaleDto>();

foreach (Sale s in sales)
{
    salesDto.Add(new SaleDto(s));
}



//context.Sales.Add(new Sale
//{
//    ProductId = 1,
//    CustomerId = 1,
//    StoreId = 1,
//    DateSold = DateTime.Now
//});


//context.SaveChanges();




Console.WriteLine("Which customer record would you like to update?");

var response = Convert.ToInt32(Console.ReadLine());

var customer = context.Customers.Include(s => s.Sales)
    .ThenInclude(p => p.Product)
    .FirstOrDefault(c => c.Id == response);


var total = customer.Sales.Select(s => s.Product.Price).Sum();


var customerSales = context.CustomerSales.ToList();

//var totalsales = customer.Sales



//Console.WriteLine($"The customer you have retrieved is {customer?.FirstName} {customer?.LastName}");

//Console.WriteLine($"Would you like to updated the firstname? y/n");

//var updateResponse = Console.ReadLine();

//if (updateResponse?.ToLower() == "y")
//{

//    Console.WriteLine($"Please enter the new name");

//    customer.FirstName = Console.ReadLine();
//    context.Customers.Add(customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
//    context.SaveChanges();
//}



Console.ReadLine();









