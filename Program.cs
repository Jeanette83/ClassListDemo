/*
    Purpose: 1. Demonstrate classes in a separate directory
             2. Load a file into a List<T>
             3. Add to the List<T>
             4. Sort the List<T>; LastName, FirstName
             5. Display the List<T>
             6. Write the List<T> to a different file
    Input:   Students.csv
    Output:  ClassList.csv
    Author:  
    Date:    
*/

using ClassListDemo.Classes; // needed as the Student class is in a separate folder

namespace ClassListDemo
{
    internal class Program
    {
        #region Class Variables & Constants
        // class level constants; make sure you update the path to these files!
        const string InputPathAndFile = @"D:\Work\ClassStudents.csv";
        const string OutputPathAndFile = @"D:\Work\ClassList.csv";

        // class level variables
        static int id = 1000; //this is the starting Student ID it will be incremented for each new Student
        #endregion

        static void Main()
        {
            Setup();
            List<Student> students = new List<Student>();
            char addAnother;
            //1. Check if the file exists before reading
            if (File.Exists(InputPathAndFile))
            {
                //2. Load the List<Student> from the input file
                LoadFromFile(students);
                
                Console.WriteLine("Original Class List");
                //3. Display the original List<Student>
                DisplayClassList(students); 
                
                //4. Sort the original List<Student> by Lastname then firstname
                Console.WriteLine("\nSorted Class List");
                SortClassList(students);
                Console.WriteLine();

                //5. Display the original list now sorted

                Console.WriteLine();
                //6. Loop to add more students to the List<Student>
                do
                {
                    //7. Call the AddStudent() method
                    AddStudent(students);   
                    //8. Prompt to add another student
                    Console.Write("Add another new student (Y): ");
                    addAnother = char.Parse(Console.ReadLine().ToUpper().Substring(0,1));
                } while (addAnother == 'Y');

                //9. Display the updated List<Student> which will be sorted
                Console.WriteLine("\nUpdated Class List");
                SortClassList(students);
                DisplayClassList(students);

                //10. Write the updated List<Student> to the ouput file and display an information message
                Console.WriteLine("\nWrite to File");
                WriteListToFile(students);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"{students.Count} rows written to {OutputPathAndFile}");
                Console.ForegroundColor= ConsoleColor.Black; 
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"The file, {InputPathAndFile}, does not exist");
                Console.ForegroundColor = ConsoleColor.Black;
            }

            Console.ReadLine();
        }//end of Main

        #region User Defined Methods
        //A. Load the List<Student> from the input file
        static void LoadFromFile(List<Student> students)
        {
            string lastName,
                firstname,
                input;
            StreamReader reader = null;
            try
            {
                //a. configure the reader
                reader = File.OpenText(InputPathAndFile);
                
                //b. loop to the end of the file
                while ((input = reader.ReadLine()) != null)
                {
                    //c. split the input on the ','
                    string[] parts = input.Split(',');
                    //d. add the new Student to the List<Student>
                    students.Add(new Student(id, parts[0], parts[1]));

                    //increment the id
                    id++;
                    
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"ERROR: {ex.Message}");
                Console.ForegroundColor = ConsoleColor.Black;
            }
            finally
            {
                reader.Close();
            }
        }//end of LoadFromFile

        //B. Display the List<Student> with column headers 
        static void DisplayClassList(List<Student>students)
        {
            //column headers
            Console.WriteLine($"{"ID", -10}{"LastName", -30}{"FirstName"}");

            //lloop thru
            foreach (Student student in students)
            {
                Console.WriteLine(student);
            }
           
        }//end of DisplayClassList

        //C. Sort the List<Student> by LastName then FirstName
        static void SortClassList(List<Student> students)
        {
            students.Sort((first, second) =>first.LastName.CompareTo(second.FullName));
        }//end of SortClassList

        //D. Add a new Student to the List<Student>
        static void AddStudent(List<Student> students)
        {
            //a. get the first and last names using GetSafeString(string prompt)
            string lastName = GetSafeString("  Enter new student's last name: ");
            string firstName = GetSafeString(" Enter new student's first name: ");
            //b. Add the new Student to the List<Student>
            students.Add(new Student(id, lastName, firstName));
            //c. do not forget to increment the id!
            id++;
        }

        //E. Write the updated List<Student> to the output file
        static void WriteListToFile(List<Student> students)
        {
            StreamWriter writer = null;
            try
            {
                //a. configure the writer to write to the output file
                writer = File.CreateText(OutputPathAndFile);

                //b. loop to write a formatted output string to the file
                foreach (Student student in students)
                { 
                    writer.WriteLine($"{student.ID}, {student.LastName}, {student.FirstName}");
                }
               
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"ERROR: {ex.Message}");
                Console.ForegroundColor = ConsoleColor.Black;
            }
            finally
            {
                writer.Close();
            }
        }//end of WriteListToFile
        #endregion

        #region Provided Methods - No Need to modify
        static string GetSafeString(string prompt)
        {
            bool isValid = false;
            string text;
            do
            {
                Console.Write(prompt);
                text = Console.ReadLine();
                if (text.Length >= 2)
                {
                    isValid = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"The name entered, {text}, is too short (min 2 letters) ... try again!");
                    Console.ForegroundColor = ConsoleColor.Black;
                }
            } while (!isValid);
            return text;
        }//end of GetSafeString

        static void Setup()
        {
            Console.Title = $"Class List Demo ({DateTime.Now})";
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
        }//end of Setup
        #endregion
    }
}