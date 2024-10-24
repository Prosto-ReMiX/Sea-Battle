using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle
{
    public class Field: IField
    {
        public char[,] field = new char[10, 10];
        public int Rows = 10; public int Columns = 10;

        public char[,] InitField()
        {
            
            for (int counterRow = 0; counterRow < field.GetLength(0); counterRow++)
                for (int counterColumn = 0; counterColumn < field.GetLength(1); counterColumn++)
                {
                    field[counterRow, counterColumn] = '■';
                }
            return field;
        }

        public void PrintField(char[,] field, int posX)
        {
            Console.SetCursorPosition(posX, 1);
            Console.WriteLine("     1 2 3 4 5 6 7 8 9 10");
            for (int counterRow = 0; counterRow < field.GetLength(0); counterRow++)
            {
                Console.SetCursorPosition(posX, 2 + counterRow);
                Console.Write((counterRow + 1).ToString().PadLeft(2) + " [ ");
                for (int counterColumn = 0; counterColumn < field.GetLength(1); counterColumn++)
                {
                    switch (field[counterRow, counterColumn])
                    {
                        case '▢':
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write($"{field[counterRow, counterColumn]} ");
                            Console.ResetColor();
                        break;
                        case '⁕':
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.Write($"{field[counterRow, counterColumn]} ");
                            Console.ResetColor();
                        break;
                        case '■':
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write($"{field[counterRow, counterColumn]} ");
                            Console.ResetColor();
                        break;
                        case '✘':
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write($"{field[counterRow, counterColumn]} ");
                            Console.ResetColor();
                        break;
                    }
                }
                Console.WriteLine("\b ]");
            }
            Console.WriteLine();
        }

        private static void AddShip(Field field, int row, int column, bool isHorizontal, bool isRight, bool isUp, int length)
        {
            int h, v;

            if (isHorizontal && isRight)
            {
                h = length;
                v = 1;

                if (length == 4 && column >= 7)
                {
                    column = 6;
                }
                if (length == 3 && column >= 8)
                {
                    column = 7;
                }
                if (length == 2 && column == 9)
                {
                    column = 8;
                }

                for (int counterRow = row; counterRow < row + v; counterRow++)
                    for (int counterColumn = column; counterColumn < column + h; counterColumn++)
                        field.field[counterRow, counterColumn] = '▢';
            }
            //теперь работает
            else if (isHorizontal && !isRight)
            {
                h = length;
                v = 1;

                if (length == 4 && column <= 2)
                    column = 3;
                if (length == 3 && column <= 1)
                    column = 2;
                if (length == 2 && column == 0)
                    column = 1;
                for (int counterRow = row; counterRow < row + v; counterRow++)
                    for (int counterColumn = column; counterColumn > column - h; counterColumn--)
                        field.field[counterRow, counterColumn] = '▢';
            }

            if (!isHorizontal && isUp)
            {
                h = 1;
                v = length;

                if (length == 4 && row <= 2)
                    row = 3;
                if (length == 3 && row <= 1)
                    row = 2;
                if (length == 2 && row == 0)
                    row = 1;
                for (int counterRow = row; counterRow > row - v; counterRow--)
                    for (int counterColumn = column; counterColumn < column + h; counterColumn++)
                        field.field[counterRow, counterColumn] = '▢';
            }
            else if (!isHorizontal && !isUp)
            {
                h = 1;
                v = length;

                if (length == 4 && row >= 7)
                    row = 6;
                if (length == 3 && row >= 8)
                    row = 7;
                if (length == 2 && row >= 9)
                    row = 8;
                for (int counterRow = row; counterRow < row + v; counterRow++)
                    for (int counterColumn = column; counterColumn < column + h; counterColumn++)
                        field.field[counterRow, counterColumn] = '▢';
            }
        }

        public void ArrangeShips(Field field, Player player)
        {
            int[] shipsLength = { 4, 3, 3, 2, 2, 2, 1, 1, 1, 1 };
            string[] ships = {"однопалубный", "двухпалубный", "трехпалубный", "четырехпалубный"};
            

            Console.WriteLine($"{player.name_f}, заполните свое поле");
            for (int counterLength = 0; counterLength < shipsLength.Length; counterLength++)
            {
                while (true)
                {
                    if (InitShip(field, shipsLength[counterLength], ships[shipsLength[counterLength] - 1])) break;
                }
            }
        }

        private static int ChangeCourse(Field field,string inputDate, int lenght)
        {
            string[] tokens;
            int row;
            int column;
            bool isHorizontal;
            bool isRight;
            bool isUp;
           
            tokens = inputDate.Split();
            row = int.Parse(tokens[0]);
            column = int.Parse(tokens[1]);

            if (field.field[row - 1, column - 1] == '▢')
                return -1;

            while (true)
            {
                Console.WriteLine("Выберете направление (←/→ || ↑/↓)");
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.RightArrow:
                        isHorizontal = isRight = true;
                        isUp = false;
                        if (!CheckCoordinate(isHorizontal, isRight, isUp, lenght, row - 1, column - 1)) return -2;
                        AddShip(field, row - 1, column - 1, isHorizontal, isRight, isUp, lenght);
                        break;
                    case ConsoleKey.LeftArrow:
                        isHorizontal = true;
                        isRight = isUp = false;
                        if (!CheckCoordinate(isHorizontal, isRight, isUp, lenght, row - 1, column - 1)) return -2;
                        AddShip(field, row - 1, column - 1, isHorizontal, isRight, isUp, lenght);
                        break;
                    case ConsoleKey.UpArrow:
                        isHorizontal = isRight = false;
                        isUp = true;
                        if (!CheckCoordinate(isHorizontal, isRight, isUp, lenght, row - 1, column - 1)) return -2;
                        AddShip(field, row - 1, column - 1, isHorizontal, isRight, isUp, lenght);
                        break;
                    case ConsoleKey.DownArrow:
                        isHorizontal = isRight = isUp = false;
                        if (!CheckCoordinate(isHorizontal, isRight, isUp, lenght, row - 1, column - 1)) return -2;
                        AddShip(field, row - 1, column - 1, isHorizontal, isRight, isUp, lenght);
                        break;
                }
                break;
            }
            return 0;
        }

        public static void SavePositionCursor(int posRow, int posColumn)
        {
            Console.SetCursorPosition(posRow, posColumn);
        }

        private static bool CheckCoordinate(bool isHorizontal, bool isRight, bool isUp, int length, int row, int column)
        {
            if (isHorizontal && !isRight && !isUp)
               if (column - length < 0) return false;
            if (isHorizontal && isRight && !isUp)
               if (column + length > 9) return false;
            if (!isHorizontal && !isRight && isUp)
               if (row - length < 0) return false;
            if (!isHorizontal && !isRight && !isUp)
               if (row + length > 9) return false;
            return true;
        }

        private static bool InitShip(Field field, int length, string shipName)
        {
            string? inputDate;
            int posX, posY;

            while (true)
            {
                Console.Write($"Введите координаты ({shipName}): ");
                posX = Console.CursorLeft; posY = Console.CursorTop;
                field.PrintField(field.field, 65);
                SavePositionCursor(posX, posY);

                inputDate = Console.ReadLine();
                if (!string.IsNullOrEmpty(inputDate))
                {
                    switch (ChangeCourse(field, inputDate, length))
                    {
                        case -1:
                            {
                                Console.WriteLine("Ошибка, клетка уже занята");
                                return false;
                            }
                        case -2:
                            {
                                Console.WriteLine("Ошибка, направление выбрано неправильно");
                                return false;
                            }
                        default:
                            goto DefEntry;
                    }
                DefEntry:
                    break;
                }
                else
                {
                    Console.WriteLine("Ошибка! Нужно ввести координаты");
                    return false;
                }
            }
            posX = Console.CursorLeft; posY = Console.CursorTop;
            field.PrintField(field.field, 65);
            SavePositionCursor(posX, posY);
            return true;
        }
    }
}
