using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    internal class Cash : Payment
    {
        public Cash() : base()
        {
            this.Type = "cash";
        }

        public override void PerformPayment(decimal total)
        {
            Console.WriteLine("The method payment you selected is cash");
            Console.WriteLine($"You need to pay {total} when receiving products");
        }
    }
}
