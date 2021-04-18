using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stellar
{    
    public class StellarCore
    {
        public class StellarAccount
        {
            string Email { get; set; }
            string PublicKey { get; set; }
            string SecretKey { get; set; }  
            List<StellarTransaction> HistoryOfTransactions { get; set; }            
            public StellarAccount()
            {
                HistoryOfTransactions = new List<StellarTransaction>();
            }
        }
        public class Draft
        {
            StellarAccount SellerAccount { get; set; }
            StellarAccount BuyerAccount { get; set; }
            StellarAccount MiddleManAccount { get; set; }
            DateTime ValitUntil { get; set; }
            string Value { get; set; }
            string TransactionDescription { get; set; }
            List<string> Files { get; set; }
            public Draft()
            {
                Files = new List<string>();
            }
        }
        public class StellarTransaction
        {
            StellarAccount SourceAccount { get; set; }
            StellarAccount DestinationAccount { get; set; }
            string Value { get; set; }
            string Memo { get; set; }
            Asset asset { get; set; }
        }
        public void CreateDraft(StellarAccount sellerAccount, StellarAccount buyerAccount, StellarAccount middlemanAccount, string transactionDescription, string value, DateTime validUntil, List<string> files) { }
        public static async void GetTransactions(string accountPublicKey)
        {
            Server server = new Server("https://horizon-testnet.stellar.org");
            var transactionsHistory = await server.Transactions.ForAccount(accountPublicKey).Execute();
            foreach(TransactionResponse transaction in transactionsHistory.Records)
            {
                Console.WriteLine(transaction.MemoValue);
            }            
        }
        public static async void GetAccountBalance(string accountPublicKey)
        {            
            Network network = new Network("Test SDF Network ; September 2015");
            Server server = new Server("https://horizon-testnet.stellar.org");
            
            KeyPair keypair = KeyPair.FromAccountId(accountPublicKey);
            
            AccountResponse accountResponse = await server.Accounts.Account(keypair.AccountId);
            
            Balance[] balances = accountResponse.Balances;
          
            for (int i = 0; i < balances.Length; i++)
            {
                Balance asset = balances[i];
                Console.WriteLine("Asset Code: " + asset.AssetType);
                Console.WriteLine("Asset Amount: " + asset.BalanceString);
            }
        }
        [Obsolete]
        public static async void CreateTransaction(string sourceSecretKey, string destinationPublicKey, Asset asset, string value, string memo)
        {
            try
            {
                Network network = new Network("Test SDF Network ; September 2015");
                Server server = new Server("https://horizon-testnet.stellar.org");                
                KeyPair source = KeyPair.FromSecretSeed(sourceSecretKey);
                KeyPair destination = KeyPair.FromAccountId(destinationPublicKey);
                var destinationAccount = await server.Accounts.Account(destination.AccountId);                                
                var sourceAccount = await server.Accounts.Account(source.AccountId);
                if (destinationAccount == null || sourceAccount == null)
                {
                    Console.WriteLine("The account does not exist!");
                    return;                    
                }
                PaymentOperation operation = new PaymentOperation.Builder(destination, asset, value).SetSourceAccount(sourceAccount.KeyPair).Build();
                Transaction transaction = new Transaction.Builder(sourceAccount).AddOperation(operation).Build();
                transaction.Sign(source.SigningKey, network);
                SubmitTransactionResponse response = await server.SubmitTransaction(transaction);                   
                if(response.IsSuccess())
                {
                    Console.WriteLine("Success!");
                    Console.WriteLine(response.Hash);
                }
                else
                {
                    Console.WriteLine("Something went wrong!");
                    Console.WriteLine(response.Result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());                
            }
        }   
    }
}
