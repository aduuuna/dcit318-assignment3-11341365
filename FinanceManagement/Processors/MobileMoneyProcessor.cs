using System;
using FinanceManagement.Interfaces;
using FinanceManagement.Models;

namespace FinanceManagement.Processors
{
  /// <summary>
  /// Concrete implementation for processing mobile money transactions.
  /// Handles mobile payment platform specific processing logic.
  /// </summary>
  public class MobileMoneyProcessor : ITransactionProcessor
  {
    /// <summary>
    /// Process transaction through mobile money platform
    /// </summary>
    /// <param name="transaction">Transaction to process via mobile money</param>
    public void Process(Transaction transaction)
    {
      // Print processing message specific to mobile money
      // Emphasizes mobile/digital nature of the transaction
      Console.WriteLine($"📱 Processing Mobile Money Payment: ${Math.Abs(transaction.Amount):F2} for {transaction.Category}");
      Console.WriteLine($"   Transaction ID: {transaction.Id} | Date: {transaction.Date:yyyy-MM-dd}");
      Console.WriteLine($"   Status: Mobile payment processed instantly");
    }
  }
}