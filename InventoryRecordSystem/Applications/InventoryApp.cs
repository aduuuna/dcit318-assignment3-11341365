// Applications/InventoryApp.cs
using System;
using System.IO;
using InventoryRecordSystem.Records;
using InventoryRecordSystem.Services;

namespace InventoryRecordSystem.Applications
{
  /// <summary>
  /// Integration layer which uses InventoryLogger to seed data, save, load, and print items.
  /// </summary>
  public class InventoryApp
  {
    // Use a single file under the Data folder for persistence
    private static readonly string DefaultDataFile = Path.Combine("Data", "inventory.json");

    // Logger for InventoryItem; manages add / save / load functionality
    private readonly InventoryLogger<InventoryItem> _logger;

    /// <summary>
    /// Create an InventoryApp using the default data file.
    /// You can change the path here if you want to store the JSON elsewhere.
    /// </summary>
    public InventoryApp()
    {
      _logger = new InventoryLogger<InventoryItem>(DefaultDataFile);
    }

    /// <summary>
    /// Adds 3-5 sample InventoryItem entries into the logger in memory.
    /// These items are immutable records created with the current DateTime.
    /// </summary>
    public void SeedSampleData()
    {
      // Create several immutable InventoryItem records
      var item1 = new InventoryItem(1, "USB-C Cable", 30, DateTime.UtcNow);
      var item2 = new InventoryItem(2, "Wireless Mouse", 15, DateTime.UtcNow);
      var item3 = new InventoryItem(3, "Mechanical Keyboard", 8, DateTime.UtcNow);
      var item4 = new InventoryItem(4, "27\" Monitor", 5, DateTime.UtcNow);
      var item5 = new InventoryItem(5, "Laptop Stand", 12, DateTime.UtcNow);

      // Add to the logger (in-memory)
      _logger.Add(item1);
      _logger.Add(item2);
      _logger.Add(item3);
      _logger.Add(item4);
      _logger.Add(item5);

      Console.WriteLine("Seeded sample data (in-memory).");
    }

    /// <summary>
    /// Persist the current in-memory log to disk.
    /// </summary>
    public void SaveData()
    {
      _logger.SaveToFile();
    }

    /// <summary>
    /// Load persisted data from disk into memory.
    /// </summary>
    public void LoadData()
    {
      _logger.LoadFromFile();
    }

    /// <summary>
    /// Print all items currently in memory (after LoadData or SeedSampleData).
    /// </summary>
    public void PrintAllItems()
    {
      var items = _logger.GetAll();

      if (items.Count == 0)
      {
        Console.WriteLine("No items available to print.");
        return;
      }

      Console.WriteLine("Inventory items:");
      Console.WriteLine("----------------");

      foreach (var it in items)
      {
        // Print each item's properties - DateAdded formatted for readability
        Console.WriteLine($"Id: {it.Id}");
        Console.WriteLine($"Name: {it.Name}");
        Console.WriteLine($"Quantity: {it.Quantity}");
        Console.WriteLine($"DateAdded (UTC): {it.DateAdded:yyyy-MM-dd HH:mm:ss}"); // use UTC format
        Console.WriteLine();
      }
    }
  }
}
