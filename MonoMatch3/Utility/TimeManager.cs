using System;
using Microsoft.Xna.Framework;

namespace MonoMatch3
{
    static class TimeManager
    {
        private static int timeLimit;
        private static double remainingTime;

        public static int RemainingTime
        {
            get { return Convert.ToInt32(remainingTime); }
        }

        public static Action OnTimeUp;

        public static void StartTimer(int time = 60)
        {
            timeLimit = time;
            remainingTime = timeLimit;
        }

        public static void UpdateTimer(GameTime gameTime)
        {
            remainingTime -= gameTime.ElapsedGameTime.TotalSeconds;
            if (remainingTime > 0) return;
            if (OnTimeUp != null)
                OnTimeUp();
        }
    }
}
