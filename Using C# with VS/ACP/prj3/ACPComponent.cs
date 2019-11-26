using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ACPtp
{
    public class ACPComponent
    {
        private double _lamda;

        public double[] Vector { get; set; }

        public double Lamda
        {
            get { return _lamda; }
            set { _lamda = value; }
        }
    }
    
}
