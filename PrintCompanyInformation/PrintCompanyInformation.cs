using System;

class PrintCompanyInformation
{
    static void Main()
    {
        Console.WriteLine("Enter company Name:");
        string companyName = Console.ReadLine();
        Console.WriteLine("Enter company's adress:");
        string companyAddress = Console.ReadLine();
        Console.WriteLine("Enter company's phone number:");
        string companyNumber = Console.ReadLine();
        Console.WriteLine("Enter company's fax number:");
        string companyFax = Console.ReadLine();
        Console.WriteLine("Enter company's web site:");
        string companySite = Console.ReadLine();
        Console.WriteLine("Enter manager's first name:");
        string managerFirstName = Console.ReadLine();
        Console.WriteLine("Enter manager's last name:");
        string managerLastName = Console.ReadLine();
        Console.WriteLine("Enter manager's age:");
        int managerAge = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter manager's phone number:");
        string managerNumber = Console.ReadLine();

        Console.WriteLine("Company name: {0}", companyName);
        Console.WriteLine("Company address: {0}", companyAddress);
        Console.WriteLine("Company number: {0}", companyNumber);
        Console.WriteLine("Company fax: {0}", companyFax);
        Console.WriteLine("Web site: {0}", companySite);
        Console.WriteLine("Manager: {0} {1} (age: {2}, tel. {3})",
            managerFirstName,
            managerLastName,
            managerAge,
            managerNumber);
    }
}

