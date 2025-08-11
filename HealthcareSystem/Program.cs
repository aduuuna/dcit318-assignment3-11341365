using System;
using HealthcareSystem.App;

namespace HealthcareSystem
{
  /// <summary>
  /// Entry point class for the Healthcare Management System.
  /// Demonstrates the integration of generics, collections, and type safety in C#.
  /// </summary>
  class Program
  {
    /// <summary>
    /// Main method - the application entry point.
    /// Follows the exact requirements specified in the assignment.
    /// </summary>
    /// <param name="args">Command line arguments (not used in this application)</param>
    static void Main(string[] args)
    {
      try
      {
        Console.WriteLine("🚀 Starting Healthcare Management System...\n");

        // Step i: Instantiate HealthSystemApp
        var healthApp = new HealthSystemApp();

        // Step ii: Call SeedData() - adds sample patients and prescriptions
        healthApp.SeedData();

        // Step iii: Call BuildPrescriptionMap() - creates efficient lookup structure
        healthApp.BuildPrescriptionMap();

        // Step iv: Print all patients
        healthApp.PrintAllPatients();

        // Step v: Select one PatientId and display all prescriptions
        // For demonstration, we'll show prescriptions for Patient 1
        Console.WriteLine("\n🎯 Demonstrating prescription lookup for Patient 1:");
        healthApp.PrintPrescriptionsForPatient(1);

        // Additional demonstration: Show prescriptions for all patients
        Console.WriteLine("\n🎯 Additional Demo - All Patient Prescriptions:");
        healthApp.PrintPrescriptionsForPatient(2);
        healthApp.PrintPrescriptionsForPatient(3);

        // Wait for user input before closing
        Console.WriteLine("\n🔚 Press any key to exit...");
        Console.ReadKey();
      }
      catch (Exception ex)
      {
        // Handle any unexpected errors gracefully
        Console.WriteLine($"\n❌ An error occurred: {ex.Message}");
        Console.WriteLine("📞 Please contact support for assistance.");
        Console.WriteLine($"🔍 Error details: {ex}");

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
      }
    }
  }
}