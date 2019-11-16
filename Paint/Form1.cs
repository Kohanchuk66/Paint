using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ShapeLibrary;
namespace Paint
{
    public partial class Form1 : Form
    {
        protected List<Shape_Point> Shapes;
        Mode mode = new Mode();
        public void AddShape(Shape_Point shape)
        {
            Shapes.Add(shape);
            ShapesList.Items.Add(shape);
            pictureBox1.Refresh();
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (color.ShowDialog() == DialogResult.OK)
            {
                buttonColor.BackColor = color.Color; 

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            buttonColor.BackColor = Color.Black;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (ShapesList.SelectedItems.Count==0)
            {
                Shapes.Clear();
                ShapesList.Items.Clear();
                pictureBox1.Refresh();
            }
            for (int i = 0; i < ShapesList.Items.Count; i++)
            {
                if (ShapesList.GetSelected(i))
                {
                    DeleteShape(i);
                    i--;
                }
                    
            }
            
        }
        private void DeleteShape(int Number)
        {
            Shapes.RemoveAt(Number);
            ShapesList.Items.RemoveAt(ShapesList.SelectedIndex);
                pictureBox1.Refresh();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if(Shapes == null)
            {
                Shapes = new List<Shape_Point>();
            }
            for (int i = 0; i < Shapes.Count; i++)
            {
                Shapes[i].Draw(e.Graphics);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Shapes == null)
            {
                Shapes = new List<Shape_Point>();
            }
            mode = Mode.DrawPoint;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Shapes == null)
            {
                Shapes = new List<Shape_Point>();
            }
            mode = Mode.DrawLine;
        }
        int MouseX, MouseY;
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if(Shapes == null)
            {
                Shapes = new List<Shape_Point>();
            }
            MouseX = e.X;
            MouseY = e.Y;
            switch (mode)
            {
                case Mode.DrawPoint:
                    ShapeLibrary.Point point = new ShapeLibrary.Point(                                             
                        e.X,
                        e.Y,
                        buttonColor.BackColor
                        );
                    AddShape(point);
                    break;
            }

        }
        int finalx, finaly;

        private void button3_Click(object sender, EventArgs e)
        {
            if (Shapes == null)
            {
                Shapes = new List<Shape_Point>();
            }
            mode = Mode.DrawCircle;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (Shapes == null)
            {
                Shapes = new List<Shape_Point>();
            }
            mode = Mode.DrawEllips;
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (Shapes == null)
            {
                Shapes = new List<Shape_Point>();
            }
            mode = Mode.DrawRetandle;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                finalx = e.X;
                finaly = e.Y;
                if (Shapes == null)
                    Shapes = new List<Shape_Point>();
                pictureBox1.Refresh();
                Graphics graphics = pictureBox1.CreateGraphics();
                switch (mode)
                {
                    case Mode.DrawPoint:
                        break;
                    case Mode.DrawLine:
                        graphics.DrawLine(
                             new Pen(buttonColor.BackColor),
                            MouseX,
                            MouseY,
                            finalx,
                            finaly
                            ) ;
                        break;
                    case Mode.DrawCircle:
                        graphics.DrawEllipse(
                             new Pen(buttonColor.BackColor),
                             MouseX - Math.Abs(e.Y - MouseY) * 2 / 2 - 1,
                                    MouseY - Math.Abs(e.Y - MouseY) * 2 / 2 - 1,
                                 Math.Abs(e.Y - MouseY) * 2,
                                 Math.Abs(e.Y - MouseY) * 2
                            );
                        break;
                    case Mode.DrawEllips:
                        graphics.DrawEllipse(
                             new Pen(buttonColor.BackColor),
                             MouseX - Math.Abs(e.X - MouseX) * 2 / 2 - 1,
                                    MouseY - Math.Abs(e.Y - MouseY) * 2 / 2 - 1,
                                 Math.Abs(e.X - MouseX) * 2,
                                 Math.Abs(e.Y - MouseY) * 2
                            );
                        break;
                    case Mode.DrawRetandle:
                        int maxx, maxy;
                        if (e.X < MouseX) maxx = e.X;
                        else maxx = MouseX;
                        if (e.Y < MouseY) maxy = e.Y;
                        else maxy = MouseY;
                        int heightrec = Math.Abs(e.Y - MouseY);
                        int widthrec = Math.Abs(e.X - MouseX);
                        graphics.DrawRectangle(
                            new Pen(buttonColor.BackColor),
                            maxx,
                            maxy,
                            widthrec,
                             heightrec

                            );

                        break;
                }
                
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (Shapes == null)
                Shapes = new List<Shape_Point>();
            switch (mode)
            {
                case Mode.DrawPoint:                    
                    break;
                case Mode.DrawLine:
                    AddShape(new ShapeLibrary.Line(
                        MouseX,
                        MouseY,
                        e.X,
                        e.Y,
                        buttonColor.BackColor
                        ));
                    break;
                case Mode.DrawCircle:
                    AddShape(new ShapeLibrary.Circle(
                        MouseX,
                        MouseY,
                        buttonColor.BackColor,
                        Math.Abs(e.Y - MouseY) * 2
                        ));
                    break;
                case Mode.DrawEllips:
                    AddShape(new ShapeLibrary.Ellips(                        
                                MouseX,
                                MouseY,
                                buttonColor.BackColor,
                             Math.Abs(e.X - MouseX) * 2,
                             Math.Abs(e.Y - MouseY) * 2
                        ));
                    break;
                case Mode.DrawRetandle:
                    int maxx, maxy;
                    if (e.X < MouseX) maxx = e.X;
                    else maxx = MouseX;
                    if (e.Y < MouseY) maxy = e.Y;
                    else maxy = MouseY;
                    int heightrec = Math.Abs(e.Y - MouseY);
                    int widthrec = Math.Abs(e.X - MouseX);
                    AddShape(new ShapeLibrary.Rectangle(
                        buttonColor.BackColor,
                        maxx,
                        maxy,                        
                         heightrec,
                         widthrec
                         )
                        );

                    break;
            }
        }
    }
}
