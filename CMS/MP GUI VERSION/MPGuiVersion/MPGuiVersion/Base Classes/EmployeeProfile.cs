using System;
using System.Collections.Generic;
using System.Text;

namespace MPGuiVersion
{
    public class EmployeeProfile
    {
        private int employeeID;
        private Name employeeName = new Name();
        private Address address = new Address();
        private string dateOfBirth;
        private long mobileNum;
        private long landline;
        private long fatherNum;
        private long motherNum;
        private string emailAddress;
        private string nationality;
        private int sex;
        private int maritalStatus;

        private ECList employeeContacts = new ECList();

        public string EmailAddress
        {
            get { return emailAddress; }
            set { emailAddress = value; }
        }
        public long MobileNum
        {
            get { return mobileNum; }
            set { mobileNum = value; }
        }
        public long Landline
        {
            get { return landline; }
            set { landline = value; }
        }
        public long MotherNum
        {
            get { return motherNum; }
            set { motherNum = value; }
        }
        public long FatherNum
        {
            get { return fatherNum; }
            set { fatherNum = value; }
        }

        public int EmployeeID { get => employeeID; set => employeeID = value; }
        public string DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }
        public int Sex { get => sex; set => sex = value; }
        public int MaritalStatus { get => maritalStatus; set => maritalStatus = value; }
        public string Nationality { get => nationality; set => nationality = value; }
        internal Name EmployeeName { get => employeeName; set => employeeName = value; }
        internal Address Address { get => address; set => address = value; }
        internal ECList EmployeeContacts { get => employeeContacts; set => employeeContacts = value; }
    }

    struct ECList
    {
        public EmergencyContact EC1;
        public EmergencyContact EC2;
        public EmergencyContact EC3;
    }

    struct EmergencyContact
    {
        public string firstName;
        public string middleName;
        public string lastName;
        public long contactnumber;
    }

    struct Name
    {
        public string firstName;
        public string middleName;
        public string lastName;
        public string suffix;
    }
    struct Address
    {
        public string BuildingNumber;
        public string CityState;
        public string CountryZIP;
    }
}
