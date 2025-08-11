using System;
using System.Collections.Generic;
using System.Linq;
using HealthcareSystem.Models;
using HealthcareSystem.Repositories;

namespace HealthcareSystem.App
{
  /// <summary>
  /// Main application class that orchestrates the healthcare system.
  /// Demonstrates the use of generics, collections, and LINQ for data management.
  /// </summary>
  public class HealthSystemApp
  {
    /// <summary>
    /// Repository for managing Patient entities using generics.
    /// Demonstrates type safety - can only store Patient objects.
    /// </summary>
    private readonly Repository<Patient> _patientRepo;

    /// <summary>
    /// Repository for managing Prescription entities using generics.
    /// Same generic repository class, different type parameter.
    /// </summary>
    private readonly Repository<Prescription> _prescriptionRepo;

    /// <summary>
    /// Dictionary that maps Patient IDs to their list of prescriptions.
    /// Key: Patient ID (int)
    /// Value: List of prescriptions for that patient
    /// 
    /// This provides O(1) lookup time for finding patient prescriptions.
    /// </summary>
    private readonly Dictionary<int, List<Prescription>> _prescriptionMap;

    /// <summary>
    /// Constructor initializes all repositories and collections.
    /// </summary>
    public HealthSystemApp()
    {
      _patientRepo = new Repository<Patient>();
      _prescriptionRepo = new Repository<Prescription>();
      _prescriptionMap = new Dictionary<int, List<Prescription>>();

      Console.WriteLine("🏥 Healthcare System initialized successfully!");
    }

    /// <summary>
    /// Seeds the system with sample data for demonstration purposes.
    /// Creates patients and prescriptions to show system functionality.
    /// </summary>
    public void SeedData()
    {
      Console.WriteLine("\n📋 Seeding sample data...");
      Console.WriteLine(new string('-', 40));

      // Add sample patients to the patient repository
      Console.WriteLine("Adding Patients:");
      _patientRepo.Add(new Patient(1, "John Doe", 45, "Male"));
      _patientRepo.Add(new Patient(2, "Jane Smith", 32, "Female"));
      _patientRepo.Add(new Patient(3, "Bob Johnson", 67, "Male"));

      // Add sample prescriptions to the prescription repository
      // Note: PatientIds correspond to the patients created above
      Console.WriteLine("\nAdding Prescriptions:");
      _prescriptionRepo.Add(new Prescription(101, 1, "Aspirin", DateTime.Now.AddDays(-5)));
      _prescriptionRepo.Add(new Prescription(102, 1, "Lisinopril", DateTime.Now.AddDays(-3)));
      _prescriptionRepo.Add(new Prescription(103, 2, "Metformin", DateTime.Now.AddDays(-7)));
      _prescriptionRepo.Add(new Prescription(104, 2, "Vitamin D3", DateTime.Now.AddDays(-1)));
      _prescriptionRepo.Add(new Prescription(105, 3, "Warfarin", DateTime.Now.AddDays(-10)));

      Console.WriteLine($"\n✅ Seeding completed! Patients: {_patientRepo.Count()}, Prescriptions: {_prescriptionRepo.Count()}");
    }

    /// <summary>
    /// Builds the prescription map by grouping prescriptions by PatientId.
    /// Demonstrates LINQ GroupBy for data aggregation and Dictionary population.
    /// </summary>
    public void BuildPrescriptionMap()
    {
      Console.WriteLine("\n🗂️ Building prescription map...");

      // Clear any existing mappings
      _prescriptionMap.Clear();

      // Get all prescriptions from the repository
      var allPrescriptions = _prescriptionRepo.GetAll();

      // Group prescriptions by PatientId using LINQ
      // This creates groups where each group has a Key (PatientId) and a collection of prescriptions
      var groupedPrescriptions = allPrescriptions
          .GroupBy(prescription => prescription.PatientId)  // Group by PatientId
          .ToList();  // Convert to list for iteration

      // Populate the dictionary with grouped data
      foreach (var group in groupedPrescriptions)
      {
        int patientId = group.Key;  // The PatientId this group represents
        List<Prescription> prescriptions = group.ToList();  // All prescriptions for this patient

        // Add to our dictionary mapping
        _prescriptionMap[patientId] = prescriptions;

        Console.WriteLine($"   📋 Mapped {prescriptions.Count} prescriptions for Patient {patientId}");
      }

      Console.WriteLine($"✅ Prescription map built successfully! Mapped {_prescriptionMap.Count} patients.");
    }

    /// <summary>
    /// Retrieves all prescriptions for a specific patient using the dictionary map.
    /// Demonstrates Dictionary lookup for efficient data retrieval.
    /// </summary>
    /// <param name="patientId">The ID of the patient whose prescriptions to retrieve</param>
    /// <returns>List of prescriptions for the specified patient, or empty list if none found</returns>
    public List<Prescription> GetPrescriptionsByPatientId(int patientId)
    {
      // Check if the patient has prescriptions in our map
      if (_prescriptionMap.ContainsKey(patientId))
      {
        // Return the list of prescriptions for this patient
        // Dictionary lookup is O(1) - very efficient!
        return _prescriptionMap[patientId];
      }

      // Return empty list if patient has no prescriptions
      Console.WriteLine($"⚠️ No prescriptions found for Patient {patientId}");
      return new List<Prescription>();
    }

    /// <summary>
    /// Prints all patients currently stored in the system.
    /// Demonstrates repository usage and data display.
    /// </summary>
    public void PrintAllPatients()
    {
      Console.WriteLine("\n👥 All Patients in the System:");
      Console.WriteLine(new string('=', 50));

      // Get all patients from the repository
      var allPatients = _patientRepo.GetAll();

      // Check if we have any patients
      if (allPatients.Count == 0)
      {
        Console.WriteLine("❌ No patients found in the system.");
        return;
      }

      // Display each patient with formatted output
      foreach (var patient in allPatients)
      {
        Console.WriteLine($"   {patient}");
      }

      Console.WriteLine($"\n📊 Total Patients: {allPatients.Count}");
    }

    /// <summary>
    /// Prints all prescriptions for a specific patient.
    /// Uses the prescription map for efficient lookup.
    /// </summary>
    /// <param name="patientId">The ID of the patient whose prescriptions to display</param>
    public void PrintPrescriptionsForPatient(int patientId)
    {
      Console.WriteLine($"\n💊 Prescriptions for Patient {patientId}:");
      Console.WriteLine(new string('=', 50));

      // First, verify the patient exists
      var patient = _patientRepo.GetById(p => p.Id == patientId);
      if (patient == null)
      {
        Console.WriteLine($"❌ Patient with ID {patientId} not found in the system.");
        return;
      }

      Console.WriteLine($"Patient: {patient.Name}");
      Console.WriteLine(new string('-', 30));

      // Get prescriptions for this patient using our efficient map
      var prescriptions = GetPrescriptionsByPatientId(patientId);

      if (prescriptions.Count == 0)
      {
        Console.WriteLine("❌ No prescriptions found for this patient.");
        return;
      }

      // Display each prescription with formatted output
      foreach (var prescription in prescriptions)
      {
        Console.WriteLine($"   {prescription}");
      }

      Console.WriteLine($"\n📊 Total Prescriptions for {patient.Name}: {prescriptions.Count}");
    }

    /// <summary>
    /// Demonstration method that shows system capabilities.
    /// Called from Main() to orchestrate the entire workflow.
    /// </summary>
    public void Run()
    {
      Console.WriteLine("🏥 Healthcare Management System");
      Console.WriteLine("Demonstrating Generics, Collections, and Type Safety");
      Console.WriteLine(new string('=', 60));

      // Step 1: Populate the system with sample data
      SeedData();

      // Step 2: Build the prescription mapping for efficient lookups
      BuildPrescriptionMap();

      // Step 3: Display all patients
      PrintAllPatients();

      // Step 4: Show prescriptions for specific patients
      // Demonstrate the system by showing prescriptions for each patient
      var allPatients = _patientRepo.GetAll();
      foreach (var patient in allPatients)
      {
        PrintPrescriptionsForPatient(patient.Id);
      }

      // Step 5: Demonstrate additional functionality
      Console.WriteLine("\n🔍 System Statistics:");
      Console.WriteLine(new string('=', 30));
      Console.WriteLine($"📋 Total Patients: {_patientRepo.Count()}");
      Console.WriteLine($"💊 Total Prescriptions: {_prescriptionRepo.Count()}");
      Console.WriteLine($"🗂️ Patients with Prescriptions: {_prescriptionMap.Count}");

      Console.WriteLine("\n✅ Healthcare system demonstration completed successfully!");
    }
  }
}