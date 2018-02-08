using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMEI_Calc
{
    public partial class Form1 : Form
    {
        public int flag = 3;
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonCalc_Click(object sender, EventArgs e)
        {
            //
            if (textBoxInput.Text.Length != 14)
            {
                Regex reg = new Regex(@"[^0-9]"); // 排除型字符组(取反思想)
                if (reg.IsMatch(textBoxInput.Text.ToString()))
                {
                    MessageBox.Show("仅能输入数字!");
                    textBoxInput.Clear();
                    goto Loop;
                }

                MessageBox.Show("请输入14位数字！");
                textBoxInput.Clear();
            }
            else
            {
                //add alorgthom Method
                //add result
                textBoxOutput.Text = textBoxInput.Text + IMEI_Calc(textBoxInput.Text);
            }
        Loop:
            { ;}
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            if (textBoxOutput.Text != "")
            {
                Clipboard.SetDataObject(textBoxOutput.Text);
                //MessageBox.Show("已复制到剪贴板！");
                timer1.Interval = 1000;
                timer1.Enabled = true;
            }
        }
        public int IMEI_Calc(string IMEI14)
        {
            //35890180697241
            //358901806972417
            int sum = 0;
            int[] ME = new int[14];
            //split string into a char array
            for (int i = 0; i < 14; i++)
            {
                ME[i] = Convert.ToInt32(IMEI14.Substring(i, 1));
            }
            int[] ME_Even = new int[7];//even means ou shu
            for (int i = 0; i < 7; i++)
            {
                ME_Even[i] = ME[i * 2 + 1] * 2;
            }
            int[] ME_Odd = new int[7];//odd means ji shu
            for (int i = 0; i < 7; i++)
            {
                ME_Odd[i] = ME[i * 2];
            }
            for (int i = 0; i < 7; i++)
            {
                sum += ME_Odd[i] + ME_Even[i] / 10 + ME_Even[i] % 10;
            }
            if (sum % 10 == 0)
            {
                return 0;
            }
            else
            {
                return sum = 10 - sum % 10;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ActiveControl = textBoxInput;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (0 != flag--)
            {
                buttonCopy.Enabled = false;
                buttonCopy.Text = "已复制！" + flag.ToString() + "S";
            }
            else
            {
                flag = 3;
                timer1.Enabled = false;
                buttonCopy.Text = "复制";
                buttonCopy.Enabled = true;
            }
        }
    }
}
