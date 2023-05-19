using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace To_Do_App
{

    internal partial class Program
    {
        public class TaskCounter
        {
            public static int count = 1;

            public static int GetNextID()
            {
                return count++;
            }
        }
            public enum PriorityLevel
        {
            Low,
            Medium,
            High
        }

        static void Main(string[] args)
        {
            List<User> users = new List<User>();
            List<bool> isCompleted = new List<bool>() { false, false, false };
            List<Task> task = new List<Task>();
            User currentUser = null;


            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("                                                  To-Do Application                                                     ");
            Console.ResetColor();
            Console.WriteLine();


            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Choose any of the following options: ");
                Console.WriteLine();
                Console.WriteLine("1.   Register");
                Console.WriteLine("2.   Login");
                Console.WriteLine("3.   Logout");
                Console.WriteLine("4.   Quit");
                Console.WriteLine();

                int command = Convert.ToInt32(Console.ReadLine());
                string name, email, password;


                if (command == 1)
                {
                    Console.WriteLine("User Registration");
                    Console.WriteLine();

                    Console.Write("Name: ");
                    name = Console.ReadLine();

                    Console.Write("Email: ");
                    email = Console.ReadLine();

                    if (!IsValidEmail(email))
                    {
                        Console.WriteLine("Invalid email format.");
                        continue;
                    }

                    Console.Write("Password: ");
                    password = string.Empty;
                    ConsoleKey key;
                 
                        
                  do
                  {
                        var keyInfo = Console.ReadKey(intercept: true);
                        key = keyInfo.Key;

                        if (key == ConsoleKey.Backspace && password.Length > 0)
                        {  
                            Console.Write("\b \b");
                            password = password[0..^1];

                            if (!IsValidPassword(password))
                            {
                                Console.WriteLine("Invalid password. Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one digit.");
                                continue;
                            }

                        }
                    else if (!char.IsControl(keyInfo.KeyChar))
                    {
                            Console.Write("*");
                            password += keyInfo.KeyChar;

                     }
                  }while (key != ConsoleKey.Enter);
                   

                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine("Registration successful.");
                 
                    User user = new User(name, email, password);
                    users.Add(user);

                  while (true)
                  {
                        if (users.Count > 0)
                        {
                            for (int i = 0; i < users.Count; i++)
                            {
                            }
                            Console.WriteLine("");
                        } else
                        {
                            Console.Clear();
                            Console.WriteLine();
                            Console.WriteLine($"Welcome {name}. You currently have no tasks in your To-Do list.");
                            Console.WriteLine("");
                        }
                        
                        Console.WriteLine("1.   Add task");
                        Console.WriteLine("2.   Complete task");
                        Console.WriteLine("3.   Edit Title");
                        Console.WriteLine("4.   Edit Description");
                        Console.WriteLine("5.   Edit date");
                        Console.WriteLine("6.   Edit priority level");
                        Console.WriteLine("7.   Delete task");
                        Console.WriteLine("8.   View task");
                        Console.WriteLine("9.   Exit");
                        Console.WriteLine();
                        int choice = int.Parse(Console.ReadLine());


                        if (choice == 1)
                        {

                            Console.Write("Enter task title: ");
                            string title = Console.ReadLine();

                            Console.Write("Enter task description: ");
                            string description = Console.ReadLine();

                            Console.Write("Enter task due date (dd/mm/yyyy): ");
                            DateTime dueDate = DateTime.Parse(Console.ReadLine());

                            Console.Write("Enter task priority (Low, Medium, High): ");
                            PriorityLevel priority = (PriorityLevel)Enum.Parse(typeof(PriorityLevel), Console.ReadLine(), true);
                            Console.WriteLine();
                            Console.WriteLine("Task added successfully!");

                            Task tasks = new Task(title, description, dueDate, priority);
                            task.Add(tasks);
                            Console.ReadLine();
                        }
                        else if (choice == 9)
                        {
                            Console.WriteLine("Ciao!");
                            break;
                        }
                        else if (choice == 2)
                        {
                            if (users.Count > 0)
                            {
                                Console.WriteLine("Enter the number of the task you want to mark as complete: ");
                                for (int i = 0; i < users.Count; i++)
                                {
                                    Console.WriteLine("(" + (i + 1) + ")" + users[i]);
                                }
                                int taskNum = int.Parse(Console.ReadLine());
                                isCompleted[taskNum] = true;
                                Console.WriteLine();
                                Console.WriteLine("Task marked as complete successfully!");
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Invalid task number.");
                                Console.WriteLine("");
                            }
                        }
                        else if (choice == 3)
                        {
                            if (task.Count > 0)
                            {
                                Console.WriteLine("Enter the number of the task title you want to edit: ");
                                for (int i = 0; i < task.Count; i++)
                                {
                                    Console.WriteLine("(" + (i + 1) + ")" + task[i]);
                                }
                                Task newEdit = task.Find(task => task.Title == task.Title);

                                int taskNum = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter new task title:");
                                string newTitle = Console.ReadLine();
                                newEdit.Title = newTitle;
                                Console.WriteLine();
                                Console.WriteLine("Task title updated successfully!");
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Invalid task number.");
                                Console.WriteLine("");
                            }
                        }
                        else if (choice == 4)
                        {
                            if (task.Count > 0)
                            {
                                Console.WriteLine("Enter the number of the task description you want to edit: ");
                                for (int i = 0; i < task.Count; i++)
                                {
                                    Console.WriteLine("(" + (i + 1) + ")" + task[i]);
                                }
                                Task newEdit = task.Find(task => task.Title == task.Title);
                                int taskNum = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter new task description:");
                                string newDescription = Console.ReadLine();
                                newEdit.Description = newDescription;
                                Console.WriteLine();
                                Console.WriteLine("Task description updated successfully!");
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Invalid task number.");
                                Console.WriteLine("");
                            }
                        }
                        else if (choice == 5)
                        {
                            if (task.Count > 0)
                            {
                                Console.WriteLine("Enter the number of task date to edit: ");
                                for (int i = 0; i < task.Count; i++)
                                {
                                    Console.WriteLine("(" + (i + 1) + ")" + task[i]);
                                }
                                Task newEdit = task.Find(task => task.Title == task.Title);
                                int taskNum = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter new due date (dd-mm-yyyy):");
                                DateTime newDueDate = DateTime.Parse(Console.ReadLine());
                                newEdit.DueDate = newDueDate;
                                Console.WriteLine();
                                Console.WriteLine("Task due date updated successfully!");
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Invalid task number.");
                                Console.WriteLine("");
                            }
                        }
                        else if (choice == 6)
                        {
                            
                            if (task.Count > 0)
                            {
                                Task newEdit = task.Find(task => task.Title == task.Title);
                                Console.WriteLine("Enter the number of the task new priority you want to edit:");
                                int taskNum = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter new priority level (Low, Medium, High) to edit:");
                                PriorityLevel newPriority = (PriorityLevel)Enum.Parse(typeof(PriorityLevel), Console.ReadLine(), true);
                                newEdit.Priority = newPriority;
                                Console.WriteLine();
                                Console.Clear();
                                Console.WriteLine("Task priority level updated successfully!");
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Invalid task number.");
                                Console.WriteLine("");
                            }
                        }
                        else if (choice == 7)
                        {
                            if (task.Count > 0)
                            {
                                Console.WriteLine("Enter the number of the task you want to delete: ");

                                for (int i = 0; i < task.Count; i++)
                                {
                                    Console.WriteLine("(" + (i + 1) + ") " + task[i]);
                                }
                                int taskNum = int.Parse(Console.ReadLine());
                                taskNum--;

                                if (taskNum >= 0 && taskNum < task.Count)
                                {
                                    task.RemoveAt(taskNum);
                                    Console.Clear();
                                    Console.WriteLine("Task deleted successfully!");
                                    Console.WriteLine("");
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("Invalid task number.");
                                    Console.WriteLine("");
                                }
                            }
                        }
                        else if (choice == 8)
                        {
                            ViewTable(tasks: task);

                        }      
                  }
                }
                else if (command == 2)
                {
                    if (currentUser != null)
                    {
                        Console.WriteLine("You are already logged in.");
                        return;
                    }

                    Console.Write("Enter your email: ");
                    string enteredEmail = Console.ReadLine();

                    Console.Write("Enter your password: ");
                    string enteredPassword = Console.ReadLine();
                    
                    User userS = users.Find(u => u.Email == enteredEmail && u.Password == enteredPassword);

                    if (userS == null)
                    {
                        Console.WriteLine("Invalid email or password.");
                        continue;
                    }

                    currentUser = userS;

                    Console.WriteLine($"Welcome, {currentUser.Name}!");
                }
                else if (command == 3)
                {
                    if (currentUser == null)
                    {
                        Console.WriteLine("You are not logged in.");
                        continue;
                    }

                    currentUser = null;

                    Console.WriteLine("Logged out.");
                }
                else if (command == 4)
                {
                    Console.WriteLine("Goodbye!");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid command.");
                }
            }

               
        }
        
        static void ViewTable(List<Task> tasks)
        {
            var lineSep = "-------------------------------------------------------------------------------------";
            var header = $"|{" ID ",5}|{" Title ",10}|{" Description ",20}|{" Due Date ",20}|{" Priority ",20}|";
            Console.WriteLine(lineSep);
            Console.WriteLine(header);
            Console.WriteLine(lineSep);
            

            foreach (Task task in tasks)
            {
                Console.WriteLine("| {0,-3} | {1,-8} | {2,-15} | {3,-20} | {4,-18} | ", task.Id, task.Title, task.Description, task.DueDate.Date, task.Priority);
                Console.WriteLine(lineSep);
            }
        }
            static bool IsValidEmail(string email)
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(email);
                    return addr.Address == email;
                }
                catch
                {
                    return false;
                }
            }

            static bool IsValidPassword(string password)
            {
                if (password.Length < 8)
                {
                    return false;
                }

                bool hasUpperCase = false;
                bool hasLowerCase = false;
                bool hasDigit = false;

                foreach (char c in password)
                {
                    if (char.IsUpper(c))
                    {
                        hasUpperCase = true;
                    }
                    else if (char.IsLower(c))
                    {
                        hasLowerCase = true;
                    }
                    else if (char.IsDigit(c))
                    {
                        hasDigit = true;
                    }
                }

                return hasUpperCase && hasLowerCase && hasDigit;
            }
    }
}



