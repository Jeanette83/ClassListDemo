/*
    +----------------------------------------------------------------+
    |                            Student                             |
    +----------------------------------------------------------------+
    | - id : Integer                                                 |
    | - lastName : String                                            |
    | - firstName : String                                           |
    | + ID : Integer                                                 |
    | + LastName : String                                            |
    | + FirstName : String                                           |
    +----------------------------------------------------------------+
    | + Student(ID : Integer, LastName : String, FirstName : String) |
    | + ToString() : String                                          |
    +----------------------------------------------------------------+
*/
using System.ComponentModel.DataAnnotations;

namespace ClassListDemo.Classes
{
    public class Student
    {
        //private member fields
        private int _id;
        private string _lastName;
        private string _firstName;

        //These are auto-implmented properties and MUST be changed to 
        // fully implemented public properties
        public int ID
        {
            get { return _id; }
            set
            {
                if (value >= 1000)
                {
                    _id = value;
                }
                else
                {
                    throw new Exception("Invalid Student ID");
                }
            
            }
        }//end of ID

        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (value.Length >= 2)
                {
                    _lastName = value;
                }
                else
                {
                    throw new Exception("Invalid Student LastName");
                }
            }
        }//end of LastName

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (value.Length >= 2)
                {
                    _firstName = value; 
                }
                else
                {

                    throw new Exception("Invalid Student First Name");
                }
            }
        }//end
         //of FirstName

        //public READ-ONLY property
        public string FullName
        {
            get { return $"{LastName},{FirstName}"; }
        }

        //Greedy Constructor
        public Student(int id, string lastName, string firstName)
        {
            ID = id;
            LastName = lastName;
            FirstName = firstName;
        }//end of Student

        //Class Method
        public override string ToString()
        {
            return $"{ID,-10}{LastName,-30}{FirstName}";
        }//end of ToString
    }
}
