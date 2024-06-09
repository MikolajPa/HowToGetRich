using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

public static class MintToken
{
    public static Root Main(string seed, string uri)
    {
        string pythonScriptPath = @"C:\studia\HowToGetRich\Enovation-master\EnovationAssignment\PythonReader\SubReaders\PythonFiles\mint_token.py";
        string argument = $"{seed} {uri} 8 0 0"; // Example arguments for the Python script

        return RunCmd(pythonScriptPath, argument);
    }

    public static Root RunCmd(string scriptPath, string arguments)
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
                    Root root = JsonConvert.DeserializeObject<Root>(result);

                    // Output the Root object to verify the deserialization
                    Console.WriteLine($"Account: {root.Account}");
                    Console.WriteLine($"Fee: {root.Fee}");
                // Add more fields as necessary
                return root;
            }
        }
    }
}


public class Root
{
    public string? Account { get; set; }
    public string? Fee { get; set; }
    public int? Flags { get; set; }
    public int? LastLedgerSequence { get; set; }
    public int? NFTokenTaxon { get; set; }
    public int? Sequence { get; set; }
    public string? SigningPubKey { get; set; }
    public string? TransactionType { get; set; }
    public int? TransferFee { get; set; }
    public string? TxnSignature { get; set; }
    public string? URI { get; set; }
    public string? ctid { get; set; }
    public int? date { get; set; }
    public string? hash { get; set; }
    public int? inLedger { get; set; }
    public int? ledger_index { get; set; }
    public Meta? meta { get; set; }
}

public class Meta
{
    public List<AffectedNode>? AffectedNodes { get; set; }
    public int? TransactionIndex { get; set; }
    public string? TransactionResult { get; set; }
    public string? nftoken_id { get; set; }
}

public class AffectedNode
{
    public ModifiedNode? ModifiedNode { get; set; }
    public CreatedNode? CreatedNode { get; set; }
}

public class ModifiedNode
{
    public FinalFields? FinalFields { get; set; }
    public string? LedgerEntryType { get; set; }
    public string? LedgerIndex { get; set; }
    public PreviousFields? PreviousFields { get; set; }
    public string? PreviousTxnID { get; set; }
    public int? PreviousTxnLgrSeq { get; set; }
}

public class FinalFields
{
    public string? Account { get; set; }
    public string? Balance { get; set; }
    public int? FirstNFTokenSequence { get; set; }
    public int? Flags { get; set; }
    public int? MintedNFTokens { get; set; }
    public int? OwnerCount { get; set; }
    public int? Sequence { get; set; }
}

public class PreviousFields
{
    public string? Balance { get; set; }
    public int? OwnerCount { get; set; }
    public int? Sequence { get; set; }
}

public class CreatedNode
{
    public string? LedgerEntryType { get; set; }
    public string? LedgerIndex { get; set; }
    public NewFields? NewFields { get; set; }
}

public class NewFields
{
    public List<NFToken>? NFTokens { get; set; }
}

public class NFToken
{
    public string? NFTokenID { get; set; }
    public string? URI { get; set; }
}