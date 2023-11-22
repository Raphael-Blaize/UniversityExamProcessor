using System;
using System.Collections.Generic;

class Program
{

    /* 
      Creating classes to store the average score and the 
      reccomendation based on the averagescore
     */

    static string GetGrade(double averageScore)
    {
        if (averageScore >= 90)
        {
            return "A";
        }
        else if (averageScore >= 80)
        {
            return "B";
        }
        else if (averageScore >= 70)
        {
            return "C";
        }
        else if (averageScore >= 60)
        {
            return "D";
        }
        else
        {
            return "F";
        }
    }

    static string GetRecommendation(double averageScore)
    {
        if (averageScore >= 80)
        {
            return "Excellent";
        }
        else if (averageScore >= 70)
        {
            return "Good";
        }
        else if (averageScore >= 60)
        {
            return "Average";
        }
        else
        {
            return "Poor";
        }
    }

    static void DisplayAvailableStudents(string[,] studentReport, int maxStudents)
    {
        //displaying the available students names and ID number for each student in the array
        Console.Write("Available Students");
        Console.WriteLine();
        for (int i = 0; i < maxStudents; i++)
        {
            Console.WriteLine($"{i + 1}: Name - {studentReport[i, 0]}, ID - {studentReport[i, 1]}");
        }
        Console.WriteLine();
    }

    static void Main()
    {
        // Variables that support data entry
        Console.Write("Enter How many students: ");
        int maxStudents = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("\n");
        string? studentName = "";

        // Multidimensional array to store the data 
        string[,] studentReport = new string[maxStudents, 6];

        // Creating initial Entries 
        for (int i = 0; i < maxStudents; i++)
        {
            string? idNumber = "";
            List<string> subjects = new List<string>();
            List<int> scoreData = new List<int>();



            // Gaining the student information and storing it dynamically to include the current value of i.
            Console.Write($"Enter student {i + 1} name: ");
            studentName = Console.ReadLine();

            Console.Write($"Enter student {i + 1} ID number: ");
            idNumber = Console.ReadLine();

            Console.WriteLine($"Enter subjects and scores for student {i + 1} (up to 5 subjects):");

            for (int j = 0; j < 5; j++)
            {
                Console.Write($"Enter subject {j + 1} (or type 'done' to finish): ");
                string subject = Console.ReadLine()!;

                //Exits prompt for the subjects when user enters done and ignores capitalization
                if (subject.Equals("done", StringComparison.CurrentCultureIgnoreCase))
                {
                    break;
                }

                Console.Write($"Enter score for {subject}: ");
                if (int.TryParse(Console.ReadLine(), out int score))
                {
                    subjects.Add(subject);
                    scoreData.Add(score);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid score.");
                    j--; // Decrement j to re-enter the score for the same subject
                }
            }
            Console.WriteLine("\n");

            // Calculating average score
            double averageScore = 0;
            int totalScores = 0;
            foreach (int score in scoreData)
            {
                totalScores += score;
            }
            if (scoreData.Count > 0)
            {
                averageScore = (double)totalScores / scoreData.Count;
            }

            // Assigning values to the multidimensional array
            studentReport[i, 0] = studentName!;
            studentReport[i, 1] = idNumber!;
            studentReport[i, 2] = DateTime.Now.ToString("MM/dd/yyyy");
            studentReport[i, 3] = string.Join(", ", subjects);
            studentReport[i, 4] = string.Join(", ", scoreData);
            studentReport[i, 5] = averageScore.ToString("0.00");

        }
        Console.Write("\n");

        // Asking for student ID to display report card
        int maxAttempts = 3; // Setting the maximum number of attempts before error
        int attemptCount = 0;
        bool validIdEntered = false;
        int attemptedStudents = 0;

        //while loop to give the user 3 chances to enter the correct student ID
        while (attemptCount < maxAttempts && !validIdEntered && attemptedStudents <= maxStudents)
        {
            //calling the object to show the availble students
            DisplayAvailableStudents(studentReport, maxStudents);

            //asking user for student ID 
            Console.Write("Enter student ID to display report card (or 'exit' to quit): ");
            string studentId = Console.ReadLine()!;

            if (studentId.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Exiting program.");
                break;
            }

            // Finding the student with the entered ID
            int studentIndex = -1;
            for (int i = 0; i < maxStudents; i++)
            {
                if (studentReport[i, 1] == studentId)
                {
                    studentIndex = i;
                    validIdEntered = true;
                    break;
                }
            }

            // Displaying report card if the student ID is found
            if (studentIndex != -1)
            {
                Console.WriteLine("\n");
                Console.WriteLine($"Report Card for: {studentReport[studentIndex, 0]}");
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine($"Id Number:  {studentReport[studentIndex, 1],-9} ");
                Console.WriteLine("-------------------------------------------------- ");
                Console.WriteLine($"Report Date: {studentReport[studentIndex, 2],-11}");
                Console.WriteLine("---------------------------------------------------");


                // Splitting subjects and scores into separate arrays
                string[] subjectsArray = studentReport[studentIndex, 3].Split(", ");
                string[] scoresArray = studentReport[studentIndex, 4].Split(", ");

                // Displaying subjects and scores in separate rows
                Console.WriteLine("Subject Names\t\tScores");
                Console.WriteLine("-----------------------------------------------");


                //The loop runs for the maximum length between subjectsArray and scoresArray. This ensures that all subjects and scores are considered.
                //Corresponding scores for a specific student.uses a loop that iterates for the maximum length between subjectsArray and scoresArray.
                //The Math.Max function ensures that the loop iterates for the larger of the two lengths, so it covers all subjects or scores.
                for (int i = 0; i < Math.Max(subjectsArray.Length, scoresArray.Length); i++)
                {
                    //The ternary operator (condition ? trueValue : falseValue) is used to get the subject and score at index i if they exist, and if not, it sets an empty string.
                    string subject = i < subjectsArray.Length ? subjectsArray[i] : "";
                    string score = i < scoresArray.Length ? scoresArray[i] : "";

                    Console.WriteLine($"{subject,-20}\t{score}");
                }

                Console.WriteLine("--------------------------------------------------");

                Console.WriteLine($"Average Score: {studentReport[studentIndex, 5],-13}");


                // Utilizing GetGrade and GetRecommendation methods
                double averageScore = double.Parse(studentReport[studentIndex, 5]);
                Console.WriteLine($"Grade: {GetGrade(averageScore)}");
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine($"Recommendation: {GetRecommendation(averageScore)}");
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine("\n");

                //incrementing k
                attemptedStudents++;

                //Checking if k is greater than maxStudents
                if (attemptedStudents >= maxStudents)
                {
                    Console.WriteLine("Maximum number of students available!");
                    Console.WriteLine("\n");
                    break;
                }

                // Asking user if they want to check another student's report card
                Console.Write("Do you want to check another student's report card? (yes/no): ");
                string answer = Console.ReadLine()!.ToLower();

                if (answer == "no")
                {
                    break; // Exit the program
                }
                else
                {
                    // resetting in-order to ask for another student's ID
                    validIdEntered = false;
                }

            }
            else
            {
                Console.WriteLine($"Student not found with the entered ID. Please enter correct ID Number({attemptCount + 1} attempts left).");
                attemptCount++;
            }
        }
        if (!validIdEntered)
        {
            Console.WriteLine($"Maximum attempts ({maxAttempts}) reached. Exiting program.");
        }
    }
}
