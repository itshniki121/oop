using Banks.CenterBanks;
using Banks.Entites;
using Xunit;

namespace Banks.Test;

public class BankTest
{
    private CentralBank _centralBank = new CentralBank();

    [Fact]
    public void CheckMoneyTransfer()
    {
        var bank = _centralBank.RegistrationBank("Alfa", 2000, 4, 3, 10000, 200);
        var builder = new Builder();
        var client = builder.GetName("Vasya").GetSurname("Petrov").GetPassport("123123").GetAddress("Veteranov").Create();
        var account = bank.AddAccountDebit(client, 20000);
        var account2 = bank.AddAccountCredit(client, 0);
        account.MoneyTransfer(5000, account2);
        Assert.Equal(15000, account.Balance);
        Assert.Equal(5000, account2.Balance);
    }

    [Fact]
    public void CheckWithdrawalOfMoney()
    {
        var bank = _centralBank.RegistrationBank("Alfa", 2000, 4, 3, 10000, 200);
        var builder = new Builder();
        var client = builder.GetName("Vasya").GetSurname("Petrov").GetPassport("123123").GetAddress("Veteranov").Create();
        var account = bank.AddAccountDebit(client, 20000);
        account.WithdrawalOfMoney(5000);
        Assert.Equal(15000, account.Balance);
    }

    [Fact]
    public void CheckReplenishmentOfTheBalance()
    {
        var bank = _centralBank.RegistrationBank("Alfa", 2000, 4, 3, 10000, 200);
        var builder = new Builder();
        var client = builder.GetName("Vasya").GetSurname("Petrov").GetPassport("123123").GetAddress("Veteranov").Create();
        var account = bank.AddAccountDebit(client, 20000);
        account.ReplenishmentOfTheBalance(10000);
        Assert.Equal(30000, account.Balance);
    }

    [Fact]
    public void CheckTimeAcceleration()
    {
        var bank = _centralBank.RegistrationBank("Alfa", 2000, 4, 10, 10000, 200);
        var builder = new Builder();
        var client = builder.GetName("Vasya").GetSurname("Petrov").GetPassport("123123").GetAddress("Veteranov").Create();
        var account = bank.AddAccountDebit(client, 20000);
        _centralBank.TimeAcceleration(365);
        Assert.Equal(22000, Convert.ToDouble(account.Balance));
    }
}