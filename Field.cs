using System;
using System.Collections.Generic;
using System.Data.Common;
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
                    field[counterRow, counterColumn] = ' ';
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
                    Console.Write($"{field[counterRow, counterColumn]} ");
                }
                Console.WriteLine("\b ]");
            }
            Console.WriteLine();
        }

        private void AddShips(Field field, int row, int column, bool isHorizontal, bool isRight, bool isUp, int lenght)
        {
            int h, v;

            if (isHorizontal && isRight)
            {
                h = lenght;
                v = 1;

                if (lenght == 4 && column >= 7)
                {
                    column = 6;
                }
                if (lenght == 3 && column >= 8)
                {
                    column = 7;
                }
                if (lenght == 2 && column == 9)
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
                h = lenght;
                v = 1;

                if (lenght == 4 && column <= 2)
                    column = 3;
                if (lenght == 3 && column <= 1)
                    column = 2;
                if (lenght == 2 && column == 0)
                    column = 1;
                for (int counterRow = row; counterRow < row + v; counterRow++)
                    for (int counterColumn = column; counterColumn > column - h; counterColumn--)
                        field.field[counterRow, counterColumn] = '▢';
            }

            if (!isHorizontal && isUp)
            {
                h = 1;
                v = lenght;

                if (lenght == 4 && row <= 2)
                    row = 3;
                if (lenght == 3 && row <= 1)
                    row = 2;
                if (lenght == 2 && row == 0)
                    row = 1;
                for (int counterRow = row; counterRow > row - v; counterRow--)
                    for (int counterColumn = column; counterColumn < column + h; counterColumn++)
                        field.field[counterRow, counterColumn] = '▢';
            }
            else if (!isHorizontal && !isUp)
            {
                h = 1;
                v = lenght;

                if (lenght == 4 && row >= 7)
                    row = 6;
                if (lenght == 3 && row >= 8)
                    row = 7;
                if (lenght == 2 && row >= 9)
                    row = 8;
                for (int counterRow = row; counterRow < row + v; counterRow++)
                    for (int counterColumn = column; counterColumn < column + h; counterColumn++)
                        field.field[counterRow, counterColumn] = '▢';
            }
        }

        public void ArrangeShips(Field field, Player player)
        {
            string? inputDate;
            int lenght;
            int posX, posY;

            for (int counterShips = 0; counterShips < 11; counterShips++)
            {
                switch (counterShips)
                {
                    case 0:
                        Console.WriteLine($"{player.name_f}, заполните свое поле");
                        break;
                    case 1:
                        lenght = 4;
                        Console.Write("Введите координаты (четырехпалубный): ");
                        posX = Console.CursorLeft; posY = Console.CursorTop;
                        field.PrintField(field.field, 65);
                        SavePositionCursor(posX, posY);

                        inputDate = Console.ReadLine();
                        if (inputDate != null)
                        {
                            PutShipOnField(field, inputDate, lenght);
                        }
                        posX = Console.CursorLeft; posY = Console.CursorTop;
                        field.PrintField(field.field, 65);
                        SavePositionCursor(posX, posY);
                        break;
                    case 2 or 3:
                        lenght = 3;
                        Console.Write("Введите координаты (трехпалубный): ");
                        posX = Console.CursorLeft; posY = Console.CursorTop;
                        field.PrintField(field.field, 65);
                        SavePositionCursor(posX, posY);

                        inputDate = Console.ReadLine();
                        if (inputDate != null)
                        {
                            PutShipOnField(field, inputDate, lenght);
                        }

                        posX = Console.CursorLeft; posY = Console.CursorTop;
                        field.PrintField(field.field, 65);
                        SavePositionCursor(posX, posY);
                        break;
                    case 4 or 5 or 6:   
                        lenght = 2;
                        Console.Write("Введите координаты (двухпалубный): ");
                        posX = Console.CursorLeft; posY = Console.CursorTop;
                        field.PrintField(field.field, 65);
                        SavePositionCursor(posX, posY);
                        inputDate = Console.ReadLine();
                        if (inputDate != null)
                        {
                            PutShipOnField(field, inputDate, lenght);
                        }
                        posX = Console.CursorLeft; posY = Console.CursorTop;
                        field.PrintField(field.field, 65);
                        SavePositionCursor(posX, posY);
                        break;
                    case 7 or 8 or 9 or 10:         
                        lenght = 1;
                        Console.Write("Введите координаты (однопалубный): ");
                        posX = Console.CursorLeft; posY = Console.CursorTop;
                        field.PrintField(field.field, 65);
                        SavePositionCursor(posX, posY);

                        inputDate = Console.ReadLine();
                        if (inputDate != null)
                        {
                            PutShipOnField(field, inputDate, lenght);
                        }
                        posX = Console.CursorLeft; posY = Console.CursorTop;
                        field.PrintField(field.field, 65);
                        SavePositionCursor(posX, posY);
                        break;
                }
            }
        }
        private void PutShipOnField(Field field,string inputDate, int lenght)
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
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.RightArrow:
                        isHorizontal = isRight = true;
                        isUp = false;
                        field.AddShips(field, row - 1, column - 1, isHorizontal, isRight, isUp, lenght);
                        break;
                    case ConsoleKey.LeftArrow:
                        isHorizontal = true;
                        isRight = isUp = false;
                        field.AddShips(field, row - 1, column - 1, isHorizontal, isRight, isUp, lenght);
                        break;
                    case ConsoleKey.UpArrow:
                        isHorizontal = isRight = false;
                        isUp = true;
                        field.AddShips(field, row - 1, column - 1, isHorizontal, isRight, isUp, lenght);
                        break;
                    case ConsoleKey.DownArrow:
                        isHorizontal = isRight = isUp = false;
                        field.AddShips(field, row - 1, column - 1, isHorizontal, isRight, isUp, lenght);
                        break;
                }
                break;
            }
        }
        public void SavePositionCursor(int posRow, int posColumn)
        {
            Console.SetCursorPosition(posRow, posColumn);
        }
        /*private int ParseIndex(char indexRow)
        {
            int row = 0;
            switch (indexRow)
            {
                case 'А':
                    row = 1;
                    break;
                case 'Б':
                    row = 2;
                    break;
                case 'В':
                    row = 3;
                    break;
                case 'Г':
                    row = 4;
                    break;
                case 'Д':
                    row = 5;
                    break;
                case 'Е':
                    row = 6;
                    break;
                case 'Ж':
                    row = 7;
                    break;
                case 'З':
                    row = 8;
                    break;
                case 'И':
                    row = 9;
                    break;
                case 'К':
                    row = 10;
                    break;
            }
            return row;
        }*/
    }
}
