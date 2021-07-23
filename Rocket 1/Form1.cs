using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        private void button1_Click(object sender, EventArgs e)
        {
            //variables
            double Ve = 0;
            double m = 0;
            double mfinal = 50;
            double Xfuel = 0;
            double g = 9.81;
            double dt = 0;
            double tmax = 50;
            //read boundary conditions
            Ve = double.Parse(textBox1.Text);
            m = double.Parse(textBox2.Text);
            Xfuel = double.Parse(textBox3.Text);
            dt = double.Parse(textBox4.Text);
            //set initial conditions
            double t = 0;
            double V = 0;
            double a = 0;
            double mo = m;
            double alt = 0;
            //show table and initial results
            richTextBox1.AppendText("t,m,V,a,alt" + "\n");
            richTextBox1.AppendText(t.ToString() + "," + m.ToString("n0") + "," + V.ToString("n2") + "," + a.ToString("n2") + "," + alt.ToString("n2") + "\n");
            //run iterations
            while (t < tmax)
            {
                t = t + dt;
                double Vold = V;
                m = m - Xfuel * dt;
                V = (Ve * Math.Log(mo / m))- g*t;
                a = (V - Vold) / dt;
                alt = alt + 0.5 * (V + Vold) / dt;
                richTextBox1.AppendText(t.ToString() + "," + m.ToString("n0") + "," + V.ToString("n2") + "," + a.ToString("n2") + "," + alt.ToString("n2") + "\n");
            }
        }
    }
}
