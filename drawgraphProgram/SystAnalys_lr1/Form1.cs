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

namespace SystAnalys_lr1
{
    public partial class Form1 : Form
    {
        DrawGraph G;
        List<Vertex> V;
        List<Edge> E;
        
        double [] count;
        double[] F;
        int[,] IMatrix; //матрица инцидентности
        public int[,] AMatrix; //матрица смежности

        int selected1; //выбранные вершины, для соединения линиями
        int selected2;
        static float rectSize = 60;
        float SizeСell = 1;
        int size_i = 1;

        int h=600; //размер окна вывода функции
        int w=600;
        int zoomx;
        int zoomy;
        int Cg;
        int R = -1;
        int ten = 10;
        int x_0, y_0;
        float x_1,x_2,x_3,x_4; 
        float y_1,y_2,y_3,y_4;
        int x5, x1, x2, x3, x4; 
        int y5, y1, y2, y3, y4;

        //Параметры для изменения в функциях 
        public double p1 = 0.99;                        //Мира 2
        public double p2 = -0.5952;                     //Мира 2
        public double A = 2.27;                         //Отображение с задержкой
        public double a = 1.4;                          //Хенон
        public double b = 0.3;                          //Хенон
        public double p_1 = 2.68;
        public double p_2 = -0.1;
        public double p_3 = 1.15;
        public double p1_julia = /*-0.22*/ -0.7055;   //Жюлиа
        public double p2_julia = /*-0.74*/ - 0.3842;  // Жюлиа 

        public double x_n=-1;
        public double y_n=-1;
        public double method = -1;

        double inter = 600;
        double inter_dec, inter_x, inter_y; //inter_x и inter_y размеры ячейки * 10 в декартовых координатах.
        double  inter_x1, inter_y1, inter_x2, inter_y2; //область определения 
        double inter_dec_x, inter_dec_y; 

        public static int one = 10;
        public static int two = 10;

        public double[,] matrx = new double[10, 2];//матрицы областей декартовой области
        public double[,] matry = new double[10, 2];

        public int[] matrixVertex = new int[1000];
        public int[,] matrixmet_ = new int[10, 10];

        //Меню выбора метода
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox2.SelectedIndex == 0)
            {
                selectButton.Enabled = true;
                V.Clear();
                E.Clear();
                G.clearSheet();
                sheet.Image = G.GetBitmap();

                method = 0;
            }
            if (comboBox2.SelectedIndex == 1)
            {
                selectButton.Enabled = true;
                V.Clear();
                E.Clear();
                G.clearSheet();
                sheet.Image = G.GetBitmap();

                method = 1;
            }
            if (comboBox2.SelectedIndex == 2)
            {
                selectButton.Enabled = true;
                V.Clear();
                E.Clear();
                G.clearSheet();
                sheet.Image = G.GetBitmap();

                method = 2;
            }
        }

        //Меню выбоа системы
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                selectButton.Enabled = true;
                V.Clear();
                E.Clear();
                G.clearSheet();
                sheet.Image = G.GetBitmap();
                Cg = 0;
                listBoxfunction.Items.Clear();
                listBoxMatrix.Items.Clear();
                listBoxfunction.Items.Add("Henon");
                listBoxfunction.Items.Add("X(n+1)= 1 - a*Xn^2 + b*Yn;");
                listBoxfunction.Items.Add("Y(n+1)=Xn");
                listBoxfunction.Items.Add("a=1.4");
                listBoxfunction.Items.Add("b=0.3");
                R = 1;
                inter_dec = 1.5;

                inter_x = 3; inter_y = 3;               //интервал для хенона от -1,5 до 1,5 по x/y
                y_n = 0; x_n = 0;
                zoomx = 200;
                zoomy = 200;

            }
            if (comboBox1.SelectedIndex == 1)
            {
                selectButton.Enabled = true;
                V.Clear();
                E.Clear();
                G.clearSheet();
                sheet.Image = G.GetBitmap();

                listBoxfunction.Items.Clear();
                listBoxMatrix.Items.Clear();
                listBoxfunction.Items.Add("Mira 1");
                listBoxfunction.Items.Add("X(n+1)= (1 - p1)*Xn + Yn;");
                listBoxfunction.Items.Add("Y(n+1)=p1*p2*Xn + p3*Yn - p1*Xn^3");
                listBoxfunction.Items.Add("p1=2.68");
                listBoxfunction.Items.Add("p2=-0.1");
                listBoxfunction.Items.Add("p3=1.15");
                Cg = 0;
                inter_dec = 1.2;
                inter_x = 2.4; inter_y = 2.4;
                y_n = 1; x_n = 1;
                zoomx = 250;
                zoomy = 250;
                R = 1;
            }
            if (comboBox1.SelectedIndex == 2)
            {
                selectButton.Enabled = true;
                V.Clear();
                E.Clear();
                G.clearSheet();
                sheet.Image = G.GetBitmap();

                listBoxfunction.Items.Clear();
                listBoxMatrix.Items.Clear();
                listBoxfunction.Items.Add("Julia");
                listBoxfunction.Items.Add("X(n+1)= Xn^2 - Yn^2 + p1");
                listBoxfunction.Items.Add("Y(n+1)= 2*Xn*Yn + p2");
                listBoxfunction.Items.Add("p1=-0.7055");
                listBoxfunction.Items.Add("p2=-0.3842");
                Cg = 0;
                inter_dec = 1.4;
                inter_x = 2.8; inter_y = 2.8;
                zoomx = 214;
                zoomy = 214;
                y_n = x_n = 2;
                R = 4;
            }
            if (comboBox1.SelectedIndex == 3)
            {
                selectButton.Enabled = true;
                V.Clear();
                E.Clear();
                G.clearSheet();
                sheet.Image = G.GetBitmap();

                listBoxfunction.Items.Clear();
                listBoxMatrix.Items.Clear();
                listBoxfunction.Items.Add("Отображение с задержкой");
                listBoxfunction.Items.Add("X(n+1)= Yn;");
                listBoxfunction.Items.Add("Y(n+1)=a*Yn*(1-Xn)");
                listBoxfunction.Items.Add("a=2.27");                
                Cg = 4;
                inter_dec = 0.5;
                R = 1;
                inter_x = 0.1; inter_y = 0.1;
                inter_x1 = 0; inter_x2 = 1;
                inter_y1 = 0; inter_y2 = 1;
                inter_dec_x =0;
                inter_dec_y =inter_y2 - inter_y1;
                zoomx = 300;
                zoomy = 300;
                y_n = x_n = 3;
            }
            if (comboBox1.SelectedIndex == 4)
            {
                selectButton.Enabled = true;
                V.Clear();
                E.Clear();
                G.clearSheet();
                sheet.Image = G.GetBitmap();

                listBoxfunction.Items.Clear();
                listBoxMatrix.Items.Clear();
                listBoxfunction.Items.Add("Mira 2");
                listBoxfunction.Items.Add("X(n+1)=p1*Xn+Yn;");
                listBoxfunction.Items.Add("Y(n+1)=p2+Xn^2");
                listBoxfunction.Items.Add("p1=1");
                listBoxfunction.Items.Add("p2=-0.5952");
                Cg = 0;
                inter_dec = 1.5;
                inter_x = 3; inter_y = 3;
                y_n = 4; x_n = 4;
                zoomx = 200;
                zoomy = 200;
                R = 1;
            }
        }

        public Form1()
        {
            InitializeComponent();
            V = new List<Vertex>();
            G = new DrawGraph(sheet.Width, sheet.Height);
            E = new List<Edge>();
            sheet.Image = G.GetBitmap();
        }

        //Кнопка старт
        private void selectButton_Click(object sender, EventArgs e)
        {
            selectButton.Enabled = false;
            V.Clear();
            E.Clear();
            G.clearSheet();
            //G.drawALLGraph(V, E);
            sheet.Image = G.GetBitmap();
            listBoxMatrix.Items.Clear();

            if (method == 0)
            {
                listBoxMatrix.Items.Add("Первая итерация началась");
                //selected1 = -1;
                SizeСell = 1;
                size_i = 1;

                
                if (Cg == 4)
                {
                    /*for (int i = 0; i < 10; i++)
                    {
                        matrx[i, 0] = (i * inter_x) + inter_x1;
                        matrx[i, 1] = ((i + 1) * inter_x) + inter_x1;
                        matry[i, 0] = (i * inter_y) + inter_y1;
                        matry[i, 1] = ((i + 1) * inter_y) + inter_y1;
                        //File.AppendAllText("C:\\Users\\SuperUser\\Google Диск\\НИР-Мат мех\\прога по нир\\drawgraphProgram\\orign.txt", matry[i, 0].ToString());
                        //File.AppendAllText("C:\\Users\\SuperUser\\Google Диск\\НИР-Мат мех\\прога по нир\\drawgraphProgram\\orign.txt", "  ");
                    }

                    double fun5x = 0, fun5y = 0, fun1x = 0, fun1y = 0, fun2x = 0, fun2y = 0, fun3x = 0, fun3y = 0, fun4x = 0, fun4y = 0;
                    for (int i = 0; i < 10; i++)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            FunctionFx((matrx[i, 0] + inter_dec / 10), (matry[j, 0] + inter_dec / 10), ref fun5x);
                            FunctionFy((matrx[i, 0] + inter_dec / 10), (matry[j, 0] + inter_dec / 10), ref fun5y);
                            float x50 = (float)fun5x * zoomx;
                            float y50 = rectSize * 10  - (float)fun5y * zoomy;
                            float xcenter = (float)(matrx[i, 0] + inter_dec / 10) * zoomx;
                            float ycenter = rectSize * 10 - (float)(matry[j, 0] + inter_dec / 10) * zoomy;

                            FunctionFx((matrx[i, 0] + inter_dec / 30), (matry[j, 0] + inter_dec / 30), ref fun1x);
                            FunctionFy((matrx[i, 0] + inter_dec / 30), (matry[j, 0] + inter_dec / 30), ref fun1y);
                            float x10 = (float)fun1x * zoomx;
                            float y10 = rectSize * 10 - (float)fun1y * zoomy;

                            FunctionFx((matrx[i, 1] - inter_dec / 30), (matry[j, 0] + inter_dec / 30), ref fun2x);
                            FunctionFy((matrx[i, 1] - inter_dec / 30), (matry[j, 0] + inter_dec / 30), ref fun2y);
                            float x20 = (float)fun2x * zoomx;
                            float y20 = rectSize * 10 - (float)fun2y * zoomy;

                            FunctionFx((matrx[i, 0] + inter_dec / 30), (matry[j, 1] - inter_dec / 30), ref fun3x);
                            FunctionFy((matrx[i, 0] + inter_dec / 30), (matry[j, 1] - inter_dec / 30), ref fun3y);
                            float x30 = (float)fun3x * zoomx;
                            float y30 = rectSize * 10 - (float)fun3y * zoomy;

                            FunctionFx((matrx[i, 1] - inter_dec / 30), (matry[j, 1] - inter_dec / 30), ref fun4x);
                            FunctionFy((matrx[i, 1] - inter_dec / 30), (matry[j, 1] - inter_dec / 30), ref fun4y);
                            float x40 = (float)fun4x * zoomx;
                            float y40 = rectSize * 10 - (float)fun4y * zoomy;
                            V.Add(new Vertex(fun5x, fun5y, x50, y50, xcenter, ycenter, rectSize, fun1x, fun1y, x10, y10, fun2x, fun2y, x20, y20, fun3x, fun3y, x30, y30, fun4x, fun4y, x40, y40));

                        }
                    }*/
                    size_i = 1;
                    ten = 10;
                    for (int i = 0; i < 10; i++)
                    {
                        matrx[i, 0] = (i * inter_x) + inter_x1;
                        matrx[i, 1] = ((i + 1) * inter_x) + inter_x1;
                        matry[i, 0] = (i * inter_y) + inter_y1;
                        matry[i, 1] = ((i + 1) * inter_y) + inter_y1;
                        //File.AppendAllText("C:\\Users\\SuperUser\\Google Диск\\НИР-Мат мех\\прога по нир\\drawgraphProgram\\orign.txt", matry[i, 0].ToString());
                        //File.AppendAllText("C:\\Users\\SuperUser\\Google Диск\\НИР-Мат мех\\прога по нир\\drawgraphProgram\\orign.txt", "  ");
                    }
                    for (int ii = 0; ii < 10; ii++)
                    {
                        for (int jj = 0; jj < 10; jj++)
                        {
                            G.drawRectangle(ii, jj, rectSize);
                        }
                    }
                    double newx = 0; float x = 0;
                    double newy = 0; float y = 0;
                    double oldx = 0.1;
                    double oldy = 0.1;
                    for (int i = 0; i < 120; i++)
                        for (int j = 0; j < 120; j++)
                        {

                            newx = oldy;
                            newy = 2.27 * oldy * (1 - oldx);
                            //newx = (1-p1) * oldx + oldy;//нов
                            //newy = p1*p2*oldx + p3*oldy - p1*oldx *oldx * oldx;//нов
                            //newx =1-a*oldx*oldx+b*oldy;
                            //newy =oldx;
                            //newx = 0.99 * oldx + oldy;
                            //newy = -0.5952 + oldx * oldx;

                            oldx = newx;
                            oldy = newy;

                            x = (float)newx * 600;
                            y = 600 - (float)newy * 600;


                            //g.DrawEllipse(Pens.LightCoral, x, y, 1, 1);
                            for (int ii = 0; ii < 10; ii++)
                            {
                                for (int jj = 0; jj < 10; jj++)
                                {
                                    if (x > ii * rectSize && x < (ii + 1) * rectSize && y > jj * rectSize && y < (jj + 1) * rectSize)
                                    {
                                        matrixmet_[ii, jj] = 1;
                                        G.drawRectangle0(ii, jj, rectSize);
                                    }
                                }
                            }
                            G.drawEllipseO(x, y, i);
                        }
                    for (int ii = 0; ii < 10; ii++)
                    {
                        for (int jj = 0; jj < 10; jj++)
                        {
                            G.drawRectangle(ii, jj, rectSize);
                        }
                    }
                    listBoxMatrix.Items.Add("Первая итерация закончилась");
                }
                if(Cg==0)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        matrx[i, 0] = (i * inter_x/10) - inter_dec;
                        matrx[i, 1] = ((i + 1) * inter_x/10) - inter_dec;
                        matry[i, 0] = (i * inter_y/10) - inter_dec;
                        matry[i, 1] = ((i + 1) * inter_y/10) - inter_dec;
                    }

                    double fun5x = 0, fun5y = 0, fun1x = 0, fun1y = 0, fun2x = 0, fun2y = 0, fun3x = 0, fun3y = 0, fun4x = 0, fun4y = 0;
                    for (int i = 0; i < 10; i++)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            FunctionFx((matrx[i, 0] + inter_dec / 10), (matry[j, 0] + inter_dec / 10), ref fun5x);
                            FunctionFy((matrx[i, 0] + inter_dec / 10), (matry[j, 0] + inter_dec / 10), ref fun5y);
                            float x50 = rectSize * 10 / 2 + (float)fun5x * zoomx;
                            float y50 = rectSize * 10 / 2 - (float)fun5y * zoomy;
                            float xcenter = rectSize * 10 / 2 + (float)(matrx[i, 0] + inter_dec / 10) * zoomx;
                            float ycenter = rectSize * 10 / 2 + (float)(matry[j, 0] + inter_dec / 10) * zoomy;

                            FunctionFx((matrx[i, 0] + inter_dec / 30), (matry[j, 0] + inter_dec / 30), ref fun1x);
                            FunctionFy((matrx[i, 0] + inter_dec / 30), (matry[j, 0] + inter_dec / 30), ref fun1y);
                            float x10 = rectSize * 10 / 2 + (float)fun1x * zoomx;
                            float y10 = rectSize * 10 / 2 - (float)fun1y * zoomy;

                            FunctionFx((matrx[i, 1] - inter_dec / 30), (matry[j, 0] + inter_dec / 30), ref fun2x);
                            FunctionFy((matrx[i, 1] - inter_dec / 30), (matry[j, 0] + inter_dec / 30), ref fun2y);
                            float x20 = rectSize * 10 / 2 + (float)fun2x * zoomx;
                            float y20 = rectSize * 10 / 2 - (float)fun2y * zoomy;

                            FunctionFx((matrx[i, 0] + inter_dec / 30), (matry[j, 1] - inter_dec / 30), ref fun3x);
                            FunctionFy((matrx[i, 0] + inter_dec / 30), (matry[j, 1] - inter_dec / 30), ref fun3y);
                            float x30 = rectSize * 10 / 2 + (float)fun3x * zoomx;
                            float y30 = rectSize * 10 / 2 - (float)fun3y * zoomy;

                            FunctionFx((matrx[i, 1] - inter_dec / 30), (matry[j, 1] - inter_dec / 30), ref fun4x);
                            FunctionFy((matrx[i, 1] - inter_dec / 30), (matry[j, 1] - inter_dec / 30), ref fun4y);
                            float x40 = rectSize * 10 / 2 + (float)fun4x * zoomx;
                            float y40 = rectSize * 10 / 2 - (float)fun4y * zoomy;
                            V.Add(new Vertex(fun5x, fun5y, x50, y50, xcenter, ycenter, rectSize, fun1x, fun1y, x10, y10, fun2x, fun2y, x20, y20, fun3x, fun3y, x30, y30, fun4x, fun4y, x40, y40));

                        }
                    }
                }
                
                for (int i = 0; i < V.Count; i++)
                {
                    for (int j = 0; j < V.Count; j++)
                    {
                        if (V[i].xpic <= (V[j].xcenter + rectSize / 2) && V[i].xpic >= (V[j].xcenter - rectSize / 2) &&
                            V[i].ypic <= (V[j].ycenter + rectSize / 2) && V[i].ypic >= (V[j].ycenter - rectSize / 2))
                        {
                            E.Add(new Edge(i, j));
                            V[j].state = true;
                        }
                        if (V[i].xpic1 <= (V[j].xcenter + rectSize / 2) && V[i].xpic1 >= (V[j].xcenter - rectSize / 2) &&
                            V[i].ypic1 <= (V[j].ycenter + rectSize / 2) && V[i].ypic1 >= (V[j].ycenter - rectSize / 2))
                        {
                            E.Add(new Edge(i, j));
                            V[j].state = true;
                        }
                        if (V[i].xpic2 <= (V[j].xcenter + rectSize / 2) && V[i].xpic2 >= (V[j].xcenter - rectSize / 2) &&
                            V[i].ypic2 <= (V[j].ycenter + rectSize / 2) && V[i].ypic2 >= (V[j].ycenter - rectSize / 2))
                        {
                            E.Add(new Edge(i, j));
                            V[j].state = true;
                        }
                        if (V[i].xpic3 <= (V[j].xcenter + rectSize / 2) && V[i].xpic3 >= (V[j].xcenter - rectSize / 2) &&
                            V[i].ypic3 <= (V[j].ycenter + rectSize / 2) && V[i].ypic3 >= (V[j].ycenter - rectSize / 2))
                        {
                            E.Add(new Edge(i, j));
                            V[j].state = true;
                        }
                        if (V[i].xpic4 <= (V[j].xcenter + rectSize / 2) && V[i].xpic4 >= (V[j].xcenter - rectSize / 2) &&
                            V[i].ypic4 <= (V[j].ycenter + rectSize / 2) && V[i].ypic4 >= (V[j].ycenter - rectSize / 2))
                        {
                            E.Add(new Edge(i, j));
                            V[j].state = true;
                        }
                    }
                }
                for (int i = 0; i < V.Count; i++)
                {
                    if (V[i].state)
                    {
                        G.drawRectanglecell(V[i].xcenter - rectSize / 2, V[i].ycenter - rectSize / 2, rectSize);
                    }
                }
                for (int i = 0; i < V.Count; i++)
                {
                    G.drawRectangle_cell(V[i].xcenter - rectSize / 2, V[i].ycenter - rectSize / 2, rectSize);
                    G.drawEllipse0(V[i].xpic, V[i].ypic);
                    /*G.drawEllipse0(V[i].xpic1, V[i].ypic1);
                    G.drawEllipse0(V[i].xpic2, V[i].ypic2);
                    G.drawEllipse0(V[i].xpic3, V[i].ypic3);
                    G.drawEllipse0(V[i].xpic4, V[i].ypic4);*/
                    //G.drawEllipse0(V[i].xcenter, V[i].ycenter);
                }
                listBoxMatrix.Items.Add("Первая итерация закончилась");
            }
            if(method == 1)
            {
                // при каждой итерации, вычисляется znew = zold² + С

                // вещественная  и мнимая части постоянной C
                double cRe, cIm;
                // вещественная и мнимая части старой и новой
                double newRe=0, newIm=0, oldRe, oldIm;
                // Можно увеличивать и изменять положение
                double zoom = 1;
                //Определяем после какого числа итераций функция должна прекратить свою работу
                int maxIterations = 300;

                //выбираем несколько значений константы С, это определяет форму фрактала Жюлиа
                cRe = -0.22;
                cIm = -0.74;
     
                //newRe = 1.5 * (0 - w / 2) / (0.5 * zoom * w) + moveX;
                //newIm = (0 - h / 2) / (0.5 * zoom * h) + moveY;
                //"перебираем" каждый пиксель
                for (int x = 0; x < 600; x++)
                    for (int y = 0; y < 600; y++)
                    {
                        //вычисляется реальная и мнимая части числа z
                        //на основе расположения пикселей,масштабирования и значения позиции
                        /*newRe = 1.5 * (x - 600 / 2) / (0.5 * zoom * w);
                        newIm = 1.5 * (y - 600 / 2) / (0.5 * zoom * h);*/

                        newRe = (x - w / 2) / ((float)zoomx);
                        newIm = -(y - h / 2) / (float)zoomy;

                        //i представляет собой число итераций 
                        int i;

                        //начинается процесс итерации
                        for (i = 0; i < maxIterations; i++)
                        {

                            //Запоминаем значение предыдущей итерации
                            oldRe = newRe;
                            oldIm = newIm;

                            // в текущей итерации вычисляются действительная и мнимая части 
                            FunctionFx(oldRe, newIm, ref newRe);
                            FunctionFy(oldRe, newIm, ref newIm);
                            //newRe = oldRe * oldRe - oldIm * oldIm + cRe;
                            //newIm = 2 * oldRe * oldIm + cIm;

                            // если точка находится вне круга с радиусом 2 - прерываемся
                            if ((newRe * newRe + newIm * newIm) > R) break;
                        }
                        G.drawEllipseG(y, x, i);
                        //определяем цвета

                    }
            }
            if (method == 2)
            {
                double oldx = 0.1;
                double oldy = 0.1;
                double newx = 0, newy = 0; float px, py;
                for (int i = 0; i < 350; i++)
                {
                    for (int j = 0; j < 350; j++)
                    {
                        FunctionFx(oldx, oldy, ref newx);
                        FunctionFy(oldx, oldy, ref newy);

                        oldx = newx;
                        oldy = newy;

                        px = (float)newx * zoomx + w / 2;
                        py = h / 2 - (float)newy * zoomy;

                        G.drawEllipse0(px, py);
                    }
                }
                x_0 = (int)rectSize * 10 / 2;
                y_0 = (int)rectSize * 10 / 2;
                G.drawEllipse(x_0, y_0);
            }
        }

        //кнопка - рисовать вершину
        /*private void drawVertexButton_Click(object sender, EventArgs e)
        {
            drawVertexButton.Enabled = false;
            selectButton.Enabled = true;
            drawEdgeButton.Enabled = true;
            deleteButton.Enabled = true;
            G.clearSheet();
            G.drawALLGraph(V, E);
            sheet.Image = G.GetBitmap();
            //int a = 0;
        }*/

        //кнопка - рисовать ребро
        /*private void drawEdgeButton_Click(object sender, EventArgs e)
        {
            drawEdgeButton.Enabled = false;
            selectButton.Enabled = true;
            drawVertexButton.Enabled = true;
            deleteButton.Enabled = true;
            G.clearSheet();
            G.drawALLGraph(V, E);
            sheet.Image = G.GetBitmap();
            selected1 = -1;
            selected2 = -1;
        }*/

        //кнопка - удалить элемент
        /*private void deleteButton_Click(object sender, EventArgs e)
        {
            deleteButton.Enabled = false;
            selectButton.Enabled = true;
            drawVertexButton.Enabled = true;
            drawEdgeButton.Enabled = true;
            G.clearSheet();
            G.drawALLGraph(V, E);
            sheet.Image = G.GetBitmap();
        }*/

        //кнопка - удалить 
        private void deleteALLButton_Click(object sender, EventArgs e)
        {
            selectButton.Enabled = true;
            const string message = "Вы действительно хотите полностью удалить отображение?";
            const string caption = "Удаление";
            var MBSave = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (MBSave == DialogResult.Yes)
            {
                V.Clear();
                E.Clear();
                G.clearSheet();
                sheet.Image = G.GetBitmap();
            }
        }

        //кнопка - матрица смежности
        /*private void buttonAdj_Click(object sender, EventArgs e)
        {
            //createAdjAndOut();
        }*/

        //кнопка - матрица инцидентности 
       /* private void buttonInc_Click(object sender, EventArgs e)
        {
            //createIncAndOut();
        }
        
        */
        //дейсвия после нажания мышкой на окно вывода отображения
        private void sheet_MouseClick(object sender, MouseEventArgs e)
        {
            if (method == 0)
            {
                if (Cg == 4)
                {
                    ten = ten * 10;
                    size_i = size_i * 2;
                    selectButton.Enabled = true;
                    G.clearSheet();
                    sheet.Image = G.GetBitmap();
                    listBoxMatrix.Items.Clear();
                    listBoxMatrix.Items.Add(size_i + " итерация началась");
                    for (int i = 0; i < 10; i++)
                    {
                        matrx[i, 0] = (i * inter_x) + inter_x1;
                        matrx[i, 1] = ((i + 1) * inter_x) + inter_x1;
                        matry[i, 0] = (i * inter_y) + inter_y1;
                        matry[i, 1] = ((i + 1) * inter_y) + inter_y1;
                        //File.AppendAllText("C:\\Users\\SuperUser\\Google Диск\\НИР-Мат мех\\прога по нир\\drawgraphProgram\\orign.txt", matry[i, 0].ToString());
                        //File.AppendAllText("C:\\Users\\SuperUser\\Google Диск\\НИР-Мат мех\\прога по нир\\drawgraphProgram\\orign.txt", "  ");
                    }
                    for (int ii = 0; ii < 10; ii++)
                    {
                        for (int jj = 0; jj < 10; jj++)
                        {
                            G.drawRectangle(ii, jj, rectSize);
                        }
                    }
                    double newx = 0; float x = 0;
                    double newy = 0; float y = 0;
                    double oldx = 0.1;
                    double oldy = 0.1;
                    for (int i = 0; i < 120; i++)
                        for (int j = 0; j < 120; j++)
                        {

                            newx = oldy;
                            newy = 2.27 * oldy * (1 - oldx);

                            oldx = newx;
                            oldy = newy;

                            x = (float)newx * 600;
                            y = 600 - (float)newy * 600;


                            //g.DrawEllipse(Pens.LightCoral, x, y, 1, 1);
                            for (int ii = 0; ii < 1000; ii++)
                            {
                                for (int jj = 0; jj < 1000; jj++)
                                {
                                    if (x > ii * rectSize / size_i && x < (ii + 1) * rectSize / size_i && y > jj * rectSize / size_i && y < (jj + 1) * rectSize / size_i)
                                    {
                                        //matrixmet_[ii, jj] = 1;
                                        G.drawRectangle0(ii, jj, rectSize / size_i);
                                        G.drawRectangle(ii, jj, rectSize / size_i);
                                    }
                                }
                            }

                            //G.drawEllipseO(x, y, i);
                        }
                    for (int ii = 0; ii < 10; ii++)
                    {
                        for (int jj = 0; jj < 10; jj++)
                        {
                            G.drawRectangle(ii, jj, rectSize);
                        }
                    }
                    sheet.Image = G.GetBitmap();
                    listBoxMatrix.Items.Add(size_i + " итерация закончилась");

                }
                if (Cg == 0)
                {
                    SizeСell = SizeСell + 1;
                    size_i = size_i * 2;
                    selectButton.Enabled = true;
                    G.clearSheet();
                    sheet.Image = G.GetBitmap();
                    listBoxMatrix.Items.Clear();
                    listBoxMatrix.Items.Add(SizeСell + " итерация началась");

                    iteracia();
                    for (int i = 0; i < 10; i++)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            G.drawRectangle1(i, j, rectSize);
                        }
                    }
                    for (int i = 0; i < V.Count; i++)
                    {
                        if (V[i].SizeCell == rectSize / size_i)
                        {
                            if (V[i].state)
                            {
                                G.drawRectanglecell(V[i].xcenter - V[i].SizeCell / 2, V[i].ycenter - V[i].SizeCell / 2, V[i].SizeCell);
                                G.drawRectangle_cell(V[i].xcenter - V[i].SizeCell / 2, V[i].ycenter - V[i].SizeCell / 2, V[i].SizeCell);

                            }
                        }
                    }
                    for (int i = 0; i < V.Count; i++)
                    {

                        if (V[i].state)
                        {
                            //G.drawRectangle_cell(V[i].xcenter - V[i].SizeCell / 2, V[i].ycenter - V[i].SizeCell / 2, V[i].SizeCell);


                            //G.drawEllipse0(V[i].xcenter + V[i].SizeCell / 2, V[i].ycenter + V[i].SizeCell / 2);
                        }
                        if (V[i].state != true)
                        {
                            if (V[i].SizeCell == rectSize)
                            {
                                //G.drawRectangle_cell(V[i].xcenter - V[i].SizeCell / 2, V[i].ycenter - V[i].SizeCell / 2, V[i].SizeCell);

                            }
                        }
                        //G.drawEllipse0(V[i].xpic, V[i].ypic);
                    }

                    x_0 = (int)rectSize * 10 / 2;
                    y_0 = (int)rectSize * 10 / 2;
                    G.drawEllipse(x_0, y_0);
                    x_1 = rectSize * 10 / 2 + (float)(-inter_dec) * zoomx;
                    y_1 = y_0;
                    G.drawEllipse1(x_1, y_1);
                    x_2 = x_0;
                    y_2 = rectSize * 10 / 2 - (float)inter_dec * zoomy;
                    G.drawEllipse1(x_2, y_2);
                    x_3 = rectSize * 10 / 2 + (float)(inter_dec) * zoomx;
                    y_3 = y_0;
                    G.drawEllipse1(x_3, y_3);
                    x_4 = x_0;
                    y_4 = rectSize * 10 / 2 - (float)(-inter_dec) * zoomy;
                    G.drawEllipse1(x_4, y_4);

                    sheet.Image = G.GetBitmap();
                    listBoxMatrix.Items.Add(SizeСell + " итерация закончилась");
                }
                
            }
            

            /*int x = 100; int y=100;
            //нажата кнопка "рисовать граф"
            if (drawgraphButton.Enabled == false)
            {
                for(int i = 0; i < k/2; i++)
                {
                    V.Add(new Vertex(x, y));
                    G.drawVertex(x, y, V.Count.ToString());
                    sheet.Image = G.GetBitmap();
                    y = y + 100;
                }
                y = 50; x = x + 100;
                for (int i = k/2; i < k; i++)
                {
                    V.Add(new Vertex(x, y));
                    G.drawVertex(x, y, V.Count.ToString());
                    sheet.Image = G.GetBitmap();
                    y = y + 100;
                }

                for(int i = 0; i < k; i++)
                {
                    for(int j = 0; j < k; j++)
                    {
                        if(AMatrix[i, j] == 1)
                        {
                            //G.drawSelectedVertex(V[0].x, V[0].y);
                            //selected2 = i;
                            E.Add(new Edge(i, j));
                            G.drawEdge(V[i], V[j], E[E.Count - 1], E.Count - 1);
                            //selected1 = -1;
                            //selected2 = -1;
                            sheet.Image = G.GetBitmap();
                            //break;
                            /*G.drawSelectedVertex(V[i].x, V[i].y);
                            //selected1 = i;
                            sheet.Image = G.GetBitmap();
                            break;*/
            /* }
         }
     }*/
            /* if (e.Button == MouseButtons.Left)
             {
                 for (int i = 0; i < V.Count; i++)
                 {
                     if (Math.Pow((V[i].x - e.X), 2) + Math.Pow((V[i].y - e.Y), 2) <= G.R * G.R)
                     {
                         if (selected1 == -1)
                         {
                             G.drawSelectedVertex(V[i].x, V[i].y);
                             selected1 = i;
                             sheet.Image = G.GetBitmap();
                             break;
                         }
                         if (selected2 == -1)
                         {
                             G.drawSelectedVertex(V[i].x, V[i].y);
                             selected2 = i;
                             E.Add(new Edge(selected1, selected2));
                             G.drawEdge(V[selected1], V[selected2], E[E.Count - 1], E.Count - 1);
                             selected1 = -1;
                             selected2 = -1;
                             sheet.Image = G.GetBitmap();
                             break;
                         }
                     }
                 }
             }
             if (e.Button == MouseButtons.Right)
             {
                 if ((selected1 != -1) &&
                     (Math.Pow((V[selected1].x - e.X), 2) + Math.Pow((V[selected1].y - e.Y), 2) <= G.R * G.R))
                 {
                     G.drawVertex(V[selected1].x, V[selected1].y, (selected1 + 1).ToString());
                     selected1 = -1;
                     sheet.Image = G.GetBitmap();
                 }
             }
             */
            /*}
            //нажата кнопка "выбрать вершину", ищем степень вершины
           /* if (selectButton.Enabled == false)
            {
                for (int i = 0; i < V.Count; i++)
                {
                    if (Math.Pow((V[i].x - e.X), 2) + Math.Pow((V[i].y - e.Y), 2) <= G.R * G.R)
                    {
                        if (selected1 != -1)
                        {
                            selected1 = -1;
                            G.clearSheet();
                            G.drawALLGraph(V, E);
                            sheet.Image = G.GetBitmap();
                        }
                        if (selected1 == -1)
                        {
                            G.drawSelectedVertex(V[i].x, V[i].y);
                            selected1 = i;
                            sheet.Image = G.GetBitmap();
                            createAdjAndOut();
                            listBoxMatrix.Items.Clear();
                            int degree = 0;
                            for (int j = 0; j < V.Count; j++)
                                degree += AMatrix[selected1, j];
                            listBoxMatrix.Items.Add("Степень вершины №" + (selected1 + 1) + " равна " + degree);
                            break;
                        }
                    }
                }
            }
            //нажата кнопка "рисовать вершину"
            if (drawVertexButton.Enabled == false)
            {
                V.Add(new Vertex(e.X, e.Y));
                G.drawVertex(e.X, e.Y, V.Count.ToString());
                sheet.Image = G.GetBitmap();
            }
            //нажата кнопка "рисовать ребро"
            if (drawEdgeButton.Enabled == false)
            {
                if (e.Button == MouseButtons.Left)
                {
                    for (int i = 0; i < V.Count; i++)
                    {
                        if (Math.Pow((V[i].x - e.X), 2) + Math.Pow((V[i].y - e.Y), 2) <= G.R * G.R)
                        {
                            if (selected1 == -1)
                            {
                                G.drawSelectedVertex(V[i].x, V[i].y);
                                selected1 = i;
                                sheet.Image = G.GetBitmap();
                                break;
                            }
                            if (selected2 == -1)
                            {
                                G.drawSelectedVertex(V[i].x, V[i].y);
                                selected2 = i;
                                E.Add(new Edge(selected1, selected2));
                                G.drawEdge(V[selected1], V[selected2], E[E.Count - 1], E.Count - 1);
                                selected1 = -1;
                                selected2 = -1;
                                sheet.Image = G.GetBitmap();
                                break;
                            }
                        }
                    }
                }
                if (e.Button == MouseButtons.Right)
                {
                    if ((selected1 != -1) &&
                        (Math.Pow((V[selected1].x - e.X), 2) + Math.Pow((V[selected1].y - e.Y), 2) <= G.R * G.R))
                    {
                        G.drawVertex(V[selected1].x, V[selected1].y, (selected1 + 1).ToString());
                        selected1 = -1;
                        sheet.Image = G.GetBitmap();
                    }
                }
            }
            //нажата кнопка "удалить элемент"
            if (deleteButton.Enabled == false)
            {
                bool flag = false; //удалили ли что-нибудь по ЭТОМУ клику
                //ищем, возможно была нажата вершина
                for (int i = 0; i < V.Count; i++)
                {
                    if (Math.Pow((V[i].x - e.X), 2) + Math.Pow((V[i].y - e.Y), 2) <= G.R * G.R)
                    {
                        for (int j = 0; j < E.Count; j++)
                        {
                            if ((E[j].v1 == i) || (E[j].v2 == i))
                            {
                                E.RemoveAt(j);
                                j--;
                            }
                            else
                            {
                                if (E[j].v1 > i) E[j].v1--;
                                if (E[j].v2 > i) E[j].v2--;
                            }
                        }
                        V.RemoveAt(i);
                        flag = true;
                        break;
                    }
                }
                //ищем, возможно было нажато ребро
                if (!flag)
                {
                    for (int i = 0; i < E.Count; i++)
                    {
                        if (E[i].v1 == E[i].v2) //если это петля
                        {
                            if ((Math.Pow((V[E[i].v1].x - G.R - e.X), 2) + Math.Pow((V[E[i].v1].y - G.R - e.Y), 2) <= ((G.R + 2) * (G.R + 2))) &&
                                (Math.Pow((V[E[i].v1].x - G.R - e.X), 2) + Math.Pow((V[E[i].v1].y - G.R - e.Y), 2) >= ((G.R - 2) * (G.R - 2))))
                            {
                                E.RemoveAt(i);
                                flag = true;
                                break;
                            }
                        }
                        else //не петля
                        {
                            if (((e.X - V[E[i].v1].x) * (V[E[i].v2].y - V[E[i].v1].y) / (V[E[i].v2].x - V[E[i].v1].x) + V[E[i].v1].y) <= (e.Y + 4) &&
                                ((e.X - V[E[i].v1].x) * (V[E[i].v2].y - V[E[i].v1].y) / (V[E[i].v2].x - V[E[i].v1].x) + V[E[i].v1].y) >= (e.Y - 4))
                            {
                                if ((V[E[i].v1].x <= V[E[i].v2].x && V[E[i].v1].x <= e.X && e.X <= V[E[i].v2].x) ||
                                    (V[E[i].v1].x >= V[E[i].v2].x && V[E[i].v1].x >= e.X && e.X >= V[E[i].v2].x))
                                {
                                    E.RemoveAt(i);
                                    flag = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                //если что-то было удалено, то обновляем граф на экране
                if (flag)
                {
                    G.clearSheet();
                    G.drawALLGraph(V, E);
                    sheet.Image = G.GetBitmap();
                }
            }*/
        }

        //Кнопка сохраниния рисунка
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (sheet.Image != null)
            {
                SaveFileDialog savedialog = new SaveFileDialog();
                savedialog.Title = "Сохранить картинку как...";
                savedialog.OverwritePrompt = true;
                savedialog.CheckPathExists = true;
                savedialog.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.JPG)|*.JPG|Image Files(*.GIF)|*.GIF|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";
                savedialog.ShowHelp = true;
                if (savedialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        sheet.Image.Save(savedialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        //Функция X(n+1)
        private void FunctionFx(double xn, double yn, ref double xn1)
        {
            if (x_n == 0 && y_n == 0)
            {
                xn1 = 1 - a * xn * xn + b * yn;  //хенон
            }
            if (x_n == 1 && y_n == 1)
            {
                xn1 = (1-p_1) * xn + yn;  //Мира   1
            }
            if (x_n == 2 && y_n == 2)
            {
                xn1 = xn * xn - yn * yn + p1_julia;
            }
            if (x_n == 3 && y_n == 3)
            {
                xn1 = yn;        //функция с задержкой
            }
            if (x_n == 4 && y_n == 4)
            {
                xn1 = p1 * xn + yn; //Мира 2
            }
        }

        //Функция Y(n+1)
        private void FunctionFy(double xn, double yn, ref double yn1)
        {
            if (x_n == 0 && y_n == 0)
            {
                yn1 = xn;  //хенон
            }
            if (x_n == 1 && y_n == 1)
            {
                //yn1 = p2 + xn * xn;  
                yn1 = p_1*p_2*xn + p_3*yn - p_1*xn*xn*xn; //Мира 1
            }
            if (x_n == 2 && y_n == 2)
            {
                yn1 = 2 * xn * yn + p2_julia;
            }
            if (x_n == 3 && y_n == 3)
            {
                yn1 = A * yn * (1 - xn);         //функция с задержкой               
            }
            if (x_n == 4 && y_n == 4)
            {
                yn1 = p2 + xn * xn;  //Мира  2              
            }
        }
        
        //функция итерации в методе символического образа
        private void iteracia()
        {
            E.Clear();
            int u = 6;

            if (Cg == 4)
            {
                for (int i = 0; i < V.Count; i++)
                {
                    if (V[i].state)
                    {
                        float xcenter1, xcenter2, xcenter3, xcenter4;
                        float ycenter1, ycenter2, ycenter3, ycenter4;
                        double xdec1 = 0, xdec2 = 0, xdec3 = 0, xdec4 = 0;
                        double ydec1 = 0, ydec2 = 0, ydec3 = 0, ydec4 = 0;
                        double xdec_1_1 = 0, xdec_2_1 = 0, xdec_3_1 = 0, xdec_4_1 = 0;
                        double ydec_1_1 = 0, ydec_2_1 = 0, ydec_3_1 = 0, ydec_4_1 = 0;
                        double xdec_1_2 = 0, xdec_2_2 = 0, xdec_3_2 = 0, xdec_4_2 = 0;
                        double ydec_1_2 = 0, ydec_2_2 = 0, ydec_3_2 = 0, ydec_4_2 = 0;
                        double xdec_1_3 = 0, xdec_2_3 = 0, xdec_3_3 = 0, xdec_4_3 = 0;
                        double ydec_1_3 = 0, ydec_2_3 = 0, ydec_3_3 = 0, ydec_4_3 = 0;
                        double xdec_1_4 = 0, xdec_2_4 = 0, xdec_3_4 = 0, xdec_4_4 = 0;
                        double ydec_1_4 = 0, ydec_2_4 = 0, ydec_3_4 = 0, ydec_4_4 = 0;
                        float xpic1, xpic2, xpic3, xpic4;
                        float ypic1, ypic2, ypic3, ypic4;
                        float xpic_1_1, xpic_2_1, xpic_3_1, xpic_4_1;
                        float ypic_1_1, ypic_2_1, ypic_3_1, ypic_4_1;
                        float xpic_1_2, xpic_2_2, xpic_3_2, xpic_4_2;
                        float ypic_1_2, ypic_2_2, ypic_3_2, ypic_4_2;
                        float xpic_1_3, xpic_2_3, xpic_3_3, xpic_4_3;
                        float ypic_1_3, ypic_2_3, ypic_3_3, ypic_4_3;
                        float xpic_1_4, xpic_2_4, xpic_3_4, xpic_4_4;
                        float ypic_1_4, ypic_2_4, ypic_3_4, ypic_4_4;

                        xcenter1 = V[i].xcenter - V[i].SizeCell / 4;
                        ycenter1 = V[i].ycenter - V[i].SizeCell / 4;
                        FunctionFx(xcenter1 / zoomx, (rectSize * 10 - ycenter1) / zoomy, ref xdec1);
                        FunctionFy(xcenter1 / zoomx, (rectSize * 10 - ycenter1) / zoomy, ref ydec1);
                        xpic1 = (float)xdec1 * zoomx;
                        ypic1 = rectSize * 10 - (float)ydec1 * zoomy;

                        FunctionFx((xcenter1 - V[i].SizeCell / u) / zoomx, (rectSize * 10 - (ycenter1 - V[i].SizeCell / u)) / zoomy, ref xdec_1_1);
                        FunctionFy((xcenter1 - V[i].SizeCell / u) / zoomx, (rectSize * 10 - (ycenter1 - V[i].SizeCell / u)) / zoomy, ref ydec_1_1);
                        xpic_1_1 = (float)xdec_1_1 * zoomx;
                        ypic_1_1 = rectSize * 10 - (float)ydec_1_1 * zoomy;
                        FunctionFx((xcenter1 + V[i].SizeCell / u) / zoomx, (rectSize * 10 - (ycenter1 - V[i].SizeCell / u)) / zoomy, ref xdec_1_2);
                        FunctionFy((xcenter1 + V[i].SizeCell / u) / zoomx, (rectSize * 10 - (ycenter1 - V[i].SizeCell / u)) / zoomy, ref ydec_1_2);
                        xpic_1_2 =  (float)xdec_1_2 * zoomx;
                        ypic_1_2 = rectSize * 10  - (float)ydec_1_2 * zoomy;
                        FunctionFx((xcenter1 - V[i].SizeCell / u) / zoomx, (rectSize * 10 - (ycenter1 + V[i].SizeCell / u)) / zoomy, ref xdec_1_3);
                        FunctionFy((xcenter1 - V[i].SizeCell / u) / zoomx, (rectSize * 10 - (ycenter1 + V[i].SizeCell / u)) / zoomy, ref ydec_1_3);
                        xpic_1_3 = (float)xdec_1_3 * zoomx;
                        ypic_1_3 = rectSize * 10  - (float)ydec_1_3 * zoomy;
                        FunctionFx((xcenter1 + V[i].SizeCell / u) / zoomx, (rectSize * 10 - (ycenter1 + V[i].SizeCell / u)) / zoomy, ref xdec_1_4);
                        FunctionFy((xcenter1 + V[i].SizeCell / u) / zoomx, (rectSize * 10 - (ycenter1 + V[i].SizeCell / u)) / zoomy, ref ydec_1_4);
                        xpic_1_4 = (float)xdec_1_4 * zoomx;
                        ypic_1_4 = rectSize * 10 - (float)ydec_1_4 * zoomy;

                        V.Add(new Vertex(xdec1, ydec1, xpic1, ypic1, xcenter1, ycenter1, V[i].SizeCell / 2, xdec_1_1, ydec_1_1, xpic_1_1, ypic_1_1, xdec_1_2, ydec_1_2, xpic_1_2, ypic_1_2, xdec_1_3, ydec_1_3, xpic_1_3, ypic_1_3, xdec_1_4, ydec_1_4, xpic_1_4, ypic_1_4));

                        xcenter2 = V[i].xcenter + V[i].SizeCell / 4;
                        ycenter2 = V[i].ycenter - V[i].SizeCell / 4;
                        FunctionFx(xcenter2 / zoomx, (rectSize * 10 - ycenter2) / zoomy, ref xdec2);
                        FunctionFy(xcenter2 / zoomx, (rectSize * 10 - ycenter2) / zoomy, ref ydec2);
                        xpic2 = (float)xdec2 * zoomx;
                        ypic2 = rectSize * 10  - (float)ydec2 * zoomy;

                        FunctionFx((xcenter2 - V[i].SizeCell / u) / zoomx, (rectSize * 10 - (ycenter2 - V[i].SizeCell / u)) / zoomy, ref xdec_2_1);
                        FunctionFy((xcenter2 - V[i].SizeCell / u) / zoomx, (rectSize * 10 - (ycenter2 - V[i].SizeCell / u)) / zoomy, ref ydec_2_1);
                        xpic_2_1 = (float)xdec_2_1 * zoomx;
                        ypic_2_1 = rectSize * 10 - (float)ydec_2_1 * zoomy;
                        FunctionFx((xcenter2 + V[i].SizeCell / u) / zoomx, (rectSize * 10 - (ycenter2 - V[i].SizeCell / u)) / zoomy, ref xdec_2_2);
                        FunctionFy((xcenter2 + V[i].SizeCell / u) / zoomx, (rectSize * 10 - (ycenter2 - V[i].SizeCell / u)) / zoomy, ref ydec_2_2);
                        xpic_2_2 = (float)xdec_2_2 * zoomx;
                        ypic_2_2 = rectSize * 10 - (float)ydec_2_2 * zoomy;
                        FunctionFx((xcenter2 - V[i].SizeCell / u) / zoomx, (rectSize * 10 - (ycenter2 + V[i].SizeCell / u)) / zoomy, ref xdec_2_3);
                        FunctionFy((xcenter2 - V[i].SizeCell / u) / zoomx, (rectSize * 10 - (ycenter2 + V[i].SizeCell / u)) / zoomy, ref ydec_2_3);
                        xpic_2_3 = (float)xdec_2_3 * zoomx;
                        ypic_2_3 = rectSize * 10 - (float)ydec_2_3 * zoomy;
                        FunctionFx((xcenter2 + V[i].SizeCell / u) / zoomx, (rectSize * 10 - (ycenter2 + V[i].SizeCell / u)) / zoomy, ref xdec_2_4);
                        FunctionFy((xcenter2 + V[i].SizeCell / u) / zoomx, (rectSize * 10 - (ycenter2 + V[i].SizeCell / u)) / zoomy, ref ydec_2_4);
                        xpic_2_4 = (float)xdec_2_4 * zoomx;
                        ypic_2_4 = rectSize * 10 - (float)ydec_2_4 * zoomy;
                        V.Add(new Vertex(xdec2, ydec2, xpic2, ypic2, xcenter2, ycenter2, V[i].SizeCell / 2, xdec_2_1, ydec_2_1, xpic_2_1, ypic_2_1, xdec_2_2, ydec_2_2, xpic_2_2, ypic_2_2, xdec_2_3, ydec_2_3, xpic_2_3, ypic_2_3, xdec_2_4, ydec_2_4, xpic_2_4, ypic_2_4));

                        xcenter3 = V[i].xcenter - V[i].SizeCell / 4;
                        ycenter3 = V[i].ycenter + V[i].SizeCell / 4;
                        FunctionFx(xcenter3 / zoomx, (rectSize * 10 - ycenter3) / zoomy, ref xdec3);
                        FunctionFy(xcenter3 / zoomx, (rectSize * 10 - ycenter3) / zoomy, ref ydec3);
                        xpic3 = (float)xdec3 * zoomx;
                        ypic3 = rectSize * 10 - (float)ydec3 * zoomy;

                        FunctionFx((xcenter3 - V[i].SizeCell / u) / zoomx, (rectSize * 10 - (ycenter3 - V[i].SizeCell / u)) / zoomy, ref xdec_3_1);
                        FunctionFy((xcenter3 - V[i].SizeCell / u) / zoomx, (rectSize * 10 - (ycenter3 - V[i].SizeCell / u)) / zoomy, ref ydec_3_1);
                        xpic_3_1 = (float)xdec_3_1 * zoomx;
                        ypic_3_1 = rectSize * 10 - (float)ydec_3_1 * zoomy;
                        FunctionFx((xcenter3 + V[i].SizeCell / u) / zoomx, (rectSize * 10 - (ycenter3 - V[i].SizeCell / u)) / zoomy, ref xdec_3_2);
                        FunctionFy((xcenter3 + V[i].SizeCell / u) / zoomx, (rectSize * 10 - (ycenter3 - V[i].SizeCell / u)) / zoomy, ref ydec_3_2);
                        xpic_3_2 = (float)xdec_3_2 * zoomx;
                        ypic_3_2 = rectSize * 10 - (float)ydec_3_2 * zoomy;
                        FunctionFx((xcenter3 - V[i].SizeCell / u) / zoomx, (rectSize * 10 - (ycenter3 + V[i].SizeCell / u)) / zoomy, ref xdec_3_3);
                        FunctionFy((xcenter3 - V[i].SizeCell / u) / zoomx, (rectSize * 10 - (ycenter3 + V[i].SizeCell / u)) / zoomy, ref ydec_3_3);
                        xpic_3_3 = (float)xdec_3_3 * zoomx;
                        ypic_3_3 = rectSize * 10 - (float)ydec_3_3 * zoomy;
                        FunctionFx((xcenter3 + V[i].SizeCell / u) / zoomx, (rectSize * 10 - (ycenter3 + V[i].SizeCell / u)) / zoomy, ref xdec_3_4);
                        FunctionFy((xcenter3 + V[i].SizeCell / u) / zoomx, (rectSize * 10 - (ycenter3 + V[i].SizeCell / u)) / zoomy, ref ydec_3_4);
                        xpic_3_4 = (float)xdec_3_4 * zoomx;
                        ypic_3_4 = rectSize * 10 - (float)ydec_3_4 * zoomy;
                        V.Add(new Vertex(xdec3, ydec3, xpic3, ypic3, xcenter3, ycenter3, V[i].SizeCell / 2, xdec_3_1, ydec_3_1, xpic_3_1, ypic_3_1, xdec_3_2, ydec_3_2, xpic_3_2, ypic_3_2, xdec_3_3, ydec_3_3, xpic_3_3, ypic_3_3, xdec_3_4, ydec_3_4, xpic_3_4, ypic_3_4));

                        xcenter4 = V[i].xcenter + V[i].SizeCell / 4;
                        ycenter4 = V[i].ycenter + V[i].SizeCell / 4;
                        FunctionFx(xcenter4 / zoomx, (rectSize * 10 - ycenter4) / zoomy, ref xdec4);
                        FunctionFy(xcenter4 / zoomx, (rectSize * 10 - ycenter4) / zoomy, ref ydec4);
                        xpic4 = (float)xdec4 * zoomx;
                        ypic4 = rectSize * 10 - (float)ydec4 * zoomy;

                        FunctionFx((xcenter4 - V[i].SizeCell / u) / zoomx, (rectSize * 10 - (ycenter4 - V[i].SizeCell / u)) / zoomy, ref xdec_4_1);
                        FunctionFy((xcenter4 - V[i].SizeCell / u) / zoomx, (rectSize * 10 - (ycenter4 - V[i].SizeCell / u)) / zoomy, ref ydec_4_1);
                        xpic_4_1 = (float)xdec_4_1 * zoomx;
                        ypic_4_1 = rectSize * 10 - (float)ydec_4_1 * zoomy;
                        FunctionFx((xcenter4 + V[i].SizeCell / u) / zoomx, (rectSize * 10 - (ycenter4 - V[i].SizeCell / u)) / zoomy, ref xdec_4_2);
                        FunctionFy((xcenter4 + V[i].SizeCell / u) / zoomx, (rectSize * 10 - (ycenter4 - V[i].SizeCell / u)) / zoomy, ref ydec_4_2);
                        xpic_4_2 = (float)xdec_4_2 * zoomx;
                        ypic_4_2 = rectSize * 10 - (float)ydec_4_2 * zoomy;
                        FunctionFx((xcenter4 - V[i].SizeCell / u) / zoomx, (rectSize * 10 - (ycenter4 + V[i].SizeCell / u)) / zoomy, ref xdec_4_3);
                        FunctionFy((xcenter4 - V[i].SizeCell / u) / zoomx, (rectSize * 10 - (ycenter4 + V[i].SizeCell / u)) / zoomy, ref ydec_4_3);
                        xpic_4_3 = (float)xdec_4_3 * zoomx;
                        ypic_4_3 = rectSize * 10 - (float)ydec_4_3 * zoomy;
                        FunctionFx((xcenter4 + V[i].SizeCell / u) / zoomx, (rectSize * 10 - (ycenter4 + V[i].SizeCell / u)) / zoomy, ref xdec_4_4);
                        FunctionFy((xcenter4 + V[i].SizeCell / u) / zoomx, (rectSize * 10 - (ycenter4 + V[i].SizeCell / u)) / zoomy, ref ydec_4_4);
                        xpic_4_4 = (float)xdec_4_4 * zoomx;
                        ypic_4_4 = rectSize * 10 - (float)ydec_4_4 * zoomy;
                        V.Add(new Vertex(xdec4, ydec4, xpic4, ypic4, xcenter4, ycenter4, V[i].SizeCell / 2, xdec_4_1, ydec_4_1, xpic_4_1, ypic_4_1, xdec_4_2, ydec_4_2, xpic_4_2, ypic_4_2, xdec_4_3, ydec_4_3, xpic_4_3, ypic_4_3, xdec_4_4, ydec_4_4, xpic_4_4, ypic_4_4));
                    }
                }
            }
            if (Cg == 0)
            {
                for (int i = 0; i < V.Count; i++)
                {
                    if (V[i].state)
                    {
                        float xcenter1, xcenter2, xcenter3, xcenter4;
                        float ycenter1, ycenter2, ycenter3, ycenter4;
                        double xdec1 = 0, xdec2 = 0, xdec3 = 0, xdec4 = 0;
                        double ydec1 = 0, ydec2 = 0, ydec3 = 0, ydec4 = 0;
                        double xdec_1_1 = 0, xdec_2_1 = 0, xdec_3_1 = 0, xdec_4_1 = 0;
                        double ydec_1_1 = 0, ydec_2_1 = 0, ydec_3_1 = 0, ydec_4_1 = 0;
                        double xdec_1_2 = 0, xdec_2_2 = 0, xdec_3_2 = 0, xdec_4_2 = 0;
                        double ydec_1_2 = 0, ydec_2_2 = 0, ydec_3_2 = 0, ydec_4_2 = 0;
                        double xdec_1_3 = 0, xdec_2_3 = 0, xdec_3_3 = 0, xdec_4_3 = 0;
                        double ydec_1_3 = 0, ydec_2_3 = 0, ydec_3_3 = 0, ydec_4_3 = 0;
                        double xdec_1_4 = 0, xdec_2_4 = 0, xdec_3_4 = 0, xdec_4_4 = 0;
                        double ydec_1_4 = 0, ydec_2_4 = 0, ydec_3_4 = 0, ydec_4_4 = 0;
                        float xpic1, xpic2, xpic3, xpic4;
                        float ypic1, ypic2, ypic3, ypic4;
                        float xpic_1_1, xpic_2_1, xpic_3_1, xpic_4_1;
                        float ypic_1_1, ypic_2_1, ypic_3_1, ypic_4_1;
                        float xpic_1_2, xpic_2_2, xpic_3_2, xpic_4_2;
                        float ypic_1_2, ypic_2_2, ypic_3_2, ypic_4_2;
                        float xpic_1_3, xpic_2_3, xpic_3_3, xpic_4_3;
                        float ypic_1_3, ypic_2_3, ypic_3_3, ypic_4_3;
                        float xpic_1_4, xpic_2_4, xpic_3_4, xpic_4_4;
                        float ypic_1_4, ypic_2_4, ypic_3_4, ypic_4_4;

                        xcenter1 = V[i].xcenter - V[i].SizeCell / 4;
                        ycenter1 = V[i].ycenter - V[i].SizeCell / 4;
                        FunctionFx((xcenter1 - rectSize * 5) / zoomx, (rectSize * 5 - ycenter1) / zoomy, ref xdec1);
                        FunctionFy((xcenter1 - rectSize * 5) / zoomx, (rectSize * 5 - ycenter1) / zoomy, ref ydec1);
                        xpic1 = rectSize * 10 / 2 + (float)xdec1 * zoomx;
                        ypic1 = rectSize * 10 / 2 - (float)ydec1 * zoomy;

                        FunctionFx((xcenter1 - V[i].SizeCell / u - rectSize * 5) / zoomx, (rectSize * 5 - (ycenter1 - V[i].SizeCell / u)) / zoomy, ref xdec_1_1);
                        FunctionFy((xcenter1 - V[i].SizeCell / u - rectSize * 5) / zoomx, (rectSize * 5 - (ycenter1 - V[i].SizeCell / u)) / zoomy, ref ydec_1_1);
                        xpic_1_1 = rectSize * 10 / 2 + (float)xdec_1_1 * zoomx;
                        ypic_1_1 = rectSize * 10 / 2 - (float)ydec_1_1 * zoomy;
                        FunctionFx((xcenter1 + V[i].SizeCell / u - rectSize * 5) / zoomx, (rectSize * 5 - (ycenter1 - V[i].SizeCell / u)) / zoomy, ref xdec_1_2);
                        FunctionFy((xcenter1 + V[i].SizeCell / u - rectSize * 5) / zoomx, (rectSize * 5 - (ycenter1 - V[i].SizeCell / u)) / zoomy, ref ydec_1_2);
                        xpic_1_2 = rectSize * 10 / 2 + (float)xdec_1_2 * zoomx;
                        ypic_1_2 = rectSize * 10 / 2 - (float)ydec_1_2 * zoomy;
                        FunctionFx((xcenter1 - V[i].SizeCell / u - rectSize * 5) / zoomx, (rectSize * 5 - (ycenter1 + V[i].SizeCell / u)) / zoomy, ref xdec_1_3);
                        FunctionFy((xcenter1 - V[i].SizeCell / u - rectSize * 5) / zoomx, (rectSize * 5 - (ycenter1 + V[i].SizeCell / u)) / zoomy, ref ydec_1_3);
                        xpic_1_3 = rectSize * 10 / 2 + (float)xdec_1_3 * zoomx;
                        ypic_1_3 = rectSize * 10 / 2 - (float)ydec_1_3 * zoomy;
                        FunctionFx((xcenter1 + V[i].SizeCell / u - rectSize * 5) / zoomx, (rectSize * 5 - (ycenter1 + V[i].SizeCell / u)) / zoomy, ref xdec_1_4);
                        FunctionFy((xcenter1 + V[i].SizeCell / u - rectSize * 5) / zoomx, (rectSize * 5 - (ycenter1 + V[i].SizeCell / u)) / zoomy, ref ydec_1_4);
                        xpic_1_4 = rectSize * 10 / 2 + (float)xdec_1_4 * zoomx;
                        ypic_1_4 = rectSize * 10 / 2 - (float)ydec_1_4 * zoomy;

                        V.Add(new Vertex(xdec1, ydec1, xpic1, ypic1, xcenter1, ycenter1, V[i].SizeCell / 2, xdec_1_1, ydec_1_1, xpic_1_1, ypic_1_1, xdec_1_2, ydec_1_2, xpic_1_2, ypic_1_2, xdec_1_3, ydec_1_3, xpic_1_3, ypic_1_3, xdec_1_4, ydec_1_4, xpic_1_4, ypic_1_4));

                        xcenter2 = V[i].xcenter + V[i].SizeCell / 4;
                        ycenter2 = V[i].ycenter - V[i].SizeCell / 4;
                        FunctionFx((xcenter2 - rectSize * 5) / zoomx, (rectSize * 5 - ycenter2) / zoomy, ref xdec2);
                        FunctionFy((xcenter2 - rectSize * 5) / zoomx, (rectSize * 5 - ycenter2) / zoomy, ref ydec2);
                        xpic2 = rectSize * 10 / 2 + (float)xdec2 * zoomx;
                        ypic2 = rectSize * 10 / 2 - (float)ydec2 * zoomy;

                        FunctionFx((xcenter2 - V[i].SizeCell / u - rectSize * 5) / zoomx, (rectSize * 5 - (ycenter2 - V[i].SizeCell / u)) / zoomy, ref xdec_2_1);
                        FunctionFy((xcenter2 - V[i].SizeCell / u - rectSize * 5) / zoomx, (rectSize * 5 - (ycenter2 - V[i].SizeCell / u)) / zoomy, ref ydec_2_1);
                        xpic_2_1 = rectSize * 10 / 2 + (float)xdec_2_1 * zoomx;
                        ypic_2_1 = rectSize * 10 / 2 - (float)ydec_2_1 * zoomy;
                        FunctionFx((xcenter2 + V[i].SizeCell / u - rectSize * 5) / zoomx, (rectSize * 5 - (ycenter2 - V[i].SizeCell / u)) / zoomy, ref xdec_2_2);
                        FunctionFy((xcenter2 + V[i].SizeCell / u - rectSize * 5) / zoomx, (rectSize * 5 - (ycenter2 - V[i].SizeCell / u)) / zoomy, ref ydec_2_2);
                        xpic_2_2 = rectSize * 10 / 2 + (float)xdec_2_2 * zoomx;
                        ypic_2_2 = rectSize * 10 / 2 - (float)ydec_2_2 * zoomy;
                        FunctionFx((xcenter2 - V[i].SizeCell / u - rectSize * 5) / zoomx, (rectSize * 5 - (ycenter2 + V[i].SizeCell / u)) / zoomy, ref xdec_2_3);
                        FunctionFy((xcenter2 - V[i].SizeCell / u - rectSize * 5) / zoomx, (rectSize * 5 - (ycenter2 + V[i].SizeCell / u)) / zoomy, ref ydec_2_3);
                        xpic_2_3 = rectSize * 10 / 2 + (float)xdec_2_3 * zoomx;
                        ypic_2_3 = rectSize * 10 / 2 - (float)ydec_2_3 * zoomy;
                        FunctionFx((xcenter2 + V[i].SizeCell / u - rectSize * 5) / zoomx, (rectSize * 5 - (ycenter2 + V[i].SizeCell / u)) / zoomy, ref xdec_2_4);
                        FunctionFy((xcenter2 + V[i].SizeCell / u - rectSize * 5) / zoomx, (rectSize * 5 - (ycenter2 + V[i].SizeCell / u)) / zoomy, ref ydec_2_4);
                        xpic_2_4 = rectSize * 10 / 2 + (float)xdec_2_4 * zoomx;
                        ypic_2_4 = rectSize * 10 / 2 - (float)ydec_2_4 * zoomy;
                        V.Add(new Vertex(xdec2, ydec2, xpic2, ypic2, xcenter2, ycenter2, V[i].SizeCell / 2, xdec_2_1, ydec_2_1, xpic_2_1, ypic_2_1, xdec_2_2, ydec_2_2, xpic_2_2, ypic_2_2, xdec_2_3, ydec_2_3, xpic_2_3, ypic_2_3, xdec_2_4, ydec_2_4, xpic_2_4, ypic_2_4));

                        xcenter3 = V[i].xcenter - V[i].SizeCell / 4;
                        ycenter3 = V[i].ycenter + V[i].SizeCell / 4;
                        FunctionFx((xcenter3 - rectSize * 5) / zoomx, (rectSize * 5 - ycenter3) / zoomy, ref xdec3);
                        FunctionFy((xcenter3 - rectSize * 5) / zoomx, (rectSize * 5 - ycenter3) / zoomy, ref ydec3);
                        xpic3 = rectSize * 10 / 2 + (float)xdec3 * zoomx;
                        ypic3 = rectSize * 10 / 2 - (float)ydec3 * zoomy;

                        FunctionFx((xcenter3 - V[i].SizeCell / u - rectSize * 5) / zoomx, (rectSize * 5 - (ycenter3 - V[i].SizeCell / u)) / zoomy, ref xdec_3_1);
                        FunctionFy((xcenter3 - V[i].SizeCell / u - rectSize * 5) / zoomx, (rectSize * 5 - (ycenter3 - V[i].SizeCell / u)) / zoomy, ref ydec_3_1);
                        xpic_3_1 = rectSize * 10 / 2 + (float)xdec_3_1 * zoomx;
                        ypic_3_1 = rectSize * 10 / 2 - (float)ydec_3_1 * zoomy;
                        FunctionFx((xcenter3 + V[i].SizeCell / u - rectSize * 5) / zoomx, (rectSize * 5 - (ycenter3 - V[i].SizeCell / u)) / zoomy, ref xdec_3_2);
                        FunctionFy((xcenter3 + V[i].SizeCell / u - rectSize * 5) / zoomx, (rectSize * 5 - (ycenter3 - V[i].SizeCell / u)) / zoomy, ref ydec_3_2);
                        xpic_3_2 = rectSize * 10 / 2 + (float)xdec_3_2 * zoomx;
                        ypic_3_2 = rectSize * 10 / 2 - (float)ydec_3_2 * zoomy;
                        FunctionFx((xcenter3 - V[i].SizeCell / u - rectSize * 5) / zoomx, (rectSize * 5 - (ycenter3 + V[i].SizeCell / u)) / zoomy, ref xdec_3_3);
                        FunctionFy((xcenter3 - V[i].SizeCell / u - rectSize * 5) / zoomx, (rectSize * 5 - (ycenter3 + V[i].SizeCell / u)) / zoomy, ref ydec_3_3);
                        xpic_3_3 = rectSize * 10 / 2 + (float)xdec_3_3 * zoomx;
                        ypic_3_3 = rectSize * 10 / 2 - (float)ydec_3_3 * zoomy;
                        FunctionFx((xcenter3 + V[i].SizeCell / u - rectSize * 5) / zoomx, (rectSize * 5 - (ycenter3 + V[i].SizeCell / u)) / zoomy, ref xdec_3_4);
                        FunctionFy((xcenter3 + V[i].SizeCell / u - rectSize * 5) / zoomx, (rectSize * 5 - (ycenter3 + V[i].SizeCell / u)) / zoomy, ref ydec_3_4);
                        xpic_3_4 = rectSize * 10 / 2 + (float)xdec_3_4 * zoomx;
                        ypic_3_4 = rectSize * 10 / 2 - (float)ydec_3_4 * zoomy;
                        V.Add(new Vertex(xdec3, ydec3, xpic3, ypic3, xcenter3, ycenter3, V[i].SizeCell / 2, xdec_3_1, ydec_3_1, xpic_3_1, ypic_3_1, xdec_3_2, ydec_3_2, xpic_3_2, ypic_3_2, xdec_3_3, ydec_3_3, xpic_3_3, ypic_3_3, xdec_3_4, ydec_3_4, xpic_3_4, ypic_3_4));

                        xcenter4 = V[i].xcenter + V[i].SizeCell / 4;
                        ycenter4 = V[i].ycenter + V[i].SizeCell / 4;
                        FunctionFx((xcenter4 - rectSize * 5) / zoomx, (rectSize * 5 - ycenter4) / zoomy, ref xdec4);
                        FunctionFy((xcenter4 - rectSize * 5) / zoomx, (rectSize * 5 - ycenter4) / zoomy, ref ydec4);
                        xpic4 = rectSize * 10 / 2 + (float)xdec4 * zoomx;
                        ypic4 = rectSize * 10 / 2 - (float)ydec4 * zoomy;

                        FunctionFx((xcenter4 - V[i].SizeCell / u - rectSize * 5) / zoomx, (rectSize * 5 - (ycenter4 - V[i].SizeCell / u)) / zoomy, ref xdec_4_1);
                        FunctionFy((xcenter4 - V[i].SizeCell / u - rectSize * 5) / zoomx, (rectSize * 5 - (ycenter4 - V[i].SizeCell / u)) / zoomy, ref ydec_4_1);
                        xpic_4_1 = rectSize * 10 / 2 + (float)xdec_4_1 * zoomx;
                        ypic_4_1 = rectSize * 10 / 2 - (float)ydec_4_1 * zoomy;
                        FunctionFx((xcenter4 + V[i].SizeCell / u - rectSize * 5) / zoomx, (rectSize * 5 - (ycenter4 - V[i].SizeCell / u)) / zoomy, ref xdec_4_2);
                        FunctionFy((xcenter4 + V[i].SizeCell / u - rectSize * 5) / zoomx, (rectSize * 5 - (ycenter4 - V[i].SizeCell / u)) / zoomy, ref ydec_4_2);
                        xpic_4_2 = rectSize * 10 / 2 + (float)xdec_4_2 * zoomx;
                        ypic_4_2 = rectSize * 10 / 2 - (float)ydec_4_2 * zoomy;
                        FunctionFx((xcenter4 - V[i].SizeCell / u - rectSize * 5) / zoomx, (rectSize * 5 - (ycenter4 + V[i].SizeCell / u)) / zoomy, ref xdec_4_3);
                        FunctionFy((xcenter4 - V[i].SizeCell / u - rectSize * 5) / zoomx, (rectSize * 5 - (ycenter4 + V[i].SizeCell / u)) / zoomy, ref ydec_4_3);
                        xpic_4_3 = rectSize * 10 / 2 + (float)xdec_4_3 * zoomx;
                        ypic_4_3 = rectSize * 10 / 2 - (float)ydec_4_3 * zoomy;
                        FunctionFx((xcenter4 + V[i].SizeCell / u - rectSize * 5) / zoomx, (rectSize * 5 - (ycenter4 + V[i].SizeCell / u)) / zoomy, ref xdec_4_4);
                        FunctionFy((xcenter4 + V[i].SizeCell / u - rectSize * 5) / zoomx, (rectSize * 5 - (ycenter4 + V[i].SizeCell / u)) / zoomy, ref ydec_4_4);
                        xpic_4_4 = rectSize * 10 / 2 + (float)xdec_4_4 * zoomx;
                        ypic_4_4 = rectSize * 10 / 2 - (float)ydec_4_4 * zoomy;
                        V.Add(new Vertex(xdec4, ydec4, xpic4, ypic4, xcenter4, ycenter4, V[i].SizeCell / 2, xdec_4_1, ydec_4_1, xpic_4_1, ypic_4_1, xdec_4_2, ydec_4_2, xpic_4_2, ypic_4_2, xdec_4_3, ydec_4_3, xpic_4_3, ypic_4_3, xdec_4_4, ydec_4_4, xpic_4_4, ypic_4_4));
                    }
                }
            }
            
            int q = 0;
            while (q< V.Count)
            {
                if (V[q].state)
                {
                    V.RemoveAt(q);
                    q = q - 1;
                    //File.AppendAllText("C:\\Users\\SuperUser\\Google Диск\\НИР-Мат мех\\прога по нир\\drawgraphProgram\\orign.txt", "+");
                }
                q++;
            }
            for (int i = 0; i < V.Count; i++)
            {
                for (int j = 0; j < V.Count; j++)
                {
                    if (V[j].state)
                    {
                        V.RemoveAt(j);
                        //File.AppendAllText("C:\\Users\\SuperUser\\Google Диск\\НИР-Мат мех\\прога по нир\\drawgraphProgram\\orign.txt", q.ToString());

                    }
                }
            }

            for (int i = 0; i < V.Count; i++)
            {
                for (int j = 0; j < V.Count; j++)
                {
                    if (V[i].xpic < (V[j].xcenter + V[j].SizeCell / 2) && V[i].xpic > (V[j].xcenter - V[j].SizeCell / 2) &&
                        V[i].ypic < (V[j].ycenter + V[j].SizeCell / 2) && V[i].ypic > (V[j].ycenter - V[j].SizeCell / 2))
                    {
                        E.Add(new Edge(i, j));
                        V[j].state = true;           
                    }
                    if (V[i].xpic1 < (V[j].xcenter + V[j].SizeCell / 2) && V[i].xpic1 > (V[j].xcenter - V[j].SizeCell / 2) &&
                        V[i].ypic1 < (V[j].ycenter + V[j].SizeCell / 2) && V[i].ypic1 > (V[j].ycenter - V[j].SizeCell / 2))
                    {
                        E.Add(new Edge(i, j));
                        V[j].state = true;
                    }
                    if (V[i].xpic2 < (V[j].xcenter + V[j].SizeCell / 2) && V[i].xpic2 > (V[j].xcenter - V[j].SizeCell / 2) && 
                        V[i].ypic2 < (V[j].ycenter + V[j].SizeCell / 2) && V[i].ypic2 > (V[j].ycenter - V[j].SizeCell / 2))
                    {
                        E.Add(new Edge(i, j));
                        V[j].state = true;
                    }
                    if (V[i].xpic4 < (V[j].xcenter + V[j].SizeCell / 2) && V[i].xpic4 > (V[j].xcenter - V[j].SizeCell / 2) &&
                        V[i].ypic4 < (V[j].ycenter + V[j].SizeCell / 2) && V[i].ypic4 > (V[j].ycenter - V[j].SizeCell / 2))
                    {
                        E.Add(new Edge(i, j));
                        V[j].state = true;
                    }
                    if (V[i].xpic3 < (V[j].xcenter + V[j].SizeCell / 2) && V[i].xpic3 > (V[j].xcenter - V[j].SizeCell / 2) &&
                        V[i].ypic3 < (V[j].ycenter + V[j].SizeCell / 2) && V[i].ypic3 > (V[j].ycenter - V[j].SizeCell / 2))
                    {
                        E.Add(new Edge(i, j));
                        V[j].state = true;
                    }
                }
            }
            //File.WriteAllText("C:\\Users\\SuperUser\\Google Диск\\НИР-Мат мех\\прога по нир\\drawgraphProgram\\matrx.txt", " ");
            //File.WriteAllText("C:\\Users\\SuperUser\\Google Диск\\НИР-Мат мех\\прога по нир\\drawgraphProgram\\matrx1.txt", " ");
        }

        //Далее не реализованые делати в символическом методе. 
        //Для добавления к символическому методу поиска в глубину.

        //создание матрицы смежности и вывод в листбокс
        /*private void createAdjAndOut()
        {
            //AMatrix = new int[V.Count, V.Count];
            //G.fillAdjacencyMatrix(V.Count, E, AMatrix);
            //AMatrix = new int[k, k];
            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < k; j++)
                {
                    AMatrix[i, j] = 0;
                }
            }
            double new_left = left;
            double new_right = right1 / k;
            count = new double[k + 1];
            F = new double[3];
            double a, b, c;
            for (int i = 0; i < (k + 1); i++)
            {
                count[i] = i * (right1 / k);
            }
            double q = count[0];
            double rq = count[1];
            double wq = count[2];
            //double tq = count[3];
            //double sq = count[4];
            //double еq = count[5];
            for (int i = 1; i < (k + 1); i++)
            {
                a = new_left + (new_right - new_left) / 4;
                b = new_left + 2 * (new_right - new_left) / 4;
                c = new_left + 3 * (new_right - new_left) / 4;

                for (int j = 1; j < (k + 1); j++)
                {
                    if (F[0] >= count[j - 1] && F[0] < count[j])
                    {
                        AMatrix[i - 1, j - 1] = 1;
                    }
                    if (F[1] >= count[j - 1] && F[1] < count[j])
                    {
                        AMatrix[i - 1, j - 1] = 1;
                    }
                    if (F[2] >= count[j - 1] && F[2] < count[j])
                    {
                        AMatrix[i - 1, j - 1] = 1;
                    }
                }
                new_left = new_right; new_right = (i + 1) * (right1 / k);
            }

            listBoxMatrix.Items.Clear();
            string sOut = "    ";
            for (int i = 0; i < V.Count; i++)
                sOut += (i + 1) + " ";
            listBoxMatrix.Items.Add(sOut);
            for (int i = 0; i < V.Count; i++)
            {
                sOut = (i + 1) + " | ";
                for (int j = 0; j < V.Count; j++)
                    sOut += AMatrix[i, j] + " ";
                listBoxMatrix.Items.Add(sOut);
            }
        }
        */

        //создание матрицы инцидентности и вывод в листбокс
        /* private void createIncAndOut()
         {
             if (E.Count > 0)
             {
                 IMatrix = new int[V.Count, E.Count];
                 G.fillIncidenceMatrix(V.Count, E, IMatrix);
                 listBoxMatrix.Items.Clear();
                 string sOut = "    ";
                 for (int i = 0; i < E.Count; i++)
                     sOut += (char)('a' + i) + " ";
                 listBoxMatrix.Items.Add(sOut);
                 for (int i = 0; i < V.Count; i++)
                 {
                     sOut = (i + 1) + " | ";
                     for (int j = 0; j < E.Count; j++)
                         sOut += IMatrix[i, j] + " ";
                     listBoxMatrix.Items.Add(sOut);
                 }
             }
             else
                 listBoxMatrix.Items.Clear();
         }

         //поиск элементарных цепей
         private void chainButton_Click(object sender, EventArgs e)
         {
             listBoxMatrix.Items.Clear();
             //1-white 2-black
             int[] color = new int[V.Count];
             for (int i = 0; i < V.Count - 1; i++)
                 for (int j = i + 1; j < V.Count; j++)
                 {
                     for (int k = 0; k < V.Count; k++)
                         color[k] = 1;
                     DFSchain(i, j, E, color, (i + 1).ToString());
                 }
         }

         //обход в глубину. поиск элементарных цепей. (1-white 2-black)
         private void DFSchain(int u, int endV, List<Edge> E, int[] color, string s)
         {
             //вершину не следует перекрашивать, если u == endV (возможно в нее есть несколько путей)
             if (u != endV)  
                 color[u] = 2;
             else
             {
                 listBoxMatrix.Items.Add(s);
                 return;
             }
             for (int w = 0; w < E.Count; w++)
             {
                 if (color[E[w].v2] == 1 && E[w].v1 == u)
                 {
                     DFSchain(E[w].v2, endV, E, color, s + "-" + (E[w].v2 + 1).ToString());
                     color[E[w].v2] = 1;
                 }
                 else if (color[E[w].v1] == 1 && E[w].v2 == u)
                 {
                     DFSchain(E[w].v1, endV, E, color, s + "-" + (E[w].v1 + 1).ToString());
                     color[E[w].v1] = 1;
                 }
             }
         }

         //поиск элементарных циклов

         private void cycleButton_Click(object sender, EventArgs e)
         {

             drawgraphButton.Enabled = false;
             selectButton.Enabled = true;
             //drawEdgeButton.Enabled = true;
             //deleteButton.Enabled = true;
             G.clearSheet();
             G.drawALLGraph(V, E);
             sheet.Image = G.GetBitmap();


             listBoxMatrix.Items.Clear();
             //1-white 2-black
             int[] color = new int[V.Count];
             for (int i = 0; i < V.Count; i++)
             {
                 for (int k = 0; k < V.Count; k++)
                     color[k] = 1;
                 List<int> cycle = new List<int>();
                 cycle.Add(i + 1);
                 DFScycle(i, i, E, color, -1, cycle);
             }
        }*/

        //обход в глубину. поиск элементарных циклов.
        /*
        private void DFScycle(int u, int endV, List<Edge> E, int[] color, int unavailableEdge, List<int> cycle)
        {
            //если u == endV, то эту вершину перекрашивать не нужно, иначе мы в нее не вернемся, а вернуться необходимо
            if (u != endV)
                color[u] = 2;
            else
            {
                if (cycle.Count >= 2)
                {
                    cycle.Reverse();
                    string s = cycle[0].ToString();
                    for (int i = 1; i < cycle.Count; i++)
                        s += "-" + cycle[i].ToString();
                    bool flag = false; //есть ли палиндром для этого цикла графа в листбоксе?
                    for (int i = 0; i < listBoxMatrix.Items.Count; i++)
                        if (listBoxMatrix.Items[i].ToString() == s)
                        {
                            flag = true;
                            break;
                        }
                    if (!flag)
                    {
                        cycle.Reverse();
                        s = cycle[0].ToString();
                        for (int i = 1; i < cycle.Count; i++)
                            s += "-" + cycle[i].ToString();
                        listBoxMatrix.Items.Add(s);
                    }
                    return;
                }
            }
            for (int w = 0; w < E.Count; w++)
            {
                if (w == unavailableEdge)
                    continue;
                if (color[E[w].v2] == 1 && E[w].v1 == u)
                {
                    List<int> cycleNEW = new List<int>(cycle);
                    cycleNEW.Add(E[w].v2 + 1);
                    DFScycle(E[w].v2, endV, E, color, w, cycleNEW);
                    color[E[w].v2] = 1;
                }
                else if (color[E[w].v1] == 1 && E[w].v2 == u)
                {
                    List<int> cycleNEW = new List<int>(cycle);
                    cycleNEW.Add(E[w].v1 + 1);
                    DFScycle(E[w].v1, endV, E, color, w, cycleNEW);
                    color[E[w].v1] = 1;
                }
            }
        }*/

    }
}
