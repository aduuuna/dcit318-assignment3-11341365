// Program.cs
using System;
using InventoryRecordSystem.Applications;

namespace InventoryRecordSystem
{
  class Program
  {
    /// <summary>
    /// Main program flow:
    /// 1. Create an InventoryApp instance.
    /// 2. Seed sample data and save to disk.
    /// 3. Simulate a new session by creating a new InventoryApp instance.
    /// 4. Load data from disk and print items to verify persistence.
    /// </summary>
    static void Main(string[] args)
    {
      Console.WriteLine("=== InventoryRecordSystem ===\n");

      // 1) Create app and seed then save
      var appWriter = new InventoryApp();
      appWriter.SeedSampleData();
      appWriter.SaveData();

      // Simulate end-of-session/clear memory by creating a new app instance
      Console.WriteLine("\n--- Simulating new session (clearing memory) ---\n");

      // 2) Create a fresh app and load previously saved data
      var appReader = new InventoryApp();
      appReader.LoadData();

      // 3) Print loaded items to confirm persistence
      appReader.PrintAllItems();

      Console.WriteLine("\nPress any key to exit...");
      Console.ReadKey();
    }
  }
}
