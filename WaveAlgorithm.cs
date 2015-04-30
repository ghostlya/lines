using System;
using System.Collections.Generic;



namespace linesk
{
    class WaveAlgorithm
    {
        private List<PointInMatrix> bars = new List<PointInMatrix>();
        private int n = 0, m = 0;

        private bool[,] matrix;
       
        /// <summary>
        /// Матрица свободных ячеек ( 1 - свободная, 0 - занятая клетка)
        /// </summary>
        public bool[,] Matrix
        {
            get { return matrix; }
            set { matrix = value; }
        }

        private void ConvertMatrixToPoints()
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i,j])
                        bars.Add(new PointInMatrix(i, j, 0, null));
                    
                }
                
            }

            m = matrix.GetLength(1);
            n = matrix.GetLength(0);
        }


        /// <summary>
        /// Проверка возможности перемещения в заданную точнку
        /// </summary>
        /// <param name="startX">координата X начальной ячейки</param>
        /// <param name="startY">координата Y начальной ячейки</param>
        /// <param name="endX">координата X конечной ячейки</param>
        /// <param name="endY">координата X конечной ячейки</param>
 
        
        public bool Check(int startX, int startY, int endX, int endY)
        {
            ConvertMatrixToPoints();

            PointInMatrix startPoint = new PointInMatrix(startX, startY, 0, null);
            PointInMatrix endPoint = new PointInMatrix(endX, endY, 0, null);
            PointInMatrix d = new PointInMatrix(n, m, 0, null);

            List<PointInMatrix> openList = new List<PointInMatrix>();
            List<PointInMatrix> closedList = new List<PointInMatrix>();
            openList.Add(startPoint);
 
            while (true)
            {
                if (openList.Count == 0)
                {
                    return false;
                }

                PointInMatrix current = openList[0];
                foreach (PointInMatrix p in openList)
                {
                    if (p.cost < current.cost)
                        current = p;
                }
 
                if (current.Equals(endPoint))
                {
                   return true;
                }
                openList.Remove(current);
                closedList.Add(current);

                PointInMatrix top = new PointInMatrix(current.x - 1, current.y, cost(current.x - 1, current.y, endPoint), current);
                PointInMatrix bot = new PointInMatrix(current.x + 1, current.y, cost(current.x + 1, current.y, endPoint), current);
                PointInMatrix left = new PointInMatrix(current.x, current.y - 1, cost(current.x, current.y - 1, endPoint), current);
                PointInMatrix right = new PointInMatrix(current.x, current.y + 1, cost(current.x, current.y + 1, endPoint), current);
 
                bool topFlag = checkPoint(top, d), botFlag = checkPoint(bot, d), 
                    leftFlag = checkPoint(left, d), rightFlag = checkPoint(right, d);
                foreach (PointInMatrix p in bars)
                {
                    if (p.Equals(top))
                        topFlag = false;
                    if (p.Equals(bot))
                        botFlag = false;
                    if (p.Equals(left))
                        leftFlag = false;
                    if (p.Equals(right))
                        rightFlag = false;
                }
                foreach (PointInMatrix p in closedList)
                {
                    if (p.Equals(top))
                        topFlag = false;
                    if (p.Equals(bot))
                        botFlag = false;
                    if (p.Equals(left))
                        leftFlag = false;
                    if (p.Equals(right))
                        rightFlag = false;
                }
 
                if (topFlag)
                    openList.Add(top);
                if (botFlag)
                    openList.Add(bot);
                if (leftFlag)
                    openList.Add(left);
                if (rightFlag)
                    openList.Add(right);
            }
        }


        private bool checkPoint(PointInMatrix p, PointInMatrix diap)
        { 
            return (p.x >= 0 && p.x < diap.x && p.y >= 0 && p.y < diap.y);
        }

        /// <summary>
        /// Растояние до конечной точки
        /// </summary>
        private int cost(int x, int y, PointInMatrix endPoint)
        { 
            return (Math.Abs(x - endPoint.x) + Math.Abs(y - endPoint.y));
        }
    }
 
    public class PointInMatrix
    {
        public PointInMatrix(int xval, int yval, int costval, PointInMatrix pval)
        {
            x = xval;
            y = yval;
            cost = costval;
            parent = pval;
        }
        public int x, y, cost;
        public PointInMatrix parent;
 
       
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            PointInMatrix p = obj as PointInMatrix;
            if ((object)p == null)
                return false;
 
            return (x == p.x && y == p.y);
        }
    }
 }

