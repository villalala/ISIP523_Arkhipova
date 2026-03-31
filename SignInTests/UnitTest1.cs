using Microsoft.VisualStudio.TestTools.UnitTesting;
using PR14.Pages;
using System;

namespace SignInTests
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Позитивный тест авторизации
        /// Проверяет, что существующий пользователь может успешно войти
        /// </summary>
        [TestMethod]
        public void AuthTest_Success_ExistingUser()
        {
            var page = new SignIn();
            bool result = page.Auth("krokgrok", "89164025560");
            Assert.IsTrue(result, "Авторизация существующего пользователя должна пройти");
        }

        /// <summary>
        /// Негативный тест авторизации
        /// Проверяет, что при неверном пароле авторизация не проходит
        /// </summary>
        [TestMethod]
        public void AuthTest_Fail_WrongPassword()
        {
            var page = new SignIn();
            bool result = page.Auth("vill", "aaa");
            Assert.IsFalse(result, "Авторизация с неправильным паролем должна вернуть false");
        }

        /// <summary>
        /// Негативный тест авторизации
        /// Проверяет, что при пустых полях авторизация не проходит
        /// </summary>
        [TestMethod]
        public void AuthTest_Fail_EmptyFields()
        {
            var page = new SignIn();
            bool result = page.Auth("", "");
            Assert.IsFalse(result, "Авторизация с пустыми полями должна вернуть false");
        }
    }
}
