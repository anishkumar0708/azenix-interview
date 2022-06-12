using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ReadAndCaptureLog
{
    public class GetLogDetails
    {
        public string GetLogFile(string path)
        {
            try
            {
                return File.ReadAllText(path);
            }
            catch (Exception)
            {
                Console.WriteLine("Unable to read log file"); Console.ReadLine();
                return null;
            }
        }

        public Dictionary<string, int> GetIp(string inputFile, string inputDetails)
        {
            try
            {
                string pattern = @"\b(?:(?:2(?:[0-4][0-9]|5[0-5])|[0-1]?[0-9]?[0-9])\.){3}(?:(?:2([0-4][0-9]|5[0-5])|[0-1]?[0-9]?[0-9]))";
                Regex r = new Regex(pattern);
                IEnumerable<string> lines = File.ReadLines(inputFile);
                MatchCollection matches = r.Matches(inputDetails);
                Dictionary<string, int> ipInstances = new Dictionary<string, int>();
                foreach (Match match in matches)
                {
                    if (match.Value.Split('.')[0] != "0")
                    {
                        if (!ipInstances.ContainsKey(match.Value))
                        {
                            ipInstances.Add(match.Value, 0);
                        }
                        ipInstances[match.Value]++;
                    }
                }
                return ipInstances;
            }
            catch (Exception)
            {
                Console.WriteLine("Unable to get IPs"); Console.ReadKey();
                return null;
            }
        }

        public List<string> PouplateUniqueIP(IEnumerable<string> ranked)
        {
            try
            {
                List<string> uniqueIps = new List<string>();
                foreach (string ip in ranked)
                {
                    uniqueIps.Add(ip);
                }
                return uniqueIps;
            }
            catch (Exception) { return null; }
        }

        public Dictionary<string, int> GetURL(string inputFile, string inputDetails)
        {
            Dictionary<string, int> urlInstances = new Dictionary<string, int>();
            try
            {
                Regex r = new Regex(@"(\bGET.*)+HTTP/1.1\b", RegexOptions.None | RegexOptions.IgnoreCase);
                IEnumerable<string> lines = File.ReadLines(inputFile);
                MatchCollection matches = r.Matches(inputDetails);
                
                foreach (Match match in matches)
                {
                    if (!urlInstances.ContainsKey(match.Value))
                    {
                        urlInstances.Add(match.Value, 0);
                    }
                    urlInstances[match.Value]++;
                }
                
                return urlInstances;
            }
            catch (Exception){
                Console.WriteLine("Unable to get URLs"); Console.ReadKey();
                return null;
            }
        }

        public List<string> PouplateUniqueUrl(IEnumerable<string> ranked)
        {
            try
            {
                List<string> uniqueUrls = new List<string>();
                foreach (string url in ranked)
                {
                    uniqueUrls.Add(url.Split(' ')[1]);
                }
                return uniqueUrls;
            }
            catch (Exception) { return null; }

        }
    }
}
