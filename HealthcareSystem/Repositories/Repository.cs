using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthcareSystem.Repositories
{
  /// <summary>
  /// Generic repository class for managing entities of type T.
  /// This class provides CRUD (Create, Read, Update, Delete) operations
  /// and demonstrates the power of C# generics for type safety and reusability.
  /// 
  /// The same repository can be used for Patients, Prescriptions, or any other entity type.
  /// </summary>
  /// <typeparam name="T">The type of entity this repository will manage</typeparam>
  public class Repository<T>
  {
    /// <summary>
    /// Internal storage using List<T> to hold all entities of type T.
    /// List provides dynamic resizing and efficient operations.
    /// </summary>
    private readonly List<T> _items;

    /// <summary>
    /// Constructor initializes the internal List<T> storage.
    /// Creates an empty repository ready to store entities.
    /// </summary>
    public Repository()
    {
      _items = new List<T>();
    }

    /// <summary>
    /// Adds a new entity to the repository.
    /// Demonstrates generic type safety - only accepts objects of type T.
    /// </summary>
    /// <param name="item">The entity to add to the repository</param>
    public void Add(T item)
    {
      // Validate that the item is not null
      if (item == null)
        throw new ArgumentNullException(nameof(item), "Cannot add null item to repository.");

      // Add the item to our internal list
      _items.Add(item);

      // Provide feedback for debugging/logging purposes
      Console.WriteLine($"✅ Added {typeof(T).Name} to repository. Total count: {_items.Count}");
    }

    /// <summary>
    /// Retrieves all entities from the repository.
    /// Returns a new List to prevent external modification of internal storage.
    /// </summary>
    /// <returns>List containing all entities of type T</returns>
    public List<T> GetAll()
    {
      // Return a new list to prevent external code from modifying our internal storage
      // This is a defensive programming practice
      return new List<T>(_items);
    }

    /// <summary>
    /// Finds the first entity that matches the given predicate condition.
    /// Uses Func<T, bool> delegate to provide flexible search criteria.
    /// Returns nullable T (T?) to handle cases where no match is found.
    /// </summary>
    /// <param name="predicate">Function that defines the search condition</param>
    /// <returns>First matching entity or null if no match found</returns>
    public T? GetById(Func<T, bool> predicate)
    {
      // Validate the predicate is not null
      if (predicate == null)
        throw new ArgumentNullException(nameof(predicate), "Search predicate cannot be null.");

      // Use LINQ's FirstOrDefault to find the first match
      // Returns default(T) (which is null for reference types) if no match found
      var result = _items.FirstOrDefault(predicate);

      // Provide feedback for debugging
      if (result != null)
      {
        Console.WriteLine($"🔍 Found matching {typeof(T).Name} in repository");
      }
      else
      {
        Console.WriteLine($"❌ No matching {typeof(T).Name} found in repository");
      }

      return result;
    }

    /// <summary>
    /// Removes the first entity that matches the given predicate condition.
    /// Returns true if an entity was removed, false otherwise.
    /// </summary>
    /// <param name="predicate">Function that defines the removal condition</param>
    /// <returns>True if an entity was removed, false if no match found</returns>
    public bool Remove(Func<T, bool> predicate)
    {
      // Validate the predicate is not null
      if (predicate == null)
        throw new ArgumentNullException(nameof(predicate), "Remove predicate cannot be null.");

      // Find the first item that matches the condition
      var itemToRemove = _items.FirstOrDefault(predicate);

      if (itemToRemove != null)
      {
        // Remove the item from our internal list
        bool removed = _items.Remove(itemToRemove);

        if (removed)
        {
          Console.WriteLine($"🗑️ Removed {typeof(T).Name} from repository. Total count: {_items.Count}");
        }

        return removed;
      }

      Console.WriteLine($"❌ No matching {typeof(T).Name} found to remove");
      return false;
    }

    /// <summary>
    /// Gets the current count of entities in the repository.
    /// Useful for monitoring repository size and debugging.
    /// </summary>
    /// <returns>Number of entities currently stored</returns>
    public int Count()
    {
      return _items.Count;
    }
  }
}
