using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StarMathLib;


namespace ACPtp
{
    public partial class Form1 : Form
    {
        List<double[]> Points = new List<double[]>();
        List<ACPComponent> res = new List<ACPComponent>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        { }

        private void button1_Click(object sender, EventArgs e)
        {
            res = acp(Points.ToArray(), Points.Count, 2);
            Invalidate(true);
        }

        private List<ACPComponent> acp(double[][] x, int n, int p)
        {
            //allocation de x prime
            double[][] xprime = new double[n][];

            //remplissage de x prime
            for (int i = 0; i < n; i++)
            {
                xprime[i] = new double[p];
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < p; j++)
                {
                    xprime[i][j] = x[i][j] - moyen(x, j) / ecartype(x, j);
                }
            }

            //allocation de w           
            double[][] w = new double[p][];//exception
            for (int i = 0; i < p; i++)
            {
                w[i] = new double[p];
            }
            //remplissage de w
            for (int i = 0; i < p; i++)
            {
                for (int j = 0; j < p; j++)
                {
                    double s = 0;
                    for (int k = 0; k < n; k++)
                    {
                        s += xprime[k][i] * xprime[k][j];
                    }
                    w[i][j] = s;
                }
            }
            //calcule les valeurs propres
            double[,] wv = Convertmatrice(w, p, p);
            double[][] V, L;

            L = StarMath.GetEigenValuesAndVectors(wv,
                                                  out V);
            List<ACPComponent> res = new List<ACPComponent>();
            for (int i = 0; i < V.GetLength(0); i++)
            {
                res.Add(new ACPComponent() { Vector = V[i], Lamda = L[0][i] });
            }
            return res;
        }

        private double moyen(double[][] x, int j)
        {
            double s = 0;
            for (int i = 0; i < x.GetLength(0); i++)
            {
                s += x[i][j];
            }
            return s / x.GetLength(0);
        }

        private double ecartype(double[][] x, int j)
        {
            double m = moyen(x, j);
            double s = 0;
            for (int i = 0; i < x.GetLength(0); i++)
            {
                s += (x[i][j] - m) * (x[i][j] - m);
            }
            return Math.Sqrt(x.GetLength(0));
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Points.Add(new double[] { e.X, e.Y });
            Invalidate(true);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
            foreach (double[] obj in Points)
            {
                int x = (int)obj[0];
                int y = (int)obj[1];
                e.Graphics.FillEllipse(Brushes.White, x - 2, y - 2, 4, 4);
            }
            //calcul du centre de nuage
            PointF A = new PointF((float)moyen(Points.ToArray(), 0), (float)moyen(Points.ToArray(), 1));
            foreach (ACPComponent c in res)
            {
                PointF B = new PointF(A.X + (float)c.Lamda * 10.0f * (float)c.Vector[0], A.Y + (float)c.Lamda * 10.0f * (float)c.Vector[1]);
                e.Graphics.DrawLine(Pens.Red, A, B);
            }
        }
        private double[,] Convertmatrice(double[][] A, int nbl, int nbc)
        {
            double[,] B;
            B = new double[nbl, nbc];
            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (int j = 0; j < nbc; j++)
                {
                    B[i, j] = A[i][j];
                }
            }
            return B;
        }
    }
}
