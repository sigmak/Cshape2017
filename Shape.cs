using System.Drawing;
using System.Drawing.Drawing2D;

namespace Cshape2017
{
    internal class Shape
    {
        public GraphicsPath Path { get; set; }
        public Color FillColor { get; set; }

        public Shape(GraphicsPath gp) { Path = gp; }
    }
}