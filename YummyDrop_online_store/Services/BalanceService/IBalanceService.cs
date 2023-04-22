namespace YummyDrop_online_store.Services.BalanceService
{
    public interface IBalanceService
    {
        public decimal Balance { get; }
        public event EventHandler BalanceUpdated;

        public void AddToBalance(decimal sum);
        public void RemoveFromBalance(decimal sum);
        public void RestartDeposit();
        public void UpdateBalance(decimal balance);

    }
}
