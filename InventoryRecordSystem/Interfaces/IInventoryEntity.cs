// Interfaces/IInventoryEntity.cs
using System;

namespace InventoryRecordSystem.Interfaces
{
  /// <summary>
  /// Marker interface that identifies an inventory entity.
  /// It exposes an Id property so generic loggers can operate on objects
  /// that guarantee an identifier.
  /// </summary>
  public interface IInventoryEntity
  {
    // Read-only Id property required for all inventory entities.
    int Id { get; }
  }
}
