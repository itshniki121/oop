using Banks.Accounts;
using Banks.Exceptions;

namespace Banks.Entites;

public class Transaction
{
    public Transaction(decimal money, string type, Account account)
    {
        Money = money;
        Type = type;
        Account = account;
        AccountToTransfer = null;
        Status = true;
    }

    public Transaction(decimal money, string type, Account account1, Account account2)
    {
        Money = money;
        Type = type;
        Account = account1;
        AccountToTransfer = account2;
        Status = true;
    }

    public bool Status { get; private set; }

    public Account? AccountToTransfer { get; set; }

    public decimal Money { get; set; }
    public string Type { get; set; }
    public Account Account { get; set; }

    public void Undo()
    {
        if (Status == false)
            throw new BanksExceptions("Transaction already canceled");
        switch (Type)
        {
            case "T":
            {
                if (AccountToTransfer != null)
                    AccountToTransfer.Balance -= Money;
                Account.Balance += Money;
                break;
            }

            case "W":
                Account.Balance += Money;
                break;
            case "R":
                Account.Balance -= Money;
                break;
        }

        Status = false;
    }
}