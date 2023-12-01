using Banks.Entites;

namespace Banks.Accounts;
using Banks.CenterBanks;

public abstract class Account
{
    private static int _id = 0;
    private List<Transaction> _historyTransactions = new List<Transaction>();
    private bool verifi = true;
    public Account(decimal balance, Bank bank, Client client)
    {
        Balance = balance;
        Bank = bank;
        Client = client;
        Verifi = verifi;
        Verification();
        Id = _id++;
    }

    public int Id { get; }
    public IReadOnlyList<Transaction> HistoryTransactions => _historyTransactions;

    public decimal Balance { get; set; }
    public Bank Bank { get; }
    public Client Client { get; }
    public bool Verifi { get; private set; }
    public abstract void MoneyTransfer(decimal money, Account account);
    public abstract void WithdrawalOfMoney(decimal money);
    public abstract void ReplenishmentOfTheBalance(decimal money);

    public abstract void AccrualOfInterest(decimal procent);
    public abstract decimal DetermineTheAccrual();

    public void Verification()
    {
        if (string.IsNullOrEmpty(Client.Address))
            Verifi = false;
        if (string.IsNullOrEmpty(Client.Passport))
            Verifi = false;
    }

    public void AddTransacrion(Transaction transaction)
    {
        _historyTransactions.Add(transaction);
    }
}