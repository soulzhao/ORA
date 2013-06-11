using System;
using System.Collections.Generic;
using System.Text;
using XmlParser;
using System.Threading;

namespace AGNES
{
    class Ward
    {
        List<List<GroupBean>> lstC;

        Similarity simi; // 相似度的类

        public delegate void Groupclustered(int current_grp_num);  
        public event Groupclustered onGroupClustered;  // 对外的凝聚接口

        public delegate void GroupclusteredFinished();
        public event GroupclusteredFinished onGroupclusteredFinished;

        private AutoResetEvent PauseEvent { get; set; }  // 线程暂停设置
        private AutoResetEvent ResumeEvent { get; set; } // 线程恢复设置
        private AutoResetEvent StopEvent { get; set; }   // 线程停止设定

        public Ward()
        {
            lstC = new List<List<GroupBean>>();
            simi = new Similarity();

            PauseEvent = new AutoResetEvent(false);
            ResumeEvent = new AutoResetEvent(false);
            StopEvent = new AutoResetEvent(false);
        }

        private Matrix initializeDistance(Matrix src)
        {
            Matrix res = new Matrix(src);

            for (int i = 0; i < src.Rows; i++)
            {
                for (int j = 0; j < src.Cols; j++)
                {
                    res.getMNode(i, j).Value = 1 - Math.Sqrt(src.getMNode(i, j).Value);
                }
            }

            return res;
        }

        /// <summary>
        /// 获取下三角矩阵的最小值，并记录其位置
        /// </summary>
        /// <param name="mat"></param>
        /// <param name="r"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        private double getSubTriangleleMin(Matrix mat, ref int r, ref int c)
        {
            double min = double.MaxValue;
            for (int i = 0; i < mat.Rows; i++ )
            {
                for (int j = 0; j < i; j++ )
                {
                    if (mat.getMNode(i, j).Value < min)
                    {
                        r = i; c = j;
                        min = mat.getMNode(i, j).Value;
                    }
                }
            }
            return min;
        }

        public List<GroupBean> getPartionSolution(int m)
        {
            int index = lstC.Count - m;
            if (index < 0 || index > lstC.Count - 1) return null;
            return lstC[index];
        }

        public void Stop()
        {
            this.StopEvent.Set();
        }

        public void Resume()
        {
            this.ResumeEvent.Set();
        }

        public void Pause()
        {
            this.PauseEvent.Set();
        }

        public void calculateWard(object obj)
        {
            simi.initialize();
            Matrix smax = simi.GetSimilarityMatrix(); // 计算全矩阵的相似度
            Matrix D = initializeDistance(smax);
            initializeGroups();

            onGroupClustered(absoluteC());

            do
            {
                #region 线程控制操作
                if (!this.StopEvent.WaitOne(0)) // 如果Stop没有置位的话就进入条件判断语句
                {
                    if (this.PauseEvent.WaitOne(0)) // 如果Pause置位了,那么就进入条件等待Resume置位
                    {
                        this.ResumeEvent.WaitOne();
                    }
                }
                else
                {
                    break;
                }
                #endregion

                int i = 0, j = 0;
                double min = getSubTriangleleMin(D, ref i, ref j);

                for (int k = 0; k < absoluteC(); k++)
                {
                    if(k == i || k == j) continue;

                    int Ni = N(i), Nk = N(k), Nj = N(j);

                    double value =
                        (((Ni + Nk) * D.getMNode(i, k).Value) + ((Nj + Nk) * D.getMNode(j, k).Value) - (Nk * D.getMNode(i, j).Value))
                        / (Ni + Nj + Nk); 

                    if(k < i)
                        D.getMNode(i, k).Value = value;
                    else
                        D.getMNode(k, i).Value = value;
                }

                lstC.Add(newC(C(i), C(j)));

                D.reduceMatrixByIndex(j);

                // 触发外界的事件
                onGroupClustered(absoluteC());

            } while (absoluteC() > 1);

            onGroupclusteredFinished();
        }

        private void initializeGroups()
        {
            int count = 0;

            List<GroupBean> lgb = new List<GroupBean>();
            foreach(NodeBean bean in MyXml.getAllNodes())
            {
                GroupBean gb = new GroupBean();
                gb.id = count.ToString();
                gb.group_type = bean.group.group_type;
                gb.lchildern.Add(bean);

                lgb.Add(gb);
                count++;
            }

            lstC.Add(lgb);
        }

        private int N(int i)
        {
            return lstC[lstC.Count - 1][i].lchildern.Count;
        }

        /// <summary>
        /// 当前解集合中的解的个数
        /// </summary>
        /// <returns></returns>
        private int absoluteC()
        {
            return C().Count;
        }

        private List<GroupBean> C()
        {
            return lstC[lstC.Count - 1];
        }

        private GroupBean C(int i)
        {
            return lstC[lstC.Count - 1][i];
        }

        private GroupBean mergeGroup(GroupBean a, GroupBean b)
        {
            GroupBean newGroup = new GroupBean();
            newGroup.id = a.id + "+" + b.id;
            newGroup.lchildern.AddRange(a.lchildern);

            foreach(NodeBean nb in b.lchildern)
            {
                if(!a.lchildern.Contains(nb))
                    newGroup.lchildern.Add(nb);
                //NodeBean bean = a.lchildern.Find(delegate(NodeBean nodebean)
                //{
                //    return nodebean.id.Equals(nb.id);
                //});
                //if(bean == null)
                //    newGroup.lchildern.Add(nb);
            }
            return newGroup;
        }

        private List<GroupBean> removeGroup(List<GroupBean> lst, GroupBean group)
        {
            // 这边将消耗很多内存！！
            List<GroupBean> lgb = new List<GroupBean>();
            foreach(GroupBean grp in lst)
            {
                if (!GroupBean.Equals(group, grp))
                {
                    GroupBean g = new GroupBean();
                    g.id = grp.id;
                    g.lchildern = grp.lchildern;

                    lgb.Add(g);
                }
            }
            return lgb;
        }

        private int findLocation(List<GroupBean> lst, GroupBean group)
        {
            int count = 0;
            foreach (GroupBean grp in lst)
            {
                if (GroupBean.Equals(group, grp))
                {
                    break;
                }
                count++;
            }
            return count;
        }

        private List<GroupBean> newC(GroupBean Ci, GroupBean Cj)
        {
            // Not append it to the result set but replace the original one at the 
            // the position where it was!
            int index = findLocation(C(), Ci);
            List<GroupBean> newSolution = removeGroup(C(), Ci);
            newSolution.Insert(index, mergeGroup(Ci, Cj));
            newSolution = removeGroup(newSolution, Cj);

            return newSolution;
        }
    }
}
