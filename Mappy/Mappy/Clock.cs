using System.Security;
using System.Runtime.InteropServices;

namespace Mappy
{
    public class Clock
    {
        [SuppressUnmanagedCodeSecurity]
        [DllImport("kernel32.dll")]
        public static extern bool QueryPerformanceFrequency(ref long frequency);

        [SuppressUnmanagedCodeSecurity]
        [DllImport("kernel32.dll")]
        public static extern bool QueryPerformanceCounter(ref long counter);

        long tps;
        long startTime;

        public long TPS { get { return tps; } set { tps = value; } }

        public Clock()
        {
            this.tps = 0;

            QueryPerformanceFrequency(ref tps);

            this.startTime = GetCurrentTime();
        }

        public long GetCurrentTime()
        {
            long time = 0;
            QueryPerformanceCounter(ref time);

            return 1000000 * time / tps;
        }

        public float GetDeltaTime()
        {
            long deltaTime = GetCurrentTime() - startTime;
            return deltaTime;
        }

        public float Restart()
        {
            long now = GetCurrentTime();
            long deltaTime = now - startTime;
            startTime = now;

            return deltaTime;
        }

        /// <summary>
        /// Converts a time into milliseconds
        /// </summary>
        /// <param name="time">The time to transform, in microseconds</param>
        /// <returns>A duration in milliseconds</returns>
        /// <remarks>The 'time' variable is expressed in microseconds. That doesn't mean you
        /// have to translate it to microseconds before passing it to this method. Indeed,
        /// the Clock class returns time as microseconds by default, so you just have to
        /// pass the result from GetDeltaTime() or Restart().</remarks>
        public static float AsMillisecond(float time)
        {
            return time / 1000;
        }

        /// <summary>
        /// Converts a time into seconds
        /// </summary>
        /// <param name="time">The time to transform, in microseconds</param>
        /// <returns>A duration in seconds</returns>
        /// <remarks>The 'time' variable is expressed in microseconds. That doesn't mean you
        /// have to translate it to microseconds before passing it to this method. Indeed,
        /// the Clock class returns time as microseconds by default, so you just have to
        /// pass the result from GetDeltaTime() or Restart().</remarks>
        public static float AsSecond(float time)
        {
            return time / 1000000;
        }
    }
}
