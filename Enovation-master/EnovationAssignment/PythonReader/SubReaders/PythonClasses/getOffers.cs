using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

public static class getOffers
{
    public static RootObject Main(string seed)
    {
        string pythonScriptPath = @"C:\studia\HowToGetRich\Enovation-master\EnovationAssignment\PythonReader\SubReaders\PythonFiles\getOffers.py";
        string argument = $"{seed}"; // Example arguments for the Python script

        return RunCmd(pythonScriptPath, argument);
    }

    public static RootObject RunCmd(string scriptPath, string arguments)
    {
        ProcessStartInfo start = new ProcessStartInfo();
        start.FileName = @"C:\Users\mikolaj.palacz\AppData\Local\Programs\Python\Python310\python.exe";
        start.Arguments = string.Format("\"{0}\" {1}", scriptPath, arguments); // Pass the script path and arguments
        start.UseShellExecute = false;
        start.RedirectStandardOutput = true;
        using (Process process = Process.Start(start))
        {
            using (StreamReader reader = process.StandardOutput)
            {
                string result = reader.ReadToEnd();

                // Print the result to verify the output
                Console.WriteLine("Received JSON:");
                Console.WriteLine(result);

                // Deserialize the JSON result to the Root object
                result = result.Replace(", 'validated': False", "");
                Console.WriteLine(result);
                RootObject root = JsonConvert.DeserializeObject<RootObject>(result);

                // Output the Root object to verify the deserialization
                // Add more fields as necessary
                return root;
            }
        }
    }

public class NFT
{
    public int Flags { get; set; }
    public string Issuer { get; set; }
    public string NFTokenID { get; set; }
    public int NFTokenTaxon { get; set; }
    public int TransferFee { get; set; }
    public string URI { get; set; }
    public int nft_serial { get; set; }
}

public class RootObject
{
    public string account { get; set; }
    public List<NFT> account_nfts { get; set; }
    public int ledger_current_index { get; set; }
    public bool validated { get; set; }
}

}

