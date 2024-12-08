using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HW_12_Interfaces
{
    interface IPaintAndMove
    {
        void Paint();
        void Move();
    }
    class Program
    {
        static void Main(string[] args)
        {
            /*
             *  1) создаем базовый класс,который хранит направление движения, скорость движения, 
            абстрактный методы полета и завершения полета и делегат для обратного вызова, 
            который вызывается в конце завершения полета.
                2) реализуем классы-наследники: феерверк, группа парашутистов, шары. 
            они инициализируют направление и абстрактные методы. соотвественно виды ферверков,
            групп парашутистов, шаров имеют свои параметры, которые используются в реализованых методах.
                4) создаем контейнер объектов базового класса. контейнер содержит событие, 
            которое сигнализирует о завершении старого объекта, т.е. запуске нового.
                5) создаем фабрику, которая генерирует случайный объект и подписывает делегат на событие контейнера.
                6) при загрузке приложения фабрика заполняет контейнер и вызывает событие у контейнера - 
            контенер по событию запускает первый объект, который сам себя рисует на форме и по завершению 
            снова вызывает событие контйнера.

             
             * Написать приложение «Праздничное небо». В праздник в небе над городом можно наблюдать фейерверки, 
             * воздушные шары, группы парашютистов. Эти объекты никак не связаны иерархией наследования, 
             * но все же ваше приложение должно реализовать объект «Праздничное небо», 
             * который является коллекцией «объектов праздничного неба» (подсказка – коллекцией/массивом интерфейсов).
             * В любой момент времени в небе можно наблюдать только один «объект праздничного неба»,
             * который пролетев по небу, исчезает и ему на смену приходит другой объект. 
             * При старте приложения коллекция «Праздничное небо» заполняется случайным образом и случайным 
             * количеством «объектов праздничного неба». После чего начинается шоу. 
             * Необходимо реализовать следующие виды фейерверков – крест, диагональный крест, цветок. 
             * Виды парашютистов – группа «треугольник», группа «ромб». Виды воздушных шаров – цветок, хаос. 
             * Продумать возможную иерархию родственных объектов.
             */

            List<IPaintAndMove> objectsOfShow = new List<IPaintAndMove>();
            Random rand = new Random();

            foreach (ConsoleColor color in Enum.GetValues(typeof(ConsoleColor)))
            {
                objectsOfShow.Add
                    (new Parachutist(rand.Next(Console.WindowWidth / 2 - 40, Console.WindowWidth / 2 + 40), color));

                objectsOfShow.Add
                    (new Firework(rand.Next(Console.WindowWidth / 2 - 40, Console.WindowWidth / 2 + 40), color));

                objectsOfShow.Add
                    (new AirBalloon(rand.Next(Console.WindowWidth / 2 - 40, Console.WindowWidth / 2 + 40), color));

                objectsOfShow.Add
                    (new AirBalloon(rand.Next(Console.WindowWidth / 2 - 40, Console.WindowWidth / 2 + 40), color));

            }

            while (true)
            {
                IPaintAndMove current = objectsOfShow[rand.Next(0, objectsOfShow.Count - 1)];
                for (int i = 0; i < 31; i++)
                {
                    current.Paint();
                    current.Move();
                    Console.SetCursorPosition(0, 0);
                    Thread.Sleep(100);
                    Console.Clear();
                }
            }

            Console.ReadKey();
        }
    }

    class AirBalloon : IPaintAndMove
    {
        public ConsoleColor Color { get; set; }
        private int x;
        private int y;

        public AirBalloon(int x, ConsoleColor color)
        {
            Color = color;
            this.x = x;
            y = Console.WindowHeight;
        }

        public void Paint()
        {
            Console.ForegroundColor = Color;
            Console.SetCursorPosition(x, y);
            Console.WriteLine("     ^^^ ");
            Console.SetCursorPosition(x + 2, y+1);
            Console.WriteLine(" ^     ^ ");
            Console.SetCursorPosition(x + 1, y+2);
            Console.WriteLine("^         ^");

            Console.SetCursorPosition(x, y + 3);
            Console.WriteLine("(");
            Console.SetCursorPosition(x + 12, y + 3);
            Console.WriteLine(")");
            for (int i = 1; i < 7; i++)
            {
                Console.SetCursorPosition(x + i, y + i + 3);
                Console.WriteLine("*");
                Console.SetCursorPosition(x - i + 12, y + i + 3);
                Console.WriteLine("*");
            }
            Console.SetCursorPosition(x + 6, y + 11);
            Console.WriteLine("|");
            Console.SetCursorPosition(x + 6, y + 10);
            Console.WriteLine("|");
            Console.SetCursorPosition(x + 6, y + 9);
            Console.WriteLine("|");
        }

        public void Move()
        {
            if (y == 0)
                y = Console.WindowHeight;
            else
                y--;
        }
    }
    class Firework : IPaintAndMove
    {
        public ConsoleColor Color { get; set; }
        private int x;
        private int y;

        public Firework(int x, ConsoleColor color)
        {
            Color = color;
            this.x = x;
            y = Console.WindowHeight;
        }

        public void Paint()
        {
            Console.ForegroundColor = Color;
            if (y == Console.WindowHeight / 2 - 5)
            {
                Console.SetCursorPosition(x, y);
                Console.WriteLine("x         x");
                Console.SetCursorPosition(x, y + 1);
                Console.WriteLine("  x     x  ");
                Console.SetCursorPosition(x, y + 2);
                Console.WriteLine("   x   x   ");
                Console.SetCursorPosition(x, y + 3);
                Console.WriteLine("    x x    ");
                Console.SetCursorPosition(x, y + 4);
                Console.WriteLine("xxxxxxxxxxx");
                Console.SetCursorPosition(x, y + 5);
                Console.WriteLine("    x x    ");
                Console.SetCursorPosition(x, y + 6);
                Console.WriteLine("   x   x   ");
                Console.SetCursorPosition(x, y + 7);
                Console.WriteLine("  x     x  ");
                Console.SetCursorPosition(x, y + 8);
                Console.WriteLine("x         x");
                Console.SetCursorPosition(x, y + 9);
                Thread.Sleep(700);
            }
            else if (y > Console.WindowHeight / 2 - 5)
            {
                Console.SetCursorPosition(x + 2, y + 2);
                Console.WriteLine("x");
                Console.SetCursorPosition(x + 2, y + 3);
                Console.WriteLine("x");
            }
        }

        public void Move()
        {
            if (y == 0)
                y = Console.WindowHeight;
            else
                y--;
        }
    }
    class Parachutist : IPaintAndMove
    {
        public ConsoleColor Color { get; set; }
        private int x;
        private int y;

        public Parachutist(int x, ConsoleColor color)
        {
            Color = color;
            this.x = x;
            y = 0;
        }

        public void Paint()
        {
            Console.ForegroundColor = Color;
            Console.SetCursorPosition(x + 1, y);
            Console.WriteLine("^^^^^^^^^^^");
            Console.SetCursorPosition(x, y + 1);
            Console.WriteLine("(");
            Console.SetCursorPosition(x + 12, y + 1);
            Console.WriteLine(")");
            for (int i = 1; i < 7; i++)
            {
                Console.SetCursorPosition(x + i, y + i + 1);
                Console.WriteLine("*");
                Console.SetCursorPosition(x - i + 12, y + i + 1);
                Console.WriteLine("*");
            }
            Console.SetCursorPosition(x + 6, y + 8);
            Console.WriteLine("|");
        }

        public void Move()
        {
            if (y == 30)
                y = 0;
            else
                y++;
        }
    }
}

