using SchoolGradingSystem.Models;
using SchoolGradingSystem.Exceptions;

namespace SchoolGradingSystem.Services
{
  /// <summary>
  /// Service class responsible for processing student data from input files
  /// and generating formatted report files with grades and validation.
  /// </summary>
  public class StudentResultProcessor
  {
    /// <summary>
    /// Reads student data from a text file and validates each record.
    /// Expected format: "StudentID,FullName,Score" per line.
    /// </summary>
    /// <param name="inputFilePath">Path to the input file containing student records</param>
    /// <returns>List of validated Student objects</returns>
    /// <exception cref="FileNotFoundException">Thrown when input file doesn't exist</exception>
    /// <exception cref="MissingFieldException">Thrown when a record has missing fields</exception>
    /// <exception cref="InvalidScoreFormatException">Thrown when score cannot be parsed</exception>
    public List<Student> ReadStudentsFromFile(string inputFilePath)
    {
      List<Student> students = new List<Student>();
      int lineNumber = 0;

      // Use 'using' statement to ensure StreamReader is properly disposed
      using (StreamReader reader = new StreamReader(inputFilePath))
      {
        string? line;

        // Read file line by line until end of file
        while ((line = reader.ReadLine()) != null)
        {
          lineNumber++;

          // Skip empty lines or lines with only whitespace
          if (string.IsNullOrWhiteSpace(line))
          {
            Console.WriteLine($"⚠️  Skipping empty line {lineNumber}");
            continue;
          }

          try
          {
            // Process the current line and add valid student to collection
            Student student = ProcessStudentLine(line, lineNumber);
            students.Add(student);
          }
          catch (SchoolGradingSystem.Exceptions.MissingFieldException ex)

          {
            Console.WriteLine($"❌ Line {lineNumber}: {ex.Message}");
            throw; // Re-throw to be handled by calling method
          }
          catch (InvalidScoreFormatException ex)
          {
            Console.WriteLine($"❌ Line {lineNumber}: {ex.Message}");
            throw; // Re-throw to be handled by calling method
          }
        }
      }

      Console.WriteLine($"✅ Successfully processed {students.Count} student records from file.");
      return students;
    }

    /// <summary>
    /// Processes a single line of student data and creates a Student object.
    /// </summary>
    /// <param name="line">Raw line from input file</param>
    /// <param name="lineNumber">Current line number for error reporting</param>
    /// <returns>Validated Student object</returns>
    private Student ProcessStudentLine(string line, int lineNumber)
    {
      // Split the line by comma separator
      string[] fields = line.Split(',');

      // Validate that we have exactly 3 fields (ID, Name, Score)
      if (fields.Length != 3)
      {
        throw new SchoolGradingSystem.Exceptions.MissingFieldException(
            $"Expected 3 fields (ID,Name,Score) but found {fields.Length} in line: '{line}'"
        );
      }

      // Trim whitespace from all fields
      string idString = fields[0].Trim();
      string fullName = fields[1].Trim();
      string scoreString = fields[2].Trim();

      // Validate that no field is empty after trimming
      if (string.IsNullOrEmpty(idString) || string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(scoreString))
      {
        throw new SchoolGradingSystem.Exceptions.MissingFieldException(
            $"One or more fields are empty in line: '{line}'"
        );
      }

      // Try to parse the student ID to integer
      if (!int.TryParse(idString, out int studentId))
      {
        throw new InvalidScoreFormatException(
            $"Student ID '{idString}' is not a valid integer in line: '{line}'"
        );
      }

      // Try to parse the score to integer
      if (!int.TryParse(scoreString, out int score))
      {
        throw new InvalidScoreFormatException(
            $"Score '{scoreString}' is not a valid integer in line: '{line}'"
        );
      }

      // Create and return the validated Student object
      return new Student(studentId, fullName, score);
    }

    /// <summary>
    /// Writes a formatted report of all students with their grades to an output file.
    /// Creates a clean summary with student details and calculated grades.
    /// </summary>
    /// <param name="students">List of Student objects to include in the report</param>
    /// <param name="outputFilePath">Path where the report file will be created</param>
    public void WriteReportToFile(List<Student> students, string outputFilePath)
    {
      // Use 'using' statement to ensure StreamWriter is properly disposed
      using (StreamWriter writer = new StreamWriter(outputFilePath))
      {
        // Write report header with timestamp
        writer.WriteLine("STUDENT GRADE REPORT");
        writer.WriteLine("===================");
        writer.WriteLine($"Generated on: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        writer.WriteLine($"Total Students: {students.Count}");
        writer.WriteLine();

        // Check if there are students to report
        if (students.Count == 0)
        {
          writer.WriteLine("No students found in the input file.");
          return;
        }

        // Write individual student records
        writer.WriteLine("INDIVIDUAL RESULTS:");
        writer.WriteLine("------------------");

        foreach (Student student in students)
        {
          // Use the Student's ToString method for consistent formatting
          writer.WriteLine(student.ToString());
        }

        // Calculate and write summary statistics
        writer.WriteLine();
        writer.WriteLine("SUMMARY STATISTICS:");
        writer.WriteLine("------------------");

        // Group students by grade and count them
        var gradeDistribution = students
            .GroupBy(s => s.GetGrade())
            .OrderBy(g => g.Key)
            .ToDictionary(g => g.Key, g => g.Count());

        foreach (var grade in gradeDistribution)
        {
          writer.WriteLine($"Grade {grade.Key}: {grade.Value} students");
        }

        // Calculate average score
        double averageScore = students.Average(s => s.Score);
        writer.WriteLine($"Average Score: {averageScore:F2}");

        // Find highest and lowest scores
        int highestScore = students.Max(s => s.Score);
        int lowestScore = students.Min(s => s.Score);
        writer.WriteLine($"Highest Score: {highestScore}");
        writer.WriteLine($"Lowest Score: {lowestScore}");
      }

      Console.WriteLine($"✅ Report successfully written to: {outputFilePath}");
    }
  }
}