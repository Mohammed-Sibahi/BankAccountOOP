// Import necessary namespaces
using System;
using System.Collections.Generic;
using System.Threading;

// Define the Bank class
public class Bank
{
    // Properties to store the bank name and a list of accounts
    public string Name { get; set; }
    public List<Account> Accounts { get; set; }

    // Constructor to initialize the bank with a name
    public Bank(string name)
    {
        Name = name;
        Accounts = new List<Account>();
    }

    // Method to simulate the process of opening an account
    public void OpenAccount()
    {
        // Display a header for the account opening process
        Console.WriteLine("=== Account Opening ===");

        // Prompt the user to enter personal information
        Console.Write("Enter your name: ");
        string name = Console.ReadLine();

        Console.Write("Enter your occupation: ");
        string occupation = Console.ReadLine();

        Console.Write("Enter the name of the company you work for: ");
        string companyName = Console.ReadLine();

        Console.Write("Enter your monthly salary: ");
        decimal monthlySalary;
        // Validate and ensure a non-negative monthly salary
        while (!decimal.TryParse(Console.ReadLine(), out monthlySalary) || monthlySalary < 0)
        {
            Console.Write("Invalid input. Please enter a valid monthly salary: ");
        }

        Console.Write("Enter your age: ");
        int age;
        // Validate and ensure the user is 18 or older
        while (!int.TryParse(Console.ReadLine(), out age) || age < 18)
        {
            Console.Write("Invalid input. Please enter a valid age (must be 18 or older): ");
        }

        Console.Write("Enter your email address: ");
        string emailAddress = Console.ReadLine();

        Console.Write("Enter your address: ");
        string address = Console.ReadLine();

        Console.Write("Enter your mobile number: ");
        string mobileNumber = Console.ReadLine();

        // Check if salary is less than 5000, deny account opening if true
        if (monthlySalary < 5000)
        {
            Console.WriteLine("Account opening request turned down. Monthly salary must be 5000 or more.");
            return;
        }

        // Create a Customer object with the provided information
        Customer customer = new Customer(name, occupation, companyName, monthlySalary, age, emailAddress, address, mobileNumber);

        // Display processing message and simulate a 5-second wait
        Console.WriteLine("Application received. Processing...");
        Thread.Sleep(5000);

        // Open an account with initial balance set to the monthly salary
        Account myAccount = new Account(customer);
        Accounts.Add(myAccount);

        // Display success message and customer information
        Console.WriteLine($"Account opened successfully for {customer.Name} at {Name}.");
        Console.WriteLine("Customer Information:");
        customer.DisplayCustomerInfo();
        Console.WriteLine($"Account Number: {myAccount.AccountNumber}");
        Console.WriteLine($"Initial Balance: {myAccount.Balance:C}");

        // Allow the user to perform deposit or withdrawal operations in a loop
        while (true)
        {
            Console.WriteLine("\nChoose an option:");
            Console.WriteLine("1. Deposit");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Exit");

            int choice;
            // Validate and ensure a valid option is selected
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3)
            {
                Console.Write("Invalid input. Please enter a valid option: ");
            }

            // Switch statement to handle user's choice
            switch (choice)
            {
                case 1:
                    Console.Write("Enter the amount to deposit: ");
                    decimal depositAmount;
                    // Validate and ensure a non-negative deposit amount
                    while (!decimal.TryParse(Console.ReadLine(), out depositAmount) || depositAmount < 0)
                    {
                        Console.Write("Invalid input. Please enter a valid deposit amount: ");
                    }
                    myAccount.Deposit(depositAmount);
                    Console.WriteLine($"Deposit completed. New Balance: {myAccount.Balance:C}");
                    break;
                case 2:
                    Console.Write("Enter the amount to withdraw: ");
                    decimal withdrawAmount;
                    // Validate and ensure a non-negative withdrawal amount
                    while (!decimal.TryParse(Console.ReadLine(), out withdrawAmount) || withdrawAmount < 0)
                    {
                        Console.Write("Invalid input. Please enter a valid withdrawal amount: ");
                    }
                    // Check if withdrawal amount is valid based on account balance
                    if (myAccount.Balance >= withdrawAmount)
                    {
                        myAccount.Withdraw(withdrawAmount);
                        Console.WriteLine($"Withdrawal completed. New Balance: {myAccount.Balance:C}");
                    }
                    else
                    {
                        Console.WriteLine("Insufficient funds. Withdrawal request denied.");
                    }
                    break;
                case 3:
                    Console.WriteLine("Exiting...");
                    return;
            }
        }
    }
}

// Define the Account class
public class Account
{
    // Properties for account number, balance, interest rate, and associated customer
    public string AccountNumber { get; set; }
    public decimal Balance { get; set; }
    public double InterestRate { get; set; }
    public Customer Customer { get; set; }

    // Constructor to initialize an account with a customer
    public Account(Customer customer)
    {
        AccountNumber = GenerateAccountNumber(); // Generate a unique account number
        Balance = 0;
        InterestRate = 0.02;
        Customer = customer;
    }

    // Static counter for generating unique account numbers
    private static long accountNumberCounter = 1000000000000000;

    // Method to generate a unique 16-digit account number
    private string GenerateAccountNumber()
    {
        return (accountNumberCounter++).ToString("D16");
    }

    // Method to deposit money into the account
    public void Deposit(decimal amount)
    {
        Balance += amount;
        Console.WriteLine($"Deposited {amount:C} into account {AccountNumber}. New Balance: {Balance:C}.");
    }

    // Method to withdraw money from the account
    public void Withdraw(decimal amount)
    {
        // Check if withdrawal amount is valid based on account balance
        if (amount <= Balance)
        {
            Balance -= amount;
            Console.WriteLine($"Withdrawn {amount:C} from account {AccountNumber}. New Balance: {Balance:C}.");
        }
        else
        {
            Console.WriteLine("Insufficient funds.");
        }
    }
}

// Define the Customer class
public class Customer
{
    // Properties for customer ID and personal information
    public int CustomerId { get; private set; }
    public string Name { get; private set; }
    public string Occupation { get; private set; }
    public string CompanyName { get; private set; }
    public decimal MonthlySalary { get; private set; }
    public int Age { get; private set; }
    public string EmailAddress { get; private set; }
    public string Address { get; private set; }
    public string MobileNumber { get; private set; }

    // Constructor to initialize a customer with personal information
    public Customer(string name, string occupation, string companyName, decimal monthlySalary, int age, string emailAddress, string address, string mobileNumber)
    {
        CustomerId = GenerateCustomerId(); // Generate a unique customer ID
        Name = name;
        Occupation = occupation;
        CompanyName = companyName;
        MonthlySalary = monthlySalary;
        Age = age;
        EmailAddress = emailAddress;
        Address = address;
        MobileNumber = mobileNumber;
    }

    // Static counter for generating unique customer IDs
    private static int customerIdCounter = 1;

    // Method to generate a unique customer ID
    private int GenerateCustomerId()
    {
        return customerIdCounter++;
    }

    // Method to display customer information
    public void DisplayCustomerInfo()
    {
        Console.WriteLine($"Customer ID: {CustomerId}");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Occupation: {Occupation}");
        Console.WriteLine($"Company: {CompanyName}");
        Console.WriteLine($"Monthly Salary: {MonthlySalary:C}");
        Console.WriteLine($"Age: {Age}");
        Console.WriteLine($"Email Address: {EmailAddress}");
        Console.WriteLine($"Address: {Address}");
        Console.WriteLine($"Mobile Number: {MobileNumber}");
    }
}

// Define the Program class for the entry point
internal class Program
{
    static void Main(string[] args)
    {
        // Create an instance of the Bank class
        Bank myBank = new Bank("My Bank");

        // Open an account with user details
        myBank.OpenAccount();

        // Read a line to prevent the console from closing immediately
        Console.ReadLine();
    }
}
