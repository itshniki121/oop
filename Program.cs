using System.Diagnostics;
using Banks.Accounts;
using Banks.CenterBanks;
using Banks.Entites;

namespace Banks.Console;

internal static class Program
{
    private static void Main()
    {
        var centerBank = new CentralBank();
        var bank = centerBank.RegistrationBank("VTB", 2000, 10, 2, 15000, 200);
        System.Console.WriteLine("Registration in the user's bank!\nEnter Your name.");
        var name = System.Console.ReadLine();
        System.Console.WriteLine("Enter Your surname.");
        var surname = System.Console.ReadLine();
        System.Console.WriteLine("Enter Your passport!");
        var passport = System.Console.ReadLine();
        System.Console.WriteLine("Enter Your Address!");
        var address = System.Console.ReadLine();
        var builder = new Builder();
        var client = builder.GetName(name!).GetSurname(surname!).GetPassport(passport).GetAddress(address).Create();
        System.Console.WriteLine("Create new account!\nChoose which account you want to create:");
        System.Console.WriteLine("1 - Credit account.\n2 - Debit account.\n3 - Deposit account.");
        var account = System.Console.ReadLine() switch
        {
            "1" => bank.AddAccountCredit(client, 0),
            "2" => bank.AddAccountDebit(client, 0),
            "3" => bank.AddAccountDepo(client, 0),
            _ => default,
        };
        while (true)
        {
            System.Console.WriteLine("Choose an action:");
            System.Console.WriteLine("1 - Transfer\n2 - Deposit\n3 - Withdrawal\n4 - Check balance\n5 - Exit!");
            var choice = Convert.ToInt32(System.Console.ReadLine());
            switch (choice)
            {
                case 1:
                    System.Console.WriteLine("Enter the number you want to transfer money");
                    int id = Convert.ToInt32(System.Console.ReadLine());
                    System.Console.WriteLine("Enter amount of money.");
                    decimal money = Convert.ToDecimal(System.Console.ReadLine());
                    account?.MoneyTransfer(money, bank.Accounts.FirstOrDefault(acc => acc.Id == id) !);
                    break;
                case 2:
                    System.Console.WriteLine("Enter the amount of money to deposit");
                    var d = Convert.ToDecimal(System.Console.ReadLine());
                    account?.ReplenishmentOfTheBalance(d);
                    break;
                case 3:
                    System.Console.WriteLine("Enter the amount to withdraw.");
                    var w = Convert.ToDecimal(System.Console.ReadLine());
                    account?.WithdrawalOfMoney(w);
                    break;
                case 4:
                    System.Console.WriteLine("Account balance");
                    System.Console.WriteLine(account?.Balance);
                    break;
                case 5:
                    return;
                default:
                    break;
            }
        }
    }
}