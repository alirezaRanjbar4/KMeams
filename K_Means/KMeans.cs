namespace K_Means
{
    public class Clusters
    {
        public Clusters()
        {
            Items = new List<Point>();
        }
        public Point Center { get; set; }
        public List<Point> Items { get; set; }
    }


    public class KMeans
    {
        private int k;
        private List<Clusters> result;

        public KMeans(int k)
        {
            this.k = k;
            result = new List<Clusters>();
        }

        public void Fit(List<Point> data)
        {
            // Initialize centroids randomly
            var random = new Random();
            result = data.OrderBy(x => random.Next()).Take(k).Select(x => new Clusters() { Center = x }).ToList();

            bool centroidsChanged;
            do
            {
                result.ForEach(x => x.Items.Clear());

                // Assign each data point to the nearest centroid
                foreach (var point in data)
                {
                    int nearestCentroidIndex = GetNearestCentroidIndex(point);
                    result[nearestCentroidIndex].Items.Add(point);
                }

                centroidsChanged = UpdateCentroids();
            }
            while (centroidsChanged);
        }

        public List<Clusters> GetClusters()
        {
            return result;
        }

        private int GetNearestCentroidIndex(Point point)
        {
            double minDistance = double.MaxValue;
            int nearestCentroidIndex = -1;

            for (int i = 0; i < k; i++)
            {
                double distance = point.DistanceTo(result[i].Center);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestCentroidIndex = i;
                }
            }

            return nearestCentroidIndex;
        }

        private bool UpdateCentroids()
        {
            bool centroidsChanged = false;

            for (int i = 0; i < k; i++)
            {
                if (result[i].Items.Count == 0) continue;

                double newX = result[i].Items.Average(p => p.X);
                double newY = result[i].Items.Average(p => p.Y);

                Point newCentr = new Point(newX, newY);

                if (!newCentr.Equals(result[i].Center))
                {          
                    result[i].Center = newCentr;
                    centroidsChanged = true;
                }
            }

            return centroidsChanged;
        }
    }
}
