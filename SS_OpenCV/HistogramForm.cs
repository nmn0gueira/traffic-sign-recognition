using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SS_OpenCV
{
    public partial class HistogramForm : Form
    {
        public HistogramForm()
        {
            InitializeComponent();
        }

        // Overloaded constructor to accept multiple histograms dynamically
        public HistogramForm(Dictionary<string, (int[] Histogram, Color Color)> histograms)
        {
            InitializeComponent();

            // Create a TabControl for the histograms
            TabControl tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;

            // Dynamically add tabs for each histogram
            foreach (var entry in histograms)
            {
                string title = entry.Key;
                int[] histogram = entry.Value.Histogram;
                Color color = entry.Value.Color;

                tabControl.TabPages.Add(CreateHistogramTab(title, histogram, color));
            }

            // Add TabControl to the form
            this.Controls.Add(tabControl);
        }

        // Helper method to create a tab with a histogram chart
        private TabPage CreateHistogramTab(string title, int[] histogram, Color color)
        {
            TabPage tabPage = new TabPage(title);

            // Create a Chart for the histogram
            Chart chart = new Chart();
            chart.Dock = DockStyle.Fill;

            // Set up the ChartArea
            ChartArea chartArea = new ChartArea();
            chartArea.AxisX.Minimum = 0;
            chartArea.AxisX.Maximum = 255;
            chartArea.AxisX.Title = "Intensity";
            chartArea.AxisY.Title = "Frequency";
            chart.ChartAreas.Add(chartArea);

            // Add a series for the histogram
            Series series = new Series
            {
                ChartType = SeriesChartType.Column,
                Color = color
            };
            chart.Series.Add(series);

            // Populate the histogram data
            for (int i = 0; i < histogram.Length; i++)
            {
                series.Points.AddXY(i, histogram[i]);
            }

            // Add the Chart to the TabPage
            tabPage.Controls.Add(chart);

            return tabPage;
        }
    }
}
