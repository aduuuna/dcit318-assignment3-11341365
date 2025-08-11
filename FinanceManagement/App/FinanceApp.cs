using System;
using System.Collections.Generic;
using FinanceManagement.Models;
using FinanceManagement.Accounts;
using FinanceManagement.Processors;
using FinanceManagement.Interfaces;

namespace FinanceManagement.App
{
  /// <summary>
  /// Main application class that orchestrates the finance management system.
  /// Demonstrates the integration of all components: transactions, processors, and accounts.
  /// </summary>
  public class FinanceApp
  {
    /// <summary>
    /// Private field to store all processed transactions
    /// List provides dynamic storage for transaction history
    /// </summary>
    private List<Transaction> _transactions;

    /// <summary>
    /// Constructor initializes the transaction list
    /// </summary>
    public FinanceApp()
    {
      _transactions = new List<Transaction>();
    }

    /// <summary>
    /// Main method that runs the finance management system simulation
    /// Demonstrates all required functionality from the specification
    /// </summary>
    public void Run()
    {
      Console.WriteLine("🏦 Welcome to the Finance Management System!");
      Console.WriteLine("=", 50);

      // Step 1: Create a SavingsAccount with initial balance
      Console.WriteLine("\n📋 Step 1: Creating Savings Account");
      var savingsAccount = new SavingsAccount("SAV-12345", 1000m);

      // Step 2: Create three sample transactions with different categories
      Console.WriteLine("\n📋 Step 2: Creating Sample Transactions");
      var transactions = new List<Transaction>
            {
                new Transaction(1, DateTime.Now.AddDays(-2), 150m, "Groceries"),
                new Transaction(2, DateTime.Now.AddDays(-1), 75m, "Utilities"),
                new Transaction(3, DateTime.Now, 200m, "Entertainment")
            };

      // Display created transactions
      foreach (var transaction in transactions)
      {
        Console.WriteLine($"   Created: Transaction {transaction.Id} - ${transaction.Amount:F2} for {transaction.Category}");
      }

      // Step 3: Create different transaction processors
      Console.WriteLine("\n📋 Step 3: Creating Transaction Processors");
      var processors = new List<ITransactionProcessor>
            {
                new MobileMoneyProcessor(),    // For Transaction 1
                new BankTransferProcessor(),   // For Transaction 2  
                new CryptoWalletProcessor()    // For Transaction 3
            };

      Console.WriteLine("   ✅ Created MobileMoneyProcessor, BankTransferProcessor, and CryptoWalletProcessor");

      // Step 4: Process each transaction with its designated processor
      Console.WriteLine("\n📋 Step 4: Processing Transactions");
      for (int i = 0; i < transactions.Count; i++)
      {
        Console.WriteLine($"\n--- Processing Transaction {i + 1} ---");

        // Process the transaction using the corresponding processor
        processors[i].Process(transactions[i]);

        // Apply the transaction to the savings account
        savingsAccount.ApplyTransaction(transactions[i]);

        // Add the transaction to our history
        _transactions.Add(transactions[i]);

        Console.WriteLine(); // Add spacing between transactions
      }

      // Step 5: Display final system state and transaction summary
      Console.WriteLine("📋 Step 5: Final System Summary");
      Console.WriteLine("=", 50);
      Console.WriteLine($"Account Number: {savingsAccount.AccountNumber}");
      Console.WriteLine($"Final Balance: ${savingsAccount.Balance:F2}");
      Console.WriteLine($"Total Transactions Processed: {_transactions.Count}");

      // Display transaction history
      Console.WriteLine("\n📊 Transaction History:");
      decimal totalTransacted = 0;
      foreach (var transaction in _transactions)
      {
        Console.WriteLine($"   ID: {transaction.Id} | Date: {transaction.Date:yyyy-MM-dd} | " +
                        $"Amount: ${transaction.Amount:F2} | Category: {transaction.Category}");
        totalTransacted += transaction.Amount;
      }

      Console.WriteLine($"\n💰 Total Amount Transacted: ${totalTransacted:F2}");
      Console.WriteLine($"🎯 System demonstration completed successfully!");
    }
  }
}