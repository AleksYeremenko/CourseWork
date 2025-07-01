using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Linq;

namespace Курсовая
{
    public partial class MainFrm : Form
    {

        public MainFrm()
        {
            InitializeComponent();
        }
        private void MainFrm_Load(object sender, EventArgs e)
        {
            string path = Application.StartupPath;
            webBrowser1.Navigate(path + "\\Документация.htm");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            Random rnd = new Random();
            int n = int.Parse(textBox1.Text);
            int[,] matrix = new int[n, n];
            int x0 = int.Parse(textBox5.Text);
            int y0 = int.Parse(textBox3.Text);
            int x1 = int.Parse(textBox6.Text);
            int y1 = int.Parse(textBox7.Text);
            int k = 0;
            int velocity = int.Parse(textBox4.Text);
            double a = double.Parse(textBox9.Text);
            double H = double.Parse(textBox8.Text);
            dataGridView1.RowCount = n;
            dataGridView1.ColumnCount = n;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = 0;
                    dataGridView1.Rows[i].Cells[j].Value = matrix[i, j];
                }
            }
            List<Point> arr = PathNode.FindPath(matrix, new Point(x0, y0), new Point(x1, y1));
            for (int i = 0; i < arr.Count; i++)
            {
                listBox1.Items.Add("X: " + arr[i].X + ", Y: " + arr[i].Y);
                k++;
            }

            for (int i = 0; i < arr.Count; i++)
            {
                dataGridView1.Rows[arr[i].X].Cells[arr[i].Y].Value = 1;
            }

            double l = Math.Round(a * k, 1);
            int time = (Int32)l / velocity;
            double D = H * time;
            listBox1.Items.Add(D);
        }

        public class PathNode
        {
            // Координаты точки на карте.
            public Point Position { get; set; }
            // Длина пути от старта (G).
            public int Path_Length { get; set; }
            // Точка, из которой пришли в эту точку.
            public PathNode From { get; set; }
            // Примерное расстояние до цели (H).
            public int distance_H { get; set; }
            // Ожидаемое полное расстояние до цели (F).
            public int distance_F
            {
                get
                {
                    return this.Path_Length + this.distance_H;
                }
            }

            public static List<Point> FindPath(int[,] field, Point start, Point goal)
            {
                // Создается 2 списка вершин — не просмотренные и просмотренные.
                var not_visited = new Collection<PathNode>();
                var visited = new Collection<PathNode>();
                // Шаг 2.
                PathNode startNode = new PathNode()
                {
                    Position = start,
                    From = null,
                    Path_Length = 0,
                    distance_H = GetHeuristicPathLength(start, goal)
                };
                visited.Add(startNode);
                while (visited.Count > 0)
                {
                    // Шаг 3.
                    var currentNode = visited.OrderBy(node =>
                      node.distance_F).First();
                    // Шаг 4.
                    if (currentNode.Position == goal)
                        return GetPathForNode(currentNode);
                    // Шаг 5.
                    visited.Remove(currentNode);
                    not_visited.Add(currentNode);
                    // Шаг 6.
                    foreach (var neighbourNode in GetNeighbours(currentNode, goal, field))
                    {
                        // Шаг 7.
                        if (not_visited.Count(node => node.Position == neighbourNode.Position) > 0)
                            continue;
                        var openNode = visited.FirstOrDefault(node =>
                          node.Position == neighbourNode.Position);
                        // Шаг 8.
                        if (openNode == null)
                            visited.Add(neighbourNode);
                        else
                          if (openNode.Path_Length > neighbourNode.Path_Length)
                        {
                            // Шаг 9.
                            openNode.From = currentNode;
                            openNode.Path_Length = neighbourNode.Path_Length;
                        }
                    }
                }
                // Шаг 10.
                return null;
            }

            private static int GetDistanceBetweenNeighbours()
            {
                return 0;
            }

            private static int GetHeuristicPathLength(Point from, Point to)
            {
                return Math.Abs(from.X - to.X) + Math.Abs(from.Y - to.Y);
            }

            private static Collection<PathNode> GetNeighbours(PathNode pathNode,
          Point goal, int[,] field)
            {
                var result = new Collection<PathNode>();

                // Соседними точками являются соседние по стороне клетки.
                Point[] neighbourPoints = new Point[8];

                neighbourPoints[0] = new Point(pathNode.Position.X + 1, pathNode.Position.Y);
                neighbourPoints[1] = new Point(pathNode.Position.X - 1, pathNode.Position.Y);
                neighbourPoints[2] = new Point(pathNode.Position.X, pathNode.Position.Y + 1);
                neighbourPoints[3] = new Point(pathNode.Position.X, pathNode.Position.Y - 1);
                neighbourPoints[4] = new Point(pathNode.Position.X + 1, pathNode.Position.Y + 1);
                neighbourPoints[5] = new Point(pathNode.Position.X + 1, pathNode.Position.Y - 1);
                neighbourPoints[6] = new Point(pathNode.Position.X - 1, pathNode.Position.Y - 1);
                neighbourPoints[7] = new Point(pathNode.Position.X - 1, pathNode.Position.Y + 1);

                foreach (var point in neighbourPoints)
                {
                    // Проверяем, что не вышли за границы карты.
                    if (point.X < 0 || point.X >= field.GetLength(0))
                        continue;
                    if (point.Y < 0 || point.Y >= field.GetLength(1))
                        continue;
                    // Проверяем, что по клетке можно ходить.
                    if ((field[point.X, point.Y] != 0) && (field[point.X, point.Y] != 1))
                        continue;
                    // Заполняем данные для точки маршрута.
                    var neighbourNode = new PathNode()
                    {
                        Position = point,
                        From = pathNode,
                        Path_Length = pathNode.Path_Length +
                        GetDistanceBetweenNeighbours(),
                        distance_H = GetHeuristicPathLength(point, goal)
                    };
                    result.Add(neighbourNode);
                }
                return result;
            }

            private static List<Point> GetPathForNode(PathNode pathNode)
            {

                var result = new List<Point>();
                var currentNode = pathNode;
                while (currentNode != null)
                {
                    result.Add(currentNode.Position);
                    currentNode = currentNode.From;
                }
                result.Reverse();
                return result;
            }
        }
    }
}
