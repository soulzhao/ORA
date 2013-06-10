using System;
using System.Collections.Generic;
using System.Text;
using XmlParser;
using System.Threading;

namespace AGNES
{
    public class Agnes
    {
        Ward ward;

        public delegate void Groupclustered(int current_grp_num);
        public event Groupclustered onGroupClustered;  // 对外的凝聚接口

        public delegate void GroupclusteredFinished();
        public event GroupclusteredFinished onGroupclusteredFinished;

        public Agnes()
        {
            ward = new Ward();
            ward.onGroupClustered +=new Ward.Groupclustered(ward_onGroupClustered);
            ward.onGroupclusteredFinished +=new Ward.GroupclusteredFinished(ward_onGroupclusteredFinished);
        }

        private void ward_onGroupClustered(int grp_count)
        {
            onGroupClustered(grp_count);
        }

        private void ward_onGroupclusteredFinished() { onGroupclusteredFinished(); }

        public void StartCalculating()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(ward.calculateWard));
        }

        public void StopCalculating() { ward.Stop(); }
        public void ResumeCalculating() { ward.Resume(); }
        public void PauseCalculating() { ward.Resume(); }

        public List<GroupBean> getPartionSolution(int m)
        {
            return ward.getPartionSolution(m);
        }
    }
}
