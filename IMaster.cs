using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle
{
    interface IMaster
    {
        public void ShowMenu();
        public bool StartGame();
        public void StopGame();
        public void ShowHistory();
    }
}
