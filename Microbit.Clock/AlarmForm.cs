using System.IO;
using System.Media;
using System.Windows.Forms;
using System;
using System.Runtime.InteropServices;

namespace Microbit.Clock
{
    public partial class AlarmForm : Form
    {
        private string _filePath = "";
        public AlarmForm(string title, string filePath)
        {
            InitializeComponent();

            this.label1.Text = title;
            _filePath = filePath;
            PlayMusic();
        }

        private void PlayMusic()
        {
            if(string.IsNullOrWhiteSpace(_filePath) || !File.Exists(_filePath))
            {
                return;
            }
            this.axWindowsMediaPlayer1.settings.playCount = 1;//播放次数；
            axWindowsMediaPlayer1.URL = _filePath;
            axWindowsMediaPlayer1.Ctlcontrols.play();//播放文件
        }

        private void AlarmForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
            axWindowsMediaPlayer1.Dispose();
        }
    }
}