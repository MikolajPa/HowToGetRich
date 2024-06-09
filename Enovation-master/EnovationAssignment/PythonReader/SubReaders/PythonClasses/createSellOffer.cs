using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;


public static class CreateSellOffer
{
    public static RootObjectSellOffer Main(string walletId, string ammount, string nftId)
    {
        string pythonScriptPath = @"C:\studia\Gowienko\TestPythonUserCase\TestPythonUserCase\main.py";
        string argument = "sEd7kVz1skvDczN8MTWjek6EanXW2fe 100 000800005FD540D41BB75AD17FFE493D0F6EE33A96203560B4B604CE00155833"; // Example arguments for the Python script

        return RunCmd(pythonScriptPath, argument);
    }


    public static RootObjectSellOffer RunCmd(string scriptPath, string arguments)
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
                result = result.Replace(", 'validated': True", "");
                Console.WriteLine(result);
                RootObjectSellOffer root = JsonConvert.DeserializeObject<RootObjectSellOffer>(result);
                return root;
                // Add more fields as necessary
            }
        }
    }
}


public class FinalFieldsSellOffer
{
    public int Flags { get; set; }
    public string Owner { get; set; }
    public string RootIndex { get; set; }
}

public class ModifiedNodeSellOffer
{
    public FinalFieldsSellOffer FinalFields { get; set; }
    public string LedgerEntryType { get; set; }
    public string LedgerIndex { get; set; }
    public string PreviousTxnID { get; set; }
}

public class AffectedNodeSellOffer
{
    public ModifiedNodeSellOffer ModifiedNode { get; set; }
}

public class MetaSellOffer
{
    public List<AffectedNodeSellOffer> AffectedNodes { get; set; }
}

public class RootObjectSellOffer
{
    public string Account { get; set; }
    public string Amount { get; set; }
    public string Fee { get; set; }
    public int Flags { get; set; }
    public int LastLedgerSequence { get; set; }
    public string NFTokenID { get; set; }
    public int Sequence { get; set; }
    public string SigningPubKey { get; set; }
    public string TransactionType { get; set; }
    public string TxnSignature { get; set; }
    public string ctid { get; set; }
    public int date { get; set; }
    public string hash { get; set; }
    public int inLedger { get; set; }
    public int ledger_index { get; set; }
    public Meta meta { get; set; }
}
