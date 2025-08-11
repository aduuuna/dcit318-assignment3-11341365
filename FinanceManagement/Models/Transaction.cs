using System;

namespace FinanceManagement.Models
{
  /// <summary>
  /// Record type representing financial transaction data.
  /// Records provide immutability by default and value-based equality.
  /// </summary>
  /// <param name="Id">Unique identifier for the transaction</param>
  /// <param name="Date">When the transaction occurred</param>
  /// <param name="Amount">Transaction amount (positive for credits, negative for debits)</param>
  /// <param name="Category">Category of the transaction (e.g., "Groceries", "Utilities")</param>
  public record Transaction(int Id, DateTime Date, decimal Amount, string Category);
}