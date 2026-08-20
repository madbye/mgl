using System;

namespace MGL.Utils;

public static class Easings
{
    public static double Interpolate(double a, double b, double n) { return a + n * (b - a); }
    // Enpo
    public static double EaseInEnpo(double n) { return n == 0 ? 0 : Math.Pow(2, 10 * n - 10 ); }
    public static double EaseOutEnpo(double n) { return n == 1 ? 1 : 1 - Math.Pow(2, -10 * n); }
    public static double EaseInOutEnpo(double n)
    {
        return n == 0 
            ? 0 
            : n == 1
                ? 1
                : n < 0.5 ? Math.Pow(2, 20 * n - 10) / 2 
                    : (2 - Math.Pow(2, -20 * n + 10)) / 2;
    }

    public static float ParabolicArc(float n)
    {
        return 4f * n * (1f - n);
    }
}