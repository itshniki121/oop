using Banks.Exceptions;

namespace Banks.Entites;

public class Builder
{
    private string? _name;
    private string? _surname;
    private string? _passport;
    private string? _address;

    public Builder GetName(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new BanksExceptions("Wrong Name");
        _name = name;
        return this;
    }

    public Builder GetSurname(string surname)
    {
        if (string.IsNullOrEmpty(surname))
            throw new BanksExceptions("Wrong surname");
        _surname = surname;
        return this;
    }

    public Builder GetPassport(string? passport)
    {
        _passport = passport;
        return this;
    }

    public Builder GetAddress(string? address)
    {
        _address = address;
        return this;
        }

    public Client Create()
    {
        {
            if (string.IsNullOrEmpty(_name))
                throw new BanksExceptions("Wrong Name");
            if (string.IsNullOrEmpty(_surname))
                throw new BanksExceptions("Wrong Surname");

            var client = new Client(_name, _surname, _passport, _address);
            return client;
        }
    }
}