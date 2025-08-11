namespace WarehouseInventorySystem.Exceptions
{
  /// <summary>
  /// Custom exception thrown when attempting to access or modify an item that doesn't exist in the inventory
  /// </summary>
  public class ItemNotFoundException : Exception
  {
    /// <summary>
    /// Constructor that accepts a custom error message
    /// </summary>
    /// <param name="message">Description of the item not found error</param>
    public ItemNotFoundException(string message) : base(message)
    {
    }

    /// <summary>
    /// Constructor that accepts a custom message and inner exception for exception chaining
    /// </summary>
    /// <param name="message">Description of the item not found error</param>
    /// <param name="innerException">The original exception that caused this error</param>
    public ItemNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
  }
}