using System.Drawing;
using MGL.GFX.Common;
using Silk.NET.OpenGL;

namespace MGL.GFX.Textures;

public enum TextureFilter
{
    Linear, 
    Nearest
}

public class Texture2D
{
    public uint Handle { get; private set; }
    public uint Width { get; private set; }
    public uint Height { get; private set; }
    
    public unsafe Texture2D(uint width, uint height)
    {
        Width = width;
        Height = height;

        Handle = Env.Gl.GenTexture();

        Bind();

        Env.Gl.PixelStore(PixelStoreParameter.UnpackAlignment, 1);
        
        Env.Gl.TexImage2D(
            TextureTarget.Texture2D,
            0,
            InternalFormat.Rgba8, 
            (uint)width,
            (uint)height,
            0,
            PixelFormat.Rgba,
            PixelType.UnsignedByte,
            null                
        );
        
        Env.Gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
        Env.Gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        
        Env.Gl.BindTexture(TextureTarget.Texture2D, 0);
    }

    public Texture2D(uint handle, uint width, uint height)
    {
        Handle = handle;
        Width = width;
        Height = height;
    }

    public static unsafe Texture2D FromImage(Image image, TextureFilter textureFilter, bool enableMipmaps)
    {
        uint handle = Env.Gl.GenTexture();

        Env.Gl.BindTexture(TextureTarget.Texture2D, handle);

        Env.Gl.PixelStore(PixelStoreParameter.UnpackAlignment, 1);
        
        switch (image.Channels)
        {
            case 1:
                fixed (byte* ptr = image.PixelData)
                    Env.Gl.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat.Red, (uint)image.Width,
                        (uint)image.Height, 0, PixelFormat.Red, PixelType.UnsignedByte, ptr);
                break;
            case 3:
                fixed (byte* ptr = image.PixelData)
                    Env.Gl.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat.Rgb, (uint)image.Width,
                        (uint)image.Height, 0, PixelFormat.Rgb, PixelType.UnsignedByte, ptr);
                break;
            case 4:
                fixed (byte* ptr = image.PixelData)
                    Env.Gl.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat.Rgba, (uint)image.Width,
                        (uint)image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, ptr);
                break;
        }

        
        var minFilter = textureFilter == TextureFilter.Linear 
            ? (enableMipmaps ? TextureMinFilter.LinearMipmapLinear : TextureMinFilter.Linear)
            : (enableMipmaps ? TextureMinFilter.NearestMipmapNearest : TextureMinFilter.Nearest);

        var magFilter = textureFilter == TextureFilter.Linear ? TextureMagFilter.Linear : TextureMagFilter.Nearest;
        
        Env.Gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
        Env.Gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
        Env.Gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)minFilter);
        Env.Gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)magFilter);
    
        if(enableMipmaps)
            Env.Gl.GenerateMipmap(TextureTarget.Texture2D);
        
        Env.Gl.BindTexture(TextureTarget.Texture2D, 0);

        return new(handle, image.Height, image.Width);
    }

    public void Bind(Silk.NET.OpenGL.TextureUnit textureSlot = Silk.NET.OpenGL.TextureUnit.Texture0)
    {
        Env.Gl.ActiveTexture(textureSlot);
        Env.Gl.BindTexture(TextureTarget.Texture2D, Handle);
    }
    
    public unsafe void SetData(Rectangle bounds, byte[] data)
    {
        Bind();
        fixed (byte* ptr = data)
        {
            Env.Gl.TexSubImage2D(
                target: TextureTarget.Texture2D,
                level: 0,
                xoffset: bounds.Left,
                yoffset: bounds.Top,
                width: (uint)bounds.Width,
                height: (uint)bounds.Height,
                format: PixelFormat.Rgba,   
                type: PixelType.UnsignedByte,
                pixels: ptr
            );
        }
    }
}