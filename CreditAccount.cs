using Banks.CenterBanks;
using Banks.Entites;
using Banks.Exceptions;

namespace Banks.Accounts;

public class CreditAccount : Account
{
    public CreditAccount(decimal balance, Bank bank, Client client)
        : base(balance, bank, client)
    {
    }

    public override void ReplenishmentOfTheBalance(decimal money)
    {
        if (money < 0)
            throw new BanksExceptions("Wrong Money");
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

        if (Bank.CreditLemit < Balance - money)
            throw new BanksExceptions("Credit limit exceeded");
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

        if (Bank.CreditLemit < Balance - money)
            throw new BanksExceptions("Exceeded the limit");
        Balance -= money;
        var trans = new Transaction(money, "W", this);
        AddTransacrion(trans);
    }

    public override void AccrualOfInterest(decimal procent)
    {
        Balance -= procent;
    }

    public override decimal DetermineTheAccrual()
    {
        if (Balance < 0)
        {
            decimal accural = Bank.CreditComission;
            return accural;
        }

        return 0;
    }
}