﻿using UnityEngine;

namespace Code
{
    public static class Constants
    {
        public const float MOVE_ANIMATION_TIME_SEC = 0.10f;
        public const float DELAY_BEFORE_SPAWN_SEC = MOVE_ANIMATION_TIME_SEC + 0.05f;
        public const int MAX_UNDO = 5;
        public static readonly Vector2Int DIMENSIONS = new Vector2Int(4, 4);

        public static class Resouces
        {
            public const string COROUTINE_RUNNER = "CoroutineRunner";
        }
    }
}