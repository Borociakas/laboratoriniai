using System;
using System.IO;
using System.Collections.Generic;

namespace LAB_V02
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Student> StudentsList = new List<Student>();
            List<Student> StudentsList1 = new List<Student>();

            SortingStudents(ReadStudentsFromFile(ref StudentsList1, "milion.txt"));
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------

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
            catch(Exception ex)
            {
                Console.WriteLine("Exception when reading text file. Check if the txt file is in the right path.", ex);
            }


            string[] textLines = System.IO.File.ReadAllLines(@"C:\Users\kosta\source\repos\LAB V02\LAB V02\bin\Debug\netcoreapp3.1\"+ txtFileName);

            int[] gradesList = new int[5];

            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            for (int i = 0; i < textLines.Length; i++)
            {
                string[] rowData = textLines[i].Split(" ");

                for(int j=0; j<5; j++)
                {
                    gradesList[j] = Int32.Parse(rowData[j+2]);
                }

                StudentsList.Add(new Student(
                    rowData[0],
                    rowData[1],
                    gradesList,
                    Int32.Parse(rowData[7])
                    ));
            }

            Console.WriteLine("Done!");
            watch.Stop();
            Console.WriteLine(StudentsList.Count + " Records from file was uploaded to the Students List. Execution time: "+ (watch.ElapsedMilliseconds) + "ms");
            StudentsList.Sort((x, y) => x.Surname.CompareTo(y.Surname));

            return StudentsList;
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------

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

        //----------------------------------------------------------------------------------------------------------------------------------------------------

        public static void GenerateStudentsFile(string fileName, int amount)
        {
            StreamWriter writer = new StreamWriter(fileName);

            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            for (int i = 0; i < amount; i++)
            {
                writer.WriteLine(
                    "Surname" + (i + 1) +" "+
                    "Name" + (i + 1) + " "
                    + RandomNumberGenerator() + " "
                    + RandomNumberGenerator() + " "
                    + RandomNumberGenerator() + " "
                    + RandomNumberGenerator() + " "
                    + RandomNumberGenerator() + " "
                    + RandomNumberGenerator());
            }
            writer.Close();

            Console.WriteLine("Done!");
            watch.Stop();
            Console.WriteLine(amount + " Records was generated to the "+ fileName +" file. Execution time: " + (watch.ElapsedMilliseconds) + "ms");
        }

        public static void Generate4StudentsFiles()
        {
            int first = 10000;
            int second = 100000;
            int third = 1000000;
            int fourth = 10000000;

            StreamWriter writer1 = new StreamWriter("tenK.txt");
            StreamWriter writer2 = new StreamWriter("hundredK.txt");
            StreamWriter writer3 = new StreamWriter("milion.txt");
            StreamWriter writer4 = new StreamWriter("tenMilion.txt");

            var watch = new System.Diagnostics.Stopwatch();

            // ----------------------------------------------------------------------------------
            // Generating first students file containing of 10000 students.

            watch.Start();
            for (int i = 0; i < first; i++)
            {
                writer1.WriteLine(
                    "Surname" + (i + 1) + " " +
                    "Name" + (i + 1) + " "
                    + RandomNumberGenerator() + " "
                    + RandomNumberGenerator() + " "
                    + RandomNumberGenerator() + " "
                    + RandomNumberGenerator() + " "
                    + RandomNumberGenerator() + " "
                    + RandomNumberGenerator());
            }
            writer1.Close();

            Console.WriteLine("Done!");
            watch.Stop();
            Console.WriteLine(first + " Records was generated to the tenK.txt file. Execution time: " + (watch.ElapsedMilliseconds) + "ms");

            // ----------------------------------------------------------------------------------
            // Generating second students file containing of 100000 students.

            watch.Start();
            for (int i = first; i < second; i++)
            {
                writer2.WriteLine(
                    "Surname" + (i + 1) + " " +
                    "Name" + (i + 1) + " "
                    + RandomNumberGenerator() + " "
                    + RandomNumberGenerator() + " "
                    + RandomNumberGenerator() + " "
                    + RandomNumberGenerator() + " "
                    + RandomNumberGenerator() + " "
                    + RandomNumberGenerator());
            }
            writer2.Close();


            Console.WriteLine("Done!");
            watch.Stop();
            Console.WriteLine(second + " Records was generated to the hundredK.txt file. Execution time: " + (watch.ElapsedMilliseconds) + "ms");

            // ----------------------------------------------------------------------------------
            // Generating third students file containing of 1000000 students.

            watch.Start();
            for (int i = second; i < third; i++)
            {
                writer3.WriteLine(
                    "Surname" + (i + 1) + " " +
                    "Name" + (i + 1) + " "
                    + RandomNumberGenerator() + " "
                    + RandomNumberGenerator() + " "
                    + RandomNumberGenerator() + " "
                    + RandomNumberGenerator() + " "
                    + RandomNumberGenerator() + " "
                    + RandomNumberGenerator());
            }
            writer3.Close();

            Console.WriteLine("Done!");
            watch.Stop();
            Console.WriteLine(third + " Records was generated to the milion.txt file. Execution time: " + (watch.ElapsedMilliseconds) + "ms");

            // ----------------------------------------------------------------------------------
            // Generating fourth students file containing of 1000000 students.

            watch.Start();
            for (int i = third; i < fourth; i++)
            {
                writer4.WriteLine(
                    "Surname" + (i + 1) + " " +
                    "Name" + (i + 1) + " "
                    + RandomNumberGenerator() + " "
                    + RandomNumberGenerator() + " "
                    + RandomNumberGenerator() + " "
                    + RandomNumberGenerator() + " "
                    + RandomNumberGenerator() + " "
                    + RandomNumberGenerator());
            }
            writer4.Close();

            Console.WriteLine("Done!");
            watch.Stop();
            Console.WriteLine(fourth + " Records was generated to the tenMilion.txt file. Execution time: " + (watch.ElapsedMilliseconds) + "ms");
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------


        public static void SortingStudents(List<Student> StudentsList)
        {
            StreamWriter writerPassedStudents = new StreamWriter("PassedStudents.txt", true);
            StreamWriter writerFailedStudents = new StreamWriter("FailedStudents.txt", true);

            Console.WriteLine("SortingStudents Received List size: " + StudentsList.Count);

            double homeworkGradesRatio = 0.3;
            double examRatio = 0.7;

            int totalHomework = 0;
            double homeworkPoints;
            double examPoints;
            double totalPoints = 0;

            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            // sorting into different txt files for passed (final grade >=5 ) and failed (final grade < 5).

            for (int i = 0; i < StudentsList.Count; i++)
            {
                totalHomework = 0;
                for (int j = 0; j < StudentsList[i].HomeworkGrades.Length; j++)
                {
                    totalHomework += StudentsList[i].HomeworkGrades[j];
                }

                double unroundedPoints = (totalHomework * homeworkGradesRatio) / StudentsList[i].HomeworkGrades.Length;
                homeworkPoints = Math.Round((Double)unroundedPoints, 2);
                examPoints = Math.Round((Double)(StudentsList[i].ExamamGrade * examRatio), 2);
                totalPoints = Math.Round((Double)(homeworkPoints + examPoints), 2);

                if( totalPoints >= 5)
                {
                    writerPassedStudents.WriteLine(StudentsList[i].Name +" "+ StudentsList[i].Surname +" Final grade: "+ totalPoints);
                }
                else
                {
                    writerFailedStudents.WriteLine(StudentsList[i].Name + " " + StudentsList[i].Surname + " Final grade: " + totalPoints);
                }
            }

            Console.WriteLine("Done!");
            watch.Stop();
            Console.WriteLine(StudentsList.Count + " Records was sorted between passed and failed students.. Execution time: " + (watch.ElapsedMilliseconds) + "ms");

            // sorting when failed students are moved to the other container and deleted from the current one.

            List<Student> failedStudentsList = new List<Student>();

            watch.Start();
            for (int i = 0; i < StudentsList.Count; i++)
            {
                totalHomework = 0;
                for (int j = 0; j < StudentsList[i].HomeworkGrades.Length; j++)
                {
                    totalHomework += StudentsList[i].HomeworkGrades[j];
                }

                double unroundedPoints = (totalHomework * homeworkGradesRatio) / StudentsList[i].HomeworkGrades.Length;
                homeworkPoints = Math.Round((Double)unroundedPoints, 2);
                examPoints = Math.Round((Double)(StudentsList[i].ExamamGrade * examRatio), 2);
                totalPoints = Math.Round((Double)(homeworkPoints + examPoints), 2);

                if (totalPoints < 5)
                {
                    failedStudentsList.Add(StudentsList[i]);
                    StudentsList.RemoveAt(i);
                }    
            }

            Console.WriteLine("Done!");
            watch.Stop();
            Console.WriteLine(StudentsList.Count + " Failed students were moved to the other list and removed from the current one.. Execution time: " + (watch.ElapsedMilliseconds) + "ms");


        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------

        public static int RandomNumberGenerator()
        {
            Random rnd = new Random();
            int randomGrade = rnd.Next(1, 10);

            return randomGrade;
        }

    }
}
