using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StickerMemo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            this.Resize += (_sender, _e) =>
            {
                this.main_text.Size
                = this.ClientSize - new Size(this.main_text.Margin.Horizontal * 4, this.main_text.Margin.Vertical * 4 - this.menu.Size.Height);
            };

            this.FormClosed += (_sender, _e) =>
            {
                Properties.Settings.Default.MainMemo = this.main_text.Text;
                Properties.Settings.Default.Save();
            };

            this.StartPosition = FormStartPosition.CenterScreen;

            this.menu.Items[0].Click += (_sender, _e) =>
            {
                if (MessageBox.Show("You want to reset?", "caption", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    this.main_text.Text = "";
                }
            };

            this.menu.Items[1].Click += (_sender, _e) =>
            {
                this.TopMost = !this.TopMost;
            };

            this.main_text.Font = new Font("굴림", 11, FontStyle.Regular);
            this.main_text.SelectionFont = this.main_text.Font;

            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();

            t.Interval = 5000;

            t.Tick += (sender, e) =>
            {
                Properties.Settings.Default.MainMemo = this.main_text.Text;
                Properties.Settings.Default.Save();
            };

            t.Start();
        }

        private void LoadForm(object sender, EventArgs e)
        {
            this.main_text.Text = Properties.Settings.Default.MainMemo;
        }
    }
}
