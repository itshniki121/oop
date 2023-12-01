using Banks.Entites;

namespace Banks.CenterBanks;

public interface IObservable
{
    void AddObserver(IObserver o);
    void RemoveObserver(IObserver o);
    void UpdateObservers();
}