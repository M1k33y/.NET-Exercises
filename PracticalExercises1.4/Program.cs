using PracticalExercises1._4.Models;
using PracticalExercises1._4.Models.Interfaces;
using PracticalExercises1._4.Services;

var registry = new AccountRegistry();

while (true)
{
    Console.WriteLine("\n=== MINIBANK ===");
    Console.WriteLine("1. List accounts");
    Console.WriteLine("2. Create account");
    Console.WriteLine("3. Deposit");
    Console.WriteLine("4. Withdraw");
    Console.WriteLine("5. View statement");
    Console.WriteLine("6. Run month-end");
    Console.WriteLine("7. Exit");
    Console.Write("Select: ");

    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1": ListAccounts(); break;
        case "2": CreateAccount(); break;
        case "3": DoDeposit(); break;
        case "4": DoWithdraw(); break;
        case "5": ViewStatement(); break;
        case "6": RunMonthEnd(); break;
        case "7": return;
        default:
            Console.WriteLine("Invalid option.");
            break;
    }
}



void ListAccounts()
{
    if (!registry.All.Any())
    {
        Console.WriteLine("No accounts yet.");
        return;
    }

    foreach (var a in registry.All)
        Console.WriteLine($"#{a.Id} {a.Owner} - {a.GetType().Name.Replace("Account", "")} - {a.Balance:C}");
}

void CreateAccount()
{
    Console.Write("Type (Checking/Savings/Loan): ");
    var type = Console.ReadLine()!.Trim();

    Console.Write("Owner: ");
    var owner = Console.ReadLine()!.Trim();

    decimal opening = AskAmount("Opening amount");

    var acc = registry.Create(type, owner, opening);
    Console.WriteLine($"Created #{acc.Id} {acc.GetType().Name} for {acc.Owner} with BAL {acc.Balance:C}");
}

void DoDeposit()
{
    var acc = AskAccount();
    if (acc == null) return;

    decimal amount = AskAmount("Deposit amount");
    acc.Deposit(amount);
    Console.WriteLine("Deposit successful.");
}

void DoWithdraw()
{
    var acc = AskAccount();
    if (acc == null) return;

    decimal amount = AskAmount("Withdraw amount");

    if (!acc.Withdraw(amount, out var error))
        Console.WriteLine($"Failed: {error}");
    else
        Console.WriteLine("Withdraw successful.");
}

void ViewStatement()
{
    var acc = AskAccount();
    acc?.PrintStatement();
}

void RunMonthEnd()
{
    foreach (var acc in registry.All)
        if (acc is IInterestBearing ib)
            ib.ApplyMonthlyInterest();

    Console.WriteLine("Month-end applied.");
}

BankAccount? AskAccount()
{
    Console.Write("Account ID: ");
    if (!int.TryParse(Console.ReadLine(), out var id))
    {
        Console.WriteLine("Invalid ID.");
        return null;
    }

    var acc = registry.Find(id);
    if (acc == null)
        Console.WriteLine("Account not found.");

    return acc;
}

decimal AskAmount(string prompt)
{
    while (true)
    {
        Console.Write($"{prompt}: ");
        if (decimal.TryParse(Console.ReadLine(), out var amount) && amount > 0)
            return amount;

        Console.WriteLine("Invalid amount. Must be > 0.");
    }
}
