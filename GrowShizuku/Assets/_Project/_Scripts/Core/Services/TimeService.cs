using System;
using UnityEngine;

namespace Shizuku.Services
{
    public static class TimeService
    {
        public static double Now => DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }
}
