namespace SchoolGradingSystem.Models
{
  /// <summary>
  /// Represents a student with their academic information and provides grade calculation
  /// </summary>
  public class Student
  {
    /// <summary>
    /// Unique identifier for the student
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Full name of the student (first name + last name)
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// Numeric score achieved by the student (0-100 range expected)
    /// </summary>
    public int Score { get; set; }

    /// <summary>
    /// Constructor to initialize all student properties
    /// </summary>
    /// <param name="id">Student's unique identifier</param>
    /// <param name="fullName">Student's complete name</param>
    /// <param name="score">Student's numeric score</param>
    public Student(int id, string fullName, int score)
    {
      Id = id;
      FullName = fullName;
      Score = score;
    }

    /// <summary>
    /// Calculates and returns the letter grade based on the numeric score
    /// Uses standard grading scale: A(80-100), B(70-79), C(60-69), D(50-59), F(below 50)
    /// </summary>
    /// <returns>Letter grade as a string</returns>
    public string GetGrade()
    {
      // Use pattern matching with relational patterns for clean grade assignment
      return Score switch
      {
        >= 80 and <= 100 => "A",  // Excellent: 80-100
        >= 70 and <= 79 => "B",   // Good: 70-79
        >= 60 and <= 69 => "C",   // Average: 60-69
        >= 50 and <= 59 => "D",   // Below Average: 50-59
        _ => "F"                   // Failing: Below 50 (also handles invalid scores above 100)
      };
    }

    /// <summary>
    /// Override ToString for better display formatting
    /// </summary>
    /// <returns>Formatted string representation of the student</returns>
    public override string ToString()
    {
      return $"{FullName} (ID: {Id}): Score = {Score}, Grade = {GetGrade()}";
    }
  }
}