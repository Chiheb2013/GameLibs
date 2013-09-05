using SFML.Graphics;

namespace Mappy.Textures
{
    public class Frame
    {
        int id;
        int nextFrameId;

        IntRect frame;

        public int ID { get { return id; } }
        public int NextFrameID { get { return nextFrameId; } set { nextFrameId = value; } }

        public IntRect FrameRect { get { return frame; } }

        public Frame(int id, int nextFrameId, IntRect frame)
        {
            this.id = id;
            this.nextFrameId = nextFrameId;

            this.frame = frame;
        }

        public override string ToString()
        {
            return "ID. " + id + " nID. " + nextFrameId;
        }
    }
}
