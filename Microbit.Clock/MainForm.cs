using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace Microbit.Clock
{
    public partial class MainForm : Form
    {
        public List<Events> EventList = new List<Events>();
        private EditForm EditForm = new EditForm();
        private bool isChecked = false;

        public MainForm()
        {
            InitializeComponent();

            InitTimer();

            InitColumn();

            InitListViewStyle();

            try
            {
                BindData();
                this.ShowInTaskbar = false;
                this.WindowState = FormWindowState.Minimized;

                this.notifyIcon1.ShowBalloonTip(50, "提示信息", "微比特电子闹钟已启动", ToolTipIcon.Info);
            }
            catch (Exception ex)
            {
                this.notifyIcon1.ShowBalloonTip(50, "异常信息", ex.Message, ToolTipIcon.Info);
            }
            CreateShortCut();
        }


        /// <summary>
        /// 初始化定时器
        /// </summary>
        void InitTimer()
        {
            timer1.Enabled = true;
            timer1.Interval = 1000;
            timer1.Start();
        }

        /// <summary>
        /// 设置ListView的列头
        /// </summary>
        void InitColumn()
        {
            this.listView1.View = View.Details;
            this.listView1.Columns.Add("", 20, HorizontalAlignment.Left);
            this.listView1.Columns.Add("编号", 0, HorizontalAlignment.Left);
            this.listView1.Columns.Add("序号", 40, HorizontalAlignment.Left);
            this.listView1.Columns.Add("标题", 250, HorizontalAlignment.Left);
            this.listView1.Columns.Add("提醒时间", 150, HorizontalAlignment.Left);
            this.listView1.Columns.Add("播放音乐", 200, HorizontalAlignment.Left);
        }

        /// <summary>
        /// 设置ListVIew样式
        /// </summary>
        void InitListViewStyle()
        {
            // 设置行高
            ImageList imgList = new ImageList();
            // 分别是宽和高
            imgList.ImageSize = new Size(1, 30);
            // 这里设置listView的SmallImageList ,用imgList将其撑大
            listView1.SmallImageList = imgList;

            //更改listView属性
            this.listView1.GridLines = true;//显示表格线
            this.listView1.View = View.Details;//显示表格细节
            this.listView1.LabelEdit = true;//是否可编辑，ListView只可编辑第一列
            this.listView1.Scrollable = true;//有滚动条
            this.listView1.HeaderStyle = ColumnHeaderStyle.Clickable;//对表头进行设置
            this.listView1.FullRowSelect = true;//是否可以选择行
            this.listView1.HideSelection = false;
        }

        /// <summary>
        /// 给ListView绑定数据
        /// </summary>
        void BindData()
        {
            this.listView1.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度

            LoadXml();

            for (int i = 0; i < EventList.Count; i++)   //添加10行数据  
            {
                ListViewItem lv = new ListViewItem();
                lv.SubItems.Add(EventList[i].Key);
                lv.SubItems.Add((i + 1).ToString());
                lv.SubItems.Add(EventList[i].Title);
                lv.SubItems.Add(EventList[i].EventTime);
                lv.SubItems.Add(EventList[i].Music);
                this.listView1.Items.Add(lv);
            }

            this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。
        }

        /// <summary>
        /// 加载db
        /// </summary>
        /// <returns></returns>
        List<Events> LoadXml()
        {
            //创建Xml文件对象，加载xml文件
            string xmlFilePath = "db.xml";
            XmlDocument xmlDoc = new XmlDocument();
            if (System.IO.File.Exists(xmlFilePath))//判断文件是否存在
            {
                //首先读取硬盘里的db.xml文件
                xmlDoc.Load(xmlFilePath);
            }
            else
            {
                //如果不存在 则从嵌入资源内读取 db.xml 
                Assembly asm = Assembly.GetExecutingAssembly();//读取嵌入式资源
                Stream sm = asm.GetManifestResourceStream("Microbit.Clock.db.xml");
                xmlDoc.Load(sm);
            }

            //获取根节点
            XmlElement nodeRoot = xmlDoc.DocumentElement;

            //遍历子节点    
            XmlNodeList list = nodeRoot.ChildNodes;
            foreach (XmlNode node in list)
            {
                string key = ((XmlElement)node).GetAttribute("key");
                string title = node["title"].InnerText;
                string eventTime = node["eventTime"].InnerText;
                string music = node["Music"].InnerText;
                EventList.Add(new Events
                {
                    Key = key,
                    Title = title,
                    EventTime = eventTime,
                    Music = music
                });
            }

            return EventList;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddEvent_Click(object sender, EventArgs e)
        {
            EditForm.Event = new Clock.Events();
            EditForm.ShowAddForm(this);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditEvent_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            EditForm.Event.Index = listView1.SelectedItems[0].Index;
            EditForm.Event.Key = listView1.SelectedItems[0].SubItems[1].Text;
            EditForm.Event.Title = listView1.SelectedItems[0].SubItems[3].Text;
            EditForm.Event.EventTime = listView1.SelectedItems[0].SubItems[4].Text;
            EditForm.Event.Music = listView1.SelectedItems[0].SubItems[5].Text;
            EditForm.ShowEditForm(this);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelEvent_Click(object sender, EventArgs e)
        {
            if (listView1.CheckedItems.Count == 0) return;
            var list = listView1.CheckedItems;
            //创建Xml文件对象，加载xml文件
            string xmlFilePath = "db.xml";
            XmlDocument xmlDoc = new XmlDocument();
            if (System.IO.File.Exists(xmlFilePath))//判断文件是否存在
            {
                //首先读取硬盘里的db.xml文件
                xmlDoc.Load(xmlFilePath);
            }
            else
            {
                //如果不存在 则从嵌入资源内读取 db.xml 
                Assembly asm = Assembly.GetExecutingAssembly();//读取嵌入式资源
                Stream sm = asm.GetManifestResourceStream("Microbit.Clock.db.xml");
                xmlDoc.Load(sm);
            }

            //获取根节点
            XmlElement nodeRoot = xmlDoc.DocumentElement;
            foreach (ListViewItem lv in list)
            {
                string key = lv.SubItems[1].Text;
                XmlNode node = nodeRoot.SelectSingleNode("event[@key='" + key + "']");
                nodeRoot.RemoveChild(node);

                listView1.Items.Remove(lv);

                var evt = EventList.FirstOrDefault(x => x.Key == key);
                if(evt != null)
                {
                    EventList.Remove(evt);
                }
            }
            xmlDoc.Save(xmlFilePath);
        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            isChecked = !isChecked;
            foreach (ListViewItem item in listView1.Items)
            {
                item.Checked = isChecked;
            }
        }

        /// <summary>
        /// 列表项双击事件，弹出编辑窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnEditEvent_Click(sender, e);
        }

        /// <summary>
        /// 定时器监听事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            string currTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (EventList.Any(x => x.EventTime + ":00" == currTime))
            {
                var list = EventList.Where(x => x.EventTime + ":00" == currTime).ToList();
                foreach (var item in list)
                {
                    new AlarmForm(item.Title, item.Music).ShowDialog(this);
                }
            }
        }

        /// <summary>
        /// 关闭窗口最小化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }

        /// <summary>
        /// 状态栏图标菜单显示按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showItem_Click(object sender, EventArgs e)
        {
            Display();
        }

        /// <summary>
        /// 状态栏菜单退出按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeItem_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Dispose();
            notifyIcon1.Dispose();
            this.Close();
            this.Dispose();
            Application.Exit();
        }

        /// <summary>
        /// 状态栏图标双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Display();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Visible = false;
            }
        }

        /// <summary>
        /// 显示主窗口
        /// </summary>
        private void Display()
        {
            //this.Visible = !this.Visible;
            this.Visible = true;
            this.TopMost = true;
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        /// <summary>
        /// 创建快捷方式到启动目录
        /// </summary>
        public void CreateShortCut()
        {
            string StartupPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Startup);//得到启动文件夹路径
            string shortcutPath = StartupPath + "\\微比特电子闹钟.lnk";
            if (System.IO.File.Exists(shortcutPath))
            {
                return;
            }
            WshShell shell = new WshShell();
            IWshShortcut shortcut = shell.CreateShortcut(shortcutPath) as IWshShortcut;
            shortcut.TargetPath = System.Windows.Forms.Application.ExecutablePath;
            shortcut.Arguments = "";// 参数
            shortcut.Description = "微比特电子闹钟快捷方式";
            shortcut.WorkingDirectory = System.IO.Directory.GetCurrentDirectory();//程序所在文件夹，在快捷方式图标点击右键可以看到此属性
            shortcut.Hotkey = "CTRL+SHIFT+Z";//热键
            shortcut.WindowStyle = 1;
            shortcut.Save();
        }
    }

    /// <summary>
    /// 提醒事件实体类
    /// </summary>
    public class Events
    {
        public int Index { get; set; }
        public string Key { get; set; }
        public string Title { get; set; }
        public string EventTime { get; set; }
        public string Music { get; set; }

        public Events()
        {
            Index = -1;
        }
    }
}
