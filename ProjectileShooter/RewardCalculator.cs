namespace bluewarp.ProjectileShooter
{
    /// <summary>
    /// Used for calculating reward points.
    /// </summary>
    public static class RewardCalculator
    {
        /// <summary>
        /// Scales reward based on difficulty (Player Health multiplier)
        /// </summary>
        /// <param name="reward">Base reward</param>
        /// <returns></returns>
        public static int CalculateRewardBasedOnDifficulty(int reward)
        {
            return (reward * (GameConstants.Player.MaxHealtMultiplier - GameSettings.Player.HealthMultiplier + 1));
        }
    }
}
