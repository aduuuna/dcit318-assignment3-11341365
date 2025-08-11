using System;

namespace HealthcareSystem.Models
{
  /// <summary>
  /// Represents a medical prescription in the healthcare system.
  /// Links medications to specific patients with issue dates.
  /// </summary>
  public class Prescription
  {
    /// <summary>
    /// Unique identifier for the prescription
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Foreign key reference to the Patient who received this prescription
    /// </summary>
    public int PatientId { get; set; }

    /// <summary>
    /// Name of the prescribed medication
    /// </summary>
    public string MedicationName { get; set; }

    /// <summary>
    /// Date when the prescription was issued
    /// </summary>
    public DateTime DateIssued { get; set; }

    /// <summary>
    /// Constructor to initialize all prescription fields
    /// Ensures every Prescription is created with complete information
    /// </summary>
    /// <param name="id">Unique prescription identifier</param>
    /// <param name="patientId">ID of the patient receiving the prescription</param>
    /// <param name="medicationName">Name of the prescribed medication</param>
    /// <param name="dateIssued">Date the prescription was issued</param>
    public Prescription(int id, int patientId, string medicationName, DateTime dateIssued)
    {
      // Validate inputs to maintain data integrity
      if (id <= 0)
        throw new ArgumentException("Prescription ID must be positive.", nameof(id));

      if (patientId <= 0)
        throw new ArgumentException("Patient ID must be positive.", nameof(patientId));

      if (string.IsNullOrWhiteSpace(medicationName))
        throw new ArgumentException("Medication name cannot be null or empty.", nameof(medicationName));

      if (dateIssued > DateTime.Now)
        throw new ArgumentException("Prescription date cannot be in the future.", nameof(dateIssued));

      // Assign validated values to properties
      Id = id;
      PatientId = patientId;
      MedicationName = medicationName;
      DateIssued = dateIssued;
    }

    /// <summary>
    /// Override ToString to provide a readable representation of the prescription
    /// Useful for displaying prescription information in console output
    /// </summary>
    /// <returns>Formatted string with prescription details</returns>
    public override string ToString()
    {
      return $"💊 Prescription [ID: {Id}] {MedicationName} for Patient {PatientId} | Issued: {DateIssued:yyyy-MM-dd}";
    }
  }
}
