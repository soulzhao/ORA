/************************************
 * Copyright (c) Plume.studio
 * Creater : Soul
 * Createtime : 2011/03/14
 * 
 * Filename : Matrix.cs
 * Description :
 * 一个通用的举证类以及定义的节点类型MNode
 *************************************/
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace AGNES
{
    class MNode
    {
        //起始节点名称
        private string sSPName;
        //终结节点名称
        private string sEPName;
        //节点权值
        private double val;

        public double Value
        {
            set
            {
                val = value;
            }
            get
            {
                return val;
            }
        }

        public string SName
        {
            get
            {
                return this.sSPName;
            }
            set
            {
                this.sSPName = value;
            }
        }

        public string EName
        {
            get
            {
                return this.sEPName;
            }
            set
            {
                this.sEPName = value;
            }
        }

        public MNode()
        {
            this.sEPName = "";
            this.sSPName = "";
            this.val = 0;
        }

        public MNode(string sname, string ename)
        {
            this.sEPName = ename;
            this.sSPName = sname;
            this.val = 0;
        }

        public MNode(string sname, string ename, double val)
        {
            this.sEPName = ename;
            this.sSPName = sname;
            this.val = val;
        }

        public static MNode operator +(MNode src, MNode des)
        {
            MNode res = new MNode(src.sSPName, src.sEPName);
            res.val = src.val + des.val;
            return res;
        }

        public static MNode operator -(MNode src, MNode des)
        {
            MNode res = new MNode(src.sSPName, src.sEPName);
            res.val = src.val - des.val;
            return res;
        }

        public static MNode operator *(MNode src, MNode des)
        {
            MNode res = new MNode(src.sSPName, src.sEPName);
            res.val = src.val * des.val;
            return res;
        }

        public static MNode operator /(MNode src, MNode des)
        {
            MNode res = new MNode(src.sSPName, src.sEPName);
            res.val = src.val / des.val;
            return res;
        }
        /// <summary>
        /// 节点的复制方法
        /// </summary>
        /// <param name="des">复制的源节点</param>
        public void Copyfrom(MNode des)
        {
            this.val = des.val;
            this.sEPName = des.sEPName;
            this.sSPName = des.sSPName;
        }
        /// <summary>
        /// 判断两个路径节点起始点和终结点是否一致
        /// </summary>
        /// <param name="src">点一</param>
        /// <param name="des">点二</param>
        /// <returns>返回判断值</returns>
        public static bool IsSamePath(MNode src, MNode des)
        {
            return (src.sSPName == des.sSPName) && (src.sEPName == des.sEPName);
        }
    }
    class Matrix
    {
        private MNode[,] x;
        private int rows;
        private int cols;

        public int Rows
        {
            get
            {
                return rows;
            }
        }

        public int Cols
        {
            get
            {
                return cols;
            }
        }

        public Matrix(int col, int row)
        {
            x = new MNode[row, col];
            rows = row;
            cols = col;

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    x[i, j] = new MNode();
                }
            }
        }

        public Matrix(MNode[,] matrix)
        {
            rows = matrix.GetLength(0);
            cols = matrix.Length / rows;
            x = new MNode[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    x[i, j] = new MNode();
                    x[i, j].Copyfrom(matrix[i, j]);
                }
            }
        }

        public Matrix(Matrix matrix)
        {
            rows = matrix.x.GetLength(0);
            cols = matrix.x.Length / rows;
            x = new MNode[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    x[i, j] = new MNode();
                    x[i, j].Copyfrom(matrix.x[i, j]);
                }
            }
        }
        /// <summary>
        /// 获取特定位置的节点
        /// </summary>
        /// <param name="col">行</param>
        /// <param name="row">列</param>
        /// <returns>返回节点</returns>
        public MNode getMNode(int row, int col)
        {
            return this.x[row, col];
        }
        /// <summary>
        /// 依据某一节点的起终点来搜寻某一节点
        /// </summary>
        /// <param name="sPoint"></param>
        /// <param name="ePoint"></param>
        /// <returns></returns>
        public MNode getMNode(string sPoint, string ePoint)
        {
            MNode node = null;
            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.cols; j++)
                {
                    if (this.x[i, j].EName == ePoint && this.x[i, j].SName == sPoint)
                        node = this.x[i, j];
                }
            }
            return node;
        }
        /// <summary>
        /// 获取当期矩阵的节点数组
        /// </summary>
        /// <returns>返回节点阵列</returns>
        public MNode[,] getMNodeList()
        {
            return this.x;
        }
        /// <summary>
        /// 设置当前矩阵某一位置的节点值
        /// </summary>
        /// <param name="node">源节点</param>
        /// <param name="col">列</param>
        /// <param name="row">行</param>
        public void setMNode(MNode node, int col, int row)
        {
            this.x[row, col].Copyfrom(node);
        }
        /// <summary>
        /// 矩阵的重定形
        /// </summary>
        /// <param name="marix">待定形矩阵</param>
        /// <param name="row">期望行数</param>
        /// <param name="col">期望列数</param>
        /// <returns>重定形之后的矩阵</returns>
        public static Matrix ReShape(Matrix matrix, int row, int col)
        {
            if (matrix.Rows * matrix.Cols != col * row)
            {
                System.Console.WriteLine("col * row isn't coincide to the array length");
                return null;
            }

            Matrix mat = new Matrix(col, row);
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    int r = ((i * (col - 1)) + j) / matrix.Cols;
                    int c = ((i * (col - 1)) + j) % matrix.Cols;
                    mat.x[i, j] = matrix.x[r, c];
                }
            }
            return mat;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        //private MNode[] MatrixToArray(Matrix matrix)
        //{
        //    MNode[] array = new MNode[matrix.Rows * matrix.Cols];
        //    int m = 0;
        //    for (int i = 0; i < matrix.Rows; i++)
        //    {
        //        for (int j = 0; j < matrix.Cols; j++)
        //        {
        //            array[m++] = matrix.x[i, j];
        //        }
        //    }
        //    return array;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        static public Matrix ArrayToMatrix(MNode[] array, int col, int row)
        {
            if (array.Length != col * row)
            {
                System.Console.WriteLine("col * row isn't coincide to the array length");
                return null;
            }
            Matrix matrix = new Matrix(col, row);
            int m = 0;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    matrix.x[i, j] = array[m++];
                }
            }
            return matrix;
        }
        /// <summary>
        /// 返回与x矩阵同名的零方阵
        /// </summary>
        /// <param name="s">行列数</param>
        /// <param name="x">命名阵</param>
        /// <returns>返回的矩阵</returns>
        public static Matrix Zeros(int s, MNode[,] x)
        {
            ArrayList f = new ArrayList();
            for (int i = 0; i < s * s; f.Add(0), i++) ;
            return Matrix.GenerateMatrix(s, f, x);
        }
        /// <summary>
        /// 返回与x矩阵同名的随机方阵
        /// </summary>
        /// <param name="s">行列数</param>
        /// <param name="n">命名阵</param>
        /// <returns>返回生成的矩阵</returns>
        public static Matrix Random(int s, MNode[,] n)
        {
            long tick = System.DateTime.Now.Ticks;
            //Random ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
            System.Random rand = new System.Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
            ArrayList f = new ArrayList();
            //for(int i = 0; i < s * s; f.Add(2 * rand.NextDouble() - 1), i++);
            for (int i = 0; i < s * s; f.Add(rand.NextDouble()), i++) ;
            //for (int i = 0; i < s * s; System.Console.Write(f[i]+"  "), i++);
            //System.Console.WriteLine();
            return Matrix.GenerateMatrix(s, f, n);
        }
        /// <summary>
        /// 返回以n阵命名的单位阵
        /// </summary>
        /// <param name="s">行列数</param>
        /// <param name="n">命名阵</param>
        /// <returns>返回生成的单位阵</returns>
        public static Matrix Eye(int s, MNode[,] n)
        {
            ArrayList f = new ArrayList();
            int x = 0;
            for (int i = 0; i < s * s; i++)
            {
                if (i == (s + 1) * x)
                {
                    f.Add(1);
                    x++;
                }
                else
                    f.Add(0);
            }
            return Matrix.GenerateMatrix(s, f, n);
        }
        /// <summary>
        /// 内部使用的矩阵生成式
        /// </summary>
        /// <param name="s"></param>
        /// <param name="value"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        private static Matrix GenerateMatrix(int s, ArrayList value, MNode[,] nodes)
        {
            Matrix m = new Matrix(s, s);
            int x = 0;
            for (int i = 0; i < m.rows; i++)
            {
                for (int j = 0; j < m.cols; x++, j++)
                {
                    m.x[i, j].Value = double.Parse(value[x].ToString());
                    m.x[i, j].EName = nodes[i, j].EName;
                    m.x[i, j].SName = nodes[i, j].SName;
                }
            }
            return m;
        }
        /// <summary>
        /// 从src到des的矩阵拷贝
        /// </summary>
        /// <param name="src">源</param>
        /// <param name="des">目的阵</param>
        public static void Copy(Matrix des, Matrix src)
        {
            for (int i = 0; i < des.Rows; i++)
            {
                for (int j = 0; j < des.Cols; j++)
                {
                    des.x[i, j].Copyfrom(src.x[i, j]);
                }
            }
        }

        public static Matrix operator +(Matrix src, Matrix des)
        {
            Matrix result = new Matrix(src);
            for (int i = 0; i < src.Rows; i++)
            {
                for (int j = 0; j < src.Cols; j++)
                {
                    result.x[i, j] = src.x[i, j] + des.x[i, j];
                }
            }
            return result;
        }

        public static Matrix operator -(Matrix src, Matrix des)
        {
            Matrix result = new Matrix(src);
            for (int i = 0; i < src.Rows; i++)
            {
                for (int j = 0; j < src.Cols; j++)
                {
                    result.x[i, j] = src.x[i, j] - des.x[i, j];
                }
            }
            return result;
        }

        public static Matrix operator *(Matrix src, Matrix des)
        {
            Matrix result = new Matrix(src);
            // 对于前置矩阵的每一行
            for (int i = 0; i < src.Rows; i++)
            {
                // 对于后置矩阵的每一列
                for (int j = 0; j < src.Cols; j++)
                {
                    result.x[i, j].SName = src.x[i, j].SName;
                    result.x[i, j].EName = src.x[i, j].EName;

                    result.x[i, j].Value = GetMarixMultValue(src, i, des, j);
                }
            }
            return result;
        }

        private static double GetMarixMultValue(Matrix src, int row, Matrix des, int col)
        {
            double res = 0;
            for (int i = 0; i < src.Rows; i++)
            {
                res += src.x[row, i].Value * des.x[i, col].Value;
            }
            return res;
        }

        public static Matrix operator /(Matrix src, long n)
        {
            Matrix result = new Matrix(src);
            for (int i = 0; i < src.Rows; i++)
            {
                for (int j = 0; j < src.Cols; j++)
                {
                    result.x[i, j].Value = src.x[i, j].Value / n;
                }
            }
            return result;
        }
        /// <summary>
        /// 测试用例，打印当前矩阵
        /// </summary>
        public void Print()
        {
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Cols; j++)
                {
                    string value = "";
                    if (this.x[i, j].Value.Equals(double.MaxValue))
                        value = "MAX";
                    else
                        value = this.x[i, j].Value.ToString("0.###");
                    System.Console.Write(value + "\t");
                    //System.Console.Write(this.x[i, j].SName + "--" + this.x[i, j].EName + ":" + value + "\t");
                }
                System.Console.WriteLine();
            }
        }

        public void reduceMatrixByIndex(int index)
        {
            this.reduceMatrixByIndex(new List<int> { index });
        }

        public void reduceMatrixByIndex(List<int> lIndex)
        {
            if (lIndex == null || lIndex.Count <= 0) return;
            foreach (int i in lIndex)
            {
                if (i >= this.Cols || i >= this.Rows) return;
            }

            int r = this.rows - lIndex.Count;
            int c = this.cols - lIndex.Count;
            //int r = lIndex.Count;
            //int c = lIndex.Count;

            if (r == 0 || c == 0) return;

            MNode[,] x = new MNode[r, c];
            int pi = 0, pj = 0;

            for (int i = 0; i < this.Rows; i++)
            {
                if (lIndex.Contains(i)) continue;

                for (int j = 0; j < this.Rows; j++)
                {
                    if (lIndex.Contains(j)) continue;

                    x[pi, pj] = new MNode();
                    x[pi, pj].Copyfrom(this.x[i, j]);
                    pj = (++pj) % c;
                }
                pi = (++pi) % r;
            }

            this.x = x;
            this.rows = r;
            this.cols = c;
        }

    }
}