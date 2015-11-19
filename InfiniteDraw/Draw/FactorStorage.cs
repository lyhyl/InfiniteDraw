using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniteDraw.Draw
{
    public class FactorStorage
    {
        private List<Factor> factors = new List<Factor>();

        public event FactorEvent FactorCreated;
        public event FactorEvent FactorRemoved;
        public event FactorEvent FactorActived;

        public FactorStorage()
        {

        }

        public void Reset()
        {
            factors.Clear();
        }

        public void Create()
        {
            Factor factor = new Factor();
            factors.Add(factor);

            if (FactorCreated != null)
                FactorCreated(this, new FactorEventArgs(factor));
        }

        public void Remove(Factor f)
        {
            factors.Remove(f);

            if (FactorRemoved != null)
                FactorRemoved(this, new FactorEventArgs(f));
        }

        public void Active(Factor f)
        {
            if (FactorActived != null)
                FactorActived(this, new FactorEventArgs(f));
        }

        public Factor this[int index] { get { return factors[index]; } }
    }

    public delegate void FactorEvent(object sender, FactorEventArgs e);
    public class FactorEventArgs : EventArgs
    {
        public Factor Factor { set; get; }
        public FactorEventArgs(Factor f)
        {
            Factor = f;
        }
    }
}
