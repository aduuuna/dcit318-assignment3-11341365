namespace SchoolGradingSystem.Exceptions
{
  /// <summary>
  /// Custom exception thrown when a score value cannot be parsed into a valid integer.
  /// This occurs when the input file contains non-numeric values in the score field.
  /// </summary>
  public class InvalidScoreFormatException : Exception
  {
    /// <summary>
    /// Constructor that accepts a custom error message
    /// </summary>
    /// <param name="message">Description of the invalid score format error</param>
    public InvalidScoreFormatException(string message) : base(message)
    {
    }

    /// <summary>
    /// Constructor that accepts a custom message and inner exception for exception chaining
    /// </summary>
    /// <param name="message">Description of the invalid score format error</param>
    /// <param name="innerException">The original exception that caused this parsing error</param>
    public InvalidScoreFormatException(string message, Exception innerException) : base(message, innerException)
    {
    }
  }
}