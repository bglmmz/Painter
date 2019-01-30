using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace draw_direct
{
    public partial class Form1 : Form
    {
        private int maxPageNo = 1;      
        private bool maxPageDrawed = false;
        private bool maxPageSaved = false;
        private int curPageNo = 1;
        private string path;
        private string root;
        private bool working = false;
        private bool review = false;
        


        private Point p1, p2;//定义两个点（启点，终点）

        private static bool drawing = false;//设置一个启动标志

        private Graphics g;
        private Bitmap bitmap;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bitmap = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);


            root = Application.StartupPath;
            //Thread t = new Thread(Draw);
            //t.Start();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (working) { 
                p1 = new Point(e.X, e.Y);
                p2 = new Point(e.X, e.Y);
                drawing = true;
                if (!maxPageDrawed && curPageNo == maxPageNo)
                {
                    maxPageDrawed = true;
            }
            }

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (working) {
                drawing = false;
            }
            
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (maxPageDrawed && !maxPageSaved && !review) {
                var fileName = root + @"\" + path + @"\" + curPageNo + ".png";
                pictureBox1.Image.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);

                maxPageDrawed = false;
                maxPageSaved = false;
                curPageNo = 1;                
            }
            try { 
                path = System.Guid.NewGuid().ToString();

                Directory.CreateDirectory(root + @"\" + path);
            }
            catch
            {
                MessageBox.Show("系统异常");
                System.Environment.Exit(-1);
            }
            g = pictureBox1.CreateGraphics();
            g.Clear(pictureBox1.BackColor);
            working = true;
            btnPrev.Enabled = true;
            btnNext.Enabled = true;
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (curPageNo == maxPageNo)
            {
                if (maxPageDrawed)
                {
                    if (!maxPageSaved && !review)
                    {
                        var fileName = root + @"\" + path + @"\" + curPageNo + ".png";
                        pictureBox1.Image.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
                    }

                }

            }
           if (curPageNo > 1)
            {
                curPageNo--;
                var fileName = root + @"\" + path + @"\" + curPageNo + ".png";
                pictureBox1.Load(fileName);
                review = true;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

            if (curPageNo == maxPageNo)
            {  
                if(maxPageDrawed ){
                    if (!maxPageSaved && !review)
                    {
                        //var fileName = root + @"\" + path + @"\" + curPageNo + ".png";
                        //pictureBox1.Image.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);

                        var fileName = root + @"\" + path + @"\" + curPageNo + ".jpg";
                        pictureBox1.Image.Save(fileName);
                    }
                    curPageNo++;
                    maxPageNo = curPageNo;
                    maxPageDrawed = false;
                    maxPageSaved = false;
                    review = false;

                    g = pictureBox1.CreateGraphics();
                    g.Clear(pictureBox1.BackColor);
                    return;
                }
                else
                {
                    maxPageDrawed = false;
                    maxPageSaved = false;
                    review = false;
                    return;
                }

            }
            else if (curPageNo < maxPageNo)
            {
                curPageNo++;
                var fileName = root + @"\" + path + @"\" + curPageNo + ".png";
                pictureBox1.Load(fileName);
            }       
        }



        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (working) { 
                if (e.Button == MouseButtons.Left)
                {
                    if (drawing)
                    {
                        drawing = true;
                        Point currentPoint = new Point(e.X, e.Y);

                        g = Graphics.FromImage(bitmap);
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;//消除锯齿
                        g.DrawLine(new Pen(Color.Blue, 2), p2, currentPoint);
                        pictureBox1.Image = (Image)bitmap;
                        p2.X = currentPoint.X;
                        p2.Y = currentPoint.Y;                       
                    }

                }
            }
        }

        //private void pictureBox1_Click(object sender, EventArgs e)
        //{

        //}


        private void Draw()
        {
            
            //Point currentPoint = new Point(e.X, e.Y);
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;//消除锯齿
            //g.DrawLine(new Pen(Color.Blue, 2), p2, currentPoint);

            //p2.X = currentPoint.X;
            //p2.Y = currentPoint.Y;
        }
    }   
}
