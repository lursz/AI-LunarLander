using Godot;

namespace MoonLander.entities;

public static class Converter
{
    public static double ConvertToRadian(double degree)
    {
        return degree * Mathf.Pi / 180;
    }

    public static double ConvertToDegrees(double radian)
    {
        return radian * 180 / Mathf.Pi;
    }

}