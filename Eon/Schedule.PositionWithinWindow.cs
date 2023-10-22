using System.Diagnostics.CodeAnalysis;

namespace Eon;

public abstract partial record Schedule
{
    /// <summary>
    /// Provides a base <see cref="Schedule"/> that allows for emissions of
    /// <see cref="Duration"/> to conform to fix schedule of a <see cref="Position"/>
    /// within a window of time.
    /// For example a specified second in a minute; hour in a day;
    /// </summary>
    /// <remarks>The window size needs to be fixed; This cannot be used to
    /// build a day in month <see cref="Schedule"/> as the window of time; month
    /// can change</remarks>
    private abstract record SchPositionWithinWindow(
        uint Position,
        Func<DateTimeOffset>? CurrentTimeFunction = null
    ) : Schedule
    {
        /// <summary>
        /// Position to maintain within the window
        /// </summary>
        public uint Position { get; } = Position;

        /// <summary>
        /// Current time function
        /// </summary>
        public Func<DateTimeOffset>? CurrentTimeFunction { get; } = CurrentTimeFunction;

        public override int? Count => null;
        public override bool CanCount => false;

        /// <summary>
        /// Minimum <see cref="Position"/> that can be requested
        /// </summary>
        protected virtual uint Min => 1;

        /// <summary>
        /// Maximum <see cref="Position"/> that can be requested
        /// </summary>
        protected virtual uint Max => Width - Min;

        /// <summary>
        /// Returns the current `position` in the window based on the current `now`
        /// date time
        /// </summary>
        /// <param name="now">current <see cref="DateTimeOffset"/></param>
        /// <returns>current position on the window</returns>
        protected abstract double Current(DateTimeOffset now);

        /// <summary>
        /// Fixed width of the window
        /// </summary>
        protected abstract uint Width { get; }

        /// <summary>
        /// "Safe" rounded <see cref="Position"/> to maintain in the window
        /// </summary>
        private uint RoundedPosition
        {
            get
            {
                if (Position > Max)
                    return Max;
                if (Position < Min)
                    return Min;
                return Position;
            }
        }

        /// <summary>
        /// Given the number of `steps` it returns a <see cref="Duration"/>
        /// that represents that; for example number of seconds, minutes or hours
        /// </summary>
        /// <param name="steps">number of steps required to get to a position in the window</param>
        /// <returns><see cref="Duration"/> representing the number of steps required
        /// to get to <see cref="Position"/> in the window</returns>
        protected abstract Duration DurationToPosition(double steps);

        [SuppressMessage("Blocker Bug", "S2190:Loops and recursions should not be infinite")]
        public override IEnumerator<Duration> GetEnumerator()
        {
            var now = CurrentTimeFunction ?? LiveCurrentTimeFunction;

            while (true)
            {
                var steps = RoundedPosition - Current(now());
                steps = steps > 0 ? steps : steps + Width;
                yield return DurationToPosition(steps);
            }
        }
    }
}
