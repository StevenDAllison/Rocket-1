using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Rocket_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        public void button1_Click(object sender, EventArgs e)
        {
            //variables
            double Ve = 0;
            double m = 0;
            double mfinal = 50;
            double Xfuel = 0;
            double g = 9.81;
            double dt = 0;
            double tmax = 0;
            //read boundary conditions
            Ve = double.Parse(textBox1.Text);
            m = double.Parse(textBox2.Text);
            tmax = double.Parse(textBox3.Text);
            dt = double.Parse(textBox4.Text);
            //set initial conditions
            double t = 0;
            double V = 0;
            double a = 0;
            double mo = m;
            double alt = 0;
            double Q = 0;
            int step = 0;
            //Calculate Xfuel
            Xfuel = (m - mfinal) / tmax;
            label6.Text = "Xfuel = " + Xfuel;
            //show table and initial results
            richTextBox1.AppendText("t" + "\t" + "m" + "\t" + "V" + "\t" + "a" + "\t" + "alt" + "\t" + "Q" + "\n");
            richTextBox1.AppendText(t.ToString("n0") + "\t" + m.ToString("n0") + "\t" + V.ToString("n1") + "\t" + a.ToString("n1") +
                "\t" + alt.ToString("n1") + "\t" + Q.ToString("n0") + "\n");
            //save to file
            string filename = "file1.txt";
            SaveFileNew(filename, t, m, V, a, alt, Q, step);
            //run iterations
            while (t < tmax)
            {
                t = t + dt;
                step = step + 1;
                double Vold = V;
                m = m - Xfuel * dt;
                V = (Ve * Math.Log(mo / m)) - g * t;
                double rho = 1.225 * Math.Exp(-0.00013 * alt);
                Q = 0.5 * rho * V * V;
                //drag model
                double Cd = 0.025;
                double S0 = Math.PI * 1.5 * 1.5;
                double drag = 0.5 * rho * V * V * S0 * Cd;
                double deltaV = drag / m * dt;
                V = V - deltaV;
                // calculate a and alt using new V value
                a = (V - Vold) / dt;
                alt = alt + 0.5 * (V + Vold) * dt;
                //show result on screen
                richTextBox1.AppendText(t.ToString("n0") + "\t" + m.ToString("n0") + "\t" + V.ToString("n1") + "\t" +
                    a.ToString("n1") + "\t" + alt.ToString("n1") + "\t" + Q.ToString("n1") + "\n");
                SaveFile(filename, t, m, V, a, alt, Q, step);
            }
        }
        public static async Task SaveFile(string filename, double t, double m, double V, double a, double alt, double Q, int step)
        {
            TextWriter line = new StreamWriter(filename, append: true);

               
            line.Write(t.ToString("n2") + "\t" + m.ToString("n1") + "\t" + V.ToString("n1") + "\t" + a.ToString("n1") +
                       "\t" + alt.ToString("n1") + "\t" + Q.ToString("n0") + "\n");
            line.Close();
        }

        public static async Task SaveFileNew(string filename, double t, double m, double V, double a, double alt, double Q, int step)
        {
            TextWriter line = new StreamWriter(filename);

                line.Write("t\tm\tV\ta\talt\tQ\n");

            line.Write(t.ToString("n2") + "\t" + m.ToString("n1") + "\t" + V.ToString("n1") + "\t" + a.ToString("n1") +
                       "\t" + alt.ToString("n1") + "\t" + Q.ToString("n0") + "\n");
            line.Close();
        }
    }
}
