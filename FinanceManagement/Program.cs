using System;
using FinanceManagement.App;

namespace FinanceManagement
{
  /// <summary>
  /// Entry point class for the Finance Management System application.
  /// Contains the Main method that starts the program execution.
  /// </summary>
  class Program
  {
    /// <summary>
    /// Main method - the entry point of the application.
    /// Creates and runs the FinanceApp to demonstrate the system functionality.
    /// </summary>
    /// <param name="args">Command line arguments (not used in this application)</param>
    static void Main(string[] args)
    {
      try
      {
        // Create an instance of the FinanceApp
        var financeApp = new FinanceApp();

        // Run the finance management system demonstration
        financeApp.Run();

        // Wait for user input before closing (helpful for debugging)
        Console.WriteLine("\n🔚 Press any key to exit...");
        Console.ReadKey();
      }
      catch (Exception ex)
      {
        // Handle any unexpected errors gracefully
        Console.WriteLine($"❌ An error occurred: {ex.Message}");
        Console.WriteLine("📞 Please contact support for assistance.");

        // Wait for user input before closing
        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
      }
    }
  }
}