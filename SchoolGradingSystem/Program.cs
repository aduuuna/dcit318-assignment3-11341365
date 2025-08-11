using SchoolGradingSystem.Models;
using SchoolGradingSystem.Services;
using SchoolGradingSystem.Exceptions;

namespace SchoolGradingSystem
{
  /// <summary>
  /// Main program class that demonstrates the school grading system.
  /// Processes student data from files and handles various error scenarios.
  /// </summary>
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("🎓 School Grading System");
      Console.WriteLine("========================\n");

      // Define file paths
      string inputFilePath = Path.Combine("Data", "students.txt");
      string outputFilePath = Path.Combine("Data", "report.txt");

      // Create Data directory if it doesn't exist
      Directory.CreateDirectory("Data");

      // Create sample input file for demonstration
      CreateSampleInputFile(inputFilePath);

      // Initialize the processor service
      StudentResultProcessor processor = new StudentResultProcessor();

      // Wrap the entire process in comprehensive try-catch blocks
      try
      {
        Console.WriteLine("📖 Reading student data from file...");

        // Step 1: Read and validate student data from input file
        List<Student> students = processor.ReadStudentsFromFile(inputFilePath);

        Console.WriteLine($"✅ Successfully loaded {students.Count} students\n");

        // Step 2: Generate and write the formatted report
        Console.WriteLine("📝 Generating grade report...");
        processor.WriteReportToFile(students, outputFilePath);

        Console.WriteLine("\n🎉 Grade processing completed successfully!");
        Console.WriteLine($"📄 Report saved to: {outputFilePath}");

        // Display a preview of the results in console
        DisplayResultsPreview(students);
      }
      catch (FileNotFoundException ex)
      {
        Console.WriteLine($"❌ File Not Found Error: {ex.Message}");
        Console.WriteLine("Please ensure the input file exists and the path is correct.");
      }
      catch (InvalidScoreFormatException ex)
      {
        Console.WriteLine($"❌ Invalid Score Format: {ex.Message}");
        Console.WriteLine("Please check that all scores are valid integers.");
      }
      catch (SchoolGradingSystem.Exceptions.MissingFieldException ex)

      {
        Console.WriteLine($"❌ Missing Field Error: {ex.Message}");
        Console.WriteLine("Please ensure each line has exactly 3 fields: ID,Name,Score");
      }
      catch (UnauthorizedAccessException ex)
      {
        Console.WriteLine($"❌ Access Denied: {ex.Message}");
        Console.WriteLine("Please check file permissions and ensure the file is not open in another program.");
      }
      catch (IOException ex)
      {
        Console.WriteLine($"❌ File I/O Error: {ex.Message}");
        Console.WriteLine("Please check file paths and ensure sufficient disk space.");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"❌ Unexpected Error: {ex.Message}");
        Console.WriteLine("An unforeseen error occurred during processing.");
        Console.WriteLine($"Error Type: {ex.GetType().Name}");
      }

      // Demonstrate error handling with intentionally problematic data
      Console.WriteLine("\n🧪 Testing Error Handling:");
      Console.WriteLine("==========================");
      DemonstrateErrorHandling(processor);

      Console.WriteLine("\n✨ Press any key to exit...");
      Console.ReadKey();
    }

    /// <summary>
    /// Creates a sample input file with both valid and invalid data for demonstration
    /// </summary>
    /// <param name="filePath">Path where the sample file will be created</param>
    static void CreateSampleInputFile(string filePath)
    {
      string[] sampleData = {
                "101,Alice Johnson,85",
                "102,Bob Smith,72",
                "103,Carol Davis,68",
                "104,David Wilson,91",
                "105,Emma Brown,45",
                "106,Frank Miller,77",
                "107,Grace Lee,83"
            };

      File.WriteAllLines(filePath, sampleData);
      Console.WriteLine($"📁 Sample input file created: {filePath}\n");
    }

    /// <summary>
    /// Displays a preview of the processing results in the console
    /// </summary>
    /// <param name="students">List of processed students</param>
    static void DisplayResultsPreview(List<Student> students)
    {
      Console.WriteLine("\n📊 RESULTS PREVIEW:");
      Console.WriteLine("==================");

      if (students.Count > 0)
      {
        // Show first few students
        int previewCount = Math.Min(3, students.Count);
        for (int i = 0; i < previewCount; i++)
        {
          Console.WriteLine($"  {students[i]}");
        }

        if (students.Count > 3)
        {
          Console.WriteLine($"  ... and {students.Count - 3} more students");
        }

        // Show grade distribution
        var gradeDistribution = students.GroupBy(s => s.GetGrade())
            .OrderBy(g => g.Key)
            .ToDictionary(g => g.Key, g => g.Count());

        Console.WriteLine("\n📈 Grade Distribution:");
        foreach (var grade in gradeDistribution)
        {
          Console.WriteLine($"  Grade {grade.Key}: {grade.Value} students");
        }
      }
    }

    /// <summary>
    /// Demonstrates various error handling scenarios by creating problematic test files
    /// </summary>
    /// <param name="processor">The processor instance to test</param>
    static void DemonstrateErrorHandling(StudentResultProcessor processor)
    {
      // Test 1: File not found
      Console.WriteLine("\nTest 1: File not found...");
      try
      {
        processor.ReadStudentsFromFile("nonexistent.txt");
      }
      catch (FileNotFoundException)
      {
        Console.WriteLine("✅ FileNotFoundException correctly caught");
      }

      // Test 2: Invalid score format
      Console.WriteLine("\nTest 2: Invalid score format...");
      string invalidScoreFile = Path.Combine("Data", "invalid_scores.txt");
      File.WriteAllText(invalidScoreFile, "201,John Doe,ABC\n202,Jane Smith,85");

      try
      {
        processor.ReadStudentsFromFile(invalidScoreFile);
      }
      catch (InvalidScoreFormatException)
      {
        Console.WriteLine("✅ InvalidScoreFormatException correctly caught");
      }

      // Test 3: Missing fields
      Console.WriteLine("\nTest 3: Missing fields...");
      string missingFieldsFile = Path.Combine("Data", "missing_fields.txt");
      File.WriteAllText(missingFieldsFile, "301,Mary Johnson\n302,Tom Wilson,78");

      try
      {
        processor.ReadStudentsFromFile(missingFieldsFile);
      }
      catch (SchoolGradingSystem.Exceptions.MissingFieldException ex)

      {
        Console.WriteLine("✅ MissingFieldException correctly caught");
      }

      // Clean up test files
      File.Delete(invalidScoreFile);
      File.Delete(missingFieldsFile);
    }
  }
}