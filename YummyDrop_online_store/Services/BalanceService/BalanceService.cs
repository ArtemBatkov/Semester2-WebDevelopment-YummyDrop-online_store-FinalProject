namespace YummyDrop_online_store.Services.BalanceService
{
    public class BalanceService: IBalanceService
    {
        public event EventHandler BalanceUpdated;
        private decimal _balance = 300;
        public decimal Balance => _balance;

        public void AddToBalance(decimal sum)
        {
            if (sum < 0) return;
            _balance += sum;
            UpdateBalance(_balance);
        }

        public void RemoveFromBalance(decimal sum)
        {
            if (sum < 0) return;
            _balance -= sum;
            UpdateBalance(_balance);
        }

        public void RestartDeposit()
        {
            _balance = 300;
        }

        public void UpdateBalance(decimal balance)
        {
            _balance = balance;
            BalanceUpdated?.Invoke(this, EventArgs.Empty);
        }

    }
}
