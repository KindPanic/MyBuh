using System;


namespace MyBuh.BL.Model
{
    [Serializable]
   public class Users
    {
        public string Name { get; }
        public double Balance { get; set; }
        /// <summary>
        /// Выдано сейчас
        /// </summary>
        public double Temp { get;  set; }

        /// <summary>
        /// Удалось сыкономить.
        /// </summary>
        public double Saveded { get;  set; }

        /// <summary>
        /// Перерасход.
        /// </summary>
        public double Overspending { get;  set; }
        /// <summary>
        /// Израсходовано.
        /// </summary>
        public double Expended { get;  set; }

        public Users(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("Имя не мождет быть равно null",nameof(name));
            }
            Name = name;
            Balance = 0;
            Temp = 0;
            Saveded = 0;
            Overspending = 0;
            Expended = 0;
        }
        public Users() { }

        public override string ToString()
        {
            return Name + " " + Balance;
        }
    }
}
