using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
                = this.ClientSize - new Size(this.main_text.Margin.Horizontal * 4, this.main_text.Margin.Vertical * 5 - this.menu.Size.Height);
            };

            this.FormClosed += (_sender, _e) =>
            {
                Properties.Settings.Default.MainMemo = this.main_text.Text;
                Properties.Settings.Default.Save();
            };

            this.menu.Items[0].Click += (_sender, _e) =>
            {
                this.main_text.Text = "";
            };

            this.menu.Items[1].Click += (_sender, _e) =>
            {
                this.TopMost = !this.TopMost;
            };

            this.main_text.Font = new Font("굴림체", 11, FontStyle.Regular);
            this.main_text.SelectionFont = this.main_text.Font;
        }

        private void LoadForm(object sender, EventArgs e)
        {
            this.main_text.Text = Properties.Settings.Default.MainMemo;
        }
    }
}
