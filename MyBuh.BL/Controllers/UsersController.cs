using MyBuh.BL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;


namespace MyBuh.BL.Controllers
{
    public class UsersController
    {
       public List<Users> User { get; } 
       public Users CurrentUser { get; }

        /// <summary>
        /// Конструктор нашего юзера.
        /// </summary>
        /// <param name="name"></param>
       public UsersController(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Имя не может быть Null", nameof(name));
            }
            User = Load();
            CurrentUser = User.SingleOrDefault(x => x.Name == name);
            if (CurrentUser == null)
            {
                CurrentUser = new Users(name);
                User.Add(CurrentUser);
                Save();
            }
        }

        /// <summary>
        /// Добавляем сумму на баланс.
        /// </summary>
        /// <param name="balance"></param>
        /// <returns></returns>
        public double Deposit(double balance)
        {
            return CurrentUser.Balance += balance;
        }

        /// <summary>
        /// Растрата.
        /// </summary>
       public double Pay(double sum)
        {
            return CurrentUser.Temp -= sum;
        }

        /// <summary>
        /// Изымаем выданную сумму из общего баланса.
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        private double TempBalance(double temp)
        {
            return CurrentUser.Balance -= temp;

        }

        /// <summary>
        /// Выдача суммы на растраты.
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public double Temp(double temp)
        {
            if (temp > 0)
            {
                Console.WriteLine($"Выдана сумма: {temp}");
                TempBalance(temp);
                return CurrentUser.Temp += temp;
            }
            else
            {
                 Console.WriteLine("Число не должно быть меньше или равно 0");
                return 0;
            }
        }

        /// <summary>
        /// Проверка на экономию средств.
        /// </summary>
        public void SavededOrOverspending()
        {
            if (CurrentUser.Saveded + CurrentUser.Overspending >= 0)
            {
                CurrentUser.Saveded += CurrentUser.Overspending;
                CurrentUser.Overspending = 0;
                Console.WriteLine($"Экономим: {CurrentUser.Saveded}");
            }
            else
            {
                CurrentUser.Overspending += CurrentUser.Saveded;
                CurrentUser.Saveded = 0;
                Console.WriteLine($"Перерасход: {CurrentUser.Overspending}");
            }
        }

        /// <summary>
        /// Уменьшаем баланс на сумму перерасхода
        /// </summary>
        public void BalanceOverspending()
        {
            if (CurrentUser.Temp < 0)
            {
                CurrentUser.Balance += CurrentUser.Temp;
            }
        }


        /// <summary>
        /// Завершаем день, перерасход отправляем в перерасход, сэкономленное отправляем в экономию, а так же обнуляем выдачу.
        /// </summary>
        public void EndDay()
        {
            BalanceOverspending();
            if (CurrentUser.Temp >= 0)
            {
                Console.WriteLine($"Удалось сэкономить: {CurrentUser.Temp}");
                CurrentUser.Saveded += CurrentUser.Temp;
            }
            else
            {
                Console.WriteLine($"Перерасход: {CurrentUser.Temp}");
                CurrentUser.Overspending += CurrentUser.Temp;
            }
            CurrentUser.Temp = 0.0;
            Console.WriteLine("Данные обновлены");
            SavededOrOverspending();
            
        }

        /// <summary>
        /// Сброс всех параметров в 0.
        /// </summary>
        public void ClearAll()
        {
            CurrentUser.Balance = 0;
            CurrentUser.Expended = 0;
            CurrentUser.Overspending = 0;
            CurrentUser.Saveded = 0;
            CurrentUser.Temp = 0;
        }
        public void GetAllValue()
        {
            Console.WriteLine($"Имя: {CurrentUser.Name}\tВыдано на расходы: {CurrentUser.Temp}\tБаланс: {CurrentUser.Balance}\tИзрасходовано: {CurrentUser.Expended}\tУдалось сэкономить: {CurrentUser.Saveded}\tПерерасход: {CurrentUser.Overspending}");

        }
        /// <summary>
        /// Сохраняем даныне по пользователю в файл
        /// </summary>
        public void Save()
        {
            var formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("users.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, User);
            }
        }

        /// <summary>
        /// Получить сохраненный список пользователей
        /// </summary>
        /// <returns></returns>
        private List<Users> Load()
        {
            var formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("users.dat", FileMode.OpenOrCreate))
            {
                    if (fs.Length>0 && formatter.Deserialize(fs) is List<Users> users)
                    {
                        return users;
                    }
                    else
                    {
                        return new List<Users>();
                    }
            }
        }

        public override string ToString()
        {
            return CurrentUser.Name +"  " + CurrentUser.Balance;
        }


    }
}
