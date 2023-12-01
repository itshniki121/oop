using Banks.Accounts;
using Banks.Entites;
using Banks.Exceptions;

namespace Banks.CenterBanks;

public class Bank : IObservable
{
    public Bank(string name, decimal limitUnverifi, decimal procentDepos, decimal interestBalance, decimal creditLemit, decimal creditComission)
    {
        Accounts = new List<Account>();
        if (string.IsNullOrEmpty(name))
            throw new BanksExceptions("Wrong name");
        Name = name;
        if (limitUnverifi < 0)
            throw new BanksExceptions(" Wrong LimitUnWerifi");
        LimitUnverifi = limitUnverifi;
        if (procentDepos < 0)
            throw new BanksExceptions(" Wrong ProcentDepos");
        ProcentDepos = procentDepos;
        if (interestBalance < 0)
            throw new BanksExceptions(" Wrong InterestBalance");
        InterestBalance = interestBalance;
        if (creditLemit < 0)
            throw new BanksExceptions(" Wrong CreditLimit");
        CreditLemit = creditLemit;
        if (creditComission < 0)
            throw new BanksExceptions(" Wrong CreditComission");
        CreditComission = creditComission;
    }

    public decimal CreditLemit { get; private set; }

    public decimal CreditComission { get; private set; }

    public decimal InterestBalance { get; private set; }

    public decimal ProcentDepos { get; private set; }

    public decimal LimitUnverifi { get; private set; }

    public string Name { get; }
    public List<Account> Accounts { get; private set; }

    public void ChangeInBankConditions(decimal limitUnverifi, decimal procentDepos, decimal interestBalance, decimal creditLemit, decimal creditComission)
    {
        LimitUnverifi = limitUnverifi;
        ProcentDepos = procentDepos;
        InterestBalance = interestBalance;
        CreditLemit = creditLemit;
        CreditComission = creditComission;
        UpdateObservers();
    }

    public Account AddAccountCredit(Client client, decimal balance)
    {
        var account = new CreditAccount(balance, this, client);
        client.AddAccount(account);
        Accounts.Add(account);
        return account;
    }

    public Account AddAccountDebit(Client client, decimal balance)
    {
        var account = new DebitAccount(balance, this, client);
        client.AddAccount(account);
        Accounts.Add(account);
        return account;
    }

    public Account AddAccountDepo(Client client, decimal balance)
    {
        var account = new DepoAccount(balance, this, DateTime.Now.AddYears(10), client);
        client.AddAccount(account);
        Accounts.Add(account);
        return account;
    }

    public void AccuralOfInterest(int days)
    {
        foreach (var account in Accounts)
        {
            decimal accural = 0;
            for (int i = 0; i < days; i++)
            {
                accural += account.DetermineTheAccrual();
            }

            account.AccrualOfInterest(accural);
        }
    }

    public void AddObserver(IObserver o)
    {
        foreach (var account in Accounts.Where(account => account.Client == o))
        {
            account.Client.Subcribe = true;
        }
    }

    public void RemoveObserver(IObserver o)
    {
        foreach (var account in Accounts.Where(account => account.Client == o))
        {
            account.Client.Subcribe = false;
        }
    }

    public void UpdateObservers()
    {
        foreach (var account in Accounts.Where(account => account.Client.Subcribe == true))
        {
            account.Client.Update($"{DateTime.Today} the bank changed the conditions!");
        }
    }
}