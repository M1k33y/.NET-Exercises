using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalExercises1._4.Models.Interfaces
{
    public interface ITransactable
    {
        void Deposit(decimal amount);
        bool Withdraw(decimal amount, out string? error);
    }
}
