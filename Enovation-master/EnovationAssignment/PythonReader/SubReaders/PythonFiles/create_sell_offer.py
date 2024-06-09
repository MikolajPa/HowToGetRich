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
    #seed, amount, nftoken_id, expiration, destination
    arg1 = sys.argv[1]
    arg2 = sys.argv[2]
    arg3 = sys.argv[3]
# Get the client
    owner_wallet = Wallet.from_seed(arg1)
    client = JsonRpcClient(testnet_url)
    expiration_date = datetime.now()
# Define the sell offer
    sell_offer_tx=xrpl.models.transactions.NFTokenCreateOffer(
        account=owner_wallet.address,
        nftoken_id=arg3,
        amount=arg2,
        destination= None,
        expiration= None,
        flags=1
    )
# Submit the transaction and report the results
    reply=""
    try:
        response=xrpl.transaction.submit_and_wait(sell_offer_tx,client,owner_wallet)
        reply=response.result
    except xrpl.transaction.XRPLReliableSubmissionException as e:
        reply=f"Submit failed: {e}"
    print(reply)

