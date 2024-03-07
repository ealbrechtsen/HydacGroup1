using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelPersistence.Model
{
    public class Employee
    {
        public string FirstName;
        public string Lastname;
        public string CPRNum;
        public string PhoneNum;
        public string Email;
        public string Address;
        public string EmploymentStatus;
        
        public Employee(string FirstName, string LastName, string CPRNum, string PhoneNum, string Email, string Address)
        {
            this.FirstName = FirstName;
            this.Lastname = LastName;
            this.CPRNum = CPRNum;
            this.PhoneNum = PhoneNum;
            this.Email = Email;
            this.Address = Address;
        }
        //private string firstName;
        //public string FirstName
        //{
        //    get { return firstName; }
        //    set { firstName = value; }
        //}
        //private string lastName;
        //public string LastName
        //{
        //    get { return lastName; }
        //    set { lastName = value; }
        //}
        //private string cPRNum;
        //public string CPRNum
        //{
        //    get { return cPRNum; }
        //    set { cPRNum = value; }
        //}
        //private string phoneNum;
        //public string PhoneNum
        //{
        //    get { return phoneNum; }
        //    set { phoneNum = value; }
        //}
        //private string email;
        //public string Email
        //{
        //    get { return email; }
        //    set { email = value; }
        //}
        //private string address;
        //public string Address
        //{
        //    get { return address; }
        //    set { address = value; }
        //}
        //private string employmentStatus;
        //public string EmployeeloymentStatus
        //{
        //    get { return employmentStatus; }
        //    set { employmentStatus = value; }
        //}
        //public Employee(string firstName, string lastName,string cPRNum, string phoneNum, string email, string address)
        //{
        //    this.firstName = firstName;
        //    this.lastName = lastName;
        //    this.cPRNum = cPRNum;
        //    this.phoneNum = phoneNum;
        //    this.email = email;
        //    this.address = address;
        //}
    }
}
