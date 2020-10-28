using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AddressBookProblem
{
    public class FileReadWriteClass
    {
        const int READ_TXT = 1;
        const int WRITE_TXT = 2;
        public static void GuidanceToFileIO(AddressBook addressBook)
        {
            Console.WriteLine("============================================================================");
            Console.WriteLine($"Welcome to the File IO Process for {addressBook.nameOfAddressBook.ToUpper()}'s Address Book");
            Console.WriteLine("============================================================================");
            Console.WriteLine("1.Read the Text IO File");
            Console.WriteLine("2.Write to the Text IO File");

            int choice = Convert.ToInt32(Console.ReadLine());
            switch(choice)
            {
                case READ_TXT:
                    ReadBinaryTextFile();
                    break;

                case WRITE_TXT:
                    WriteBinaryTextFile(addressBook);
                    break;

                default:
                    Console.WriteLine("Invalid Choice... Kindly enterthe right choice...");
                    Console.Clear();
                    break;
            }

        }

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
    }
}
