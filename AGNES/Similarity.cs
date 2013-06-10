using System;
using System.Collections.Generic;
using System.Text;
using XmlParser;

namespace AGNES
{
    class Similarity
    {
        Matrix graph;   // A

        /// <summary>
        ///   ?? 包含本身嘛？
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private int getEdgesCount(NodeBean node)
        {
            int r = int.Parse(node.id);
            int count = 0;
            for (int j = 0; j < graph.Cols; j++ )
            {
                if (!graph.getMNode(r, j).Value.Equals(0))
                    count++;
            }
            return count;
        }

        private List<NodeBean> getStOfNode(NodeBean node)
        {
            List<NodeBean> lres = new List<NodeBean>();

            int r = int.Parse(node.id);
            // 找邻居节点
            for (int j = 0; j < graph.Cols; j++)
            {
                if (!graph.getMNode(r, j).Value.Equals(0)) // 当A(r, j) != 0
                    lres.Add(MyXml.findNodeById(j.ToString()));
            }

            return lres;
        }

        private List<NodeBean> getCrossSt(List<NodeBean> lst1, List<NodeBean> lst2)
        {
            List<NodeBean> lsCrt = new List<NodeBean>();

            foreach(NodeBean bean in lst1)
            {
                NodeBean b = lst2.Find(delegate(NodeBean nodebean)
                {
                    return nodebean.id.Equals(bean.id);
                });

                if(b != null)
                    lsCrt.Add(bean);
                //if (lst2.Contains(bean)) // pay attention!
                //    lsCrt.Add(bean);
            }

            return lsCrt;
        }

        private double getSimilarity(int i, int j)
        {
            NodeBean node_i = MyXml.findNodeById(i.ToString());
            NodeBean node_j = MyXml.findNodeById(j.ToString());

            List<NodeBean> lSt_i = getStOfNode(node_i); // St(i)
            List<NodeBean> lSt_j = getStOfNode(node_j); // St(j)
            List<NodeBean> lSt_ij = getCrossSt(lSt_i, lSt_j); // 获取St(i)和St(j)的交集

            double numerator = 0;
            // 分子
            foreach (NodeBean node in lSt_ij)
            {
                int count = getEdgesCount(node);
                if (count == 0)
                {
                    numerator = double.MaxValue;
                }
                else
                {
                    numerator += (1 / (double)(getEdgesCount(node)));
                }
            }

            // 分母
            double denominator = 0;
            double temp_1 = 0, temp_2 = 0;

            foreach (NodeBean node in lSt_i)
            {
                int c = (getEdgesCount(node));
                if(c == 0) temp_1 = double.MaxValue;
                else
                    temp_1 += (1 / (double) c);
            }

            foreach (NodeBean node in lSt_j)
            {
                int c = (getEdgesCount(node));
                if (c == 0) temp_2 = double.MaxValue;
                else
                    temp_2 += (1 / (double) c);
            }

            denominator = (Math.Sqrt(temp_1) * Math.Sqrt(temp_2));

            if (denominator.Equals(0)) return double.MaxValue;
            return numerator / denominator; 
        }

        public Matrix GetSimilarityMatrix()
        {
            Matrix mat = new Matrix(graph.Rows, graph.Cols);
            for (int i = 0; i < graph.Rows; i++ )
            {
                for (int j = 0; j < graph.Cols; j++)
                {
                    MNode node = mat.getMNode(i, j);
                    node.Value = getSimilarity(i, j);
                }
            }

            System.Console.WriteLine("****************  相似度矩阵   ***********************");
            mat.Print();
            System.Console.WriteLine("****************************************************");
            return mat;
        }

        /// <summary>
        ///  初始化matrix(取值为 0 或者 1)
        /// </summary>
        public void initialize()
        {
            int count = MyXml.getNodesCount();
            graph = new Matrix(count, count);
            for (int i = 0; i < graph.Rows; i++)
            {
                for (int j = 0; j < graph.Cols; j++)
                {
                    MNode node = graph.getMNode(i, j);
                    node.SName = i.ToString();
                    node.EName = j.ToString();

                    LinkBean bean = new LinkBean();     // 正向
                    bean.source = MyXml.findNodeById(node.SName);
                    bean.target = MyXml.findNodeById(node.EName);

                    LinkBean bean_2 = new LinkBean();   // 反向
                    bean_2.source = MyXml.findNodeById(node.EName);
                    bean_2.target = MyXml.findNodeById(node.SName);

                    foreach (LinkBean lb in MyXml.getAllLinks())
                    {
                        if (lb.Equals(bean) || lb.Equals(bean_2)) 
                        {
                            node.Value = 1;
                            break; 
                        }
                    }
                }
            }
        
            for (int i = 0; i < graph.Rows; i++){
                for (int j = 0; j < graph.Cols; j++)
                    if (i == j) {graph.getMNode(i, j).Value = 1; continue;}
            }
        }
    }
}