using System;

namespace Code.Data
{
    [Serializable]
    public class Vector2IntData
    {
        public int X;
        public int Y;

        public Vector2IntData(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}