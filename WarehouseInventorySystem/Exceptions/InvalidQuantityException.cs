namespace WarehouseInventorySystem.Exceptions
{
  /// <summary>
  /// Custom exception thrown when attempting to set an invalid quantity (e.g., negative values)
  /// </summary>
  public class InvalidQuantityException : Exception
  {
    /// <summary>
    /// Constructor that accepts a custom error message
    /// </summary>
    /// <param name="message">Description of the invalid quantity error</param>
    public InvalidQuantityException(string message) : base(message)
    {
    }

    /// <summary>
    /// Constructor that accepts a custom message and inner exception for exception chaining
    /// </summary>
    /// <param name="message">Description of the invalid quantity error</param>
    /// <param name="innerException">The original exception that caused this error</param>
    public InvalidQuantityException(string message, Exception innerException) : base(message, innerException)
    {
    }
  }
}