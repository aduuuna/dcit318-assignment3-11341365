using System;

namespace HealthcareSystem.Models
{
  /// <summary>
  /// Represents a patient in the healthcare system.
  /// This class encapsulates all patient-related information.
  /// </summary>
  public class Patient
  {
    /// <summary>
    /// Unique identifier for the patient
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Full name of the patient
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Age of the patient in years
    /// </summary>
    public int Age { get; set; }

    /// <summary>
    /// Gender of the patient (e.g., "Male", "Female", "Other")
    /// </summary>
    public string Gender { get; set; }

    /// <summary>
    /// Constructor to initialize all patient fields
    /// Ensures that every Patient object is created with complete information
    /// </summary>
    /// <param name="id">Unique patient identifier</param>
    /// <param name="name">Patient's full name</param>
    /// <param name="age">Patient's age</param>
    /// <param name="gender">Patient's gender</param>
    public Patient(int id, string name, int age, string gender)
    {
      // Validate inputs to ensure data integrity
      if (id <= 0)
        throw new ArgumentException("Patient ID must be positive.", nameof(id));

      if (string.IsNullOrWhiteSpace(name))
        throw new ArgumentException("Patient name cannot be null or empty.", nameof(name));

      if (age < 0 || age > 150)
        throw new ArgumentException("Patient age must be between 0 and 150.", nameof(age));

      if (string.IsNullOrWhiteSpace(gender))
        throw new ArgumentException("Patient gender cannot be null or empty.", nameof(gender));

      // Assign validated values to properties
      Id = id;
      Name = name;
      Age = age;
      Gender = gender;
    }

    /// <summary>
    /// Override ToString to provide a readable representation of the patient
    /// Useful for displaying patient information in console output
    /// </summary>
    /// <returns>Formatted string with patient details</returns>
    public override string ToString()
    {
      return $"👤 Patient [ID: {Id}] {Name}, Age: {Age}, Gender: {Gender}";
    }
  }
}