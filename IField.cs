using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle
{
    interface IField
    {
        public char[,] InitField();
        public void PrintField(char[,] field, int posX);
        public void ArrangeShips(Field field, Player player);
    }
}
