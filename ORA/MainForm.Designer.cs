namespace ORA
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView = new System.Windows.Forms.TreeView();
            this.myPictureBox1 = new MyPictureBox.MyPictureBox();
            this.Start_btn = new System.Windows.Forms.Button();
            this.Pause_btn = new System.Windows.Forms.Button();
            this.Stop_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_GroupNum = new System.Windows.Forms.ComboBox();
            this.openFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDlg = new System.Windows.Forms.SaveFileDialog();
            this.Clear_btn = new System.Windows.Forms.Button();
            this.lbl_Calculating = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(724, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.导入ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 导入ToolStripMenuItem
            // 
            this.导入ToolStripMenuItem.Name = "导入ToolStripMenuItem";
            this.导入ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.导入ToolStripMenuItem.Text = "导入";
            this.导入ToolStripMenuItem.Click += new System.EventHandler(this.导入ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.myPictureBox1);
            this.splitContainer1.Size = new System.Drawing.Size(724, 335);
            this.splitContainer1.SplitterDistance = 204;
            this.splitContainer1.TabIndex = 1;
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(204, 335);
            this.treeView.TabIndex = 0;
            this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
            // 
            // myPictureBox1
            // 
            this.myPictureBox1.BackColor = System.Drawing.Color.Black;
            this.myPictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myPictureBox1.Img = null;
            this.myPictureBox1.Location = new System.Drawing.Point(0, 0);
            this.myPictureBox1.Name = "myPictureBox1";
            this.myPictureBox1.rRate = 0F;
            this.myPictureBox1.Size = new System.Drawing.Size(516, 335);
            this.myPictureBox1.TabIndex = 0;
            this.myPictureBox1.zRate = 0F;
            // 
            // Start_btn
            // 
            this.Start_btn.Location = new System.Drawing.Point(212, 375);
            this.Start_btn.Name = "Start_btn";
            this.Start_btn.Size = new System.Drawing.Size(64, 23);
            this.Start_btn.TabIndex = 2;
            this.Start_btn.Text = "开始";
            this.Start_btn.UseVisualStyleBackColor = true;
            this.Start_btn.Click += new System.EventHandler(this.Start_btn_Click);
            // 
            // Pause_btn
            // 
            this.Pause_btn.Location = new System.Drawing.Point(289, 375);
            this.Pause_btn.Name = "Pause_btn";
            this.Pause_btn.Size = new System.Drawing.Size(60, 23);
            this.Pause_btn.TabIndex = 3;
            this.Pause_btn.Text = "暂停";
            this.Pause_btn.UseVisualStyleBackColor = true;
            this.Pause_btn.Click += new System.EventHandler(this.Pause_btn_Click);
            // 
            // Stop_btn
            // 
            this.Stop_btn.Location = new System.Drawing.Point(368, 375);
            this.Stop_btn.Name = "Stop_btn";
            this.Stop_btn.Size = new System.Drawing.Size(62, 23);
            this.Stop_btn.TabIndex = 4;
            this.Stop_btn.Text = "结束";
            this.Stop_btn.UseVisualStyleBackColor = true;
            this.Stop_btn.Click += new System.EventHandler(this.Stop_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 378);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "选择划分社区数目：";
            // 
            // comboBox_GroupNum
            // 
            this.comboBox_GroupNum.FormattingEnabled = true;
            this.comboBox_GroupNum.Location = new System.Drawing.Point(133, 375);
            this.comboBox_GroupNum.Name = "comboBox_GroupNum";
            this.comboBox_GroupNum.Size = new System.Drawing.Size(71, 20);
            this.comboBox_GroupNum.TabIndex = 6;
            this.comboBox_GroupNum.SelectedIndexChanged += new System.EventHandler(this.comboBox_GroupNum_SelectedIndexChanged);
            // 
            // openFileDlg
            // 
            this.openFileDlg.FileName = "openFileDialog";
            // 
            // Clear_btn
            // 
            this.Clear_btn.Location = new System.Drawing.Point(660, 375);
            this.Clear_btn.Name = "Clear_btn";
            this.Clear_btn.Size = new System.Drawing.Size(52, 23);
            this.Clear_btn.TabIndex = 7;
            this.Clear_btn.Text = "清图";
            this.Clear_btn.UseVisualStyleBackColor = true;
            this.Clear_btn.Click += new System.EventHandler(this.Clear_btn_Click);
            // 
            // lbl_Calculating
            // 
            this.lbl_Calculating.AutoSize = true;
            this.lbl_Calculating.Location = new System.Drawing.Point(450, 380);
            this.lbl_Calculating.Name = "lbl_Calculating";
            this.lbl_Calculating.Size = new System.Drawing.Size(0, 12);
            this.lbl_Calculating.TabIndex = 8;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 410);
            this.Controls.Add(this.lbl_Calculating);
            this.Controls.Add(this.Clear_btn);
            this.Controls.Add(this.comboBox_GroupNum);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Stop_btn);
            this.Controls.Add(this.Pause_btn);
            this.Controls.Add(this.Start_btn);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "社区划分模拟";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView;
        private MyPictureBox.MyPictureBox myPictureBox1;
        private System.Windows.Forms.Button Start_btn;
        private System.Windows.Forms.Button Pause_btn;
        private System.Windows.Forms.Button Stop_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_GroupNum;
        private System.Windows.Forms.OpenFileDialog openFileDlg;
        private System.Windows.Forms.SaveFileDialog saveFileDlg;
        private System.Windows.Forms.Button Clear_btn;
        private System.Windows.Forms.Label lbl_Calculating;

    }
}

