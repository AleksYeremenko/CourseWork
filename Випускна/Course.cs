using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using System.Collections.ObjectModel;

namespace Випускна
{
    public partial class MainFrm : Form
    {
        public Theory Next_form1 = new Theory();

        public MainFrm()
        {
            InitializeComponent();
            AddOwnedForm(Next_form1);
        }

        public static int[,] GetDoubleArray(int n, int k, int l)//Метод для заполнения матрицы
        {
            Random rnd = new Random();
            int[,] temp = new int[n, n];
            k = rnd.Next(0, n);
            l = rnd.Next(0, n);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == k & j == l)
                    {
                        temp[i, j] = 150;
                    }
                    else
                    {
                        temp[i, j] = rnd.Next(10, 50);
                    }
                }
            }
            return temp;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            var g = new Matrix();
            int x0 = int.Parse(textBox5.Text);
            int y0 = int.Parse(textBox3.Text);
            int x1 = int.Parse(textBox7.Text);
            int y1 = int.Parse(textBox6.Text);
            int n = int.Parse(textBox1.Text);
            int velocity = int.Parse(textBox7.Text);
            int a = int.Parse(textBox9.Text);
            int[,] radioactive_path = new int[n, n];
            dataGridView1.RowCount = n;
            dataGridView1.ColumnCount = n;
            dataGridView1.Rows[0].HeaderCell.Value = "X";
            dataGridView1.Columns[0].HeaderText = "Y";


            radioactive_path = GetDoubleArray(n, 0, 0);
            List<string> check = new List<string>() { };
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    g.AddVertex((i).ToString() + (j).ToString());
                    check.Add((i).ToString() + (j).ToString());

                }
            }
            listBox1.Items.Add(check.Count+" Кол-во вершин");
            //Добавление дуг
            /* 
            for (int i = 0; i < check.Count; i++)
            {
                for (int j = 0; j < check.Count; j++)
                {
                    g.AddEdge(check[i], check[i + 1], radioactive_path[i, j]);
                }
            }
            */
           
            
            /*
            //Заполнение матрицы
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = radioactive_path[i, j];
                    if (radioactive_path[i, j] == 150)
                    {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.Red;
                    }
                }
            }
           

            //Вызов алгоритма
            var dijkstra = new Algoritm(g);
            
            var path = dijkstra.FindShortestPath(x0.ToString() + "," + y0.ToString() + ",", x1.ToString() + "," + y1.ToString() + ",");
            listBox1.Items.Add(path);
            
             int[,] matrix = new int[n, n];
             List<Point> arr = PathNode.FindPath(matrix, new Point(x0, y0), new Point(x1, y1));
             int sumPath = 0;
             for (int i = 0; i < arr.Count; i++)
             {
                listBox1.Items.Add(arr[i]+"///");
                 sumPath += radioactive_path[arr[i].X, arr[i].Y];
             }

             string FindPath = path.ToString();
             FindPath = FindPath.Remove(FindPath.Length - 1, 1);
            
             string[] F = FindPath.Split(',');

             int[] GH = new int[F.Length];
             int[] X = new int[F.Length / 2];
             int[] Y = new int[F.Length / 2];
             for (int j = 0; j < F.Length; j++)
             {
                 GH[j] = Convert.ToInt32(F[j]);
             }
             int k = 0;
             int sum = 0;
             int k_d = 0;
             int countL = 0;
             for (int i = 0; i < F.Length; i += 2)
             {

                 X[k] = GH[i];
                 Y[k] = GH[i + 1];
                 
                 k++;
             }
             for (int i = 0; i < F.Length / 2; i++)
             {
                 if (X[i] == Y[i])
                 {
                     k_d++;
                 }
                 else
                 {
                     countL++;
                 }
                 sum += radioactive_path[X[i], Y[i]];
             }
             if (sumPath > sum)
             {
                 for (int i = 0; i < F.Length / 2; i++)
                 {
                     dataGridView1.Rows[X[i]].Cells[Y[i]].Style.BackColor = System.Drawing.Color.Green;
                    sum = sumPath;
                 }

             }
             else
             {
                 for (int i = 0; i < arr.Count; i++)
                 {
                     dataGridView1.Rows[arr[i].X].Cells[arr[i].Y].Style.BackColor = System.Drawing.Color.Green;
                 }
             }



             double l = a * k + (k_d - 1) * a * Math.Sqrt(2);
             double time = 60 * Math.Round(l / velocity, 2);
             double D = (sum / time)*1000;
             listBox1.Items.Add("Отримана доза на цьому шляху: " + Math.Round(D, 2) + " мкЗіверт/час");
             */

         }


         public class PathNode // Створюємо клас точки
         {
             public Point Position
             { get; set; }// Координати точки на карті.
             public int Path_Length
             { get; set; } // Довжина шляху від старту.
             public PathNode From
             { get; set; }// Точка, з якої прийшли в цю точку.
             public int distance_H
             { get; set; } 
             public int distance_F // Очікуване повне відстань до цілі.
             {
                 get
                 {
                     return this.Path_Length + this.distance_H;
                 }
             }
             public static List<Point> FindPath(int[,] field, Point start, Point finish)//Створюємо список для обчислення маршруту
             {
                 //Створюється 2 списку вершин - НЕ переглянуті і переглянуті.
                 var not_visited = new Collection<PathNode>();
                 var visited = new Collection<PathNode>();
                 // Створюємо початкову точку.
                 PathNode Start_N = new PathNode()
                 {
                     Position = start,
                     From = null,
                     Path_Length = 0,
                     distance_H = GetHeuristicPathLength
                     
                     (start, finish)//Функція яка розраховує приблизну відстань між точками
                 };
                 visited.Add(Start_N);//додаємо до колекції початкову точку.
                 while (visited.Count > 0)
                 {
                     var NodeN = visited.OrderBy(node =>
                       node.distance_F).First(); //Сортуємо елементи послідовності.
                     if (NodeN.Position == finish)//Якщо координати точки, яку ми відвідали це координати кінцевої точки, то ми знайшли шлях.
                         return Path_Node(NodeN);
                     visited.Remove(NodeN);//Перенести точку зі списку чекають на розгляд у список вже розглянутих.
                     not_visited.Add(NodeN);
                     foreach (var neighbourNode in Neighbours(NodeN, finish, field))// переглядаємо сусідні точки
                     {
                         if (not_visited.Count(node => node.Position == neighbourNode.Position) > 0)//Якщо сусідню точку ми переглянули - ми продовжуємо переглядати.
                             continue;
                         var openNode = visited.FirstOrDefault(node =>
                           node.Position == neighbourNode.Position);//ствоюємо точку і запам'ятаємо її як сусідню
                         if (openNode == null)//Якщо ця точка немає в списку не переглянуті, то ми її додаємо туди.
                             visited.Add(neighbourNode);
                         else
                           if (openNode.Path_Length > neighbourNode.Path_Length)//перевіряємо чи прийшли ми у сусідню точку коротким шляхом.
                         {
                             openNode.From = NodeN;
                             openNode.Path_Length = neighbourNode.Path_Length;
                         }
                     }
                 }
                 return null;// Повертаємо значення, якщо список точок, які не переглянуті пуст, то шляху не існує.

             }
             private static int Distance_Between_Neighbours()//Додаємо функцію відстані між сусідніми точками і заповнюємо її значення.
             {
                 return 1;
             }
             private static int GetHeuristicPathLength(Point from, Point to)//Додаємо функцію приблизної оцінки відстані до цілі.
             {
                 return Math.Abs(from.X - to.X) + Math.Abs(from.Y - to.Y);
             }
             private static Collection<PathNode> Neighbours(PathNode path_dot, Point goal, int[,] graph) //Створюємо колікцію в якій знаходиться сусідні точки.
             {
                 var result = new Collection<PathNode>();
                 Point[] neighbourPoints = new Point[8];//Записуємо у колекцію які точки можуть бути сусідніми.
                 neighbourPoints[0] = new Point(path_dot.Position.X + 1, path_dot.Position.Y);
                 neighbourPoints[1] = new Point(path_dot.Position.X - 1, path_dot.Position.Y);
                 neighbourPoints[2] = new Point(path_dot.Position.X, path_dot.Position.Y + 1);
                 neighbourPoints[3] = new Point(path_dot.Position.X, path_dot.Position.Y - 1);
                 neighbourPoints[4] = new Point(path_dot.Position.X + 1, path_dot.Position.Y + 1);
                 neighbourPoints[5] = new Point(path_dot.Position.X + 1, path_dot.Position.Y - 1);
                 neighbourPoints[6] = new Point(path_dot.Position.X - 1, path_dot.Position.Y - 1);
                 neighbourPoints[7] = new Point(path_dot.Position.X - 1, path_dot.Position.Y + 1);
                 foreach (var point in neighbourPoints)
                 {
                     if (point.X < 0 || point.X >= graph.GetLength(0)) // Перевіряємо щоб ці точки не виходили за межу.
                     {
                         continue;
                     }
                     if (point.Y < 0 || point.Y >= graph.GetLength(1))
                     {
                         continue;
                     }

                     if ((graph[point.X, point.Y] != 0) && (graph[point.X, point.Y] != 1))// Перевіряємо, щоб можна було переходити з однієї вершини на іншу.
                     {
                         continue;
                     }
                     var Node_Neughbour = new PathNode()//Заповнюємо дані маршрута.
                     {
                         Position = point,
                         From = path_dot,
                         Path_Length = path_dot.Path_Length + Distance_Between_Neighbours(),
                         distance_H = GetHeuristicPathLength(point, goal)
                     };
                     result.Add(Node_Neughbour);//Додаємо дані до списку і повертаємо його.
                 }
                 return result;
             }
             private static List<Point> Path_Node(PathNode path)//Це маршрут, який ми отримали.
             {
                 var result = new List<Point>();
                 var count_Node = path;
                 while (count_Node != null)
                 {
                     result.Add(count_Node.Position);
                     count_Node = count_Node.From;
                 }
                 result.Reverse();
                 return result;
             }
             
        }
        
        private void MainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Next_form1.Show();
        }
        private void MainFrm_Load(object sender, EventArgs e)
        {
            webBrowser1.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (webBrowser1.Visible == true)
            {
                webBrowser1.Visible = false;

            }
        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            int n = int.Parse(textBox1.Text);
            listBox1.Items.Clear();


            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.White;

                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Next_form1.Show();
        }
    }
}
