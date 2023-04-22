namespace YummyDrop_online_store.Services.BonusService
{
    public class BonusService: IBonusService
    {
        public event EventHandler BonusUpdated;
        private int _bonus = 300;
        public int Bonus => _bonus;

        public void AddBonuses(int bonus)
        {
            if (bonus < 0) return;
            _bonus += bonus;
            UpdateBonus(_bonus);
        }

        public void RemoveBonuses(int bonus)
        {
            if (bonus < 0) return;
            _bonus -= bonus;
            UpdateBonus(_bonus);
        }

        public void RestartBonusDeposit()
        {
            _bonus = 300;
            UpdateBonus(_bonus);
        }

        public void UpdateBonus(int bonus)
        {
            _bonus = bonus;
            BonusUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
