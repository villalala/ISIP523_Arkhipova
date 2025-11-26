using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP523_Arkhipova
{
    internal class Program
    {
        static user currentuser;

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            bool outt = true;
            while (outt)
            {
                Console.WriteLine("МЕНЮ");
                Console.WriteLine("1. Регистрация пользователя");
                Console.WriteLine("2. Авторизация пользователя");
                Console.WriteLine("3. Просмотр списка товаров");
                Console.WriteLine("4. Корзина");
                Console.WriteLine("5. Оформление заказа");
                Console.WriteLine("6. История заказов");
                Console.WriteLine("0. Выход");
                Console.Write("Введите выбор: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Register();
                        break;
                    case "2":
                        currentuser = Login();
                        break;
                    case "3":
                        ViewingProducts();
                        break;
                    case "4":
                        ViewingCart();
                        break;
                    case "5":
                        PlaceAnOrder();
                        break;
                    case "6":
                        ShowOrders();
                        break;
                    case "0":
                        outt = false;
                        break;
                    default:
                        break;
                }
            }
        }

        static void Register()
        {
            Console.Write("Введите логин: ");
            string login = Console.ReadLine();

            if (CorePR8.Context.user.Any(u => u.login == login))
            {
                Console.WriteLine("Такой логин уже существует!");
                return;
            }

            Console.Write("Введите пароль: ");
            string pass1 = Console.ReadLine();

            Console.Write("Повторите пароль: ");
            string pass2 = Console.ReadLine();

            if (pass1 != pass2)
            {
                Console.WriteLine("Пароли не совпадают!");
                return;
            }

            var user = new user { login = login, password = pass1 };
            CorePR8.Context.user.Add(user);
            CorePR8.Context.SaveChanges();

            var korzina = new korzina { ID_user = user.ID_user };
            CorePR8.Context.korzina.Add(korzina);
            CorePR8.Context.SaveChanges();

            Console.WriteLine("Регистрация успешна!");
        }

        static user Login()
        {
            Console.Write("Введите логин: ");
            string login = Console.ReadLine();

            Console.Write("Введите пароль: ");
            string pass = Console.ReadLine();

            var user = CorePR8.Context.user.FirstOrDefault(u => u.login == login && u.password == pass);

            if (user == null)
            {
                Console.WriteLine("Неверный логин или пароль!");
                return null;
            }

            Console.WriteLine($"Добро пожаловать, {user.login}!");
            return user;
        }

        static void AddToCart()
        {
            if (currentuser == null)
            {
                Console.WriteLine("Сначала авторизуйтесь, чтобы добавить товар в корзину");
                return;
            }

            var korzina = CorePR8.Context.korzina.FirstOrDefault(c => c.ID_user == currentuser.ID_user);
            if (korzina == null)
            {
                korzina = new korzina { ID_user = currentuser.ID_user };
                CorePR8.Context.korzina.Add(korzina);
                CorePR8.Context.SaveChanges();
            }

            Console.Write("Введите ID товара: ");
            if (!int.TryParse(Console.ReadLine(), out int productId))
            {
                Console.WriteLine("Некорректный ввод ID товара!");
                return;
            }

            var product = CorePR8.Context.product.Find(productId);
            if (product == null)
            {
                Console.WriteLine("Товар не найден");
                return;
            }

            Console.Write("Количество: ");
            if (!int.TryParse(Console.ReadLine(), out int qty) || qty <= 0)
            {
                Console.WriteLine("Некорректное количество");
                return;
            }

            var existing = CorePR8.Context.product_korzina
                .FirstOrDefault(pc => pc.ID_korzina == korzina.ID_korzina && pc.ID_product == productId);

            if (existing != null)
            {
                CorePR8.Context.product_korzina.Add(new product_korzina
                {
                    ID_korzina = korzina.ID_korzina,
                    ID_product = productId
                });
            }
            else
            {
                CorePR8.Context.product_korzina.Add(new product_korzina
                {
                    ID_korzina = korzina.ID_korzina,
                    ID_product = productId
                });
            }

            CorePR8.Context.SaveChanges();
            Console.WriteLine($"Добавлен '{product.name}' в корзину.");
        }

        static void ViewingProducts()
        {
            Console.WriteLine("\nСписок товаров:");
            foreach (var p in CorePR8.Context.product)
            {
                Console.WriteLine($"{p.ID_product}. {p.name}: {p.opisanie} — {p.price}₽");
            }
            Console.Write("Хотите добавить какой-то товар в корзину? (да / нет) ");
            string ans = Console.ReadLine();
            if (ans.ToLower() == "да")
            {
                AddToCart();
            }
        }

        static void ViewingCart()
        {
            if (currentuser == null)
            {
                Console.WriteLine("Сначала авторизуйтесь, чтобы просмотреть корзину");
                return;
            }

            var korzina = CorePR8.Context.korzina.FirstOrDefault(o => o.ID_user == currentuser.ID_user);
            if (korzina == null)
            {
                Console.WriteLine("Ваша корзина пуста!");
                return;
            }

            var items = CorePR8.Context.product_korzina.Where(pc => pc.ID_korzina == korzina.ID_korzina)
                .Join(CorePR8.Context.product,
                        pc => pc.ID_product,
                        p => p.ID_product,
                        (pc, p) => new
                        {
                            ProductName = p.name,
                            Price = p.price
                        })
                .ToList();

            if (items.Count == 0)
            {
                Console.WriteLine("Ваша корзина пуста!");
                return;
            }

            Console.WriteLine("\nТовары в корзине:");
            decimal totalSum = 0;

            foreach (var item in items)
            {
                Console.WriteLine($"{item.ProductName} — {item.Price}₽");
                totalSum += (decimal)item.Price;
            }

            Console.WriteLine($"\nИтого к оплате: {totalSum}₽");
        }

        static void PlaceAnOrder()
        {
            if (currentuser == null)
            {
                Console.WriteLine("Сначала авторизуйтесь, чтобы оформить заказ");
                return;
            }

            var korzina = CorePR8.Context.korzina.FirstOrDefault(c => c.ID_user == currentuser.ID_user);
            if (korzina == null)
            {
                Console.WriteLine("Корзина пуста!");
                return;
            }

            var cartItems = CorePR8.Context.product_korzina
                .Where(pc => pc.ID_korzina == korzina.ID_korzina)
                .ToList();

            if (!cartItems.Any())
            {
                Console.WriteLine("Корзина пуста!");
                return;
            }

            Console.WriteLine("\nЧто вы хотите купить?");
            Console.WriteLine("1 — Один товар из корзины");
            Console.WriteLine("2 — Все товары сразу");
            Console.Write("Ваш выбор: ");
            string choice = Console.ReadLine();

            List<product_korzina> itemsToBuy = new List<product_korzina>();

            if (choice == "1")
            {
                Console.WriteLine("\nВыберите товар по ID из корзины:");
                foreach (var item in cartItems)
                {
                    var product = CorePR8.Context.product.Find(item.ID_product);
                    if (product != null)
                        Console.WriteLine($"{product.ID_product}. {product.name} — {product.price}₽");
                }

                Console.Write("Введите ID товара, который хотите купить: ");
                if (!int.TryParse(Console.ReadLine(), out int selectedId))
                {
                    Console.WriteLine("Неверный ввод!");
                    return;
                }

                var selectedItem = cartItems.FirstOrDefault(i => i.ID_product == selectedId);
                if (selectedItem == null)
                {
                    Console.WriteLine("Такого товара нет в корзине!");
                    return;
                }

                itemsToBuy.Add(selectedItem);
            }
            else if (choice == "2")
            {
                itemsToBuy = cartItems;
            }
            else
            {
                Console.WriteLine("Неверный выбор!");
                return;
            }

            Console.WriteLine("\nВыберите пункт выдачи:");
            foreach (var pvz in CorePR8.Context.pvz)
                Console.WriteLine($"{pvz.ID_pvz}. {pvz.city}: {pvz.adress}");

            Console.Write("Введите ID ПВЗ: ");
            if (!int.TryParse(Console.ReadLine(), out int pvzId))
            {
                Console.WriteLine("Неверный ввод!");
                return;
            }

            var zakaz = new zakaz
            {
                ID_user = currentuser.ID_user,
                ID_pvz = pvzId,
                data = DateTime.Now
            };

            CorePR8.Context.zakaz.Add(zakaz);
            CorePR8.Context.SaveChanges();

            foreach (var item in itemsToBuy)
            {
                var product = CorePR8.Context.product.Find(item.ID_product);
                if (product == null) continue;

                CorePR8.Context.zakaz_product.Add(new zakaz_product
                {
                    ID_zakaz = zakaz.ID_zakaz,
                    ID_product = product.ID_product,
                    price = product.price
                });
            }

            if (choice == "1")
            {
                CorePR8.Context.product_korzina.Remove(itemsToBuy.First());
            }
            else if (choice == "2")
            {
                CorePR8.Context.product_korzina.RemoveRange(cartItems);
            }

            CorePR8.Context.SaveChanges();

            Console.WriteLine("Заказ оформлен успешно!");
        }

        static void ShowOrders()
        {
            if (currentuser == null)
            {
                Console.WriteLine("Сначала авторизуйтесь");
                return;
            }

            var orders = CorePR8.Context.zakaz
                .Where(o => o.ID_user == currentuser.ID_user)
                .OrderByDescending(o => o.data)
                .ToList();

            if (!orders.Any())
            {
                Console.WriteLine("У вас нет заказов");
                return;
            }

            Console.WriteLine("\nИстория заказов: ");

            foreach (var order in orders)
            {
                Console.WriteLine($"\nЗаказ #{order.ID_zakaz} — {order.data}");

                var items = CorePR8.Context.zakaz_product
                    .Where(op => op.ID_zakaz == order.ID_zakaz)
                    .ToList();

                if (!items.Any())
                {
                    Console.WriteLine("(пустой заказ)");
                    continue;
                }

                foreach (var item in items)
                {
                    var product = CorePR8.Context.product.Find(item.ID_product);
                    if (product != null)
                    {
                        Console.WriteLine($"{product.name} — {item.price}₽");
                    }
                }
            }
        }
    }
}
