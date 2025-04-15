using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blocknot
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Form1 form = new Form1();
            form.ShowDialog();
        }
    }
}
