using System;
using System.IO;
using System.Collections.Generic;

namespace LAB_V01
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Student> StudentsList = new List<Student>();

            GetStudentDataFromConsole(ref StudentsList);
            FinalPointsCalculator(ref StudentsList);
        }

        public static List<Student> GetStudentDataFromConsole(ref List<Student> StudentsList)
        {
            string name, surname;
            int examGrade;

            List<int> gradesList = new List<int>();

            Console.WriteLine("Name: ");
            name = Console.ReadLine();

            Console.WriteLine("Surname: ");
            surname = Console.ReadLine();

            bool insertAnother = true;
            while (insertAnother == true)
            {
                Console.WriteLine("Enter a homework nr." + (gradesList.Count + 1) + " grade: ");
                gradesList.Add(Int32.Parse(Console.ReadLine()));

                Console.WriteLine("Would you like to enter another one ? y/n");
                if (Console.ReadLine() != "y")
                {
                    insertAnother = false;
                }
            }

            Console.WriteLine("Final exam grade 1-10: ");
            examGrade = Int32.Parse(Console.ReadLine());

            int[] homeworkGradesList = new int[gradesList.Count];

            for (int i = 0; i < gradesList.Count; i++)
            {
                homeworkGradesList[i] = gradesList[i];
            }

            StudentsList.Add(new Student(name, surname, homeworkGradesList, examGrade));

            return StudentsList;
        }

        public static void FinalPointsCalculator(ref List<Student> StudentsList)
        {
            double homeworkGradesRatio = 0.3;
            double examRatio = 0.7;

            int totalHomework = 0;

            double homeworkPoints;
            double examPoints;
            double totalPoints;

            int MenuOption = 0;

            Console.WriteLine("1 - average final points calculation \n2 - median final points calculation");
            MenuOption = Int32.Parse(Console.ReadLine());

            switch (MenuOption)
            {
                case 1:
                    Console.WriteLine("Surname      Name            Final points (Avg.) \n------------------------------------------");

                    for (int i = 0; i < StudentsList.Count; i++)
                    {
                        for (int j = 0; j < StudentsList[i].HomeworkGrades.Length; j++)
                        {
                            totalHomework += StudentsList[i].HomeworkGrades[j];
                        }

                        double unroundedPoints = (totalHomework * homeworkGradesRatio) / StudentsList[i].HomeworkGrades.Length;
                        homeworkPoints = Math.Round((Double)unroundedPoints, 2);
                        examPoints = Math.Round((Double)(StudentsList[i].ExamamGrade * examRatio), 2);
                        totalPoints = Math.Round((Double)(homeworkPoints + examPoints), 2);


                        Console.WriteLine(StudentsList[i].Surname + "     " + StudentsList[i].Name + "            " + totalPoints);
                    }

                    break;

                case 2:
                    Console.WriteLine("Surname      Name            Final points (Med.) \n------------------------------------------");

                    for (int i = 0; i < StudentsList.Count; i++)
                    {
                        int[] gradesCopy = new int[StudentsList[i].HomeworkGrades.Length];

                        StudentsList[i].HomeworkGrades.CopyTo(gradesCopy, 0);
                        Array.Sort(gradesCopy);

                        int size = gradesCopy.Length;
                        int mid = size / 2;

                        double mediana = (size % 2 != 0) ? ((double)gradesCopy[mid]) * 0.3 : (((double)gradesCopy[mid] + (double)gradesCopy[mid - 1]) / 2) * 0.3;
                        examPoints = Math.Round((Double)(StudentsList[i].ExamamGrade * examRatio), 2);
                        totalPoints = Math.Round((Double)(mediana + examPoints), 2);

                        Console.WriteLine(StudentsList[i].Surname + "     " + StudentsList[i].Name + "            " + totalPoints);

                    }

                    break;
            }

        }

    }
}
