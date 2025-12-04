using PracticalExercises1._4.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalExercises1._4.Models
{
    internal abstract class BankAccount:ITransactable,IStatement
    {
        private readonly List<string> _operations = new();

        public int Id { get; }
        public string Owner { get; }
        public decimal Balance { get; protected set; }
        protected BankAccount(int id,string owner,decimal openingBalance)
        {
            Id = id;
            Owner = owner;
            Balance = openingBalance;
            Log($"Account opened with balance {Balance:C}.");
        }
        protected void Log(string message)
        {
            _operations.Add($"{DateTime.Now:HH:mm:ss} - {message}");
        }
        public virtual void Deposit(decimal amount)
        {
            Balance += amount;
            Log($"Deposited {amount:C}. New Balance {Balance:C}.");
        }
        public abstract bool Withdraw(decimal amount, out string? error);

        public void PrintStatement()
        {
            Console.WriteLine($"=== Statement for Account #{Id} ({Owner}) ===");
            foreach (var op in _operations)
                Console.WriteLine(op);
            Console.WriteLine($"Current balance: {Balance:C}");
            Console.WriteLine("=========================================");
        }
    }
}
