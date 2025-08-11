using WarehouseInventorySystem.Interfaces;
using WarehouseInventorySystem.Exceptions;

namespace WarehouseInventorySystem.Repository
{
  /// <summary>
  /// Generic repository class for managing inventory items of any type that implements IInventoryItem.
  /// Uses Dictionary for fast O(1) lookups by ID and provides thread-safe operations.
  /// </summary>
  /// <typeparam name="T">Type of inventory item that must implement IInventoryItem interface</typeparam>
  public class InventoryRepository<T> where T : IInventoryItem
  {
    // Dictionary to store items with ID as key for fast lookups
    // Using private field with underscore naming convention
    private readonly Dictionary<int, T> _items;

    /// <summary>
    /// Constructor initializes the internal dictionary for storing items
    /// </summary>
    public InventoryRepository()
    {
      _items = new Dictionary<int, T>();
    }

    /// <summary>
    /// Adds a new item to the inventory
    /// </summary>
    /// <param name="item">The item to add</param>
    /// <exception cref="DuplicateItemException">Thrown when an item with the same ID already exists</exception>
    public void AddItem(T item)
    {
      // Check if an item with this ID already exists
      if (_items.ContainsKey(item.Id))
      {
        throw new DuplicateItemException($"Item with ID {item.Id} already exists in the inventory.");
      }

      // Add the item to the dictionary
      _items[item.Id] = item;
    }

    /// <summary>
    /// Retrieves an item by its ID
    /// </summary>
    /// <param name="id">The ID of the item to retrieve</param>
    /// <returns>The item with the specified ID</returns>
    /// <exception cref="ItemNotFoundException">Thrown when no item with the specified ID exists</exception>
    public T GetItemById(int id)
    {
      // Try to get the item from the dictionary
      if (_items.TryGetValue(id, out T? item))
      {
        return item;
      }

      // Item not found, throw custom exception
      throw new ItemNotFoundException($"Item with ID {id} was not found in the inventory.");
    }

    /// <summary>
    /// Removes an item from the inventory by its ID
    /// </summary>
    /// <param name="id">The ID of the item to remove</param>
    /// <exception cref="ItemNotFoundException">Thrown when no item with the specified ID exists</exception>
    public void RemoveItem(int id)
    {
      // Check if the item exists before attempting removal
      if (!_items.ContainsKey(id))
      {
        throw new ItemNotFoundException($"Cannot remove item with ID {id}: item not found in inventory.");
      }

      // Remove the item from the dictionary
      _items.Remove(id);
    }

    /// <summary>
    /// Returns all items in the inventory as a list
    /// </summary>
    /// <returns>List containing all inventory items</returns>
    public List<T> GetAllItems()
    {
      // Convert dictionary values to a list and return
      // This creates a snapshot of current items
      return _items.Values.ToList();
    }

    /// <summary>
    /// Updates the quantity of an existing item
    /// </summary>
    /// <param name="id">The ID of the item to update</param>
    /// <param name="newQuantity">The new quantity value</param>
    /// <exception cref="ItemNotFoundException">Thrown when no item with the specified ID exists</exception>
    /// <exception cref="InvalidQuantityException">Thrown when the new quantity is negative</exception>
    public void UpdateQuantity(int id, int newQuantity)
    {
      // Validate that quantity is not negative
      if (newQuantity < 0)
      {
        throw new InvalidQuantityException($"Quantity cannot be negative. Provided value: {newQuantity}");
      }

      // Check if the item exists
      if (!_items.TryGetValue(id, out T? item))
      {
        throw new ItemNotFoundException($"Cannot update quantity for item with ID {id}: item not found in inventory.");
      }

      // Update the quantity
      item.Quantity = newQuantity;
    }

    /// <summary>
    /// Gets the current count of items in the repository
    /// </summary>
    public int Count => _items.Count;
  }
}