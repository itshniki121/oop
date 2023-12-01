using Banks.CenterBanks;
using Banks.Entites;
using Banks.Exceptions;

namespace Banks.Accounts;

public class DebitAccount : Account
{
    public DebitAccount(decimal balance, Bank bank, Client client)
        : base(balance, bank, client)
    {
    }

    public override void ReplenishmentOfTheBalance(decimal money)
    {
        if (money < 0)
            throw new BanksExceptions("Error Money!");
        Balance += money;
        var trans = new Transaction(money, "R", this);
        AddTransacrion(trans);
    }

    public override void MoneyTransfer(decimal money, Account account)
    {
        Verification();
        if (Verifi == false)
        {
            if (money > Bank.LimitUnverifi)
                throw new BanksExceptions("The transfer limit has been exceeded");
        }

        if (Balance < money)
            throw new BanksExceptions("Wrong Balance");
        Balance -= money;
        account.Balance += money;
        var trans = new Transaction(money, "T", this, account);
        AddTransacrion(trans);
    }

    public override void WithdrawalOfMoney(decimal money)
    {
        Verification();
        if (Verifi == false)
        {
            if (money > Bank.LimitUnverifi)
                throw new BanksExceptions("The withdrawal limit has been exceeded");
        }

        if (money > Balance)
            throw new BanksExceptions("Not enough money");
        Balance -= money;
        var trans = new Transaction(money, "W", this);
        AddTransacrion(trans);
    }

    public override void AccrualOfInterest(decimal procent)
    {
        Balance += procent;
    }

    public override decimal DetermineTheAccrual()
    {
        var accural = Balance * ((Bank.InterestBalance / 365) / 100);
        return accural;
    }
}