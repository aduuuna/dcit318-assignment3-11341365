// Records/InventoryItem.cs
using System;
using InventoryRecordSystem.Interfaces;

namespace InventoryRecordSystem.Records
{
  /// <summary>
  /// Immutable record representing an item in the inventory.
  /// This uses C# 'record' positional syntax which creates init-only properties
  /// and value-based equality semantics.
  /// Implements IInventoryEntity so it can be used with generic inventory services.
  /// </summary>
  /// <param name="Id">Unique identifier for the inventory item</param>
  /// <param name="Name">Human-readable name</param>
  /// <param name="Quantity">Available quantity</param>
  /// <param name="DateAdded">Date/time the item was added</param>
  public record InventoryItem(int Id, string Name, int Quantity, DateTime DateAdded) : IInventoryEntity;
}
