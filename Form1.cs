using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cshape2017
{
    public partial class Form1 : Form
    {
        //출처 : https://stackoverflow.com/questions/41659729/how-to-draw-shapes-and-color-them-with-a-button

        Shape selected = null;
        List<Shape> paths = new List<Shape>();

        public Form1()
        {
            InitializeComponent();

            numericUpDown1.Value = 2;
            numericUpDown2.Value = 4;

            numericUpDown3.Minimum = 0;
            numericUpDown3.Maximum = 360;

            numericUpDown3.Value = 90;


            numericUpDown4.Minimum = 0;
            numericUpDown4.Maximum = 360;
            numericUpDown4.Value = 270;


            numericUpDown5.Minimum = 0;
            numericUpDown5.Maximum = 360;
            numericUpDown5.Value = 150;


            numericUpDown6.Minimum = 0;
            numericUpDown6.Maximum = 1000;
            numericUpDown6.Value = 300;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            


            ARcReDraw();
        }

        List<Shape> FillList(int segments, int angle1, int angle2, int inner, int outer, int rings)
        {
            try
            {
                List<Shape> paths = new List<Shape>();

                float deltaA = 1f * (angle2 - angle1) / segments;
                float width = 1f * (outer - inner) / rings;
                for (int s = 0; s < segments; s++)
                {
                    float a = angle1 + s * deltaA;
                    for (int r = 0; r < rings; r++)
                    {
                        float w1 = r * width;
                        float w2 = w1 + width;
                        GraphicsPath gp = new GraphicsPath();

                        RectangleF rect1 = new RectangleF(w1, w1, (outer - w1) * 2, (outer - w1) * 2);
                        RectangleF rect2 = new RectangleF(w2, w2, (outer - w2) * 2, (outer - w2) * 2);
                        gp.AddArc(rect1, a, deltaA);
                        gp.AddArc(rect2, a + deltaA, -deltaA);
                        gp.CloseFigure();
                        paths.Add(new Shape(gp));
                    }
                }
                return paths;

            }
            catch (Exception ex)
            {

                return null;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Shape gp in paths)
            {
                using (SolidBrush br = new SolidBrush(gp.FillColor))
                    if (gp.FillColor != null) e.Graphics.FillPath(br, gp.Path);
                e.Graphics.DrawPath(Pens.Black, gp.Path);
                if (gp == selected) e.Graphics.DrawPath(Pens.OrangeRed, gp.Path);
                //break;
            }



            //var g = e.Graphics;
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            //var center = new Point(100, 100);
            //var innerR = 30;
            //var thickness = 60;
            //var startAngle = 150; //270;
            //var arcLength = 120;
            //var outerR = innerR + thickness;
            //var outerRect = new Rectangle
            //                (center.X - outerR, center.Y - outerR, 2 * outerR, 2 * outerR);
            //var innerRect = new Rectangle
            //                (center.X - innerR, center.Y - innerR, 2 * innerR, 2 * innerR);

            //using (var p = new GraphicsPath())
            //{
            //    p.AddArc(outerRect, startAngle, arcLength);
            //    p.AddArc(innerRect, startAngle + arcLength, -arcLength);
            //    p.CloseFigure();
            //    e.Graphics.FillPath(Brushes.Green, p);
            //    e.Graphics.DrawPath(Pens.Black, p);
            //}
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            selected = null;
            foreach (Shape gp in paths)
                if (gp.Path.IsVisible(e.Location)) { selected = gp; break; }
            Invalidate();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            ARcReDraw();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            ARcReDraw();

        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            ARcReDraw();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            ARcReDraw();
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            ARcReDraw();
        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            ARcReDraw();
        }

        private void ARcReDraw()
        {
            int segments = Decimal.ToInt32(numericUpDown2.Value);//(numericUpDown1.Value
            int angle1 = Decimal.ToInt32(numericUpDown3.Value); //numericUpDown2.Value
            int angle2 = Decimal.ToInt32(numericUpDown4.Value); //numericUpDown3.Value
            int inner = Decimal.ToInt32(numericUpDown5.Value); //numericUpDown4.Value
            int outer = Decimal.ToInt32(numericUpDown6.Value); //numericUpDown5.Value
            int rings = Decimal.ToInt32(numericUpDown1.Value); //numericUpDown6.Value


            paths = FillList(segments, angle1, angle2, inner, outer, rings);

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (selected != null)
            {
                selected.FillColor = ((Bitmap)pictureBox1.Image).GetPixel(e.X, e.Y);
                Invalidate();
            }
        }
    }
}
