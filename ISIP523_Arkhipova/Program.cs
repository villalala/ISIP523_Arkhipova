using System;
using System.Collections.Generic;

namespace ISIP523_Zemdikhanova
{
    class Person
    {
        private string name;
        private string surname;
        private int age;

        public Person(string name, string surname, int age)
        {
            this.name = name;
            this.surname = surname;
            this.age = age;
        }

        public virtual void Print()
        {
            Console.WriteLine($"Имя: {name}\nФамилия: {surname}\nВозраст: {age}");
        }

        public string GetFullName()
        {
            return $"{name} {surname}";
        }

        public string GetSurname()
        {
            return surname;
        }
    }

    class Student : Person
    {
        private string group;
        private int year;

        public Student(string name, string surname, int age, string group, int year)
            : base(name, surname, age)
        {
            this.group = group;
            this.year = year;
        }

        public override void Print()
        {
            Console.WriteLine("Информация о студенте: ");
            base.Print();
            Console.WriteLine($"Группа: {group}\nКурс: {year}");
        }
    }

    class Teacher : Person
    {
        private string salary;
        private int experience;

        public Teacher(string name, string surname, int age, string salary, int experience)
            : base(name, surname, age)
        {
            this.salary = salary;
            this.experience = experience;
        }

        public override void Print()
        {
            Console.WriteLine("Информация о преподавателе: ");
            base.Print();
            Console.WriteLine($"Зарплата: {salary}\nСтаж: {experience}");
        }
    }
    class Course
    {
        public string title;
        private Teacher teacher;
        private string duration;
        private List<Student> Students = new List<Student>();

        public Course(string title, string duration)
        {
            this.title = title;
            this.duration = duration;
        }

        public void AddStudentInCourse(Student student)
        {
            if (!Students.Contains(student))
            {
                Students.Add(student);
            }
        }

        public void GetTeacher(Teacher teacher)
        {
            this.teacher = teacher;
        }

        public void Print()
        {
            string teacherName = teacher != null ? teacher.GetFullName() : "Не назначен";
            Console.WriteLine($"Название: {title}\nУчитель: {teacherName}\nДлительность: {duration}");
        }

        public bool IsStudent(Student s)
        {
            return Students.Contains(s);
        }

        public void PrintStudents()
        {
            if (Students.Count == 0)
            {
                Console.WriteLine("На этом курсе пока нет студентов.");
                return;
            }

            foreach (var s in Students)
                Console.WriteLine("- " + s.GetFullName());
        }
    }

    class Program
    {
        static List<Student> AllStudents = new List<Student>();
        static List<Teacher> AllTeachers = new List<Teacher>();
        static List<Course> AllCourses = new List<Course>();

        static void AddStudent()
        {
            Console.WriteLine("Введите имя: ");
            string name = Console.ReadLine();
            Console.WriteLine("Введите фамилию: ");
            string surname = Console.ReadLine();
            Console.WriteLine("Введите возраст: ");
            int age = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите группу: ");
            string group = Console.ReadLine();
            Console.WriteLine("Введите год обучения: ");
            int year = Convert.ToInt32(Console.ReadLine());
            AllStudents.Add(new Student(name, surname, age, group, year));
            Console.WriteLine("Студент добавлен");
        }

        static void AddTeacher()
        {
            Console.WriteLine("Введите имя: ");
            string name = Console.ReadLine();
            Console.WriteLine("Введите фамилию: ");
            string surname = Console.ReadLine();
            Console.WriteLine("Введите возраст: ");
            int age = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите зарплату: ");
            string salary = Console.ReadLine();
            Console.WriteLine("Введите стаж: ");
            int experience = Convert.ToInt32(Console.ReadLine());
            AllTeachers.Add(new Teacher(name, surname, age, salary, experience));
            Console.WriteLine("Преподаватель добавлен");
        }
    }
}