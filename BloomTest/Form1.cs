using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BloomTest
{
    public partial class Form1 : Form
    {
        BloomFilter bloom = new BloomFilter(20000, 3);
        int setSize = 2000;
        public Form1()
        {
            InitializeComponent();
            //生成布隆缓存数组
            for (int i = 0; i < setSize; i++)
            {
                bloom.Add("kiba" + i);
            }
        } 
        private void btnSearch_Click(object sender, EventArgs e)
        { 
            Stopwatch sw = new Stopwatch();
            sw.Start();
            string con = tbCon.Text.Trim();
            var ret = bloom.IsExist(con);
            sw.Stop();
            lblRet.Text = $@"结果:{ret}{Environment.NewLine}
耗时:{sw.ElapsedTicks}{Environment.NewLine}
错误概率:{bloom.GetFalsePositiveProbability(setSize)}{Environment.NewLine} 
最佳数量:{bloom.OptimalNumberOfHashes(setSize)}";

           
        } 
    }
}
