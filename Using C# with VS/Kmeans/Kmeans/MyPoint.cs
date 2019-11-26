using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KMeans
{
   public  class MyPoint
    {
       private float m_x;
       private float m_y;
       private int m_c;

       public float X
       {
           get { return m_x; }
           set { m_x = value; } 
       }
       public float Y
       {
           get { return m_y; }
           set { m_y = value; }
       }
       public int C
       {
           get { return m_c; }
           set { m_c = value; }
       }

    }
}
