using System;
using Color = System.Drawing.Color;
using Font = System.Drawing.Font;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;

namespace Net.Charts
{
    class ChartsPersonalizado
    {
        public byte[] ChartImagen()
        {
            var chart = new Chart
            {
                Width = 300,
                Height = 450,
                AntiAliasing = AntiAliasingStyles.All,
                TextAntiAliasingQuality = TextAntiAliasingQuality.High
            };

            chart.Titles.Add("Sales By Employee");
            chart.Titles[0].Font = new Font("Arial", 16f);

            chart.ChartAreas.Add("");
            chart.ChartAreas[0].AxisX.Title = "Employee";
            chart.ChartAreas[0].AxisY.Title = "Sales";
            chart.ChartAreas[0].AxisX.TitleFont = new Font("Arial", 12f);
            chart.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 12f);
            chart.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 10f);
            chart.ChartAreas[0].AxisX.LabelStyle.Angle = -90;
            chart.ChartAreas[0].BackColor = Color.White;

            chart.Series.Add("");
            chart.Series[0].ChartType = SeriesChartType.Column;

            foreach (var q in query)
            {
                var Name = q.Employee.FirstName + ' ' + q.Employee.LastName;
                chart.Series[0].Points.AddXY(Name, Convert.ToDouble(q.NoOfOrders));
            }
            using (var chartimage = new MemoryStream())
            {
                chart.SaveImage(chartimage, ChartImageFormat.Png);
                return chartimage.GetBuffer();
            }
        }
    }
}
