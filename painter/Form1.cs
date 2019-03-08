using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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

        private List<string> dataList = new List<string>();
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
            initData();
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
            //if (maxPageDrawed && !maxPageSaved && !review) {
            //    var fileName = root + @"\" + path + @"\" + curPageNo + ".png";
            //    if (System.IO.File.Exists(fileName))
            //    {
            //        Image clone = (Image)pictureBox1.Image.Clone();
            //        pictureBox1.Image.Dispose();

            //        System.IO.File.Delete(fileName);
            //        pictureBox1.Image = clone;
            //    }

                


            //    pictureBox1.Image.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);

            //    maxPageDrawed = false;
            //    maxPageSaved = false;
            //    curPageNo = 1;
            //}
            //try {
            //    path = System.Guid.NewGuid().ToString();

            //    Directory.CreateDirectory(root + @"\" + path);
            //}
            //catch
            //{
            //    MessageBox.Show("系统异常");
            //    System.Environment.Exit(-1);
            //}
            //g = pictureBox1.CreateGraphics();
            //g.Clear(pictureBox1.BackColor);
            //working = true;
            //btnPrev.Enabled = true;
            //btnNext.Enabled = true;
            Draw();
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
                        if (System.IO.File.Exists(fileName))
                        {
                            Image clone = (Image)pictureBox1.Image.Clone();
                            pictureBox1.Image.Dispose();

                            System.IO.File.Delete(fileName);
                            pictureBox1.Image = clone;
                        }
                        pictureBox1.Image.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);                        

                        maxPageSaved = true;
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
            working = false;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (curPageNo < maxPageNo)
            {
                curPageNo++;

                if (curPageNo < maxPageNo)
                {
                    var fileName = root + @"\" + path + @"\" + curPageNo + ".png";
                    pictureBox1.Load(fileName);
                }
                else
                {
                    if (maxPageDrawed && maxPageSaved)
                    {
                        var fileName = root + @"\" + path + @"\" + curPageNo + ".png";
                        pictureBox1.Load(fileName);

                        maxPageDrawed = true;
                    }
                    else
                    {
                        //g = Graphics.FromImage(bitmap);
                        //g.Clear(pictureBox1.BackColor);
                        pictureBox1.Image = null;
                        //g = Graphics.FromImage(pictureBox1.Image);
                        g.Clear(pictureBox1.BackColor);

                        maxPageDrawed = false;


                    }                   
                    maxPageSaved = false;
                    review = false;
                    review = false;
                    working = true;
                }

            }
            else
            { 
                if (maxPageDrawed)
                {
                    if (!maxPageSaved && !review)
                    {
                        //var fileName = root + @"\" + path + @"\" + curPageNo + ".png";
                        //pictureBox1.Image.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);

                        var fileName = root + @"\" + path + @"\" + curPageNo + ".png";
                        pictureBox1.Image.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
                    }
                    curPageNo++;
                    maxPageNo = curPageNo;
                    maxPageDrawed = false;
                    maxPageSaved = false;
                    review = false;


                    //g = Graphics.FromImage(bitmap);
                    //g.Clear(pictureBox1.BackColor);
                    //pictureBox1.Image = null;
                    pictureBox1.Image.Dispose();
                    //g = Graphics.FromImage(pictureBox1.Image);
                    g.Clear(pictureBox1.BackColor);

                    working = true;
                    return;
                }
                else
                {
                    maxPageDrawed = false;
                    maxPageSaved = false;
                    review = false;
                    working = true;
                    return;
                }

            }



            //if (curPageNo == maxPageNo)
            //{
            //    if (maxPageDrawed) {
            //        if (!maxPageSaved && !review)
            //        {
            //            //var fileName = root + @"\" + path + @"\" + curPageNo + ".png";
            //            //pictureBox1.Image.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);

            //            var fileName = root + @"\" + path + @"\" + curPageNo + ".png";
            //            pictureBox1.Image.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
            //        }
            //        curPageNo++;
            //        maxPageNo = curPageNo;
            //        maxPageDrawed = false;
            //        maxPageSaved = false;
            //        review = false;


            //        //g = Graphics.FromImage(bitmap);
            //        //g.Clear(pictureBox1.BackColor);
            //        pictureBox1.Image = null;
            //        //g = Graphics.FromImage(pictureBox1.Image);
            //        g.Clear(pictureBox1.BackColor);

            //        working = true;
            //        return;
            //    }
            //    else
            //    {
            //        maxPageDrawed = false;
            //        maxPageSaved = false;
            //        review = false;
            //        working = true;
            //        return;
            //    }

            //}
            //else if (curPageNo < maxPageNo)
            //{
            //    curPageNo++;

            //    if (curPageNo < maxPageNo) { 
            //        var fileName = root + @"\" + path + @"\" + curPageNo + ".png";
            //        pictureBox1.Load(fileName);
            //    }

            //}
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
                        g.DrawLine(new Pen(Color.Blue, 6), p2, currentPoint);
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

        private void Draw_FillRectangle()
        {

            //Point currentPoint = new Point(e.X, e.Y);
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;//消除锯齿
            //g.DrawLine(new Pen(Color.Blue, 2), p2, currentPoint);

            //p2.X = currentPoint.X;
            //p2.Y = currentPoint.Y;
            SolidBrush brush = new SolidBrush(Color.Blue);

            for (int i = 0; i < dataList.Count; i++)
            {
                Dot[] dotList = parseToDot(dataList[i]);
                for (int j = 0; j < dotList.Length; j++)
                {
                    Dot dot = dotList[j];

                    int x = (int)((1280.00 / 32767.00) * (dot.x - dot.width / 2));

                    int y = (int)((800.00 / 32767.00) * (dot.y - dot.height / 2));

                    float w = (float)((1280.00 / 32767.00) * dot.width);

                    float h = (float)((800.00 / 32767.00) * dot.height);

                    Point currentPoint = new Point(x, y);

                    if (p2.IsEmpty && dot.status == 7)
                    {   
                        p2 = currentPoint;
                    }

                    g = Graphics.FromImage(bitmap);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;//消除锯齿

                    
                    g.FillRectangle(brush, x,y, x+w, y+h);

                    pictureBox1.Image = (Image)bitmap;

                    pictureBox1.Refresh();

                    Thread.Sleep(100);

                }                
            }
        }

        
        private void Draw()
        {

            //Point currentPoint = new Point(e.X, e.Y);
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;//消除锯齿
            //g.DrawLine(new Pen(Color.Blue, 2), p2, currentPoint);

            //p2.X = currentPoint.X;
            //p2.Y = currentPoint.Y;
            for (int i=0; i<dataList.Count; i++)
            {
                Dot[] dotList = parseToDot(dataList[i]);
                for(int j=0; j<dotList.Length; j++)
                {
                    Dot dot = dotList[j];

                    int x = (int)((1280.00 / 32767.00) * (dot.x - dot.width / 2));

                    int y = (int)((800.00 / 32767.00) * (dot.y - dot.height / 2));
     
                    float w = (float)((1280.00 / 32767.00) * dot.width);

                    float h = (float)((800.00 / 32767.00) * dot.height);
     
                    Point currentPoint = new Point(x, y);

                    if (p2.IsEmpty && dot.status==7) {
                        p2 = currentPoint;
                    }
                    
                    g = Graphics.FromImage(bitmap);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;//消除锯齿

                    Pen pen = new Pen(Color.Blue, w);
                    pen.StartCap = LineCap.Round;
                    pen.EndCap = LineCap.Round;
                    g.DrawLine(pen, p2, currentPoint);
                    pictureBox1.Image = (Image)bitmap;
                    p2.X = currentPoint.X;
                    p2.Y = currentPoint.Y;

                    if (dot.status == 4)
                    {
                        p2 = Point.Empty;
                    }

                    pictureBox1.Refresh();

                    Thread.Sleep(10);
                }
            }
        }

        private void Draw2()
        {

            //Point currentPoint = new Point(e.X, e.Y);
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;//消除锯齿
            //g.DrawLine(new Pen(Color.Blue, 2), p2, currentPoint);

            //p2.X = currentPoint.X;
            //p2.Y = currentPoint.Y;
            for (int i = 0; i < dataList.Count; i++)
            {
                Dot[] dotList = parseToDot(dataList[i]);
                for (int j = 0; j < dotList.Length; j++)
                {
                    Dot dot = dotList[j];

                    int x = 200+ (int)((1280.00 / 32767.00) * (dot.x - dot.width / 2));

                    int y = (int)((800.00 / 32767.00) * (dot.y - dot.height / 2));

                    float w = (float)((1280.00 / 32767.00) * dot.width);

                    float h = (float)((800.00 / 32767.00) * dot.height);

                    Point currentPoint = new Point(x, y);

                    if (dot.status == 7)
                    {
                        if (p2.IsEmpty)
                        {
                            p2 = currentPoint;
                        }
                        else
                        {
                            if (p2.X == currentPoint.X && p2.Y == currentPoint.Y)
                            {
                                continue;
                            }
                        }
                    
                    }


                    g = Graphics.FromImage(bitmap);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;//消除锯齿

                    Pen pen = new Pen(Color.Blue, 3);
                    pen.StartCap = LineCap.Round;
                    pen.EndCap = LineCap.Round;

                    g.DrawLine(pen, p2, currentPoint);
                    pictureBox1.Image = (Image)bitmap;
                    //p2.X = currentPoint.X;
                    //p2.Y = currentPoint.Y;

                    p2 = currentPoint;

                    if (dot.status == 4)
                    {
                        p2 = Point.Empty;
                    }

                    pictureBox1.Refresh();

                    Thread.Sleep(10);
                }
            }

        }


        private void Draw3()
        {

            for (int i = 0; i < dataList.Count; i++)
            {
                Dot[] dotList = parseToDot(dataList[i]);
                for (int j = 0; j < dotList.Length; j++)
                {
                    Dot dot = dotList[j];

                    int x = 400 + (int)((1280.00 / 32767.00) * dot.x);

                    int y = (int)((800.00 / 32767.00) * dot.y);

                    float w = (float)((1280.00 / 32767.00) * dot.width);



                    Point currentPoint = new Point(x, y);

                    if (p2.IsEmpty && dot.status == 7)
                    {
                        p2 = currentPoint;
                    }

                    g = Graphics.FromImage(bitmap);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;//消除锯齿

                    Pen pen = new Pen(Color.Blue, w);
                    pen.StartCap = LineCap.Round;
                    pen.EndCap = LineCap.Round;

                    g.DrawLine(pen, p2, currentPoint);
                    pictureBox1.Image = (Image)bitmap;
                    p2.X = currentPoint.X;
                    p2.Y = currentPoint.Y;

                    if (dot.status == 4)
                    {
                        p2 = Point.Empty;
                    }

                    pictureBox1.Refresh();

                    Thread.Sleep(10);
                }
            }

        }

        private void Draw4()
        {

            for (int i = 0; i < dataList.Count; i++)
            {

                //WriteLog(" data line No: "+i);

                Dot[] dotList = parseToDot(dataList[i]);

                int sumX = 0;
                int sumY = 0;

                int status = 0;

                for (int j = 0; j < dotList.Length; j++)
                {
                    Dot dot = dotList[j];
                    sumX += dot.x;
                    sumY += dot.y;
                    status = dot.status;
                }
                int x = sumX / dotList.Length;
                int y = sumY / dotList.Length;


                //float w = (float)((1280.00 / 32767.00) * dot.width);

                //WriteLog(" dot No: "+ j + ", x=" + x + ", y=" + y);

                Point currentPoint = new Point(x, y);

                if (p2.IsEmpty && status == 7)
                {
                    p2 = currentPoint;
                }

                g = Graphics.FromImage(bitmap);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;//消除锯齿

                Pen pen = new Pen(Color.Blue, 3);
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;

                g.DrawLine(pen, p2, currentPoint);
                pictureBox1.Image = (Image)bitmap;
                p2.X = currentPoint.X;
                p2.Y = currentPoint.Y;

                if (status == 4)
                {
                    p2 = Point.Empty;
                }

                pictureBox1.Refresh();

                    Thread.Sleep(10);
                
            }

        }
        private void initData()
        {
            dataList.Add("a107015002f80cb200c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a107015002080db200c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a107015002080db200c8020702f003500dd600c80200000000000000000000000000000000000000000000000000000000000000000000000000000000020000");
            dataList.Add("a107015002080d1e01c8020702f003500dd600c80200000000000000000000000000000000000000000000000000000000000000000000000000000000020000");
            dataList.Add("a107017002300dd600c8020702f003f00dd600c80200000000000000000000000000000000000000000000000000000000000000000000000000000000020000");
            dataList.Add("a107017002300dd600c8020702f003f00db200640100000000000000000000000000000000000000000000000000000000000000000000000000000000020000");
            dataList.Add("a10701a002680d6401c8020702f003f00dd600640100000000000000000000000000000000000000000000000000000000000000000000000000000000020000");
            dataList.Add("a10701a002680d6401c8020702f003d00e1e01c80200000000000000000000000000000000000000000000000000000000000000000000000000000000020000");
            dataList.Add("a10701e002d80dd001c80207020004f00f1e01c80200000000000000000000000000000000000000000000000000000000000000000000000000000000020000");
            dataList.Add("a10701e002500e88019005070230044010d600900500000000000000000000000000000000000000000000000000000000000000000000000000000000020000");
            dataList.Add("a10701e002500e1e01c80207023004d810fa00640100000000000000000000000000000000000000000000000000000000000000000000000000000000020000");
            dataList.Add("a107013003280fb200c8020702600470118e00640100000000000000000000000000000000000000000000000000000000000000000000000000000000020000");
            dataList.Add("a10701a00308108e00b403070270046012b200640100000000000000000000000000000000000000000000000000000000000000000000000000000000020000");
            dataList.Add("a10701a00308106a006401070270046012b2006401070330033011fa00c802000000000000000000000000000000000000000000000000000000000000030000");
            dataList.Add("a107011004f0108e00c802040270046012b20064010703300378111e01c802000000000000000000000000000000000000000000000000000000000000030000");
            dataList.Add("a107011004f0108e00c8020703300378111e01c80200000000000000000000000000000000000000000000000000000000000000000000000000000000020000");
            dataList.Add("a107011004f0108e00c802070330037811d600c80200000000000000000000000000000000000000000000000000000000000000000000000000000000020000");
            dataList.Add("a104011004f0108e00c802070330037811d600640100000000000000000000000000000000000000000000000000000000000000000000000000000000020000");
            dataList.Add("a1070320039811d60064010000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a1040320039811d60064010000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a107019002900ed600c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a10701a002880ed600c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a10701a002880eb200c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a10701d002880ed600c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a107012003680ed60064010000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a107019003380e8e00c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a107012004000ed600c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a10701c004e00d8e00c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a107016005e00dd600c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a107011006c80dd6002c040000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a10701c006900db200c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a107016007580d6a00c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a10701d007300d8e00c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a107013008300dd600c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a107017008380dd600c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a107018008380dd600c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a107018008580dd6002c040000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a107017008900d6a00c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a107015008c80dd600c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a107012008180e8e00c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a10701f007500ed600c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a10701a007c00eb200c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a107015007200fb2002c040000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a10701e006980fb200c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a1070180061810b200c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a10701100670108e00c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a10701d005d010d6002c040000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a10701a0050811d600c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a10701800538116a00c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a1070160055011d600c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a10701300530118e0064010000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a10701500538116a00c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a10701200500116a00c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a10401200500116a00c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a1070180030012d600c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a10701b003c811d600c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a10701f0039811d600c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a1070150046011d600c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a10701d00458116a00c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a1070160052011d600c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a10701f005e810d600c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a107016006b810d600c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a107016006b8108e00c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a10701d006b810d600c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a10701d006b8108e00c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a107011007b0106a00c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a1070100078810460064010000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a1040100078810460064010000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a107012005900ad600c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a107012005880ad600c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a107012005880ad60064010000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a107012005a00ad600c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a107013005180bb200b4030000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a107013005180b8e002c040000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a107013005c80b8e00c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a107013005880cd600c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a107012005500dd60064010000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a107012005d00d8e00c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a107012005b80eb200c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a107012005c00fd600c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a107012005e010b200c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a1070120052012d600c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a10701200550138e00c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a1070110058814b200c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a1070110059015b200c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a107011005d016d6002c040000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a1070110054817b200c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a10701100580176a00c8020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a10701f0049017460050020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
            dataList.Add("a10401f0049017460050020000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000");
        }

        private Dot[] parseToDot(string s)
        {
            byte[] data = HexStringToByteArray(s);
            int count = (int)data[61];

            Dot[] result = new Dot[count];
            for(int i=0; i<count; i++)
            {
                int status = data[10 * i + 1];
                int x = (data[10 * i + 4] & 0xFF) << 8 | data[10 * i + 3];
                int y = (data[10 * i + 6] & 0xFF) << 8 | data[10 * i + 5];
                int width = (data[10 * i + 8] & 0xFF) << 8 | data[10 * i + 7];
                int height = (data[10 * i + 10] & 0xFF) << 8 | data[10 * i + 9];
                Dot dot = new Dot();
                dot.status = status;
                dot.x = x;
                dot.y = y;
                dot.width = width;
                dot.height = height;
                result[i] = dot;

            }

            return result;
        }

        private byte[] HexStringToByteArray(string s)
        {
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }

        class Dot
        {
            public int status;
            public int x, y;
            public int width, height;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Draw2();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Draw3();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Draw4();
        }

        class Page
        {
            int no;
            int save;
        }

        //日志写法
        public void WriteLog(string msg)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "Log";  //路径
            if (!Directory.Exists(filePath))  //不存在则创建
            {
                Directory.CreateDirectory(filePath);
            }
            //string logPath = AppDomain.CurrentDomain.BaseDirectory + "Log\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";  //文档路径
            string logPath = "D:\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";  //文档路径
            try
            {
                using (StreamWriter sw = File.AppendText(logPath))
                {
                    sw.WriteLine("消息：" + msg);
                    sw.WriteLine("时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    sw.WriteLine("**************************************************");
                    sw.WriteLine();
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                }
            }
            catch (IOException e)
            {
                using (StreamWriter sw = File.AppendText(logPath))
                {
                    sw.WriteLine("异常：" + e.Message);
                    sw.WriteLine("时间：" + DateTime.Now.ToString("yyy-MM-dd HH:mm:ss"));
                    sw.WriteLine("**************************************************");
                    sw.WriteLine();
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                }
            }
        }
    }   
}
