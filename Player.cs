using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle
{
    public class Player : IPlayer
    {
        public string name_f;
        public int score_f;
        public Player(string name, int score) { name_f = name; score_f = score; }
        public bool Shot(Player player, Field field)
        {
            string? inputDate;
            string[] tokens;
            int row, column;


            Console.Write($"{player.name_f}, введите координаты для выстрела: ");
            inputDate = Console.ReadLine();
            if (!string.IsNullOrEmpty(inputDate))
            {
                tokens = inputDate.Split(' ');
                row = int.Parse(tokens[0]) - 1;
                column = int.Parse(tokens[1]) - 1;

                if (field.field[row, column] == '▢')
                {
                    player.score_f += 1;
                    field.field[row, column] = '✖';
                    return true;
                }
                else
                {
                    field.field[row, column] = '■';
                }
            }
            return false;
        }
    }
}
