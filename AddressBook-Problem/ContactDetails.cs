// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContactDetails.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator Name="Praveen Kumar Upadhyay"/>
// --------------------------------------------------------------------------------------------------------------------
namespace AddressBookProblem
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class ContactDetails
    {
        /// <summary>
        /// contact PRoperties mentioned in the Use-Case
        /// </summary>
        public string firstName;
        public string secondName;
        public string address;
        public string city;
        public string state;
        public long zip;
        public long phoneNumber;
        public string emailId;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactDetails"/> class.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="secName">Name of the sec.</param>
        /// <param name="address">The address.</param>
        /// <param name="city">The city.</param>
        /// <param name="state">The state.</param>
        /// <param name="zip">The zip.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="emailId">The email identifier.</param>
        public ContactDetails(string firstName, string secName, string address, string city, string state, long zip, long phoneNumber, string emailId)
        {
            this.firstName = firstName.ToLower();
            this.secondName = secName.ToLower();
            this.address = address;
            this.city = city;
            this.state = state;
            this.zip = zip;
            this.phoneNumber = phoneNumber;
            this.emailId = emailId;
        }
    }
}
