import sys
import xrpl
from xrpl.clients import JsonRpcClient
from xrpl.wallet import Wallet
from xrpl.models.requests import AccountNFTs


testnet_url = "https://s.devnet.rippletest.net:51234/"
if __name__ == "__main__":
    #seed, uri, flags, transfer_fee, taxon
    arg1 = sys.argv[1]
    arg2 = sys.argv[2]
    arg3 = sys.argv[3]
    arg4 = sys.argv[4]
    arg5 = sys.argv[5]
# Get the client
    minter_wallet=Wallet.from_seed(arg1)
    client=JsonRpcClient(testnet_url)
# Define the mint transaction
    mint_tx=xrpl.models.transactions.NFTokenMint(
        account=minter_wallet.address,
        uri=xrpl.utils.str_to_hex(arg2),
        flags=int(arg3),
        transfer_fee=int(arg4),
        nftoken_taxon=int(arg5)
    )
# Submit the transaction and get results
    reply=""
    try:
        response=xrpl.transaction.submit_and_wait(mint_tx,client,minter_wallet)
        reply=response.result
    except xrpl.transaction.XRPLReliableSubmissionException as e:
        reply=f"Submit failed: {e}"
    print(reply)