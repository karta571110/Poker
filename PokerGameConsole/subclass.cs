using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGameConsole
{
    class subclass
    {
        private double salary = 100.0;
        //salary 成員則是透過公用唯讀屬性存取
        public double Salary
        {
            get { return salary; }
            set { salary = value; }
        }
    }
}
