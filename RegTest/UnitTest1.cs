using Microsoft.VisualStudio.TestTools.UnitTesting;
using PR14.Pages;
using System;

namespace RegTest
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Позитивный тест регистрации
        /// Проверяет, что новый пользователь успешно регистрируется
        /// </summary>
        [TestMethod]
        public void RegisterTest_Success_NewUser()
        {
            var page = new Reg();

            bool result = page.Register(
                login: "newuser",
                password: "password123",
                username: "Новый Тестовый Пользователь",
                dateOfBirth: new DateTime(2000, 1, 17)
            );
            Assert.IsTrue(result, "Регистрация нового пользователя должна пройти успешно");
        }

        /// <summary>
        /// Негативный тест регистрации
        /// Проверяет несколько ошибочных случаев
        /// </summary>
        [TestMethod]
        public void RegisterTest_Fail_InvalidData()
        {
            var page = new Reg();

            bool result1 = page.Register("", "password123", "Юзер", new DateTime(2000, 1, 1));
            Assert.IsFalse(result1, "Регистрация с пустым логином должна вернуть false");

            bool result2 = page.Register("user123", "password123", "Юзер", DateTime.Now.AddDays(1));
            Assert.IsFalse(result2, "Регистрация с датой рождения в будущем должна вернуть false");

            bool result3 = page.Register("bashlik", "password123", "Юзер", new DateTime(2000, 1, 1));
            Assert.IsFalse(result3, "Регистрация с уже занятым логином должна вернуть false");
        }
    }
}
