using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using CsvHelper;
using System.Linq;
using CsvHelper.Configuration;

namespace AddressBookProblem
{
    public class FileReadWriteClass
    {
        const int READ_TXT = 1;
        const int WRITE_TXT = 2;
        const int READ_CSV = 3;
        const int WRITE_CSV = 4;

        /// <summary>
        /// Guidance menu for the file IO Operation
        /// </summary>
        /// <param name="addressBook"></param>
        public static void GuidanceToFileIO(AddressBook addressBook)
        {
            Console.WriteLine("============================================================================");
            Console.WriteLine($"Welcome to the File IO Process for {addressBook.nameOfAddressBook.ToUpper()}'s Address Book");
            Console.WriteLine("============================================================================");
            Console.WriteLine("1.Read the Text IO File");
            Console.WriteLine("2.Write to the Text IO File");
            Console.WriteLine("3.Read the CSV IO File");
            Console.WriteLine("4.Write to the CSV IO File");

            int choice = Convert.ToInt32(Console.ReadLine());
            switch(choice)
            {
                case READ_TXT:
                    ReadBinaryTextFile();
                    break;

                case WRITE_TXT:
                    WriteBinaryTextFile(addressBook);
                    break;

                case READ_CSV:
                    ReadCSVFile(addressBook);
                    break;

                case WRITE_CSV:
                    WriteToCSVFile(addressBook);
                    break;
                default:
                    Console.WriteLine("Invalid Choice... Kindly enterthe right choice...");
                    Console.Clear();
                    GuidanceToFileIO(addressBook);
                    break;
            }

        }
        /// <summary>
        /// UC13 - Writing and reading operation onto a text file
        /// </summary>
        public static void ReadBinaryTextFile()
        {
            string path = @"F:\Program files(x64)\Microsoft Visual Studio\BridgeLabzAssignments\AddressBookProblem-FileIOCSV\AddressBookDictionary.txt";
            if (File.Exists(path))
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    string line = "";
                    while ((line = sr.ReadLine()) != null)
                        Console.WriteLine(line); ;
                }
            }
            else
                Console.WriteLine("File not found");
        }
        /// <summary>
        /// UC13 - Writing and reading operation onto a text file
        /// </summary>
        /// <param name="addressBook"></param>
        public static void WriteBinaryTextFile(AddressBook addressBook)
        {
            string path = @"F:\Program files(x64)\Microsoft Visual Studio\BridgeLabzAssignments\AddressBookProblem-FileIOCSV\AddressBookDictionary.txt";
            if (File.Exists(path))
            {
                //Quick Creation of the object 
                using (StreamWriter streamObject = File.AppendText(path))
                {
                    /// Writes the entered string into the file
                    streamObject.Write("\nContact inside the address book of: {0}=>\n", addressBook.nameOfAddressBook.ToUpper());
                    for (int i = 0; i < addressBook.contactList.Count; i++)
                    {
                        //The entire string line which is to be written in the file stream
                        string line = "\n" + (i + 1) + ".\tFullName: " + addressBook.contactList[i].firstName + " " 
                            + addressBook.contactList[i].secondName + "\n\tAddress: " + addressBook.contactList[i].address 
                            + "\n\tCity: " + addressBook.contactList[i].city + "\n\tState: " + addressBook.contactList[i].state +
                            "\n\tZip: " + addressBook.contactList[i].zip + "\n\tPhoneNumber: " + addressBook.contactList[i].phoneNumber 
                            + "\n\tEmail: " + addressBook.contactList[i].emailId + "\n";
                        streamObject.Write(line);
                    }
                }
            }
        }
        /// <summary>
        /// UC14- Reading the stored contacts to the address book file
        /// </summary>
        /// <param name="addressBook"></param>
        public static void ReadCSVFile(AddressBook addressBook)
        {
            try
            {
                string csvFilePath = @$"F:\Program files(x64)\Microsoft Visual Studio\BridgeLabzAssignments\AddressBookProblem-FileIOCSV\{addressBook.nameOfAddressBook}AddressBookCSV.csv";
                /// Create a new object of the StreamReader class and initialise the file path
                var reader = new StreamReader(csvFilePath);
                /// Creates a new CSV reader instance
                var csv = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture);
                csv.Configuration.Delimiter = "," ;
                /// Store the records in the records to be invoked by GetRecords method
                var records = csv.GetRecords<ContactDetails>().ToList();
                /// Iterating over the records to display the contacts
                foreach (ContactDetails contact in records)
                {
                    Console.WriteLine("\nFullName: " + contact.firstName + " " + contact.secondName + "\nAddress: " + 
                        contact.address + "\nCity: " + contact.city + "\nState: " + contact.state + "\nZip: " + contact.zip + 
                        "\nPhoneNumber: " + contact.phoneNumber + "\nEmail: " + contact.emailId + "\n");
                }
                /// Close the stream reader so as to avoid any conflict with reopen
                reader.Close();
            }
            catch (Exception exception)
            {
                /// If the file is not present at the path you need to create new file at that path
                Console.WriteLine(exception.Message);
                Console.WriteLine("File you are trying to access does not exist please create one");
            }
        }
        /// <summary>
        /// UC14- Writing to the CSV file using CsvHelper
        /// </summary>
        /// <param name="addressBook"></param>
        public static void WriteToCSVFile(AddressBook addressBook)
        {
            string csvFilePath = @$"F:\Program files(x64)\Microsoft Visual Studio\BridgeLabzAssignments\AddressBookProblem-FileIOCSV\{addressBook.nameOfAddressBook}AddressBookCSV.csv";
            /// Initialize an instance of StreamWriter class to perform write operation
            using (StreamWriter sw = new StreamWriter(csvFilePath))
            {
                var csv = new CsvHelper.CsvWriter(sw, CultureInfo.InvariantCulture);
                csv.Configuration.MemberTypes = CsvHelper.Configuration.MemberTypes.Fields;
                csv.WriteRecords(addressBook.contactList);
                sw.Flush();
            }
        }
    }
}
