using K_Means;
using Point = K_Means.Point;
using OxyPlot;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Start");

        Console.WriteLine("Start Createing Data");
        Random random = new Random();
        List<Point> data = new List<Point>();

        for (int i = 1; i < 1000; i++)
        {
            data.Add(new Point(random.NextDouble(), random.NextDouble()));
        }


        Console.WriteLine("Finish Createing Data");



        Console.WriteLine("Start Clustering");

        int k = 5;
        KMeans kmeans = new KMeans(k);
        kmeans.Fit(data);

        Console.WriteLine("Finish Clustering");


        Console.WriteLine("Showing Result");



        var clusters = kmeans.GetClusters();

        foreach (var cluster in clusters)
        {
            Console.WriteLine("------------------------------------------------------------------------");
            Console.WriteLine($"cluster number {clusters.IndexOf(cluster) + 1} : {cluster.Center} items count : {cluster.Items.Count}");

            foreach (var item in cluster.Items)
            {
                Console.WriteLine($"Point {item}");
            }
        }





        //for (int i = 0; i < clusters.Count; i++)
        //{
        //    Console.WriteLine($"Cluster {i + 1}:");
        //    foreach (var point in clusters[i])
        //    {
        //        Console.WriteLine(point);
        //    }
        //}

        Console.WriteLine("Finish");



        var plotModel = new PlotModel { Title = "K-Means Clustering" };

    }
}