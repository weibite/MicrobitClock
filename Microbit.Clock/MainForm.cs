using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

            BindData();
        }

        void InitTimer()
        {
            timer1.Enabled = true;
            timer1.Interval = 1000;
            timer1.Start();
        }

        void InitColumn()
        {
            this.listView1.View = View.Details;
            this.listView1.Columns.Add("", 20, HorizontalAlignment.Left);
            this.listView1.Columns.Add("编号", 0, HorizontalAlignment.Left);
            this.listView1.Columns.Add("序号", 40, HorizontalAlignment.Left);
            this.listView1.Columns.Add("标题", 450, HorizontalAlignment.Left);
            this.listView1.Columns.Add("提醒时间", 150, HorizontalAlignment.Left);
        }

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
                this.listView1.Items.Add(lv);
            }

            this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。
        }

        List<Events> LoadXml()
        {
            //创建Xml文件对象，加载xml文件
            string xmlFilePath = "db.xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);

            //获取根节点
            XmlElement nodeRoot = xmlDoc.DocumentElement;

            //遍历子节点    
            XmlNodeList list = nodeRoot.ChildNodes;
            foreach (XmlNode node in list)
            {
                string key = ((XmlElement)node).GetAttribute("key");
                string title = node["title"].InnerText;
                string eventTime = node["eventTime"].InnerText;
                EventList.Add(new Events
                {
                    Key = key,
                    Title = title,
                    EventTime = eventTime
                });
            }

            return EventList;
        }

        private void btnAddEvent_Click(object sender, EventArgs e)
        {
            EditForm.Event = new Clock.Events();
            EditForm.ShowAddForm(this);
        }

        private void btnEditEvent_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            EditForm.Event.Index = listView1.SelectedItems[0].Index;
            EditForm.Event.Key = listView1.SelectedItems[0].SubItems[1].Text;
            EditForm.Event.Title = listView1.SelectedItems[0].SubItems[3].Text;
            EditForm.Event.EventTime = listView1.SelectedItems[0].SubItems[4].Text;
            EditForm.ShowEditForm(this);
        }

        private void btnDelEvent_Click(object sender, EventArgs e)
        {
            if (listView1.CheckedItems.Count == 0) return;
            var list = listView1.CheckedItems;
            //创建Xml文件对象，加载xml文件
            string xmlFilePath = "db.xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);

            //获取根节点
            XmlElement nodeRoot = xmlDoc.DocumentElement;
            foreach (ListViewItem lv in list)
            {
                string key = lv.SubItems[1].Text;
                XmlNode node = nodeRoot.SelectSingleNode("event[@key='" + key + "']");
                nodeRoot.RemoveChild(node);

                listView1.Items.Remove(lv);
            }
            xmlDoc.Save(xmlFilePath);
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            isChecked = !isChecked;
            foreach (ListViewItem item in listView1.Items)
            {
                item.Checked = isChecked;
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnEditEvent_Click(sender, e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string currTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if(EventList.Any(x=>x.EventTime + ":00" == currTime))
            {
                var list = EventList.Where(x => x.EventTime + ":00" == currTime).ToList();
                foreach(var item in list)
                {
                    new AlarmForm(item.Title).ShowDialog(this);
                }
            }
        }
    }

    public class Events
    {
        public int Index { get; set; }
        public string Key { get; set; }
        public string Title { get; set; }
        public string EventTime { get; set; }

        public Events()
        {
            Index = -1;
        }
    }
}
