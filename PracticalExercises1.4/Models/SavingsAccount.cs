using PracticalExercises1._4.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalExercises1._4.Models
{
    internal class SavingsAccount:BankAccount,IInterestBearing
    {
        public SavingsAccount(int id,string owner,decimal openingBalance):base(id,owner,openingBalance) { }
        private const decimal InterestRate = 0.01m;

        public override bool Withdraw(decimal amount, out string? error)
        {
            if (amount > Balance)
            {
                error = "Insufficient funds — savings accounts cannot overdraft.";
                return false;
            }
            Balance -= amount;
            Log($"Withdrew {amount:C}. New balance {Balance:C}.");
            error = null;
            return true;
        }
        public void ApplyMonthlyInterest()
        {
            if (Balance > 0)
            {
                var interest = Balance * InterestRate;
                Balance += interest;
                Log($"Monthly interest applied ({InterestRate:P}): {interest:C}. New balance {Balance:C}.");
            }
        }
    }
}
