import xrpl
import json
from xrpl.clients import JsonRpcClient
from xrpl.wallet import Wallet
from xrpl.models.requests import AccountNFTs
from datetime import datetime
from datetime import timedelta
from xrpl.models.requests import NFTSellOffers
from xrpl.models.requests import NFTBuyOffers
from xrpl.models.transactions import NFTokenAcceptOffer

testnet_url = "https://s.devnet.rippletest.net:51234/" #"https://s.altnet.rippletest.net:51234"


# TA FUNKCJA BIERZE CI KONOT I SPRAWDZA JAKI WALLET JEST PRZYPISANY JESLI NIE MA TO TWORZY NOWY WALLET
def get_account(seed): # seed -> a string that serves as the secret key for the wallet
    """get_account
    
    Output: 
        public_key: EDEFC0B7447039051039AF3BF7F227E96115246DF96EE8AFAF7E6C414801F0296E
        private_key: -HIDDEN-
        classic_address: rnAWJdcZx4d6wW93k7HdMx7ULLDS6a3hxw
    """
    client = xrpl.clients.JsonRpcClient(testnet_url)
    if (seed == ''): # if seed empty make new wallet 
        new_wallet = xrpl.wallet.generate_faucet_wallet(client) # a new wallet is generated using a faucet
    else: # if seed not empty created wallet from that seed 
        new_wallet = xrpl.wallet.Wallet.from_seed(seed)
    return new_wallet



#DAJE INFORMACJE NA TEMAT KONTA JAKIE NFT I ILE MA PIENIEDZY
def get_account_info(accountId): # The account ID (address) for which information is to be fetched.
    """get_account_info
    
    {'Account': 'rHsMGQEkVNJmpGWs8XUBoTBiAAbwxZN5v3', 
    'Balance': '11999988', 
    'Flags': 0, 
    'LedgerEntryType': 'AccountRoot', 
    'OwnerCount': 1, 
    'PreviousTxnID': 'ED4F625172A9959F9476F4DF8FEE6FF165DC71CC7FB277FF1B66E046C651BD5B', 
    'PreviousTxnLgrSeq': 1341099, 
    'Sequence': 1095092, 
    'index': '984E286CCF3F34102599A422F46ADFDC6C07DD41D1C2FC28391955721E351CD8'}
    """
    client = xrpl.clients.JsonRpcClient(testnet_url) # A JSON RPC client is created to connect to the testnet.
    acct_info = xrpl.models.requests.account_info.AccountInfo(
        account=accountId,
        ledger_index="validated"
    )
    response = client.request(acct_info)
    return response.result['account_data'] # get account data]

#WYSYLASZ XRP NA DANY WALLET SEED TO ACCOUNT ID
def send_xrp(seed, amount, destination): # seed of the wallet, amount of XRP send, The destination account address.
    """
    {
    "Account": "rPV8fPLoVdkDiQHCEyRdtwsKwwVjGn5YKG",
    "Balance": "99999990",
    "Flags": 0,
    "LedgerEntryType": "AccountRoot",
    "OwnerCount": 0,
    "PreviousTxnID": "8FA0D635861EB6705B33CCB2741A7654A5C623291D08BC5483528D6CD15AD6BB",
    "PreviousTxnLgrSeq": 1400157,
    "Sequence": 1400126,
    "index": "A713ED37FF7BA2BBB90DA2474032B84E3CFD2210AF63A61CBA512AFA8DB78CD5"
    }
    """
    sending_wallet = xrpl.wallet.Wallet.from_seed(seed) # create wallet form the seed 
    client = xrpl.clients.JsonRpcClient(testnet_url) # create client to connect to testnet 
    # create transaction 
    payment = xrpl.models.transactions.Payment(
        account=sending_wallet.address, # sending wallet's address
        amount=xrpl.utils.xrp_to_drops(int(amount)), # the amount 
        destination=destination, # the destination address. 
    )
    try:	
        response = xrpl.transaction.submit_and_wait(payment, client, sending_wallet) # The transaction is submitted and waited for confirmation using
    except xrpl.transaction.XRPLReliableSubmissionException as e:	
        response = f"Submit failed: {e}"

    return response # payment succesful or failed 



###############
# get_balance #
###############

def get_balance(sb_account_seed, op_account_seed):
    """get_balance

    sb_account_seed: The seed of the wallet to check the balance for.
    op_account_seed: Another wallet's seed (not used in the function directly).

    output:
    {'account': 'r9j5CK7W1K5JhzGg3c7W7oimRb91maAQq4', 
    'ledger_hash': 'F35B8657364AE9EC9997ABA32E184364047621CFB020D686C6A07659EA5DE09E', 
    'ledger_index': 1399865, 
    'validated': True}
    """
    wallet = Wallet.from_seed(sb_account_seed) # create wallets 
    opWallet = Wallet.from_seed(op_account_seed)
    client=JsonRpcClient(testnet_url) # client 
    balance=xrpl.models.requests.GatewayBalances( # send request and wait for result 
        account=wallet.address,
        ledger_index="validated"
    )
    response = client.request(balance)
    return response.result
    
#####################
# configure_account #
#####################

def configure_account(seed, default_setting): # Boolean indicating whether to set or clear the DefaultRipple flag.
    """configure_account
    
    default_setting -->  (a boolean indicating whether to set or clear the DefaultRipple flag).

    The Default Ripple flag is an account setting that enables rippling on all incoming trust lines by default. 
    Issuers MUST enable this flag for their customers to be able to send tokens to each other.

    {'Account': 'rU8FfvCGUMsEBRDeN7HerEhed2GekLAX4G', 
    'Fee': '10', 
    'Flags': 0, 
    'LastLedgerSequence': 1401273, 
    'Sequence': 1401137, 
    'SetFlag': 8, 
    'SigningPubKey': 'ED760ABCAD964CA3DAEA761860C69A90E2BA1F67DE4596E7D3CB2A7C866424FEC1', 
    'TransactionType': 'AccountSet', 
    'TxnSignature': 'B4D500F859DE7718F5ED4558F939ECD8B9F0F1635EA0D9EB2DF3232D614B0047F2D483A27118F905E1587319469B2063EDD764FEDC2CD9197E43DD5FF460D60A', 
    'ctid': 'C01561A700000002', 
    'date': 771193912, 
    'hash': '7D1248ECB5DF20E02BBF16E1E1252208693B2F42628625E3967C851CD3BB8E40', 
    'inLedger': 1401255, 
    'ledger_index': 1401255, 
    'meta': {'AffectedNodes': [{'ModifiedNode': {'FinalFields': {'Account': 'rU8FfvCGUMsEBRDeN7HerEhed2GekLAX4G', 'Balance': '99999980', 'FirstNFTokenSequence': 1401136, 'Flags': 8388608, 'MintedNFTokens': 1, 'OwnerCount': 1, 'Sequence': 1401138}, 
    'LedgerEntryType': 'AccountRoot', 
    'LedgerIndex': '927413B41EEA9F10C5F6C308018157262F6D30BA573D3702FEA2BAC432F6F5F1', 
    'PreviousFields': {'Balance': '99999990', 'Flags': 0, 'Sequence': 1401137}, 
    'PreviousTxnID': '89F91287A4007CD92F8DE57DA51CB648EF8105A9B2B74B006B6711507A77EDCE', 
    'PreviousTxnLgrSeq': 1401150}}], 
    'TransactionIndex': 0, 
    'TransactionResult': 'tesSUCCESS'}, 
    'validated': True}
    """
    # Get the client
    wallet=Wallet.from_seed(seed)
    client=JsonRpcClient(testnet_url)
    # Create transaction
    if (default_setting):
        setting_tx=xrpl.models.transactions.AccountSet(
            account=wallet.classic_address,
            set_flag=xrpl.models.transactions.AccountSetAsfFlag.ASF_DEFAULT_RIPPLE # flag 
        )
    else:
        setting_tx=xrpl.models.transactions.AccountSet(
            account=wallet.classic_address,
            clear_flag=xrpl.models.transactions.AccountSetAsfFlag.ASF_DEFAULT_RIPPLE # no flag 
        )
    response=xrpl.transaction.submit_and_wait(setting_tx,client,wallet)
    return response.result    
    


#TWORZYSZ TOKEN DO SEED = id konta (wallet id), URI tokena (jpg), flags ustawaimy na 4, transfer fee 10000 i taxon 0
def mint_token(seed, uri, flags, transfer_fee, taxon):
    """mint(create)_token
    
    This function takes several parameters including seed (the seed of the minter's XRPL account), 
    uri (the Uniform Resource Identifier of the token), 
    flags (additional flags for the token), (1,2,4,8)
    transfer_fee (the fee for transferring the token), 
    taxon (the tax on the token).

    output:
    {'Account': 'rU8FfvCGUMsEBRDeN7HerEhed2GekLAX4G', 
    'Fee': '10', 
    'Flags': 8, 
    'LastLedgerSequence': 1401332, 
    'NFTokenTaxon': 0, 
    'Sequence': 1401138, 
    'SigningPubKey': 'ED760ABCAD964CA3DAEA761860C69A90E2BA1F67DE4596E7D3CB2A7C866424FEC1', 
    'TransactionType': 'NFTokenMint', 
    'TransferFee': 10000, 
    'TxnSignature': '3EC2C4216E4C5C4F30D6E80A58492BD079430F68CD5D3792CA1AEA985C3C8D8F896884EEA5E994916764E8730C15F0C7C0EA2DE063948229EB4FC433CAF9FA0D', 
    'URI': '697066733A2F2F62616679626569676479727A74357366703775646D37687537367568377932366E6634646675796C71616266336F636C67747179353566627A6469', 
    'ctid': 'C01561E200000002', 
    'date': 771194091, 
    'hash': '76154CD6C51BC67DF7FBFCA3B267A884E189B0BF11011F5C4A1DC20508BCFFB7', 
    'inLedger': 1401314,
     'ledger_index': 1401314, 
     'meta': {'AffectedNodes': [{'ModifiedNode': {'FinalFields': {'Flags': 0, 'NFTokens': [{'NFToken': {'NFTokenID': '0008271081536F68FFB7D7B36FF2D67B0B89C01B7AA88B99845D9ACB00156130', 
     'URI': '697066733A2F2F62616679626569676479727A74357366703775646D37687537367568377932366E6634646675796C71616266336F636C67747179353566627A6469'}}, 
     {'NFToken': {'NFTokenID': '0008271081536F68FFB7D7B36FF2D67B0B89C01B7AA88B999B436BCC00156131', 'URI': '697066733A2F2F62616679626569676479727A74357366703775646D37687537367568377932366E6634646675796C71616266336F636C67747179353566627A6469'}}]}, 
     'LedgerEntryType': 'NFTokenPage', 
     'LedgerIndex': '81536F68FFB7D7B36FF2D67B0B89C01B7AA88B99FFFFFFFFFFFFFFFFFFFFFFFF', 
     'PreviousFields': {'NFTokens': [{'NFToken': {'NFTokenID': '0008271081536F68FFB7D7B36FF2D67B0B89C01B7AA88B99845D9ACB00156130', 'URI': '697066733A2F2F62616679626569676479727A74357366703775646D37687537367568377932366E6634646675796C71616266336F636C67747179353566627A6469'}}]}, 
     'PreviousTxnID': '89F91287A4007CD92F8DE57DA51CB648EF8105A9B2B74B006B6711507A77EDCE', 'PreviousTxnLgrSeq': 1401150}}, 
     {'ModifiedNode': {'FinalFields': {'Account': 'rU8FfvCGUMsEBRDeN7HerEhed2GekLAX4G', 'Balance': '99999970', 'FirstNFTokenSequence': 1401136, 'Flags': 8388608, 'MintedNFTokens': 2, 'OwnerCount': 1, 'Sequence': 1401139}, 
     'LedgerEntryType': 'AccountRoot', 
     'LedgerIndex': '927413B41EEA9F10C5F6C308018157262F6D30BA573D3702FEA2BAC432F6F5F1', 
     'PreviousFields': {'Balance': '99999980', 'MintedNFTokens': 1, 'Sequence': 1401138}, 
     'PreviousTxnID': '7D1248ECB5DF20E02BBF16E1E1252208693B2F42628625E3967C851CD3BB8E40', 'PreviousTxnLgrSeq': 1401255}}], 
     'TransactionIndex': 0, 
     'TransactionResult': 'tesSUCCESS', 
     'nftoken_id': '0008271081536F68FFB7D7B36FF2D67B0B89C01B7AA88B999B436BCC00156131'}, 
     'validated': True}
    """
# Get the client
    minter_wallet=Wallet.from_seed(seed)
    client=JsonRpcClient(testnet_url)
# Define the mint transaction
    mint_tx=xrpl.models.transactions.NFTokenMint(
        account=minter_wallet.address,
        uri=xrpl.utils.str_to_hex(uri),
        flags=int(flags),
        transfer_fee=int(transfer_fee),
        nftoken_taxon=int(taxon)
    )
# Submit the transaction and get results
    reply=""
    try:
        response=xrpl.transaction.submit_and_wait(mint_tx,client,minter_wallet)
        reply=response.result
    except xrpl.transaction.XRPLReliableSubmissionException as e:
        reply=f"Submit failed: {e}"
    return reply

#SPRWDZASZ JAKIEGO MASZ TOKENA
def get_tokens(account): # accoundID
    """get_tokens
    {'account': 'rU8FfvCGUMsEBRDeN7HerEhed2GekLAX4G', 
    'account_nfts': [{'Flags': 8, 
        'Issuer': 'rU8FfvCGUMsEBRDeN7HerEhed2GekLAX4G', 
        'NFTokenID': '0008271081536F68FFB7D7B36FF2D67B0B89C01B7AA88B99845D9ACB00156130', 
        'NFTokenTaxon': 0,
        'TransferFee': 10000, 
        'URI': '697066733A2F2F62616679626569676479727A74357366703775646D37687537367568377932366E6634646675796C71616266336F636C67747179353566627A6469', 
        'nft_serial': 1401136}, 
        {'Flags': 8, 
        'Issuer': 'rU8FfvCGUMsEBRDeN7HerEhed2GekLAX4G', 
        'NFTokenID': '0008271081536F68FFB7D7B36FF2D67B0B89C01B7AA88B999B436BCC00156131', 
        'NFTokenTaxon': 0, 
        'TransferFee': 10000, 
        'URI': '697066733A2F2F62616679626569676479727A74357366703775646D37687537367568377932366E6634646675796C71616266336F636C67747179353566627A6469', 
        'nft_serial': 1401137}], 'ledger_current_index': 1401396, 'validated': False}
    """
    client=JsonRpcClient(testnet_url) 
    acct_nfts=AccountNFTs(
        account=account
    )
    response=client.request(acct_nfts) #It constructs a request to get the NFTs associated with the provided account.
    return response.result

# NISZCZYSZ TOKENA
def burn_token(seed, nftoken_id):
    """burn_token
    
    nftoken_id --> (the ID of the NFT to burn)

    {'Account': 'rU8FfvCGUMsEBRDeN7HerEhed2GekLAX4G', 
    'Fee': '10',
    'Flags': 0, 
    'LastLedgerSequence': 1401457, 
    'NFTokenID': '0008271081536F68FFB7D7B36FF2D67B0B89C01B7AA88B999B436BCC00156131', 
    'Sequence': 1401139, 
    'SigningPubKey': 'ED760ABCAD964CA3DAEA761860C69A90E2BA1F67DE4596E7D3CB2A7C866424FEC1', 
    'TransactionType': 'NFTokenBurn', 
    'TxnSignature': '8F52E7ADCBDE668DCE5A486F74DB95CC4962CD4CF13DBCB82F31C943B113B0ACA3BD2A144980230F0B2927C689EB3F17B480EDF44DEFCADE6DFEE8DF2E402A07', 
    'ctid': 'C015625F00000002', 
    'date': 771194470, 
    'hash': '47D21FD1E5222E824EA18E17D876FC47D385EA596C42F4D206BF93E04ACB8572', 
    'inLedger': 1401439, 
    'ledger_index': 1401439, 
    'meta': {'AffectedNodes': [{'ModifiedNode': {'FinalFields': {'Flags': 0, 'NFTokens': [{'NFToken': {'NFTokenID': '0008271081536F68FFB7D7B36FF2D67B0B89C01B7AA88B99845D9ACB00156130', 'URI': '697066733A2F2F62616679626569676479727A74357366703775646D37687537367568377932366E6634646675796C71616266336F636C67747179353566627A6469'}}]}, 
    'LedgerEntryType': 'NFTokenPage', 
    'LedgerIndex': '81536F68FFB7D7B36FF2D67B0B89C01B7AA88B99FFFFFFFFFFFFFFFFFFFFFFFF', 
    'PreviousFields': {'NFTokens': [{'NFToken': {'NFTokenID': '0008271081536F68FFB7D7B36FF2D67B0B89C01B7AA88B99845D9ACB00156130', 'URI': '697066733A2F2F62616679626569676479727A74357366703775646D37687537367568377932366E6634646675796C71616266336F636C67747179353566627A6469'}}, 
    {'NFToken': {'NFTokenID': '0008271081536F68FFB7D7B36FF2D67B0B89C01B7AA88B999B436BCC00156131', 'URI': '697066733A2F2F62616679626569676479727A74357366703775646D37687537367568377932366E6634646675796C71616266336F636C67747179353566627A6469'}}]}, 
    'PreviousTxnID': '76154CD6C51BC67DF7FBFCA3B267A884E189B0BF11011F5C4A1DC20508BCFFB7', 'PreviousTxnLgrSeq': 1401314}}, 
    {'ModifiedNode': {'FinalFields': {'Account': 'rU8FfvCGUMsEBRDeN7HerEhed2GekLAX4G', 'Balance': '99999960', 'BurnedNFTokens': 1, 'FirstNFTokenSequence': 1401136, 'Flags': 8388608, 'MintedNFTokens': 2, 'OwnerCount': 1, 'Sequence': 1401140}, 'LedgerEntryType': 'AccountRoot', 'LedgerIndex': '927413B41EEA9F10C5F6C308018157262F6D30BA573D3702FEA2BAC432F6F5F1', 
    'PreviousFields': {'Balance': '99999970', 'Sequence': 1401139}, 
    'PreviousTxnID': '76154CD6C51BC67DF7FBFCA3B267A884E189B0BF11011F5C4A1DC20508BCFFB7', 
    'PreviousTxnLgrSeq': 1401314}}], 
    'TransactionIndex': 0, 
    'TransactionResult': 'tesSUCCESS'}, 
    'validated': True}
    """
# Get the client
    owner_wallet=Wallet.from_seed(seed)
    client=JsonRpcClient(testnet_url)
    burn_tx=xrpl.models.transactions.NFTokenBurn(
        account=owner_wallet.address,
        nftoken_id=nftoken_id    
    )
# Submit the transaction and get results
    reply=""
    try:
        response=xrpl.transaction.submit_and_wait(burn_tx,client,owner_wallet)
        reply=response.result
    except xrpl.transaction.XRPLReliableSubmissionException as e:
        reply=f"Submit failed: {e}"
    return reply



def create_sell_offer(seed, amount, nftoken_id, expiration, destination):
    """This function is used to create a sell offer for an NFT.
    
    seed: The seed of the seller's XRPL account.
    amount: The amount of the NFT to sell.(str)
    nftoken_id: The ID of the NFT being sold.
    expiration: The expiration time of the offer.(in s)
    destination: The destination address for the sale proceeds.    

    OutPut(no expiration, no destination):
    {'Account': 'rU8FfvCGUMsEBRDeN7HerEhed2GekLAX4G', 
    'Amount': '10000', 
    'Fee': '10', 
    'Flags': 1, 
    'LastLedgerSequence': 1401588, 
    'NFTokenID': '0008271081536F68FFB7D7B36FF2D67B0B89C01B7AA88B99B2293CCD00156132', 
    'Sequence': 1401142, 
    'SigningPubKey': 'ED760ABCAD964CA3DAEA761860C69A90E2BA1F67DE4596E7D3CB2A7C866424FEC1', 
    'TransactionType': 'NFTokenCreateOffer', 
    'TxnSignature': '0A634D8BE8CC6F407DC95C0F2AEFAB67F13FEB778D3F12B3B247CC5A805F84B32E0747C4C1D90E4403ACE738C3221F6CE4826AF64A3C042B82BD8CAE816B120D', 
    ctid': 'C01562E200000002', 
    'date': 771194861, 
    'hash': 'D3641AC267EEB138D7F5771C55A2606DDA660EDDCF7BF83996254DEC3CAAF3EB', 
    'inLedger': 1401570, 
    'ledger_index': 1401570, 
    'meta': {'AffectedNodes': [{'CreatedNode': {'LedgerEntryType': 'NFTokenOffer', 'LedgerIndex': '30924A5DB193AFC733DC932AA6FDB1EDC48D29BD6EEC5836FB1E3B037B6D330A', 'NewFields': {'Amount': '10000', 'Flags': 1, 'NFTokenID': '0008271081536F68FFB7D7B36FF2D67B0B89C01B7AA88B99B2293CCD00156132', 'Owner': 'rU8FfvCGUMsEBRDeN7HerEhed2GekLAX4G'}}}, 
    {'ModifiedNode': {'FinalFields': {'Account': 'rU8FfvCGUMsEBRDeN7HerEhed2GekLAX4G', 'Balance': '99999930', 'BurnedNFTokens': 1, 'FirstNFTokenSequence': 1401136, 'Flags': 8388608, 'MintedNFTokens': 3, 'OwnerCount': 2, 'Sequence': 1401143}, 
    'LedgerEntryType': 'AccountRoot', 
    'LedgerIndex': '927413B41EEA9F10C5F6C308018157262F6D30BA573D3702FEA2BAC432F6F5F1', 
    'PreviousFields': {'Balance': '99999940', 'OwnerCount': 1, 'Sequence': 1401142}, 
    'PreviousTxnID': '5478892C7EFFA1703A9A4B491FD6D9DBF7C5DF79FC4E24BCBCE2914388FAC9D9', 'PreviousTxnLgrSeq': 1401563}}, 
    {'CreatedNode': {'LedgerEntryType': 'DirectoryNode', 'LedgerIndex': '9601BB97F1C629BE21E0EBDCF67F9770188FAE4B7924D4C2E05E9CC38EA40B5C', 'NewFields': {'Owner': 'rU8FfvCGUMsEBRDeN7HerEhed2GekLAX4G', 'RootIndex': '9601BB97F1C629BE21E0EBDCF67F9770188FAE4B7924D4C2E05E9CC38EA40B5C'}}}, 
    {'CreatedNode': {'LedgerEntryType': 'DirectoryNode', 'LedgerIndex': 'CDFBD610B3C8B82D863EF2762DC6317F559473F37AB1CD33D443286A85B1E35B', 'NewFields': {'Flags': 2, 'NFTokenID': '0008271081536F68FFB7D7B36FF2D67B0B89C01B7AA88B99B2293CCD00156132', 'RootIndex': 'CDFBD610B3C8B82D863EF2762DC6317F559473F37AB1CD33D443286A85B1E35B'}}}], 
    'TransactionIndex': 0, 
    'TransactionResult': 'tesSUCCESS', 
    'offer_id': '30924A5DB193AFC733DC932AA6FDB1EDC48D29BD6EEC5836FB1E3B037B6D330A'}, 
    'validated': True}
    """
# Get the client
    owner_wallet = Wallet.from_seed(seed)
    client = JsonRpcClient(testnet_url)
    expiration_date = datetime.now()
    if expiration != '':
        expiration_date = xrpl.utils.datetime_to_ripple_time(expiration_date)
        expiration_date = expiration_date + int(expiration)
# Define the sell offer
    sell_offer_tx=xrpl.models.transactions.NFTokenCreateOffer(
        account=owner_wallet.address,
        nftoken_id=nftoken_id,
        amount=amount,
        destination=destination if destination != '' else None,
        expiration=expiration_date if expiration != '' else None,
        flags=1
    )
# Submit the transaction and report the results
    reply=""
    try:
        response=xrpl.transaction.submit_and_wait(sell_offer_tx,client,owner_wallet)
        reply=response.result
    except xrpl.transaction.XRPLReliableSubmissionException as e:
        reply=f"Submit failed: {e}"
    return reply

def accept_sell_offer(seed, offer_index):
    """accept_sell_offer"""
    buyer_wallet=Wallet.from_seed(seed)
    client=JsonRpcClient(testnet_url)
    accept_offer_tx=xrpl.models.transactions.NFTokenAcceptOffer(
       account=buyer_wallet.classic_address,
       nftoken_sell_offer=offer_index
    )
# Submit the transaction and report the results
    reply=""
    try:
        response=xrpl.transaction.submit_and_wait(accept_offer_tx,client,buyer_wallet)
        reply=response.result
    except xrpl.transaction.XRPLReliableSubmissionException as e:
        reply=f"Submit failed: {e}"
    return reply


def create_buy_offer(seed, amount, nft_id, owner, expiration, destination):
    """create_buy_offer"""
# Get the client
    buyer_wallet=Wallet.from_seed(seed)
    client=JsonRpcClient(testnet_url)
    expiration_date=datetime.now()
    if (expiration!=''):
        expiration_date=xrpl.utils.datetime_to_ripple_time(expiration_date)
        expiration_date=expiration_date + int(expiration)
# Define the buy offer transaction with an expiration date
    buy_offer_tx=xrpl.models.transactions.NFTokenCreateOffer(
        account=buyer_wallet.address,
        nftoken_id=nft_id,
        amount=amount,
        owner=owner,
        expiration=expiration_date if expiration!='' else None,
        destination=destination if destination!='' else None,
        flags=0
    )
# Submit the transaction and report the results
    reply=""
    try:
        response=xrpl.transaction.submit_and_wait(buy_offer_tx,client,buyer_wallet)
        reply=response.result
    except xrpl.transaction.XRPLReliableSubmissionException as e:
        reply=f"Submit failed: {e}"
    return reply


def accept_buy_offer(seed, offer_index):
    """accept_buy_offer"""
    buyer_wallet=Wallet.from_seed(seed)
    client=JsonRpcClient(testnet_url)
    accept_offer_tx=xrpl.models.transactions.NFTokenAcceptOffer(
       account=buyer_wallet.address,
       nftoken_buy_offer=offer_index
    )
# Submit the transaction and report the results
    reply=""
    try:
        response=xrpl.transaction.submit_and_wait(accept_offer_tx,client,buyer_wallet)
        reply=response.result
    except xrpl.transaction.XRPLReliableSubmissionException as e:
        reply=f"Submit failed: {e}"
    return reply

def get_offers(nft_id):
    """get_offers"""
    client=JsonRpcClient(testnet_url)
    offers=NFTBuyOffers(
        nft_id=nft_id
    )
    response=client.request(offers)
    allOffers="Buy Offers:\n"+json.dumps(response.result, indent=4)
    offers=NFTSellOffers(
        nft_id=nft_id
    )
    response=client.request(offers)
    allOffers+="\n\nSell Offers:\n"+json.dumps(response.result, indent=4)
    return allOffers

def cancel_offer(seed, nftoken_offer_ids):
    """cancel_offer"""
    owner_wallet=Wallet.from_seed(seed)
    client=JsonRpcClient(testnet_url)
    tokenOfferIDs=[nftoken_offer_ids]
    nftSellOffers="No sell offers"
    cancel_offer_tx=xrpl.models.transactions.NFTokenCancelOffer(
				account=owner_wallet.classic_address,
				nftoken_offers=tokenOfferIDs
    )
    reply=""
    try:
        response=xrpl.transaction.submit_and_wait(cancel_offer_tx,client,owner_wallet)
        reply=response.result
    except xrpl.transaction.XRPLReliableSubmissionException as e:
        reply=f"Submit failed: {e}"
    return reply



### test runs 

# print(get_account("sEdViuetxoebhTv9nwdDDyUnb8qFCTW")) seed 
# print(get_account_info("rHsMGQEkVNJmpGWs8XUBoTBiAAbwxZN5v3")) accoundID
# print(send_xrp("sEdS54WpwoEPy8m5gyErYiPbWg7Rc9R", 100000, "sEd7fok4C1xEPEB2rDS5dSo6oVpzY4Z"))
# print(configure_account("sEd7QUQq8Y7BYp6iKpk768vvLbhZ6Hw", True ))
# print(mint_token("sEd7QUQq8Y7BYp6iKpk768vvLbhZ6Hw", "ipfs://bafybeigdyrzt5sfp7udm7hu76uh7y26nf4dfuylqabf3oclgtqy55fbzdi", 8, 10000, 0))
# print(mint_token("sEd7QUQq8Y7BYp6iKpk768vvLbhZ6Hw", "ipfs://bafybeigdyrzt5sfp7udm7hu76uh7y26nf4dfuylqabf3oclgtqy55fbzdi", 8, 10000, 0))
# print(get_tokens("rU8FfvCGUMsEBRDeN7HerEhed2GekLAX4G"))
# print(burn_token("sEd7QUQq8Y7BYp6iKpk768vvLbhZ6Hw", "0008271081536F68FFB7D7B36FF2D67B0B89C01B7AA88B999B436BCC00156131"))
# print(create_sell_offer("sEd7QUQq8Y7BYp6iKpk768vvLbhZ6Hw", "10000", "0008271081536F68FFB7D7B36FF2D67B0B89C01B7AA88B99B2293CCD00156132", "10000",""))
print(accept_sell_offer())

# def accept_sell_offer(seed, offer_index):
