using System;
using System.Collections.Generic;
using System.Text;

namespace VERSION_03
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Student> StudentsList = new List<Student>();

            ReadStudentsFromFile(ref StudentsList, "Students.txt");
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

        //----------------------------------------------------------------------------------------------------------------------------------------------------

        public static List<Student> ReadStudentsFromFile(ref List<Student> StudentsList, string txtFileName)
        {
            try
            {
                System.IO.File.ReadAllLines(@"C:\Users\kosta\source\repos\LAB V02\LAB V02\bin\Debug\netcoreapp3.1\" + txtFileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception when reading text file. Check if the txt file is in the right path.", ex);
            }


            string[] textLines = System.IO.File.ReadAllLines(@"C:\Users\kosta\source\repos\LAB V02\LAB V02\bin\Debug\netcoreapp3.1\" + txtFileName);

            int[] gradesList = new int[5];

            for (int i = 1; i < textLines.Length; i++)
            {
                string[] rowData = textLines[i].Split(" ");

                for (int j = 0; j < 5; j++)
                {
                    gradesList[j] = Int32.Parse(rowData[j + 2]);
                }

                StudentsList.Add(new Student(
                    rowData[0],
                    rowData[1],
                    gradesList,
                    Int32.Parse(rowData[7])
                    ));
            }

            StudentsList.Sort((x, y) => x.Surname.CompareTo(y.Surname));

            return StudentsList;
        }

        //-------------------------------------------------------------------------------------------------------------------------

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
                        homeworkPoints = Math.Round((Double)unroundedPoints * homeworkGradesRatio, 2);
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
