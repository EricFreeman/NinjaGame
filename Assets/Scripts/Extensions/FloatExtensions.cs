using System;

namespace Assets.Scripts.Extensions
{
    public static class FloatExtensions
    {
        public static float IncrementTo(this float source, float destination, float increment)
        {
            if (source == destination) return source;
            source += destination > source ? increment : (increment * -1);
            if (Math.Abs(source - destination) < increment) source = destination;

            return source;
        }
    }
}