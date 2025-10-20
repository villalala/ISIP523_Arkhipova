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
}