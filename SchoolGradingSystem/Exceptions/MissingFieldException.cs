namespace SchoolGradingSystem.Exceptions
{
  /// <summary>
  /// Custom exception thrown when a student record line is missing required fields.
  /// Expected format: "StudentID,FullName,Score" - all three fields are mandatory.
  /// </summary>
  public class MissingFieldException : Exception
  {
    /// <summary>
    /// Constructor that accepts a custom error message
    /// </summary>
    /// <param name="message">Description of the missing field error</param>
    public MissingFieldException(string message) : base(message)
    {
    }

    /// <summary>
    /// Constructor that accepts a custom message and inner exception for exception chaining
    /// </summary>
    /// <param name="message">Description of the missing field error</param>
    /// <param name="innerException">The original exception that caused this error</param>
    public MissingFieldException(string message, Exception innerException) : base(message, innerException)
    {
    }
  }
}