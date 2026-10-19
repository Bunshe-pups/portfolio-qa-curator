namespace Game
{
    internal class Monster
    {
        private string name;
        private int power;
        private int health;
        protected static Random rnd = new Random();

        public Monster(string name)
        {
            this.name = name;
            this.power = rnd.Next(1, 10);
            this.health = rnd.Next(50, 200);
        }

        public virtual int Damage() => power;
        
        public bool IsDie() => health <= 0;

        public bool Attack(Monster other)
        {
            int h = this.health;
            while (!this.IsDie() && !other.IsDie())
            {
                other.health -= this.Damage();
                this.power += 2;
                this.health -= other.Damage();
                other.power += 2;
            }
            
            if (this.IsDie()) return false;
            
            this.health = h; // восстановление здоровья
            return true;
        }
    }

    internal class Demon : Monster
    {
        private int brain;

        public Demon(string name) : base(name)
        {
            this.brain = rnd.Next(1, 6);
        }

        public override int Damage()
        {
            return base.Damage() + base.Damage() * brain / 10;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Начнем игру. Введите имя героя:");
            string n = Console.ReadLine();
            Monster hero = new Monster(n);
            
            List<Monster> bots = new List<Monster>();
            string[] botsName = { "Ящер", "Зомби", "Титан", "Халк" };
            
            for (int i = 0; i < botsName.Length; i++)
            {
                if (i < 2)
                    bots.Add(new Monster(botsName[i]));
                else
                    bots.Add(new Demon(botsName[i]));
            }

            Random rnd = new Random();
            while (bots.Count > 0)
            {
                int randomIndex = rnd.Next(bots.Count);
                Monster currentBot = bots[randomIndex];
                
                bool isWin = hero.Attack(currentBot);
                
                if (isWin)
                {
                    Console.WriteLine("\nПобеда! Вы одолели противника.");
                    bots.RemoveAt(randomIndex);
                }
                else
                {
                    Console.WriteLine("\nВы проиграли сражение. Игра окончена.");
                    return;
                }
            }
            Console.WriteLine("\nСпасибо за игру!");
        }
    }
}
