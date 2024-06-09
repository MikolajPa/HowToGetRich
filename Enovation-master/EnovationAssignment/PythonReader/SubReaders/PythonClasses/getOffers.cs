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

                // Extract the sell offers part from the result
                int sellOffersIndex = result.IndexOf("Sell Offers:");
                if (sellOffersIndex == -1)
                {
                    throw new Exception("Sell Offers not found in the response");
                }

                string sellOffersJson = result.Substring(sellOffersIndex + "Sell Offers:\r\n".Length);
                sellOffersJson = sellOffersJson.Trim(); // Trim any extra whitespace

                // Deserialize the sell offers JSON to the Root object
                RootObject root = JsonConvert.DeserializeObject<RootObject>(sellOffersJson);

                // Output the Root object to verify the deserialization
                Console.WriteLine("Deserialized Sell Offers:");
                Console.WriteLine(JsonConvert.SerializeObject(root, Formatting.Indented));

                return root;
            }
        }
    }

    public class Offer
    {
        public string amount { get; set; }
        public int flags { get; set; }
        public string nft_offer_index { get; set; }
        public string owner { get; set; }
    }

    public class NFT
    {
        public int flags { get; set; }
        public string issuer { get; set; }
        public string nfTokenID { get; set; }
        public int nfTokenTaxon { get; set; }
        public int transferFee { get; set; }
        public string uri { get; set; }
        public int nft_serial { get; set; }
    }

    public class RootObject
    {
        public string nft_id { get; set; }
        public List<Offer> offers { get; set; }
    }
}
