using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle
{
    class Master
    {
        public Master() { }
        public void ShowMenu()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Clear();
            Console.Title = "Sea Battle";
            Console.WriteLine("|----------Игровое меню-----------|\n" +
                              "|● Начать игру - F1               |\n" +
                              "|● Посмотреть историю - F2        |\n" +
                              "|● Справка - F3                   |\n" +
                              "|● Выход - ESC                    |\n" +
                              "|---------------------------------|");
            //ShowField();
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.F1:
                        StartGame(); break;

                    case ConsoleKey.F2:
                        ShowHistory(); break;

                    case ConsoleKey.F3:
                        ShowHistory(); break;

                    case ConsoleKey.Escape: return;
                }
            }
        }
        public void StartGame()
        {
            Random rnd = new Random();
            int startNumb = rnd.Next(1,2);
            Console.Clear();

            Console.Write("Игрок 1, введите свой ник: ");
            Player player1 = new Player(InitNickname(), 0);

            Console.Write("Игрок 2, введите свой ник: ");
            Player player2 = new Player(InitNickname(), 0);

            CheckName(player1, player2);
            GetConfirmation("Готовы начать игру? Для старта нажмите - Enter");
            

            Field field1 = new Field();
            field1.field = field1.InitField();

            Field field2 = new Field();
            field2.field = field2.InitField();

            field1.ArrangeShips(field1, player1);
            GetConfirmation($"Нажмите - Enter, для передачи управления {player2.name_f}");
            field2.ArrangeShips(field2, player2);

            GetConfirmation("Для продолжения нажмите - Enter");

            
            
            if (startNumb == 1)
            {
                while (true)
                {
                    if (!player1.Shot(player1, field2)) 
                    {
                        GetConfirmation("Вы промахнулись! Для передачи хода - Enter");
                        break;
                    }
                    if (StopGame(player1, player2))
                        return;
                }
                while (true)
                {
                    if (!player2.Shot(player2, field1)) 
                    {
                        GetConfirmation("Вы промахнулись! Для передачи хода - Enter");
                        break;
                    }
                    if (StopGame(player1, player2))
                        return;
                }
                                       
            }
            else
            {
                while (true)
                {
                    if (!player2.Shot(player2, field1)) 
                    {
                        GetConfirmation("Вы промахнулись! Для передачи хода - Enter");
                        break; 
                    }
                    if (StopGame(player1, player2))
                        return;
                }
                while (true)
                {
                    if (!player1.Shot(player1, field2)) 
                    {
                        GetConfirmation("Вы промахнулись! Для передачи хода - Enter");
                        break;
                    }
                    if (StopGame(player1, player2))
                        return;
                }
            }
                 
            
        }
        private string InitNickname()
        {
            string? name;
            name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                name = "Player";
            }
            return name;
        }
        private void CheckName(Player player1, Player player2)
        {
            if (player1.name_f == player2.name_f)
            {
                player1.name_f += " (1)";
                player2.name_f += " (2)";
            }
        }
        public bool StopGame(Player player1, Player player2)
        {
            if (player1.score_f == 20 || player2.score_f == 20)
            {
                Console.WriteLine($"{(player1.score_f == 20 ? player1.name_f : player2.name_f)} выиграл игру!");
                return true;
            }
            return false;
        }
        public void ShowHistory()
        {
        }
        public void ShowReference(string filename)
        {
        }
        private void GetConfirmation(string message)
        {
            while (true)
            {
                Console.WriteLine($"{message}");
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Ошибка! Вы нажали не Enter");
                }
            }
        }
        public void SavePositionCursor(int posRow, int posColumn)
        {
            Console.SetCursorPosition(posRow, posColumn);
        }
        /*public void ShowField()
        {
            char[,] field = new char[10, 10];
            for (int counterRow = 0; counterRow < field.GetLength(0); counterRow++)
                for (int counterColumn = 0; counterColumn < field.GetLength(1); counterColumn++)
                {
                    field[counterRow, counterColumn] = ' ';
                }

            Console.WriteLine("     А Б В Г Д Е Ж З И К");
            for (int counterRow = 0; counterRow < field.GetLength(0); counterRow++)
            {
                Console.Write((counterRow + 1).ToString().PadLeft(2) + " [ ");
                for (int counterColumn = 0; counterColumn < field.GetLength(1); counterColumn++)
                {
                    Console.Write($"{field[counterRow, counterColumn]} ");
                }
                Console.WriteLine("\b ]");
            }
            Console.WriteLine();
        }*/
    }
}
