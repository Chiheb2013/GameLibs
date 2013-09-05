using System.Collections.Generic;

namespace Mappy.Entities
{
    public class DirectionAnimationCouple
    {
        Dictionary<Direction, string> couples;

        public Dictionary<Direction, string> Couples { get { return couples; } }

        public string this[Direction direction]
        {
            get
            {
                ExceptionHelper.AssertIsInDictionary<Direction, string>(couples, direction, "DAC.this[]");
                return couples[direction];
            }
        }

        public DirectionAnimationCouple()
        {
            this.couples = new Dictionary<Direction, string>();
        }

        public DirectionAnimationCouple(Dictionary<Direction, string> couples)
        {
            this.couples = couples;
        }
    }
}
