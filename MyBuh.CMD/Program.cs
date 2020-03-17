using MyBuh.BL.Controllers;
using System;


namespace MyBuh.CMD
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Console.Write("Введите имя пользователя: ");
            var name = Console.ReadLine();
            var userController = new UsersController(name);
            Console.WriteLine(userController.CurrentUser);
            bool value = true; //Переменная для выхода из меню.
                do
            {
                userController.GetAllValue();
                Console.WriteLine("Выберите действие");
                Console.WriteLine("1.Добавить на баланс");
                Console.WriteLine("2.Выдать на растраты");
                Console.WriteLine("3.Ввести потраченную сумму");
                Console.WriteLine("4.Удалить всё");
                Console.WriteLine("5.Закончить день");
                Console.WriteLine("6.Сохранить и выйти");

                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    switch (result)
                    {
                        case 1:
                            Console.WriteLine("Введите сумму для добавления");
                            
                            if (double.TryParse(Console.ReadLine(), out double balance))
                            {
                                userController.Deposit(balance);
                            }
                            else
                            {
                                Console.WriteLine("Данные должны быть введены в числовом виде");
                            }
                         break;
                        case 2:
                            Console.WriteLine("Введите сумму для добавления");
                            if (double.TryParse(Console.ReadLine(), out double temp))
                            {
                                userController.Temp(temp);
                            }
                            else
                            {
                                Console.WriteLine("Данные должны быть введены в числовом виде");
                            }
                            break;
                        case 3:
                            Console.Write("Сколько вы потратили: ");
                            if (double.TryParse(Console.ReadLine(), out double pay))
                            {
                                userController.Pay(pay);
                            }
                            else
                            {
                                Console.WriteLine("Данные должны быть введены в числовом виде");
                            }
                            break;

                        case 4:
                            userController.ClearAll();
                            break;

                        case 5:
                            userController.EndDay();
                            break;

                        case 6:
                            value = false;
                            userController.Save();
                            break;

                        default:
                            break;

                    }
                }
                else
                {
                    Console.WriteLine("Данные должны быть введены в числовом виде");
                   
                }
            }
            while (value);
            Console.WriteLine("Для выхода нажмите Enter");






            Console.ReadLine();
        }
    }
}
