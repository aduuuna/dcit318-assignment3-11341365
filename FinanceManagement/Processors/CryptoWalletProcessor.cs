using System;
using FinanceManagement.Interfaces;
using FinanceManagement.Models;

namespace FinanceManagement.Processors
{
  /// <summary>
  /// Concrete implementation for processing cryptocurrency wallet transactions.
  /// Handles blockchain-based payment processing logic.
  /// </summary>
  public class CryptoWalletProcessor : ITransactionProcessor
  {
    /// <summary>
    /// Process transaction through cryptocurrency wallet
    /// </summary>
    /// <param name="transaction">Transaction to process via crypto wallet</param>
    public void Process(Transaction transaction)
    {
      // Print processing message specific to cryptocurrency
      // Highlights decentralized and blockchain nature
      Console.WriteLine($"₿ Processing Crypto Wallet Transaction: ${Math.Abs(transaction.Amount):F2} for {transaction.Category}");
      Console.WriteLine($"   Transaction ID: {transaction.Id} | Date: {transaction.Date:yyyy-MM-dd}");
      Console.WriteLine($"   Status: Blockchain transaction confirmed");
    }
  }
}