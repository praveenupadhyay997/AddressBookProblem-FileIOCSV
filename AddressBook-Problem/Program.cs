// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator Name="Praveen Kumar Upadhyay"/>
// --------------------------------------------------------------------------------------------------------------------
namespace AddressBookProblem
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Interface for The Entire address book
    /// </summary>
    public interface IAddressBook
    {
        void AddOrAccessAddressBook();
        void ViewAllAddressBooks();
        void DeleteAddressBook();
    }
    
    public class Program
    {
        public static void GuidanceMenu()
        {
            AddressBookDetail addressBookDetail = new AddressBookDetail();
            // Driving the Execution through menu guidation
            Console.WriteLine("***************************");
            Console.WriteLine("Welcome to the Address Book");
            Console.WriteLine("****************************");
            Console.WriteLine("1. Add or Access the Address Book");
            Console.WriteLine("2. Display the present address Books");
            Console.WriteLine("3. Delete the address book");
            Console.WriteLine("4. Check if duplicate exist of the address book");
            Console.WriteLine("5. List Contacts in the address book by State");
            Console.WriteLine("5. List Contacts in the address book by City");
            Console.WriteLine("Press any other Key to Exit!!!!!!!");

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    addressBookDetail.AddOrAccessAddressBook();
                    Console.Clear();
                    break;

                case 2:
                    addressBookDetail.ViewAllAddressBooks();
                    Console.Clear();
                    break;

                case 3:
                    addressBookDetail.DeleteAddressBook();
                    Console.Clear();
                    break;

                case 4:
                    Console.WriteLine("\n Enter the name of the address book whose duplicate you have to search");
                    string addressBookName = Console.ReadLine().ToLower();
                    addressBookDetail.DuplicateCheck(addressBookName);
                    break;

                case 5:
                    addressBookDetail.TraverseAllAddressBooksToOrderByState();
                    break;

                case 6:
                    addressBookDetail.TraverseAllAddressBooksToOrderByCity();
                    break;

                default:
                    Console.WriteLine("Enter the correct choice Please!!!!!!");
                    break;               
            }
            GuidanceMenu();
        }
        static void Main(string[] args)
        {
            GuidanceMenu();         
        }
    }
}
