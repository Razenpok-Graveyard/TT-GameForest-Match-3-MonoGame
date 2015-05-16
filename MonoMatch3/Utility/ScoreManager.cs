namespace MonoMatch3
{
    static class ScoreManager
    {
        public static int Score { get; set; }

        public static void Add(int value)
        {
            Score += value;
        }

        public static void Subtract(int value)
        {
            Score -= value;
        }
    }
}
