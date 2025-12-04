using PracticalExercises1._4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalExercises1._4.Services
{
    internal class AccountRegistry
    {
        private int _nextId = 1;
        private readonly List<BankAccount> _accounts = new();
        public IReadOnlyList<BankAccount> All => _accounts;
        public BankAccount? Find(int id) =>
        _accounts.FirstOrDefault(a => a.Id == id);

        public BankAccount Create(string type, string owner, decimal opening)
        {
            BankAccount account = type.ToLower() switch
            {
                "checking" => new CheckingAccount(_nextId++, owner, opening),
                "savings" => new SavingsAccount(_nextId++, owner, opening),
                "loan" => new LoanAccount(_nextId++, owner, opening),
                _ => throw new Exception("Invalid account type")
            };

            _accounts.Add(account);
            return account;
        }
    }
}
