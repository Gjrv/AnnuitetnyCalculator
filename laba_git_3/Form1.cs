using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Excel = Microsoft.Office.Interop.Excel;

namespace laba_git_3
{
    public class Shedule
    {
        public double _summ { get; set; }
        public double _persent { get; set; }
        public int _period { get; set; }

        public double _monthlyPay() => this._summ * (this._persent / 100 / 12 + ((this._persent / 100 / 12) / (Math.Pow((1 + this._persent / 100 / 12), this._period) - 1)));
        public double _getPersent()
        {
            double allPersents = 0;
            double mounthIPay;
            double tmpSumm = this._summ;
            for (int i = 0; i < _period; i++)
            {
                mounthIPay = tmpSumm * this._persent / 100 / 12;

                allPersents += mounthIPay;

                tmpSumm -= _monthlyPay() - mounthIPay;
            }
            return allPersents;
        }

    }
    public partial class Form1 : Form
    {
        List<Shedule> collection = new List<Shedule>();
        Excel.Application excelApp = new Excel.Application();
       


       
        public Form1()
        {
            InitializeComponent();
   
        }

        private void calculat_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();

            var obj = new Shedule
            {
                _summ = Convert.ToDouble(sum.Text),
                _persent = Convert.ToDouble(percent.Text),
                _period = Convert.ToInt32(period.Text)
            };
            collection.Add(obj);

            MessageBox.Show("Объект добавлен в коллекцию!");

            string[] seriesArray = { "Сумма", "проценты" };
            int[] pointsArray = {432,112303};

            // Add series.
            label6.Text = $"{Math.Round(obj._monthlyPay(), 2)} Р";
            chart1.Series[0].Points.AddXY("Cумма", obj._summ);
            chart1.Series[0].Points.AddXY("Проценты", obj._getPersent());


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(collection.Count == 0)
            {
                MessageBox.Show("Коллекция пуста");
                return;
            }

            excelApp.Visible = true;

            excelApp.Workbooks.Add();

            Excel._Worksheet workSheet = excelApp.ActiveSheet;

            workSheet.Cells[1, 1] = "Название кредита";
            workSheet.Cells[1, 2] = "Сумма кредита";
            workSheet.Cells[1, 3] = "Срок(мес.)";
            workSheet.Cells[1, 4] = "Процентная ставка";
            workSheet.Cells[1, 5] = "Сумма ануитетного платежа";
            workSheet.Cells[1, 6] = "Сумма переплат по процентам";
            int i = 2;
            foreach(var a in collection)
            {
                workSheet.Cells[i, 1] = $"Кредит №{i-1}";
                workSheet.Cells[i, 2] = $"{a._summ} Р";
                workSheet.Cells[i, 3] = a._period;
                workSheet.Cells[i, 4] = $"{ a._persent}%";
                workSheet.Cells[i, 5] = $"{ Math.Round(a._monthlyPay(),2) } Р";
                workSheet.Cells[i, 6] = $"{ Math.Round(a._getPersent(), 2) } Р";
                i++;
            }
        }
    }
}
