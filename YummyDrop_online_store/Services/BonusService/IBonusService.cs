namespace YummyDrop_online_store.Services.BonusService
{
    public interface IBonusService
    {
        public event EventHandler BonusUpdated;
        public int Bonus { get; }
        public void AddBonuses(int bonus);
        public void RemoveBonuses(int bonus);
        public void RestartBonusDeposit();
        public void UpdateBonus(int bonus);
    }
}
