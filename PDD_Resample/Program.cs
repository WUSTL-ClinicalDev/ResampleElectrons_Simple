using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDD_Resample
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                foreach (var file in Directory.EnumerateFiles(Path.GetDirectoryName(ofd.FileName), "*"))
                {
                    PDD data = GetPDDFromFile(file);
                    using(StreamWriter sw = new StreamWriter(file + "_resampled.asc"))
                    {
                        sw.WriteLine($"Energy: {Path.GetFileName(file)}");
                        sw.WriteLine($"Cone: 15cm");
                        sw.WriteLine("Depth [mm],Dose");
                        for (int i = 0; i < 200; i++)//200mm should be deep enough for the 30% necessary for IROC PDD
                        {
                            if (i > data.DataPoints.Max(x => x.Depth))
                            {
                                sw.WriteLine("PDD goes no further");
                                break;
                            }
                            if (data.DataPoints.Any(x => Math.Abs(x.Depth - i) < 0.001))
                            {
                                var point = data.DataPoints.FirstOrDefault(x => Math.Abs(x.Depth - i) < 0.001);
                                sw.WriteLine($"{point.Depth:F3},{point.Dose:F3}");
                                if (point.Depth>20 && point.Dose < 30) { break; }//iroc only wants data down to 30%. 
                            }
                            else //interpolation needed. 
                            {                               
                                var point_before = data.DataPoints.Last(x => x.Depth < i);
                                var point_after = data.DataPoints.First(x => x.Depth > i);
                                var dose_interp = point_before.Dose + (i - point_before.Depth) * ((point_after.Dose - point_before.Dose) / (point_after.Depth - point_before.Depth));
                                sw.WriteLine($"{i:F3},{dose_interp:F3}");
                                if(point_after.Depth>20 && point_after.Dose < 30) { break; }
                            }                            
                        }
                        sw.Flush();
                    }
                }
            }

        }

        private static PDD GetPDDFromFile(string file)
        {
            PDD pdd = new PDD();
            foreach(var line in File.ReadAllLines(file))
            {
                if (line.StartsWith("<"))
                {
                    pdd.DataPoints.Add(new DataPoint(Convert.ToDouble(line.Split(' ').First().TrimStart('<')),
                        Convert.ToDouble(line.Split(' ').Last().TrimEnd('>'))));
                }
            }
            return pdd;
        }
    }
    public class PDD
    {
        public List<DataPoint> DataPoints { get; set; }
        public PDD()
        {
            DataPoints = new List<DataPoint>();
        }
    }

    public class DataPoint
    {
        public double Depth { get; set; }
        public double Dose { get; set; }
        public DataPoint(double depth, double dose)
        {
            Depth = depth;
            Dose = dose;
        }
    }
}
