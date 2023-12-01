using Banks.CenterBanks;
using Banks.Entites;
using Banks.Exceptions;

namespace Banks.Accounts;

public class DepoAccount : Account
{
    public DepoAccount(decimal balance, Bank bank, DateTime dateTime, Client client)
        : base(balance, bank, client)
    {
        DepositTerm = dateTime;
    }

    public DateTime DepositTerm { get; }

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
        if (DepositTerm < DateTime.Now)
            throw new BanksExceptions("The deadline has not passed yet");
        Verification();
        if (Verifi == false)
        {
            if (money > Bank.LimitUnverifi)
                throw new BanksExceptions("The transfer limit has been exceeded");
        }

        Balance -= money;
        account.Balance += money;
        var trans = new Transaction(money, "T", this, account);
        AddTransacrion(trans);
    }

    public override void WithdrawalOfMoney(decimal money)
    {
        if (DepositTerm < DateTime.Now)
            throw new BanksExceptions("The deadline has not passed yet");
        Verification();
        if (Verifi == false)
        {
            if (money > Bank.LimitUnverifi)
                throw new BanksExceptions("The withdrawal limit has been exceeded");
        }

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
        var accural = Balance * ((Bank.ProcentDepos / 365) / 100);
        return accural;
    }
}