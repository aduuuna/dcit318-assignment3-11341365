using WarehouseInventorySystem.Interfaces;
using WarehouseInventorySystem.Models;
using WarehouseInventorySystem.Repository;
using WarehouseInventorySystem.Exceptions;

namespace WarehouseInventorySystem.Managers
{
  /// <summary>
  /// Main manager class that coordinates inventory operations for both electronic and grocery items.
  /// Provides high-level operations and demonstrates generic method usage.
  /// </summary>
  public class WarehouseManager
  {
    // Separate repositories for different types of inventory items
    private readonly InventoryRepository<ElectronicItem> _electronics;
    private readonly InventoryRepository<GroceryItem> _groceries;

    /// <summary>
    /// Constructor initializes both electronic and grocery repositories
    /// </summary>
    public WarehouseManager()
    {
      _electronics = new InventoryRepository<ElectronicItem>();
      _groceries = new InventoryRepository<GroceryItem>();
    }

    /// <summary>
    /// Seeds the repositories with sample data for demonstration purposes.
    /// Adds 2-3 items of each type to populate the inventory.
    /// </summary>
    public void SeedData()
    {
      Console.WriteLine("🌱 Seeding inventory with sample data...\n");

      try
      {
        // Add sample electronic items
        _electronics.AddItem(new ElectronicItem(1, "iPhone 15 Pro", 25, "Apple", 12));
        _electronics.AddItem(new ElectronicItem(2, "Samsung Galaxy S24", 30, "Samsung", 24));
        _electronics.AddItem(new ElectronicItem(3, "Dell XPS 13 Laptop", 15, "Dell", 36));

        // Add sample grocery items  
        _groceries.AddItem(new GroceryItem(101, "Organic Bananas", 50, DateTime.Now.AddDays(7)));
        _groceries.AddItem(new GroceryItem(102, "Whole Milk", 20, DateTime.Now.AddDays(5)));
        _groceries.AddItem(new GroceryItem(103, "Sourdough Bread", 12, DateTime.Now.AddDays(3)));

        Console.WriteLine("✅ Sample data loaded successfully!");
        Console.WriteLine($"   📱 {_electronics.Count} electronic items added");
        Console.WriteLine($"   🛒 {_groceries.Count} grocery items added\n");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"❌ Error seeding data: {ex.Message}\n");
      }
    }

    /// <summary>
    /// Generic method to print all items in a repository.
    /// Demonstrates use of generic constraints and type parameters.
    /// </summary>
    /// <typeparam name="T">Type of inventory item that implements IInventoryItem</typeparam>
    /// <param name="repo">Repository containing items to display</param>
    public void PrintAllItems<T>(InventoryRepository<T> repo) where T : IInventoryItem
    {
      // Get all items from the repository
      List<T> items = repo.GetAllItems();

      if (items.Count == 0)
      {
        Console.WriteLine("   📦 No items found in this category.\n");
        return;
      }

      // Display each item using their ToString() method
      foreach (T item in items)
      {
        Console.WriteLine($"   {item}");
      }
      Console.WriteLine($"   📊 Total items: {items.Count}\n");
    }

    /// <summary>
    /// Generic method to increase stock quantity for an item.
    /// Demonstrates generic method with exception handling.
    /// </summary>
    /// <typeparam name="T">Type of inventory item that implements IInventoryItem</typeparam>
    /// <param name="repo">Repository containing the item</param>
    /// <param name="id">ID of the item to update</param>
    /// <param name="quantity">Quantity to add to current stock</param>
    public void IncreaseStock<T>(InventoryRepository<T> repo, int id, int quantity) where T : IInventoryItem
    {
      try
      {
        // Get current item to access its current quantity
        T item = repo.GetItemById(id);
        int newQuantity = item.Quantity + quantity;

        // Update with the new total quantity
        repo.UpdateQuantity(id, newQuantity);
        Console.WriteLine($"✅ Stock increased! Item '{item.Name}' quantity updated from {item.Quantity - quantity} to {newQuantity}");
      }
      catch (ItemNotFoundException ex)
      {
        Console.WriteLine($"❌ Cannot increase stock: {ex.Message}");
      }
      catch (InvalidQuantityException ex)
      {
        Console.WriteLine($"❌ Invalid quantity operation: {ex.Message}");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"❌ Unexpected error while increasing stock: {ex.Message}");
      }
    }

    /// <summary>
    /// Generic method to remove an item by ID from any repository.
    /// Demonstrates generic constraint usage with exception handling.
    /// </summary>
    /// <typeparam name="T">Type of inventory item that implements IInventoryItem</typeparam>
    /// <param name="repo">Repository to remove item from</param>
    /// <param name="id">ID of the item to remove</param>
    public void RemoveItemById<T>(InventoryRepository<T> repo, int id) where T : IInventoryItem
    {
      try
      {
        // Get item details before removal for confirmation message
        T item = repo.GetItemById(id);
        string itemName = item.Name;

        // Remove the item
        repo.RemoveItem(id);
        Console.WriteLine($"✅ Item removed successfully! '{itemName}' (ID: {id}) has been removed from inventory.");
      }
      catch (ItemNotFoundException ex)
      {
        Console.WriteLine($"❌ Cannot remove item: {ex.Message}");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"❌ Unexpected error while removing item: {ex.Message}");
      }
    }

    /// <summary>
    /// Provides public access to the electronics repository for external operations
    /// </summary>
    public InventoryRepository<ElectronicItem> Electronics => _electronics;

    /// <summary>
    /// Provides public access to the groceries repository for external operations
    /// </summary>
    public InventoryRepository<GroceryItem> Groceries => _groceries;
  }
}