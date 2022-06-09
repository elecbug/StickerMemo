using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
                = this.ClientSize - new Size(this.main_text.Margin.Horizontal * 4, this.main_text.Margin.Vertical * 3 + this.menu.Size.Height);
            };
            this.FormClosed += (_sender, _e) =>
            {
                Properties.Settings.Default.MainMemo = this.main_text.Text;
                Properties.Settings.Default.FormSize = this.Size;
                Properties.Settings.Default.Save();
            };
            this.Size = Properties.Settings.Default.FormSize;

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
            this.menu.Items[2].Click += (_sender, _e) =>
            {
                bool top = this.TopMost;
                this.TopMost = false;

                SaveFileDialog save = new SaveFileDialog() { Filter = "text file(*.txt)|*.txt" };

                if (save.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter stream = new StreamWriter(new FileStream(save.FileName, FileMode.Create));
                    stream.Write(this.main_text.Text);
                    stream.Close();
                }

                this.TopMost = top;
            };

            this.main_text.DragDrop += (_sender, _e) =>
            {
                bool top = this.TopMost;
                this.TopMost = false;

                if (MessageBox.Show("You want to copy data?", "caption", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    this.main_text.Text = "";
                    SendKeys.Send("{DELETE}");

                    foreach (string path in (string[])_e.Data.GetData(DataFormats.FileDrop, true))
                    {
                        FileStream stream = new FileStream(path, FileMode.Open);
                        string str = new StreamReader(stream).ReadToEnd();

                        this.main_text.Text = str;

                        stream.Close();
                    }
                }
                else
                {
                    string str = this.main_text.Text;

                    this.main_text.Text = "";
                    SendKeys.Send("{DELETE}");

                    this.main_text.Text = str;
                }


                this.TopMost = top;
            };
            this.main_text.Text = Properties.Settings.Default.MainMemo;
            this.main_text.SelectionFont = this.main_text.Font;

            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer
            {
                Interval = 5000
            };
            t.Tick += (sender, e) =>
            {
                Properties.Settings.Default.MainMemo = this.main_text.Text;
                Properties.Settings.Default.FormSize = this.Size;
                Properties.Settings.Default.Save();
            };
            t.Start();
        }
    }
}
