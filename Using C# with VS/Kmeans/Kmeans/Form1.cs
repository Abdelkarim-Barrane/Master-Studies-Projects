using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace KMeans
{
    public partial class Form1 : Form
    {
        private List<MyPoint> tabl = new List<MyPoint>();
        private List<PointF> clusters = new List<PointF>();
        public Form1()
        {
            InitializeComponent();
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            MyPoint pf = new MyPoint();
            pf.X = e.X;
            pf.Y = e.Y;
            pf.C = -1;
            tabl.Add(pf);
            Invalidate(true);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Brush[] colors = new Brush[] { Brushes.Black, Brushes.Red, Brushes.Blue, Brushes.Green, Brushes.Violet };
            e.Graphics.Clear(Color.Gold);
            MyPoint p ;
            for (int i = 0; i < tabl.Count; i++)
            {
                p = tabl[i];
                e.Graphics.FillEllipse(colors[p.C+1], p.X-2, p.Y-2, 4, 4);
            }
           
        }
        //clustring
        private void button1_Click(object sender, EventArgs e)
        {
            Random r = new Random();
           
            // choix des classes initiale
            int K = Convert.ToInt32(textBox1.Text);
            for (int i = 0; i < K; i++)
            {
               int z = r.Next(0, tabl.Count);
               tabl[z].C = i;
               clusters.Add(new PointF(tabl[z].X, tabl[z].Y));
            }

            
            //boucle principale
            while (true) 
            {
                // classification des points
                for (int i = 0; i < tabl.Count; i++)
                {
                    tabl[i].C = classifier(tabl[i].X, tabl[i].Y); 
                }

                //Calcule des centres 
                for (int i = 0; i < clusters.Count; i++)
                {
                    clusters[i] = calcule_centre(i);
                }

                //affichage
                Invalidate(true);
                Thread.Sleep(3000 );
                Application.DoEvents();
            }
            
        }

        private PointF calcule_centre(int c) 
        {
            float sumx = 0;
            float sumy = 0;
            int count = 0;
            for (int i = 0; i < tabl.Count; i++)
            {
                if (tabl[i].C == c)
                {
                    sumx += tabl[i].X;
                    sumy += tabl[i].Y;
                    count++;
                }
            }
            sumx = sumx / count;
            sumy = sumy / count;
            return new PointF(sumx, sumy);
        }

        private int classifier(float x, float y) 
        {
            int imin=0;
            float dmin= float.MaxValue;
            for (int i = 0; i < clusters.Count; i++)
            {
               float d= distance(x, y, clusters[i].X, clusters[i].Y);
               if (d < dmin) 
               { 
                  dmin = d;
                  imin = i;
               }
            }
            return imin;
        }
        private float distance(float x1, float y1, float x2, float y2)
        {
           return (float)Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
         }
    }
}
