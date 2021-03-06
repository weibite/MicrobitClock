﻿using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace Microbit.Clock
{
    public partial class EditForm : Form
    {
        public Events Event { get; set; }

        public EditForm()
        {
            InitializeComponent();
            this.lblMusicPath.Text = string.Empty;
            this.cboxCanPlay.Checked = true;
            Event = new Clock.Events();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.txtTitle.Text = "";
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
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
            MainForm mainForm = (MainForm)this.Owner;

            if (this.Event.Index == -1)
            {
                //新增
                XmlElement nodeEvent = xmlDoc.CreateElement("event");
                string key = Guid.NewGuid().ToString().ToLower();
                nodeEvent.SetAttribute("key", key);
                XmlElement nodeTitle = xmlDoc.CreateElement("title");
                XmlElement nodeEventTime = xmlDoc.CreateElement("eventTime");
                XmlElement nodeMusic = xmlDoc.CreateElement("Music");
                nodeTitle.InnerText = this.txtTitle.Text.Trim();
                nodeEventTime.InnerText = this.dtpEventTime.Text;
                nodeMusic.InnerText = this.lblMusicPath.Text;
                nodeEvent.AppendChild(nodeTitle);
                nodeEvent.AppendChild(nodeEventTime);
                nodeEvent.AppendChild(nodeMusic);
                nodeRoot.AppendChild(nodeEvent);

                //插入到ListView中
                ListViewItem lv = new ListViewItem();
                lv.SubItems.Add(key);
                lv.SubItems.Add((mainForm.listView1.Items.Count + 1).ToString());
                lv.SubItems.Add(this.txtTitle.Text.Trim());
                lv.SubItems.Add(this.dtpEventTime.Text);
                lv.SubItems.Add(this.lblMusicPath.Text);
                mainForm.listView1.Items.Add(lv);

                mainForm.EventList.Add(new Events
                {
                    Key = key,
                    Title = this.txtTitle.Text.Trim(),
                    EventTime = this.dtpEventTime.Text,
                    Music = this.lblMusicPath.Text
                });
            }
            else
            {
                //编辑
                XmlNode nodeEvent = nodeRoot.SelectSingleNode("event[@key='" + this.Event .Key+ "']");
                XmlNode nodeTitle = nodeEvent.SelectSingleNode("title");
                nodeTitle.InnerText = this.txtTitle.Text.Trim();
                XmlNode nodeEventTime = nodeEvent.SelectSingleNode("eventTime");
                nodeEventTime.InnerText = this.dtpEventTime.Text;
                XmlNode nodeMusic = nodeEvent.SelectSingleNode("Music");
                nodeMusic.InnerText = this.lblMusicPath.Text;

                //更新到ListView中
                mainForm.listView1.Items[this.Event.Index].SubItems[3].Text = this.txtTitle.Text.Trim();
                mainForm.listView1.Items[this.Event.Index].SubItems[4].Text = this.dtpEventTime.Text;
                mainForm.listView1.Items[this.Event.Index].SubItems[5].Text = this.lblMusicPath.Text;

                var evt = mainForm.EventList.FirstOrDefault(x => x.Key == this.Event.Key);
                if(evt != null)
                {
                    evt.Title = this.txtTitle.Text.Trim();
                    evt.EventTime = this.dtpEventTime.Text;
                    evt.Music = this.lblMusicPath.Text;
                }
            }
            xmlDoc.Save(xmlFilePath);
            btnClose_Click(sender, e);
        }

        public void ShowAddForm(IWin32Window owner)
        {
            this.txtTitle.Text = string.Empty;
            this.dtpEventTime.ResetText();
            this.openFileDialog1.Reset();
            this.lblMusicPath.Text = string.Empty;
            this.ShowDialog(owner);
        }

        public void ShowEditForm(IWin32Window owner)
        {
            LoadDetail();
            this.ShowDialog(owner);
        }

        public void LoadDetail()
        {
            this.txtTitle.Text = this.Event.Title;
            this.dtpEventTime.Text = this.Event.EventTime;
            this.lblMusicPath.Text = this.Event.Music;
        }

        private void btnSelectMusic_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "C:\\";//初始加载路径为C盘；
            openFileDialog1.Filter = "音乐文件 (*.mp3)|*.mp3|All files (*.*)|*.*";//过滤你想设置的文本文件类型（mp3）
            // openFileDialog1.Filter = "文本文件 (*.mp3)|*.mp3|All files (*.*)|*.*";（这是全部类型文件）
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                lblMusicPath.Text = Path.GetFullPath(openFileDialog1.FileName);//显示文件的名字
            }
        }
    }
}
