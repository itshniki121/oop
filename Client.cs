using Banks.Accounts;
using Banks.CenterBanks;
using Banks.Exceptions;

namespace Banks.Entites;

public class Client : IObserver
{
    private List<Account> _accounts;
    private List<string> _updatedList;
    public Client(string name, string surname, string? passport, string? address)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new BanksExceptions("Wrong Name");
        }

        if (string.IsNullOrEmpty(surname))
        {
            throw new BanksExceptions("Wrong surname");
        }

        Name = name;
        Surname = surname;
        Address = address;
        _accounts = new List<Account>();
        _updatedList = new List<string>();
        Passport = passport;
        Subcribe = false;
    }

    public bool Subcribe { get; set; }

    public string? Passport { get; }

    public string? Address { get; }

    public string Surname { get; }

    public string Name { get; }

    public void AddAccount(Account account)
    {
        if (_accounts.Contains(account))
            throw new BanksExceptions("Account already exists");
        _accounts.Add(account);
    }

    public void Update(string update)
    {
       _updatedList.Add(update);
    }
}