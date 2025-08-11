using WarehouseInventorySystem.Interfaces;

namespace WarehouseInventorySystem.Models
{
  /// <summary>
  /// Represents a grocery product in the inventory with expiry date tracking
  /// </summary>
  public class GroceryItem : IInventoryItem
  {
    /// <summary>
    /// Unique identifier for this grocery item
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Name/description of the grocery item
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Current stock quantity - can be updated for inventory management
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Expiration date of the grocery item
    /// </summary>
    public DateTime ExpiryDate { get; private set; }

    /// <summary>
    /// Constructor to initialize all fields of the grocery item
    /// </summary>
    /// <param name="id">Unique identifier</param>
    /// <param name="name">Product name</param>
    /// <param name="quantity">Initial stock quantity</param>
    /// <param name="expiryDate">Product expiration date</param>
    public GroceryItem(int id, string name, int quantity, DateTime expiryDate)
    {
      Id = id;
      Name = name;
      Quantity = quantity;
      ExpiryDate = expiryDate;
    }

    /// <summary>
    /// Override ToString for better display formatting
    /// </summary>
    public override string ToString()
    {
      return $"[Grocery] ID: {Id}, Name: {Name}, Quantity: {Quantity}, Expiry: {ExpiryDate:yyyy-MM-dd}";
    }
  }
}