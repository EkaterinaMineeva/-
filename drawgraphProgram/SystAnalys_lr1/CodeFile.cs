using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystAnalys_lr1
{
    class Vertex
    {
        public double xdec, ydec;
        public float xcenter, ycenter;
        public float xpic, ypic;
        public bool state=false;
        public float SizeCell;
        public double xdec1, ydec1;
        public float xpic1, ypic1;
        public double xdec2, ydec2;
        public float xpic2, ypic2;
        public double xdec3, ydec3;
        public float xpic3, ypic3;
        public double xdec4, ydec4;
        public float xpic4, ypic4;

        public Vertex(double xdec, double ydec, float xpic, float ypic, float xcenter, float ycenter,float SizeCell, double xdec1, double ydec1, float xpic1, float ypic1, double xdec2, double ydec2, float xpic2, float ypic2, double xdec3, double ydec3, float xpic3, float ypic3, double xdec4, double ydec4, float xpic4, float ypic4)
        {
            this.xdec = xdec;
            this.ydec = ydec;
            this.xpic = xpic;
            this.ypic = ypic;
            this.xcenter = xcenter;
            this.ycenter = ycenter;
            this.SizeCell = SizeCell;
            this.xdec1 = xdec1;
            this.ydec1 = ydec1;
            this.xpic1 = xpic1;
            this.ypic1 = ypic1;
            this.xdec2 = xdec2;
            this.ydec2 = ydec2;
            this.xpic2 = xpic2;
            this.ypic2 = ypic2;
            this.xdec3 = xdec3;
            this.ydec3 = ydec3;
            this.xpic3 = xpic3;
            this.ypic3 = ypic3;
            this.xdec4 = xdec4;
            this.ydec4 = ydec4;
            this.xpic4 = xpic4;
            this.ypic4 = ypic4;
        }
        public void Vertex_bool()
        {
            state = false;
        }

    }

    class Edge
    {
        public int vout, vin;

        public Edge(int vout, int vin)
        {
            this.vout = vout;
            this.vin = vin;
        }
    }

    class DrawGraph
    {
        Bitmap bitmap;
        Pen blackPen;
        Pen redPen;
        Pen darkGoldPen;
        Graphics gr;
        Font fo;
        Brush br;
        PointF point;
        public int R = 20; //радиус окружности вершины

        public DrawGraph(int width, int height)
        {
            bitmap = new Bitmap(width, height);
            gr = Graphics.FromImage(bitmap);
            clearSheet();
            blackPen = new Pen(Color.Black);
            blackPen.Width = 2;
            redPen = new Pen(Color.Red);
            redPen.Width = 2;
            darkGoldPen = new Pen(Color.DarkGoldenrod);
            darkGoldPen.Width = 2;
            fo = new Font("Arial", 15);
            br = Brushes.Black;
        }

        public Bitmap GetBitmap()
        {
            return bitmap;
        }

        public void clearSheet()
        {
            gr.Clear(Color.White);
        }

        /*public void drawVertex(int x, int y, string number)
        {
            gr.FillEllipse(Brushes.White, (x - R), (y - R), 2 * R, 2 * R);
            gr.DrawEllipse(blackPen, (x - R), (y - R), 2 * R, 2 * R);
            point = new PointF(x - 9, y - 9);
            gr.DrawString(number, fo, br, point);
        }*/

        public void drawRectangle(float x, float y, float rectSize)
        {
            gr.DrawRectangle(Pens.Black, x * rectSize, y * rectSize, rectSize, rectSize);
        }
        public void drawRectangle0(float x, float y, float rectSize)
        {
            SolidBrush brush = new SolidBrush(Color.Pink);
            gr.FillRectangle(brush, x * rectSize, y * rectSize, rectSize, rectSize);
            //gr.DrawRectangle(Pens.Pink, x * rectSize, y * rectSize, rectSize, rectSize);
        }
        public void drawRectangle_cell(float x, float y, float Size)
        {
            gr.DrawRectangle(Pens.Black, x, y, Size, Size);
        }
        public void drawRectanglecell(float x, float y, float Size)
        {
            SolidBrush brush = new SolidBrush(Color.Pink);
            gr.FillRectangle(brush, x , y , Size, Size);
        }
        public void drawEllipse(int w, int h)
        {
            int Rad = 3;
            SolidBrush brush = new SolidBrush(Color.Red);
            gr.FillEllipse(brush, (w - Rad), (h - Rad), 2 * Rad, 2 * Rad);
        }
        public void drawEllipse1(float x, float y)
        {
            int Rad = 3;
            SolidBrush brush = new SolidBrush(Color.Yellow);
            gr.FillEllipse(brush, (x - Rad), (y - Rad), 2 * Rad, 2 * Rad);
        }
        public void drawEllipse0(float x, float y)
        {
            int Rad = 1;
            SolidBrush brush = new SolidBrush(Color.BlueViolet);
            gr.FillEllipse(brush, x , y , Rad, Rad);
        }
        public void drawEllipseG(float x, float y, int i)
        {
            Pen myPen = new Pen(Color.Black, 1);
            myPen.Color = Color.FromArgb(250, (9 * i) % 255, 0, (9 * i) % 255);
            //рисуем пиксель
            gr.DrawRectangle(myPen, x, y, 1, 1);
        }
        public void drawEllipseO(float x, float y, int i)
        {
            //Pen myPen = new Pen(Color.White, 1);
            //int Rad = 2;
            //SolidBrush brush = new SolidBrush(Color.BlueViolet);
            //gr.FillEllipse(brush, (x - Rad), (y - Rad), 2 * Rad, 2 * Rad);
            //myPen.Color = Color.FromArgb(100, (100 * i) % 255, 0, (100 * i) % 255);
            //рисуем пиксель
            gr.DrawEllipse(Pens.Green, x, y, 1, 1);
        }
        public void drawRectangle1(int x, int y, float rectSize)
        {
            gr.DrawRectangle(Pens.Black, x * rectSize, y * rectSize, rectSize, rectSize);
        }


        /*public void drawSelectedVertex(int x, int y)
        {
            gr.DrawEllipse(redPen, (x - R), (y - R), 2 * R, 2 * R);
        }*/

       /*public void drawEdge(Vertex V1, Vertex V2, Edge E, int numberE)
        {
            if (E.vout == E.vin)
            {
                gr.DrawArc(darkGoldPen, (V1.x - 2 * R), (V1.y - 2 * R), 2 * R, 2 * R, 90, 270);
                point = new PointF(V1.x - (int)(2.75 * R), V1.y - (int)(2.75 * R));
                //gr.DrawString(((char)('a' + numberE)).ToString(), fo, br, point);
                drawVertex(V1.x, V1.y, (E.v1 + 1).ToString());
            }
            else
            {
                gr.DrawLine(darkGoldPen, V1.x, V1.y, V2.x, V2.y);
                point = new PointF((V1.x + V2.x) / 2, (V1.y + V2.y) / 2);
                //gr.DrawString(((char)('a' + numberE)).ToString(), fo, br, point);
                drawVertex(V1.x, V1.y, (E.v1 + 1).ToString());
                drawVertex(V2.x, V2.y, (E.v2 + 1).ToString());
            }
        }*/

        /*public void drawALLGraph(List<Vertex> V, List<Edge> E)
        {
            //рисуем ребра
            for (int i = 0; i < E.Count; i++)
            {
                if (E[i].vout == E[i].vin)
                {
                    gr.DrawArc(darkGoldPen, (V[E[i].vout].xpic - 2 * R), (V[E[i].vout].ypic - 2 * R), 2 * R, 2 * R, 90, 270);
                    point = new PointF(V[E[i].vout].xpic - (int)(2.75 * R), V[E[i].vout].ypic - (int)(2.75 * R));
                    //gr.DrawString(((char)('a' + i)).ToString(), fo, br, point);
                }
                else
                {
                    gr.DrawLine(darkGoldPen, V[E[i].vout].xpic, V[E[i].vout].ypic, V[E[i].vin].xpic, V[E[i].vin].ypic);
                    point = new PointF((V[E[i].vout].xpic + V[E[i].vin].xpic) / 2, (V[E[i].vout].ypic + V[E[i].vin].ypic) / 2);
                    //gr.DrawString(((char)('a' + i)).ToString(), fo, br, point);
                }
            }
            //рисуем вершины
            for (int i = 0; i < V.Count; i++)
            {
                //drawVertex(V[i].xpic, V[i].ypic, (i + 1).ToString());
            }
        }*/

        //заполняет матрицу смежности
        public void fillAdjacencyMatrix(int numberV, List<Edge> E, int[,] matrix)
        {
            for (int i = 0; i < numberV; i++)
                for (int j = 0; j < numberV; j++)
                    matrix[i, j] = 0;
            for (int i = 0; i < E.Count; i++)
            {
                matrix[E[i].vout, E[i].vout] = 1;
                matrix[E[i].vin, E[i].vin] = 1;
            }
        }

        //заполняет матрицу инцидентности
        public void fillIncidenceMatrix(int numberV, List<Edge> E, int[,] matrix)
        {
            for (int i = 0; i < numberV; i++)
                for (int j = 0; j < E.Count; j++)
                    matrix[i, j] = 0;
            for (int i = 0; i < E.Count; i++)
            {
                matrix[E[i].vout, i] = 1;
                matrix[E[i].vin, i] = 1;
            }
        }

        
    }
}