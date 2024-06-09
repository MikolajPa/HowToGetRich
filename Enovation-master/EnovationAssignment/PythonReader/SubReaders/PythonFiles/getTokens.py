import sys
import xrpl
from xrpl.clients import JsonRpcClient
from xrpl.wallet import Wallet
from xrpl.models.requests import AccountNFTs

testnet_url = "https://s.devnet.rippletest.net:51234/"

if __name__ == "__main__":
    #account
    arg1 = sys.argv[1]
    client=JsonRpcClient(testnet_url)
    acct_nfts=AccountNFTs(
        account=arg1
    )
    response=client.request(acct_nfts)
    print(response.result)