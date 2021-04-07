using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stellar
{
    public class StellarCore
    {
        [Obsolete]
        public static async void CreateTransaction(string sourceSecretKey, string destinationPublicKey, Asset asset, int value, string memo)
        {
            try
            {
                var server = new stellar_dotnet_sdk.Server("https://horizon-testnet.stellar.org");
                KeyPair source = KeyPair.FromSecretSeed(sourceSecretKey);
                KeyPair destination = KeyPair.FromAccountId(destinationPublicKey);
                var destinationAccount = await server.Accounts.Account(destination.AccountId);                                
                var sourceAccount = await server.Accounts.Account(source.AccountId);
                if (destinationAccount == null || sourceAccount == null)
                {
                    Console.WriteLine("The account does not exist!");
                    return;                    
                }
                var transactionBuilder = new Transaction.Builder(sourceAccount);
                transactionBuilder.AddOperation(new PaymentOperation.Builder(destinationAccount.MuxedAccount, asset, "10").Build());
                transactionBuilder.AddMemo(Memo.Text(memo));
                transactionBuilder.AddTimeBounds(new TimeBounds(0, 0));                 
                var transaction = transactionBuilder.Build();                
                transaction.Sign(source.SigningKey, Network.Test());
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
