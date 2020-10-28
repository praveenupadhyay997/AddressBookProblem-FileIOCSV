// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddressBookDetail.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator Name="Praveen Kumar Upadhyay"/>
// --------------------------------------------------------------------------------------------------------------------
namespace AddressBookProblem
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Text;
    /// <summary>
    /// This is the higher order directory class containing value like dictionary and key input like address book
    /// </summary>
    /// <seealso cref="AddressBookProblem.IAddressBook" />
    public class AddressBookDetail : IAddressBook
    {
        string addressBookName;
        const int ADD_CONTACT = 1;
        const int EDIT_CONTACT = 2;
        const int DELETE_CONTACT = 3;
        const int GET_ALL_CONTACTS =4;
        const int GET_ALL_CONTACTS_BY_STATE = 5;
        const int GET_ALL_CONTACTS_BY_CITY = 6;
        const int GET_COUNT_BY_STATE = 7;
        const int GET_COUNT_BY_CITY = 8;
        const int FILE_IO = 9;

        /// <summary>
        /// Dictionary to store key as the address book name and the value as instance of the address book class
        /// </summary>
        public static Dictionary<string, AddressBook> addressBookList = new Dictionary<string, AddressBook>();
       
        /// <summary>
        /// Return the instance of address book class when we are done with accessing the details of the address book
        /// </summary>
        /// <returns></returns>
        public AddressBook GetAddressBook()
        {
            Console.WriteLine("\nEnter name of Address Book to be accessed or to be added");
            addressBookName = Console.ReadLine().ToLower();

            //Searcing for address book in dictionary
            if (addressBookList.ContainsKey(addressBookName))
            {
                Console.WriteLine("\nAddressBook Identified");
                return addressBookList[addressBookName];
            }

            //Offer to create a address book if not found in dictionary
            else
            {
                Console.WriteLine("\nAddress book not found. Type y to create a new address book or n to exit");
                if ((Console.ReadLine() == "y" || Console.ReadLine() == "Y"))
                {
                    AddressBook addressBook = new AddressBook(addressBookName);
                    addressBookList.Add(addressBookName, addressBook);
                    Console.WriteLine("\nNew Address Book Created");
                    return addressBookList[addressBookName];
                }
                else
                {
                    Console.WriteLine("\nAction Aborted");
                    return null;
                }
            }
        }

        /// <summary>
        /// Adds the or access address book.
        /// </summary>
        public void AddOrAccessAddressBook()
        {
            AddressBook addressBook = GetAddressBook();
            // Condition to check whether the address book returned by the Get address book function returns a null
            if (addressBook == null)
            {
                Console.WriteLine("Action aborted");
                return;
            }

            Outer:
            Console.WriteLine("******************************************");
            Console.WriteLine("Welcome to the {0}'s Address Book", addressBookName.ToUpper());
            Console.WriteLine("******************************************");
            Console.WriteLine("1. Create A New Contact");
            Console.WriteLine("2. Edit a contact");
            Console.WriteLine("3. Delete a contact");
            Console.WriteLine("4. Display Stored Contact");
            Console.WriteLine("5. Display Contact Name as per State");
            Console.WriteLine("6. Display Contact Name as per City");
            Console.WriteLine("7. Display Count of contact as per State");
            Console.WriteLine("8. Display Count of contact as per City");
            Console.WriteLine("9. Handle File Input Output Operation");
            Console.WriteLine("Press any Key to Exit!!!!!!!");

            switch (Convert.ToInt32(Console.ReadLine().ToLower()))
            {
                case ADD_CONTACT:
                    addressBook.AddContact();
                    break;

                case EDIT_CONTACT:
                    addressBook.EditContactDetails();
                    break;

                case GET_ALL_CONTACTS:
                    addressBook.DisplayDetails();
                    break;

                case DELETE_CONTACT:
                    addressBook.DeleteDetails();
                    break;

                case GET_ALL_CONTACTS_BY_STATE:
                    addressBook.DisplayByState();
                    break;

                case GET_ALL_CONTACTS_BY_CITY:
                    addressBook.DisplayByCity();
                    break;

                case GET_COUNT_BY_STATE:
                    addressBook.DisplayCountByState();
                    break;

                case GET_COUNT_BY_CITY:
                    addressBook.DisplayCountByCity();
                    break;

                case FILE_IO:
                    FileReadWriteClass.GuidanceToFileIO(addressBook);
                    break;

                default:
                    Console.WriteLine("\nInvalid option. Exiting from the address book");
                    return;
            }

            Console.WriteLine("\nType y to continue in same address Book or any other key to exit");
            if (!(Console.ReadLine().ToLower() == "y"))
            {
                return;
            }
            else
                goto Outer;
        }

        /// <summary>
        /// Views all address books.
        /// </summary>
        public void ViewAllAddressBooks()
        {
            if (addressBookList.Count == 0)
                Console.WriteLine("No record found");
            else
            {
                Console.WriteLine("\nThe namesof address books available are :");
                foreach (KeyValuePair<string, AddressBook> keyValuePair in addressBookList)
                    Console.WriteLine(keyValuePair.Key);
            }
        }

        /// <summary>
        /// Deletes the address book.
        /// </summary>
        public void DeleteAddressBook()
        {
            // Counting the number of records present in the address book list to initiate the process of record deletion
            if (addressBookList.Count == 0)
                Console.WriteLine("No record in the Address Book. Enter some record via the main menu.");
            else
            {
                Console.WriteLine("\nEnter the name of address book to be deleted ==>>");
                addressBookName = Console.ReadLine();
                //Searching for address book with given name using exception handling as this process is prone to no count error
                try
                {
                    addressBookList.Remove(addressBookName);
                    Console.WriteLine("Address book was deleted with success");                   
                }
                catch
                {
                    Console.WriteLine("Address book not found");
                }
            }
        }
        /// <summary>
        /// Check for the existence of the duplicate of the address book in the directory
        /// </summary>
        /// <param name="addressBookName"></param>
        public void DuplicateCheck(string addressBookName)
        {
            if (addressBookList.ContainsKey(addressBookName))
            {
                Console.WriteLine("\nAddressBook Identified");
                Console.WriteLine("\n Duplicate exists");
            }
            else
                Console.WriteLine("Address Book does not exist");
        }

        /// <summary>
        /// Traverse all address books to get the contact details as per state or city
        /// </summary>
        public void TraverseAllAddressBooksToOrderByState()
        {
            //Dictionary to store list of contacts by the name of state
            Dictionary<string, List<string>> nameByState = new Dictionary<string, List<string>>();
            List<string> distinctState = new List<string>();
            //Check the count exception case in begining only
            if (addressBookList.Count == 0)
                Console.WriteLine("No record found");
            else
            {
                foreach (KeyValuePair<string, AddressBook> keyValuePair in addressBookList)
                {
                    //Creating an instance of the address book to store the key
                    AddressBook addressBook = new AddressBook(keyValuePair.Key);
                    List<ContactDetails> contacts = keyValuePair.Value.contactList;
                    foreach (ContactDetails details in contacts)
                    {
                        //Adding only the distinct state
                        if (distinctState.Contains(details.state))
                            continue;
                        else
                            distinctState.Add(details.state);
                    }                                                        
                }
                //Reiterating the distinct state for adding the contacts
                foreach(var stateName in distinctState)
                {
                    // String list aimed to add the contact values as per distinct state name
                    List<string> contactValues = new List<string>();
                    foreach (KeyValuePair<string, AddressBook> keyValuePair in addressBookList)
                    {
                        AddressBook addressBook = new AddressBook(keyValuePair.Key);
                        List<ContactDetails> contacts = keyValuePair.Value.contactList;
                        foreach (var contactInAddressBook in contacts)
                        {
                            //Adding the contacts in case the state name matches the distinct state list element
                            if (contactInAddressBook.state == stateName)
                                contactValues.Add(contactInAddressBook.firstName + " \t" + contactInAddressBook.secondName);
                        }
                    }
                    nameByState.Add(stateName, contactValues);
                }
                //Displaying the name of the contact stored in the dictionary
                foreach (var dictionaryElement in nameByState)
                {
                    Console.WriteLine("================" + dictionaryElement.Key + "================");
                    List<string> name = dictionaryElement.Value;
                    foreach (string contactName in name)
                        Console.WriteLine(contactName + "\n");
                }
            }
        }

        /// <summary>
        /// Traverse all address books to get the contact details as per state or city
        /// </summary>
        public void TraverseAllAddressBooksToOrderByCity()
        {
            //Dictionary to store the list of contacts as per city name
            Dictionary<string, List<string>> nameByCity = new Dictionary<string, List<string>>();
            List<string> distinctCity = new List<string>();
            if (addressBookList.Count == 0)
                Console.WriteLine("No record found");
            else
            {
                //Address Book traversing to get distinct name
                foreach (KeyValuePair<string, AddressBook> keyValuePair in addressBookList)
                {
                    //Creating instance of the address book usingthe key value of the address book
                    AddressBook addressBook = new AddressBook(keyValuePair.Key);
                    List<ContactDetails> contacts = keyValuePair.Value.contactList;
                    foreach (ContactDetails details in contacts)
                    {
                        //Adding only the distinct city
                        //In if using the distinct case element
                        //Else add to the city
                        if (distinctCity.Contains(details.city))
                            continue;
                        else
                            distinctCity.Add(details.city);
                    }
                }
                //Iterating the city name  in distinct city list and then iterating the entire address book to add list of contacts
                foreach (var cityName in distinctCity)
                {
                    List<string> contactValues = new List<string>();
                    foreach (KeyValuePair<string, AddressBook> keyValuePair in addressBookList)
                    {
                        AddressBook addressBook = new AddressBook(keyValuePair.Key);
                        List<ContactDetails> contacts = keyValuePair.Value.contactList;
                        //Adding the contact list in the dictionary
                        foreach (var contactInAddressBook in contacts)
                            if (contactInAddressBook.city == cityName)
                                contactValues.Add(contactInAddressBook.firstName + " \t" + contactInAddressBook.secondName);
                    }
                    nameByCity.Add(cityName, contactValues);
                }
                //Traversing the dictionary to display the entire mapped set of city and contact name
                foreach (var dictionaryElement in nameByCity)
                {
                    Console.WriteLine("================" + dictionaryElement.Key + "================");
                    List<string> name = dictionaryElement.Value;
                    foreach (string contactName in name)
                        Console.WriteLine(contactName + "\n");
                }
            }
        }
    }
}
