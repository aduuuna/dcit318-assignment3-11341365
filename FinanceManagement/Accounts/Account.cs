using System;
using FinanceManagement.Models;

namespace FinanceManagement.Accounts
{
  /// <summary>
  /// Base class for all account types in the finance management system.
  /// Provides common functionality that can be inherited and extended.
  /// </summary>
  public class Account
  {
    /// <summary>
    /// Public property for account identification
    /// Can be read by external classes but only set internally
    /// </summary>
    public string AccountNumber { get; private set; }

    /// <summary>
    /// Protected property for account balance
    /// Can be modified by derived classes but not external classes
    /// This ensures balance modifications go through proper channels
    /// </summary>
    public decimal Balance { get; protected set; }

    /// <summary>
    /// Constructor to initialize a new account
    /// </summary>
    /// <param name="accountNumber">Unique identifier for the account</param>
    /// <param name="initialBalance">Starting balance for the account</param>
    public Account(string accountNumber, decimal initialBalance)
    {
      // Validate that account number is not null or empty
      if (string.IsNullOrWhiteSpace(accountNumber))
        throw new ArgumentException("Account number cannot be null or empty.", nameof(accountNumber));

      // Validate that initial balance is not negative
      if (initialBalance < 0)
        throw new ArgumentException("Initial balance cannot be negative.", nameof(initialBalance));

      AccountNumber = accountNumber;
      Balance = initialBalance;
    }

    /// <summary>
    /// Virtual method to apply a transaction to the account.
    /// Virtual allows derived classes to override with specialized behavior.
    /// </summary>
    /// <param name="transaction">Transaction to apply to this account</param>
    public virtual void ApplyTransaction(Transaction transaction)
    {
      // Basic implementation: deduct the transaction amount from balance
      // Note: We use Math.Abs to ensure we're always deducting a positive amount
      Balance -= Math.Abs(transaction.Amount);

      // Provide feedback about the transaction
      Console.WriteLine($"💳 Applied transaction to Account {AccountNumber}");
      Console.WriteLine($"   Amount deducted: ${Math.Abs(transaction.Amount):F2}");
      Console.WriteLine($"   New balance: ${Balance:F2}");
    }
  }
}
