using System;
using System.Collections.Generic;
using System.IO;

namespace ReadAndCaptureLog
{
    public class WriteLog
    {
        public void CaptureLog(string outputPath, List<string> outputDetails)
        {
            try
            {
                File.WriteAllLines(outputPath, outputDetails);
            }
            catch (Exception)
            {
                Console.WriteLine("Unable to generate output file");
            }
        }
        
    }
}
