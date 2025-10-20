using System;
using System.Collections.Generic;

namespace ISIP523_Arkhipova
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
            Console.WriteLine($"Название: {title}\nУчитель: {teacher?.GetFullName() ?? "Не назначен"}\nДлительность: {duration}");
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

        static void AddCourse()
        {
            Console.Write("Введите название курса: ");
            string title = Console.ReadLine();
            Console.Write("Введите длительность курса: ");
            string duration = Console.ReadLine();

            Course newCourse = new Course(title, duration);

            if (AllTeachers.Count == 0)
            {
                Console.WriteLine("Нет доступных преподавателей. Сначала добавьте хотя бы одного.");
            }
            else
            {
                Console.WriteLine("\nСписок преподавателей:");
                foreach (var t in AllTeachers)
                {
                    t.Print();
                    Console.WriteLine();
                }

                Console.Write("Введите фамилию преподавателя для назначения на курс: ");
                string surnameSearch = Console.ReadLine();

                Teacher foundTeacher = AllTeachers.Find(t => t.GetSurname().Equals(surnameSearch, StringComparison.OrdinalIgnoreCase));

                if (foundTeacher != null)
                {
                    newCourse.GetTeacher(foundTeacher);
                    Console.WriteLine($"Преподаватель {foundTeacher.GetFullName()} назначен на курс.");
                }
                else
                {
                    Console.WriteLine("Преподаватель с такой фамилией не найден.");
                }
            }

            if (AllStudents.Count == 0)
            {
                Console.WriteLine("\nНет студентов для добавления.");
            }
            else
            {
                Console.WriteLine("\nДобавление студентов на курс (введите '0', чтобы закончить):");
                foreach (var s in AllStudents)
                {
                    s.Print();
                    Console.WriteLine();
                }

                bool adding = true;
                while (adding)
                {
                    Console.Write("Введите фамилию студента для добавления (или '0' для выхода): ");
                    string studSurname = Console.ReadLine();

                    if (studSurname == "0")
                    {
                        adding = false;
                        continue;
                    }

                    Student foundStudent = AllStudents.Find(s => s.GetSurname().Equals(studSurname, StringComparison.OrdinalIgnoreCase));

                    if (foundStudent != null)
                    {
                        newCourse.AddStudentInCourse(foundStudent);
                        Console.WriteLine($" Студент {foundStudent.GetFullName()} добавлен на курс.");
                    }
                    else
                    {
                        Console.WriteLine(" Студент с такой фамилией не найден.");
                    }
                }
            }

            AllCourses.Add(newCourse);
            Console.WriteLine("\n Курс успешно создан и добавлен!");
        }

        static void AddStudentToCourse()
        {
            if (AllCourses.Count == 0 || AllStudents.Count == 0)
            {
                Console.WriteLine(" Нет доступных курсов или студентов.");
                return;
            }

            Console.WriteLine("\nСписок курсов:");
            for (int i = 0; i < AllCourses.Count; i++)
                Console.WriteLine($"{i + 1}. {AllCourses[i].title}");

            Console.Write("Выберите номер курса: ");
            int courseIndex = Convert.ToInt32(Console.ReadLine()) - 1;
            if (courseIndex < 0 || courseIndex >= AllCourses.Count)
            {
                Console.WriteLine("Неверный выбор курса.");
                return;
            }

            Console.WriteLine("\nСписок студентов:");
            for (int i = 0; i < AllStudents.Count; i++)
                Console.WriteLine($"{i + 1}. {AllStudents[i].GetFullName()}");

            Console.Write("Выберите номер студента: ");
            int studentIndex = Convert.ToInt32(Console.ReadLine()) - 1;
            if (studentIndex < 0 || studentIndex >= AllStudents.Count)
            {
                Console.WriteLine("Неверный выбор студента.");
                return;
            }

            AllCourses[courseIndex].AddStudentInCourse(AllStudents[studentIndex]);
            Console.WriteLine("Студент успешно добавлен на курс!");
        }

        static void ShowStudentCourses()
        {
            if (AllStudents.Count == 0)
            {
                Console.WriteLine("Нет студентов в системе.");
                return;
            }

            Console.Write("Введите фамилию студента: ");
            string surname = Console.ReadLine();

            Student foundStudent = AllStudents.Find(s =>
                s.GetSurname().Equals(surname, StringComparison.OrdinalIgnoreCase));

            if (foundStudent == null)
            {
                Console.WriteLine("Студент с такой фамилией не найден.");
                return;
            }

            Console.WriteLine($"\nКурсы студента {foundStudent.GetFullName()}:");

            bool found = false;

            foreach (var course in AllCourses)
            {
                if (course.IsStudent(foundStudent))
                {
                    Console.WriteLine($"- {course.title}");
                    found = true;
                }
            }

            if (!found)
                Console.WriteLine("Студент пока не записан ни на один курс.");
        }

        static void ShowCourseStudents()
        {
            if (AllCourses.Count == 0)
            {
                Console.WriteLine("Нет курсов в системе.");
                return;
            }

            Console.Write("Введите название курса: ");
            string title = Console.ReadLine();

            Course foundCourse = AllCourses.Find(c => string.Equals(c.title, title, StringComparison.OrdinalIgnoreCase));

            if (foundCourse == null)
            {
                Console.WriteLine("Курс с таким названием не найден.");
                return;
            }

            Console.WriteLine($"\nСписок студентов на курсе '{foundCourse.title}':");
            foundCourse.PrintStudents();
        }

        static void Main(string[] args)
        {
        
    }
}
    