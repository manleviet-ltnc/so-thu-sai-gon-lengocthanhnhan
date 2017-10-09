using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace So_thu_saigon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            lstDanhSach.Items.Add(lstThuMoi.SelectedItem);

        }

        private void ListBox_MouseDown(object sender, MouseEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            int index = lb.IndexFromPoint(e.X, e.Y);

            if (index != -1)
                lb.DoDragDrop(lb.Items[index].ToString(), DragDropEffects.Copy);
        }

        private void ListBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.Move;
        }
        bool isItemChange = false;
        private void lstDanhSach_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                bool test = false;
                for (int i = 0; i < lstDanhSach.Items.Count; i++)
                {
                    string st = lstDanhSach.Items[i].ToString();
                    string dt = e.Data.GetData(DataFormats.Text).ToString();
                    if (dt == st)
                        test = true;
                }
                if (test == false)
                {
                    int newindex = lstDanhSach.IndexFromPoint(lstDanhSach.PointToClient(new Point(e.X, e.Y)));
                    lstDanhSach.Items.Remove(e.Data.GetData(DataFormats.Text));
                    if (newindex != -1)
                        lstDanhSach.Items.Insert(newindex, e.Data.GetData(DataFormats.Text));
                    else
                    {
                        ListBox lb = (ListBox)sender;
                        lb.Items.Add(e.Data.GetData(DataFormats.Text));
                    }
                }
            }
        }

        bool isSaves = false;
        private void Save(object sender, EventArgs e)
        {
            StreamWriter write = new StreamWriter("danhsachthu.txt");

            if (write == null) return;

            foreach (var item in lstDanhSach.Items)
                write.WriteLine(item.ToString());

            write.Close();
            isSaves = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void mnuClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void mnuLoad_Click(object sender, EventArgs e)
        {
            StreamReader reader = new StreamReader("thumoi.txt");

            if (reader == null) return;

            string input;
            while ((input = reader.ReadLine()) != null)
            {
                lstThuMoi.Items.Add(input);
            }
            reader.Close();

            using (StreamReader rs = new StreamReader("danhsachthu.txt"))
            {
                input = null;
                while ((input = rs.ReadLine()) != null)
                {
                    lstDanhSach.Items.Add(input);
                }
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = string.Format("bay gio la {0}:{1}:{2} ngay {3} thang {4} nam {5}",
                                            DateTime.Now.Hour,
                                            DateTime.Now.Minute,
                                            DateTime.Now.Second,
                                            DateTime.Now.Day,
                                            DateTime.Now.Month,
                                            DateTime.Now.Year);

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }



        private void btnXoa_Click(object sender, EventArgs e)
        {
            lstDanhSach.Items.Remove(lstDanhSach.SelectedItem);
        }

        

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isSaves == false)
            {
                DialogResult kq = MessageBox.Show(" Ban co muon luu lai danh sach khong ?", "thong bao", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (kq == DialogResult.Yes)
                {
                    Save(sender, e);
                    e.Cancel = false;
                }

                else if (kq == DialogResult.No)
                    e.Cancel = false;
                else
                    e.Cancel = true;
            }
            else
                mnuClose_Click(sender, e);
        }
    }
 }
                
  

       
       
