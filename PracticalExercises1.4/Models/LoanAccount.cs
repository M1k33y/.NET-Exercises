using PracticalExercises1._4.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalExercises1._4.Models
{
    internal class LoanAccount:BankAccount,IInterestBearing
    {
        public LoanAccount(int id,string owner,decimal openingAmount):base(id,owner,-Math.Abs(openingAmount)) { }
        private const decimal InterestRate = 0.02m;

        public override bool Withdraw(decimal amount, out string? error)
        {
            Balance -= amount;
            Log($"Borrowed {amount:C}. New debt {Balance:C}.");
            error = null;
            return true;
        }

        public override void Deposit(decimal amount)
        {
            Balance += amount;
            Log($"Loan repayment {amount:C}. New debt {Balance:C}.");
        }

        public void ApplyMonthlyInterest()
        {
            var interest = Math.Abs(Balance) * InterestRate;
            Balance -= interest;
            Log($"Loan interest ({InterestRate:P}) charged: {interest:C}. New debt {Balance:C}.");
        }
    }
}
