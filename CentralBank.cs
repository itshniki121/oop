using Banks.Exceptions;

namespace Banks.CenterBanks;

public class CentralBank
{
    private List<Bank> _listBanks;
    private DateTime _dateTime;
    public CentralBank()
    {
        _listBanks = new List<Bank>();
        _dateTime = DateTime.Today;
    }

    public Bank RegistrationBank(string name, decimal limitUnverifi, decimal procentDepos, decimal interestBalance, decimal creditLemit, decimal creditComission)
    {
        var bank = new Bank(name, limitUnverifi,  procentDepos, interestBalance,  creditLemit, creditComission);
        if (_listBanks.Contains(bank))
            throw new BanksExceptions("The bank already exists");
        _listBanks.Add(bank);
        return bank;
    }

    public void AccuralOfInterestForBanks()
    {
        int date = (DateTime.Today - _dateTime).Days;
        _dateTime = DateTime.Today;
        foreach (var bank in _listBanks)
        {
            bank.AccuralOfInterest(date);
        }
    }

    public void TimeAcceleration(int days)
    {
        foreach (var bank in _listBanks)
        {
            bank.AccuralOfInterest(days);
        }
    }
}