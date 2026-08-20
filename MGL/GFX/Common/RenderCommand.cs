using System.Drawing;
using Silk.NET.OpenGL;

namespace MGL.GFX.Common;

public static class RenderCommand
{
    public static bool DepthTest
    {
        get;
        set
        {
            if (value)
                Env.Gl.Enable(EnableCap.DepthTest);
            else
                Env.Gl.Disable(EnableCap.DepthTest);
        }
    } = true;
    public static bool Blend
    {
        get;
        set
        {
            if (value)
                Env.Gl.Enable(EnableCap.Blend);
            else
                Env.Gl.Disable(EnableCap.Blend);
        }
    } = true;
    public static bool Multisample
    {
        get;
        set
        {
            if (value)
                Env.Gl.Enable(EnableCap.Multisample);
            else
                Env.Gl.Disable(EnableCap.Multisample);
        }
    } = true;
    public static void ClearColor(Color color)
    {
        Env.Gl.ClearColor(color);
        Env.Gl.Clear(ClearBufferMask.ColorBufferBit);
    }
    public static void ClearDepth()
    {
        Env.Gl.Clear(ClearBufferMask.DepthBufferBit);
    }
}