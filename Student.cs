using System;
using System.Collections.Generic;
using System.Text;

namespace VERSION_03
{
    class Student
    {
        private string name;
        private string surname;
        private int[] homeworkGrades;
        private int examamGrade;

        public Student(string name, string surname, int[] homeworkGrades, int examamGrade)
        {
            this.name = name;
            this.surname = surname;
            this.homeworkGrades = homeworkGrades;
            this.examamGrade = examamGrade;
        }

        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public int[] HomeworkGrades { get => homeworkGrades; set => homeworkGrades = value; }
        public int ExamamGrade { get => examamGrade; set => examamGrade = value; }
    }
}
