using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Microbit.Clock
{
    public partial class AlarmForm : Form
    {
        public AlarmForm(string title)
        {
            InitializeComponent();

            this.label1.Text = title;
        }
    }
}
