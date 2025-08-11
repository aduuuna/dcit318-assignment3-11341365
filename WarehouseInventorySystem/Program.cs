using WarehouseInventorySystem.Managers;
using WarehouseInventorySystem.Models;
using WarehouseInventorySystem.Exceptions;

namespace WarehouseInventorySystem
{
  /// <summary>
  /// Main program class that demonstrates the warehouse inventory management system.
  /// Shows collections, generics, and exception handling in action.
  /// </summary>
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("🏭 Warehouse Inventory Management System");
      Console.WriteLine("==========================================\n");

      // Step 1: Instantiate the WarehouseManager
      WarehouseManager warehouseManager = new WarehouseManager();

      // Step 2: Seed the system with initial data
      warehouseManager.SeedData();

      // Step 3: Print all grocery items
      Console.WriteLine("🛒 GROCERY INVENTORY:");
      Console.WriteLine("--------------------");
      warehouseManager.PrintAllItems(warehouseManager.Groceries);

      // Step 4: Print all electronic items
      Console.WriteLine("📱 ELECTRONICS INVENTORY:");
      Console.WriteLine("-------------------------");
      warehouseManager.PrintAllItems(warehouseManager.Electronics);

      // Step 5: Demonstrate exception handling scenarios
      Console.WriteLine("🧪 TESTING EXCEPTION HANDLING:");
      Console.WriteLine("==============================\n");

      // Test 1: Try to add a duplicate item (should throw DuplicateItemException)
      Console.WriteLine("Test 1: Adding duplicate item...");
      try
      {
        ElectronicItem duplicateItem = new ElectronicItem(1, "Duplicate iPhone", 10, "Apple", 12);
        warehouseManager.Electronics.AddItem(duplicateItem);
      }
      catch (DuplicateItemException ex)
      {
        Console.WriteLine($"❌ {ex.Message}");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"❌ Unexpected error: {ex.Message}");
      }
      Console.WriteLine();

      // Test 2: Try to remove a non-existent item (should throw ItemNotFoundException)
      Console.WriteLine("Test 2: Removing non-existent item...");
      warehouseManager.RemoveItemById(warehouseManager.Electronics, 999);
      Console.WriteLine();

      // Test 3: Try to update with invalid (negative) quantity (should throw InvalidQuantityException)
      Console.WriteLine("Test 3: Setting invalid negative quantity...");
      try
      {
        warehouseManager.Electronics.UpdateQuantity(1, -5);
      }
      catch (InvalidQuantityException ex)
      {
        Console.WriteLine($"❌ {ex.Message}");
      }
      catch (ItemNotFoundException ex)
      {
        Console.WriteLine($"❌ {ex.Message}");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"❌ Unexpected error: {ex.Message}");
      }
      Console.WriteLine();

      // Bonus demonstrations: Show successful operations
      Console.WriteLine("✨ SUCCESSFUL OPERATIONS DEMO:");
      Console.WriteLine("==============================\n");

      // Successfully increase stock
      Console.WriteLine("Demo 1: Increasing stock for existing item...");
      warehouseManager.IncreaseStock(warehouseManager.Groceries, 101, 20);
      Console.WriteLine();

      // Successfully remove an item
      Console.WriteLine("Demo 2: Removing an existing item...");
      warehouseManager.RemoveItemById(warehouseManager.Electronics, 3);
      Console.WriteLine();

      // Show updated inventories
      Console.WriteLine("📊 UPDATED INVENTORIES:");
      Console.WriteLine("=======================\n");

      Console.WriteLine("🛒 Updated Grocery Inventory:");
      warehouseManager.PrintAllItems(warehouseManager.Groceries);

      Console.WriteLine("📱 Updated Electronics Inventory:");
      warehouseManager.PrintAllItems(warehouseManager.Electronics);

      Console.WriteLine("🎉 Warehouse management system demonstration completed!");
      Console.WriteLine("Press any key to exit...");
      Console.ReadKey();
    }
  }
}