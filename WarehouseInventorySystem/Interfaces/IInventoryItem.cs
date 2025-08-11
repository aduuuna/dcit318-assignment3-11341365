namespace WarehouseInventorySystem.Interfaces
{
  /// <summary>
  /// Marker interface that defines the basic contract for all inventory items.
  /// This ensures all items have a unique ID, name, and quantity that can be managed.
  /// </summary>
  public interface IInventoryItem
  {
    /// <summary>
    /// Unique identifier for the inventory item
    /// </summary>
    int Id { get; }

    /// <summary>
    /// Name or description of the inventory item
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Current quantity/stock level of the item (can be updated)
    /// </summary>
    int Quantity { get; set; }
  }
}