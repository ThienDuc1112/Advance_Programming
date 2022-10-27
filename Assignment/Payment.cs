using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    internal class Payment
    {
        protected int idPayment;
        protected string type;

        public int IdPayment
        {
            get { return idPayment; }
            
        }
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public Payment()
        {
            if (Program.payments.Count > 0)
            {
                int setID = Program.payments.ElementAt(Program.payments.Count - 1).IdPayment;
                this.idPayment = setID + 1;
            }
            else { this.idPayment = 1; }
        }

        public virtual void PerformPayment(decimal total) { }
    }
}
