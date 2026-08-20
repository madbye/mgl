using System.Drawing;
using System.IO;
using MGL.GFX;
using MGL.GFX.Common;
using StbImageSharp;

namespace MGL.IO;

public enum ColorComponents
{
    R,
    RGB,
    RGBA
}
public static class ImageLoader
{
    public static Image LoadImage(Stream stream, ColorComponents colorComponents)
    {
        StbImage.stbi_set_flip_vertically_on_load(1);
        var (cc, channels) = GetStbColorComponents(colorComponents);
        
        ImageResult result = ImageResult.FromStream(stream, cc);
        return new Image((uint)result.Width, (uint)result.Height, channels, result.Data);
    }
    
    public static Image LoadImage(string path, ColorComponents colorComponents)
    {
        using (FileStream stream = File.OpenRead(path))
        {
            return LoadImage(stream, colorComponents);
        }
    }

    public static Image GenCheckerboard(Color a, Color b, int size)
    {
        byte[] pixels = new byte[size * size * 3];

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                bool useColorA = ((x + y) & 1) == 0;
                Color current = useColorA ? a : b;

                int index = (y * size + x) * 3;
                pixels[index]     = current.R;
                pixels[index + 1] = current.G;
                pixels[index + 2] = current.B;
            }
        }

        return new((uint)size, (uint)size, 3, pixels);
    }
    private static (StbImageSharp.ColorComponents cc, byte channels) GetStbColorComponents(ColorComponents colorComponents)
    {
        return colorComponents switch
        {
            ColorComponents.R    => (StbImageSharp.ColorComponents.Grey, 1),
            ColorComponents.RGB  => (StbImageSharp.ColorComponents.RedGreenBlue, 3),
            ColorComponents.RGBA => (StbImageSharp.ColorComponents.RedGreenBlueAlpha, 4),
            _ => throw new System.ArgumentOutOfRangeException(nameof(colorComponents))
        };
    }
}