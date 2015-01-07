using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculatorTip
{
    public partial class Form1 : Form
    {
        private double tip = 10;
        private double totalBill;
        private double[] tipList = { 1, 2, 3, 1.5, 1.7, 1.3, 2.5 };
        private bool[] flag = new bool[7];

        public Form1()
        {
            InitializeComponent();


            //regester radio button event handler
            rb3Star.CheckedChanged += new EventHandler(radioButtons_CheckedChanged);
            rb4Star.CheckedChanged += new EventHandler(radioButtons_CheckedChanged);
            rb5Star.CheckedChanged += new EventHandler(radioButtons_CheckedChanged);

            //regester CheckBox event Handler
            cbClecn.CheckStateChanged += new EventHandler(checkBox_CheckedChanged);
            cbService.CheckStateChanged += new EventHandler(checkBox_CheckedChanged);
            cbLike.CheckStateChanged += new EventHandler(checkBox_CheckedChanged);
            cbAtmo.CheckStateChanged += new EventHandler(checkBox_CheckedChanged);
        }

        private void tbBill_TextChanged(object sender, EventArgs e)
        {
            update();

        }

        private void update()
        {
            tip = 10;
            for (int i = 0; i < flag.Length; i++)
            {
                if (flag[i])
                {
                    tip += tipList[i];
                }
            }
            totalBill = Double.Parse(tbBill.Text);
            totalBill += totalBill * tip / 100;
            tbTotalBill.Text = "" + totalBill;
            tbTip.Text = "" + tip;
            trackBar1.Value = (int)tip;
        }

        private void radioButtons_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (rb3Star.Checked)
            {
                flag[0] = true;
                flag[1] = false;
                flag[2] = false;
            }
            else if (rb4Star.Checked)
            {
                flag[0] = false;
                flag[1] = true;
                flag[2] = false;
            }
            else if (rb5Star.Checked)
            {
                flag[0] = false;
                flag[1] = false;
                flag[2] = true;
            }
            update();

        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            int i = -1;
            CheckBox cb = sender as CheckBox;
            Boolean ff = cb.Checked;
            if (cb == cbClecn) i = 3;
            if (cb == cbAtmo) i = 4;
            if (cb == cbService) i = 5;
            if (cb == cbLike) i = 6;
            flag[i] = ff;
            update();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            tip = trackBar1.Value;
            totalBill = Double.Parse(tbBill.Text);
            totalBill += totalBill * tip / 100;
            tbTotalBill.Text = "" + totalBill;
            tbTip.Text = "" + tip;
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog so the user can save the Image
            // assigned to Button2.
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            saveFileDialog1.Title = "Save an Text File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {

                System.IO.Stream fileStream = saveFileDialog1.OpenFile();
                System.IO.StreamWriter sw = new System.IO.StreamWriter(fileStream);
                sw.WriteLine("Resturant Name: "+ tbResturanName.Text);
                sw.WriteLine("Bill: " + tbBill.Text);
                if (flag[0]) sw.WriteLine("Rate: 3 Star");
                if (flag[1]) sw.WriteLine("Rate: 4 Star");
                if (flag[2]) sw.WriteLine("Rate: 5 Star");
                if (flag[3]) sw.WriteLine("Clean");
                if (flag[4]) sw.WriteLine("Atmosphere");
                if (flag[5]) sw.WriteLine("Service");
                if (flag[6]) sw.WriteLine("Like");
                sw.WriteLine("Tip: " + tbTip.Text+"%");
                sw.WriteLine("Bill: " + tbTotalBill.Text);
                sw.Flush();
                sw.Close();

            }
        }


    }


}
