// Services/InventoryLogger.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using InventoryRecordSystem.Interfaces;

namespace InventoryRecordSystem.Services
{
  /// <summary>
  /// Generic logger/manager for inventory entities.
  /// T must implement IInventoryEntity (so it guarantees an Id).
  /// This class manages an in-memory List<T> and reads/writes JSON to a file.
  /// </summary>
  public class InventoryLogger<T> where T : IInventoryEntity
  {
    // Internal in-memory log of items
    private readonly List<T> _log;

    // File path where items are persisted as JSON
    private readonly string _filePath;

    // JSON serializer options used both for serialization and deserialization
    private static readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web)
    {
      WriteIndented = true, // make the JSON human readable
      PropertyNameCaseInsensitive = true
    };

    /// <summary>
    /// Creates an InventoryLogger that stores data in the given file path.
    /// If the path's directory does not exist it will be created when saving.
    /// </summary>
    /// <param name="filePath">Relative or absolute path to the JSON file</param>
    public InventoryLogger(string filePath)
    {
      _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
      _log = new List<T>();
    }

    /// <summary>
    /// Adds an item to the in-memory log.
    /// This does NOT automatically persist; call SaveToFile() to persist.
    /// </summary>
    /// <param name="item">Item to add</param>
    public void Add(T item)
    {
      if (item == null) throw new ArgumentNullException(nameof(item));

      // Optionally guard against duplicate Ids:
      if (_log.Exists(x => x.Id == item.Id))
      {
        // If duplicates are not desired you could throw or update instead.
        // For now, we'll allow duplicates but warn on the console.
        Console.WriteLine($"Warning: An item with Id {item.Id} already exists in the log.");
      }

      _log.Add(item);
    }

    /// <summary>
    /// Returns a shallow copy of all items currently in memory.
    /// </summary>
    /// <returns>List of items</returns>
    public List<T> GetAll()
    {
      // return a copy to avoid external mutation of internal collection
      return new List<T>(_log);
    }

    /// <summary>
    /// Saves the in-memory collection to disk as JSON.
    /// Uses "using" for file handling and wraps operations in try/catch to handle errors gracefully.
    /// </summary>
    public void SaveToFile()
    {
      try
      {
        // Ensure directory exists
        var directory = Path.GetDirectoryName(_filePath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
          Directory.CreateDirectory(directory);
        }

        // Serialize the list to JSON string
        string json = JsonSerializer.Serialize(_log, _jsonOptions);

        // Use StreamWriter with 'using' to ensure the file handle is released
        using (var writer = new StreamWriter(_filePath, false)) // overwrite file
        {
          writer.Write(json);
        }

        Console.WriteLine($"Saved {_log.Count} item(s) to '{_filePath}'.");
      }
      catch (UnauthorizedAccessException ex)
      {
        Console.WriteLine($"Permission error writing to file '{_filePath}': {ex.Message}");
      }
      catch (IOException ex)
      {
        Console.WriteLine($"I/O error while saving file '{_filePath}': {ex.Message}");
      }
      catch (Exception ex)
      {
        // Catch-all to avoid crashing; could rethrow in other contexts
        Console.WriteLine($"Unexpected error when saving file '{_filePath}': {ex.Message}");
      }
    }

    /// <summary>
    /// Loads items from the JSON file into the in-memory log.
    /// The current in-memory list is cleared and replaced with loaded items.
    /// </summary>
    public void LoadFromFile()
    {
      try
      {
        if (!File.Exists(_filePath))
        {
          Console.WriteLine($"No file found at '{_filePath}'. Nothing to load.");
          _log.Clear();
          return;
        }

        // Use StreamReader with 'using' to ensure the file handle is released
        using (var reader = new StreamReader(_filePath))
        {
          string json = reader.ReadToEnd();

          if (string.IsNullOrWhiteSpace(json))
          {
            // Empty file -> nothing to load
            _log.Clear();
            Console.WriteLine($"File '{_filePath}' is empty. Loaded 0 items.");
            return;
          }

          // Deserialize into List<T>; if it fails, handle gracefully
          try
          {
            var items = JsonSerializer.Deserialize<List<T>>(json, _jsonOptions);
            if (items == null)
            {
              _log.Clear();
              Console.WriteLine("Deserialization returned null. Loaded 0 items.");
            }
            else
            {
              _log.Clear();
              _log.AddRange(items);
              Console.WriteLine($"Loaded {_log.Count} item(s) from '{_filePath}'.");
            }
          }
          catch (JsonException jex)
          {
            Console.WriteLine($"JSON deserialization error for file '{_filePath}': {jex.Message}");
            _log.Clear();
          }
        }
      }
      catch (UnauthorizedAccessException ex)
      {
        Console.WriteLine($"Permission error reading file '{_filePath}': {ex.Message}");
      }
      catch (IOException ex)
      {
        Console.WriteLine($"I/O error while loading file '{_filePath}': {ex.Message}");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Unexpected error when loading file '{_filePath}': {ex.Message}");
      }
    }
  }
}
