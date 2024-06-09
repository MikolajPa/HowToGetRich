import sys
import xrpl
import json
from xrpl.clients import JsonRpcClient
from xrpl.wallet import Wallet
from datetime import datetime
from datetime import timedelta
from xrpl.models.requests import NFTSellOffers
from xrpl.models.requests import NFTBuyOffers
from xrpl.models.transactions import NFTokenAcceptOffer
testnet_url = "https://s.devnet.rippletest.net:51234/"

if __name__ == "__main__":
    #seed, offer_index
    arg1 = sys.argv[1]
    arg2 = sys.argv[2]
    buyer_wallet=Wallet.from_seed(arg1)
    client=JsonRpcClient(testnet_url)
    accept_offer_tx=xrpl.models.transactions.NFTokenAcceptOffer(
       account=buyer_wallet.classic_address,
       nftoken_sell_offer=arg2
    )
# Submit the transaction and report the results
    reply=""
    try:
        response=xrpl.transaction.submit_and_wait(accept_offer_tx,client,buyer_wallet)
        reply=response.result
    except xrpl.transaction.XRPLReliableSubmissionException as e:
        reply=f"Submit failed: {e}"
    print(reply)