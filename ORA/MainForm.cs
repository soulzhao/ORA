using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using XmlParser;
using AGNES;
using System.Threading;
using ORA.draw;
using ORA.Utils;

namespace ORA
{
    public partial class MainForm : Form
    {
        MyXml xml; // xml文件处理类

        Image img; //当前的地图对象
        Drawer drawer = null;

        Agnes agnes = null;

        const string lbl_text = "计算信息:";

        private bool hasCalculated = false;

        public MainForm()
        {
            InitializeComponent();

            myPictureBox1.MyPaint = SimulatePaint;
            myPictureBox1.rRate = 0.9f;
            myPictureBox1.zRate = 1.1f;

            agnes = new Agnes();
            agnes.onGroupClustered +=new Agnes.Groupclustered(Delegate_agnes_onGroupClustered);
            agnes.onGroupclusteredFinished +=new Agnes.GroupclusteredFinished(agnes_onGroupclusteredFinished);
        }

        private void 导入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 导入XML文件
            importFile(FileType.XML, new importOrExportFile(delegate()
            {
                string path = openFileDlg.FileName;
                if (File.Exists(path))
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(initializeXml), path);
                }
            }));
            hasCalculated = false;
        }

        private delegate void DelegateMethod();

        private void drawGraph(Object obj)
        {
            Delegate_DelegateMethod(new MethodInvoker(drawMyXmlGraph));
        }

        private void drawMyXmlGraph()
        {
            SimulatePaint(MyXml.getAllNodes(), MyXml.getAllLinks());
        }

        private void SimulatePaint(List<NodeBean> lnb, List<LinkBean> llb)
        {
            if (drawer == null)
            {
                if (img != null) img.Dispose();
                img = new Bitmap(myPictureBox1.Width - 5, myPictureBox1.Height - 5);
                Graphics grPic = Graphics.FromImage(img);
                drawer = new Drawer(grPic, myPictureBox1.Width - 10, myPictureBox1.Height - 10);
            }

            if (drawer != null)
            {
                drawer.ClearSimuMap();
                drawer.DrawSimuMap(lnb, llb);
            }
            myPictureBox1.Img = img;
        }

        private void SimulatePaint(int w, int h)
        {

            if (img != null) img.Dispose();
            img = new Bitmap(w - 5, h - 5);
            Graphics grPic = Graphics.FromImage(img);
            drawer = new Drawer(grPic, w - 10, h - 10);

            drawer.ClearSimuMap();
            drawer.DrawSimuMap(null, null);

            myPictureBox1.Img = img;
        }

        private void SimulatePaint()
        {
            if (drawer == null)
            {
                if (img != null) img.Dispose();
                img = new Bitmap(myPictureBox1.Width - 5, myPictureBox1.Height - 5);
                Graphics grPic = Graphics.FromImage(img);
                drawer = new Drawer(grPic, myPictureBox1.Width - 10, myPictureBox1.Height - 10);
            }

            if (drawer != null)
            {
                drawer.ClearSimuMap();
                drawer.DrawSimuMap(null, null);
            }
            myPictureBox1.Img = img;
        }

        private void initializeXml(Object path)
        {
            xml = new MyXml(path.ToString());
            Delegate_DelegateMethod(new MethodInvoker(MainForm_AddTreeNodes));
            ThreadPool.QueueUserWorkItem(new WaitCallback(drawGraph));
        }

        private void MainForm_AddTreeNodes()
        {
            AddTreeNodes(MyXml.groups);
        }

        private void AddTreeNodes(List<GroupBean> lgb)
        {
            treeView.Nodes.Clear();
            foreach (GroupBean gb in lgb)
            {
                TreeNode root = treeView.Nodes.Add("Group : " + gb.id);

                foreach (NodeBean nb in gb.lchildern)
                {
                    root.Nodes.Add(nb.id + " : " + nb.attributes["name"].ToString());
                }
            }
        }

        private void Delegate_DelegateMethod(MethodInvoker mi)
        {
            try
            {
                this.BeginInvoke(new DelegateMethod(mi));
            }
            catch
            {
                System.Threading.Thread.CurrentThread.Abort();
            };
        }

        private bool isFileExisted(String filename)
        {
            return File.Exists(filename);
        }

        private delegate void importOrExportFile();
        
        
        // 文件类型类
        private enum FileType
        {
            XML
        }

        private string getFileType(FileType type)
        {
            string fileFilter = "";
            switch (type)
            {
                case FileType.XML:
                    fileFilter = "XML文件(*.xml)|*.xml";
                    break;
            }
            return fileFilter;
        }

        /// <summary>
        /// 导入文件的方法
        /// </summary>
        /// <param name="type"></param>
        /// <param name="method"></param>
        private void importFile(FileType type, importOrExportFile method)
        {
            //“文本文件(*.txt)|*.txt|所有文件(*.*)|*.*”
            this.openFileDlg.Filter = getFileType(type);
            this.openFileDlg.FileName = "";
            this.openFileDlg.ShowDialog();

            if (!String.IsNullOrEmpty(this.openFileDlg.FileName))
            {
                if (isFileExisted(this.openFileDlg.FileName))
                {
                    method();
                }
                else
                {
                    MessageBox.Show("导入文件不存在！！");
                }
            }
        }

        private void 导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exportFile(FileType.XML, new importOrExportFile(delegate()
            {
                //Stream file = File.Create(this.saveFileDlg.FileName);

                //file.Close();
            }));
        }

        /// <summary>
        /// 导出文件的方法
        /// </summary>
        /// <param name="type">文件的类型</param>
        /// <param name="write"></param>
        private void exportFile(FileType type, importOrExportFile method)
        {
            this.saveFileDlg.Filter = getFileType(type);
            this.saveFileDlg.FileName = "";
            this.saveFileDlg.ShowDialog();
            if (String.IsNullOrEmpty(this.saveFileDlg.FileName)) return;

            if (!isFileExisted(this.saveFileDlg.FileName))
            {
                DialogResult result = MessageBox.Show("确定保存文件" + saveFileDlg.FileName + "?", "保存确认框", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                    method();
            }
            else
                MessageBox.Show("同名文件已经存在！");
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// 鼠标单击节点时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            SimulatePaint(); // 
            TreeNode node = e.Node;
            if (node.Level == 0)
            {
                // 为Group
                string[] elements = node.Text.Split(new char[] { ':' });
                string id = elements[1].Trim();

                GroupBean gbean = null;
                if (!hasCalculated)
                {
                    gbean = MyXml.findGroupById(id);
                }
                else
                {
                    try
                    {
                        int grp_count = int.Parse(comboBox_GroupNum.SelectedItem.ToString());
                        List<GroupBean> lgb = agnes.getPartionSolution(grp_count);
                        gbean = lgb.Find(delegate(GroupBean gb)
                        {
                            return gb.id.Equals(id);
                        });
                    }
                    catch
                    {
                        MessageBox.Show("请先选择分区数目", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }


                drawer.DrawGroup(gbean);
            }
            else
            {
                string[] elements = node.Text.Split(new char[] { ':' });
                string id = elements[0].Trim();
                NodeBean nbean = MyXml.findNodeById(id);
                drawer.DrawPointer(nbean);
            }
            myPictureBox1.UpdateImg();
        }

        /// <summary>
        /// 开始计算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Start_btn_Click(object sender, EventArgs e)
        {
            setBtnEnable(false, true, true);
            agnes.StartCalculating();
            hasCalculated = true;
        }

        /// <summary>
        /// 暂停计算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pause_btn_Click(object sender, EventArgs e)
        {
            setBtnEnable(true, false, true);
            agnes.PauseCalculating();
        }

        /// <summary>
        /// 终止计算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Stop_btn_Click(object sender, EventArgs e)
        {
            setBtnEnable(true, false, false);
            agnes.StopCalculating();
        }

        private void Clear_btn_Click(object sender, EventArgs e)
        {
            Delegate_DelegateMethod(new MethodInvoker(SimulatePaint));
        }

        private void comboBox_GroupNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            int grp_count = int.Parse(comboBox_GroupNum.SelectedItem.ToString());
            List<GroupBean> lgb = agnes.getPartionSolution(grp_count);
            AddTreeNodes(lgb); // 加上了nodes Tree

            // 按组别进行绘图
            //ThreadPool.QueueUserWorkItem(new WaitCallback(Delegate_DrawGroupGraph), lgb);
        }

        private delegate void DelegateDrawGraph(List<GroupBean> lgb);

        private void Delegate_DrawGroupGraph(object obj)
        {
            List<GroupBean> lgb = (List<GroupBean>)obj;
            try
            {
                this.BeginInvoke(new DelegateDrawGraph(DrawGroupGraph), lgb);
            }
            catch
            {
                System.Threading.Thread.CurrentThread.Abort();
            };
        }

        private void DrawGroupGraph(List<GroupBean> lgb)
        {
            //List<NodeBean>
            //foreach(GroupBean gb in lgb)
            //{
            //    gb.lchildern 
            //}
            //SimulatePaint();
        }


        private delegate void DelegateGroupMethod(int count);
        private void Delegate_agnes_onGroupClustered(int count)
        {
            try
            {
                this.BeginInvoke(new DelegateGroupMethod(agnes_onGroupClustered), count);
            }
            catch
            {
                System.Threading.Thread.CurrentThread.Abort();
            };
        }

        private void agnes_onGroupClustered(int count)
        {
            lbl_Calculating.Text = lbl_text + "当前已经凝聚至" + count + "个社区";
            this.comboBox_GroupNum.Items.Add(count.ToString());
        }

        private void agnes_onGroupclusteredFinished()
        {
            Delegate_DelegateMethod(new MethodInvoker(GroupclusteredFinished));
        }

        private void GroupclusteredFinished()
        {
            setBtnEnable(true, false, false);
            lbl_Calculating.Text = lbl_text + "当前已经凝聚算法已结束！";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            setBtnEnable(true, false, false);
        }

        private void setBtnEnable(bool start, bool pause, bool stop)
        {
            this.Start_btn.Enabled = start;
            this.Pause_btn.Enabled = pause;
            this.Stop_btn.Enabled = stop;
        }
    }
}
