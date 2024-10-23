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

        public string InitNickname()
        {
            string? name;
            name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                name = "Player";
            }
            return name;
        }

        public bool Shot(Player player, Field field1, Field field2)
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

                if (field1.field[row, column] == '▢')
                {
                    player.score_f += 1;
                    field2.field[row, column] = '✘';
                    return true;
                }
                else
                {
                    field2.field[row, column] = '⁕';
                }
            }
            return false;
        }
    }
}
