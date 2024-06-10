using K_Means;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using Point = K_Means.Point;

namespace KMeansWinForm
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            var numberOfPoints = 1000000;
            int numberOfClusters = 8;  //k


            //Generate some random point
            Random random = new Random();
            List<Point> data = new List<Point>();
            for (int i = 1; i < numberOfPoints; i++)
            {
                data.Add(new Point(random.NextDouble(), random.NextDouble()));
            }

            var startDate = DateTime.Now;
            KMeans kmeans = new KMeans(numberOfClusters);
            kmeans.Fit(data);
            var duration = DateTime.Now - startDate;

            var clusters = kmeans.GetClusters();
            var numberOfAttempts = kmeans.GetNumberOfAttempts();

            //Generate model for showing points
            var plotModel = new PlotModel
            {
                Title = $"K-Means clustering with {numberOfClusters} cluster",
                Subtitle = $"{numberOfAttempts} attempts in {duration} for {numberOfPoints} points."
            };

            // Define colors for clusters
            var colors = new[] {
                OxyColors.Red,
                OxyColors.Blue,
                OxyColors.Green,
                OxyColors.Purple,
                OxyColors.Orange,
                OxyColors.Cyan,
                OxyColors.RosyBrown,
                OxyColors.Yellow
            };


            foreach (var cluster in clusters)
            {
                //Items
                var scatterSeries = new ScatterSeries
                {
                    MarkerType = MarkerType.Circle,
                    MarkerFill = colors[clusters.IndexOf(cluster) % colors.Length],
                    MarkerSize = 4,
                };
                foreach (var item in cluster.Items)
                {
                    scatterSeries.Points.Add(new ScatterPoint(item.X, item.Y));
                }
                plotModel.Series.Add(scatterSeries);


                //Center
                var centerScatterSeries = new ScatterSeries
                {
                    MarkerType = MarkerType.Triangle,
                    MarkerFill = OxyColors.Black,
                    Title = $"center {cluster.Center} items count : {cluster.Items.Count}",
                    MarkerSize = 8,
                    Points = { new ScatterPoint(cluster.Center.X, cluster.Center.Y) }
                };

                plotModel.Series.Add(centerScatterSeries);
            }


            // Create a form to display the plot
            var plotView = new PlotView { Model = plotModel, Dock = DockStyle.Fill };
            var form = new Form { Width = 800, Height = 600 };
            form.Controls.Add(plotView);
            Application.Run(form);
        }
    }
}