using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    internal class Banking:Payment
    {
        public Banking() : base()
        {
            this.Type = "banking";
        }

        public override void PerformPayment(decimal total)
        {
            Console.WriteLine("The method payment you selected is banking");
            Console.WriteLine($"You need to pay {total} now");
        }
    }
}
