using System;
using FinanceManagement.Interfaces;
using FinanceManagement.Models;

namespace FinanceManagement.Processors
{
  /// <summary>
  /// Concrete implementation for processing bank transfer transactions.
  /// Implements specific logic for traditional banking operations.
  /// </summary>
  public class BankTransferProcessor : ITransactionProcessor
  {
    /// <summary>
    /// Process transaction through bank transfer system
    /// </summary>
    /// <param name="transaction">Transaction to process via bank transfer</param>
    public void Process(Transaction transaction)
    {
      // Print processing message specific to bank transfers
      // Shows the amount being processed and the category
      Console.WriteLine($"🏦 Processing Bank Transfer: ${Math.Abs(transaction.Amount):F2} for {transaction.Category}");
      Console.WriteLine($"   Transaction ID: {transaction.Id} | Date: {transaction.Date:yyyy-MM-dd}");
      Console.WriteLine($"   Status: Bank transfer initiated successfully");
    }
  }
}