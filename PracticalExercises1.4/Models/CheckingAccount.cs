using PracticalExercises1._4.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalExercises1._4.Models
{
    internal class CheckingAccount : BankAccount, IOverdraftPolicy
    {
        public CheckingAccount(int id, string owner, decimal openingBalance)
            : base(id, owner, openingBalance) { }

        public decimal OverdraftLimit => -200m;

        public override bool Withdraw(decimal amount, out string? error)
        {
            if (Balance - amount < OverdraftLimit)
            {
                error = $"Overdraft limit reached. Minimum allowed: {OverdraftLimit:C}.";
                return false;
            }

            Balance -= amount;
            Log($"Withdrew {amount:C}. New balance {Balance:C}.");
            error = null;
            return true;
        }

    }
}
