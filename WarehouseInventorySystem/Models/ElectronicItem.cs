using WarehouseInventorySystem.Interfaces;

namespace WarehouseInventorySystem.Models
{
  /// <summary>
  /// Represents an electronic product in the inventory with brand and warranty information
  /// </summary>
  public class ElectronicItem : IInventoryItem
  {
    /// <summary>
    /// Unique identifier for this electronic item
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Name/model of the electronic item
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Current stock quantity - can be updated for inventory management
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Brand/manufacturer of the electronic item
    /// </summary>
    public string Brand { get; private set; }

    /// <summary>
    /// Warranty period in months
    /// </summary>
    public int WarrantyMonths { get; private set; }

    /// <summary>
    /// Constructor to initialize all fields of the electronic item
    /// </summary>
    /// <param name="id">Unique identifier</param>
    /// <param name="name">Product name</param>
    /// <param name="quantity">Initial stock quantity</param>
    /// <param name="brand">Manufacturer brand</param>
    /// <param name="warrantyMonths">Warranty period in months</param>
    public ElectronicItem(int id, string name, int quantity, string brand, int warrantyMonths)
    {
      Id = id;
      Name = name;
      Quantity = quantity;
      Brand = brand;
      WarrantyMonths = warrantyMonths;
    }

    /// <summary>
    /// Override ToString for better display formatting
    /// </summary>
    public override string ToString()
    {
      return $"[Electronic] ID: {Id}, Name: {Name}, Quantity: {Quantity}, Brand: {Brand}, Warranty: {WarrantyMonths} months";
    }
  }
}