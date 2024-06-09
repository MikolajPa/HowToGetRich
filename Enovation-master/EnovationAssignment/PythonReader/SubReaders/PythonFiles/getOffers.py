import sys
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
    #nft_id
    arg1 = sys.argv[1]
    client=JsonRpcClient(testnet_url)
    offers=NFTBuyOffers(
        nft_id=arg1
    )
    response=client.request(offers)
    allOffers="Buy Offers:\n"+json.dumps(response.result, indent=4)
    offers=NFTSellOffers(
        nft_id=arg1
    )
    response=client.request(offers)
    allOffers+="\n\nSell Offers:\n"+json.dumps(response.result, indent=4)
    print(allOffers)
