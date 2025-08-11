using System;
using FinanceManagement.Models;

namespace FinanceManagement.Accounts
{
  /// <summary>
  /// Sealed class representing a savings account with special withdrawal restrictions.
  /// Sealed prevents further inheritance, ensuring this implementation cannot be modified.
  /// </summary>
  public sealed class SavingsAccount : Account
  {
    /// <summary>
    /// Constructor for SavingsAccount that calls the base Account constructor
    /// </summary>
    /// <param name="accountNumber">Unique identifier for the savings account</param>
    /// <param name="initialBalance">Starting balance for the savings account</param>
    public SavingsAccount(string accountNumber, decimal initialBalance)
        : base(accountNumber, initialBalance) // Call parent constructor
    {
      // Additional initialization could be added here if needed
      Console.WriteLine($"💰 Savings Account {accountNumber} created with initial balance: ${initialBalance:F2}");
    }

    /// <summary>
    /// Override the base ApplyTransaction method with savings-specific logic.
    /// Implements overdraft protection by checking sufficient funds before allowing withdrawal.
    /// </summary>
    /// <param name="transaction">Transaction to apply to this savings account</param>
    public override void ApplyTransaction(Transaction transaction)
    {
      Console.WriteLine($"\n--- Processing transaction for Savings Account {AccountNumber} ---");

      // Get the absolute amount to check against balance
      decimal transactionAmount = Math.Abs(transaction.Amount);

      // Check if there are sufficient funds for this transaction
      if (transactionAmount > Balance)
      {
        // Insufficient funds - reject the transaction
        Console.WriteLine($"❌ Insufficient funds!");
        Console.WriteLine($"   Attempted withdrawal: ${transactionAmount:F2}");
        Console.WriteLine($"   Available balance: ${Balance:F2}");
        Console.WriteLine($"   Transaction declined for {transaction.Category}");
        return; // Exit without processing the transaction
      }

      // Sufficient funds available - process the transaction
      Console.WriteLine($"✅ Sufficient funds available");
      Console.WriteLine($"   Current balance: ${Balance:F2}");
      Console.WriteLine($"   Transaction amount: ${transactionAmount:F2}");

      // Apply the transaction (deduct from balance)
      Balance -= transactionAmount;

      // Display updated balance
      Console.WriteLine($"   Updated balance: ${Balance:F2}");
      Console.WriteLine($"   Transaction completed for {transaction.Category}");
    }
  }
}