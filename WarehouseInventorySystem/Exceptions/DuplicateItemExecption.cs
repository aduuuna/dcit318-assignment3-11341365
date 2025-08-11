namespace WarehouseInventorySystem.Exceptions
{
  /// <summary>
  /// Custom exception thrown when attempting to add an item with an ID that already exists in the inventory
  /// </summary>
  public class DuplicateItemException : Exception
  {
    /// <summary>
    /// Constructor that accepts a custom error message
    /// </summary>
    /// <param name="message">Description of the duplicate item error</param>
    public DuplicateItemException(string message) : base(message)
    {
    }

    /// <summary>
    /// Constructor that accepts a custom message and inner exception for exception chaining
    /// </summary>
    /// <param name="message">Description of the duplicate item error</param>
    /// <param name="innerException">The original exception that caused this error</param>
    public DuplicateItemException(string message, Exception innerException) : base(message, innerException)
    {
    }
  }
}