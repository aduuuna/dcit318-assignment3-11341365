using FinanceManagement.Models;

namespace FinanceManagement.Interfaces
{
  /// <summary>
  /// Interface defining contract for transaction processing.
  /// This enables different payment methods to implement their own processing logic.
  /// </summary>
  public interface ITransactionProcessor
  {
    /// <summary>
    /// Process a financial transaction using specific payment method logic
    /// </summary>
    /// <param name="transaction">The transaction to process</param>
    void Process(Transaction transaction);
  }
}