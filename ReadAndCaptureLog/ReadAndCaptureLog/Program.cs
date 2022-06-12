using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ReadAndCaptureLog
{
    public class Program
    {
        static string inputFile = null;

        static void Main(string[] args)
        {
            string path = @"C:\Temp\log-data.txt";
            string outputFile = @"C:\Temp\output.txt";
            List<string> outputInfos = new List<string>();
            var logFile = new GetLogDetails();  var writeFile = new WriteLog();
            Dictionary<string, int> ipInstances = new Dictionary<string, int>();
            Dictionary<string, int> urlInstances = new Dictionary<string, int>();
            try
            {      
                inputFile = logFile.GetLogFile(path);                
                ipInstances = logFile.GetIp(path, inputFile);
                urlInstances = logFile.GetURL(path, inputFile);
                
                
                var ipRanked = ipInstances.OrderByDescending(kvp => kvp.Value).Select(kvp => kvp.Key);
                List<string> uniqueIps = logFile.PouplateUniqueIP(ipRanked.Take(3));
                var urlRanked = urlInstances.OrderByDescending(kvp => kvp.Value).Select(kvp => kvp.Key);
                List<string> uniqueUrls = logFile.PouplateUniqueUrl(urlRanked.Take(3));

                outputInfos.Add("Number of unique IP Addresses : " + ipInstances.Count.ToString());

                outputInfos.Add(""); outputInfos.Add("3 most visited URLs :");
                foreach (var url in uniqueUrls)
                    outputInfos.Add(url);

                outputInfos.Add(""); outputInfos.Add("3 most active IP Addresss :");
                foreach (var ip in uniqueIps)
                    outputInfos.Add(ip);
                
                writeFile.CaptureLog(outputFile, outputInfos);
                Console.WriteLine(@"File:output.txt is generated at C:\Temp location");
                Console.ReadKey();
            }
            catch (Exception)
            {
                Console.WriteLine("Unable to read and write log");
                Console.ReadKey();
            }
        }       
    }
}
