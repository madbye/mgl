using System;
using Silk.NET.OpenGL;

namespace MGL.GFX.Shaders;

public enum ShaderType
{
    Vertex,
    Fragment,
    Geometry
}

public class Shader(string source, ShaderType shaderType) : IDisposable
{
    public uint Handle { get; private set; }

    public ShaderType Type { get; } = shaderType;
    public string Source { get; } = source;

    public void Compile()
    {
        switch (Type)
        {
            case ShaderType.Vertex:
                Handle = Env.Gl.CreateShader(GLEnum.VertexShader);
                break;
            case ShaderType.Fragment:
                Handle = Env.Gl.CreateShader(GLEnum.FragmentShader);
                break;
        }
        Env.Gl.ShaderSource(Handle, Source);
        
        Env.Gl.CompileShader(Handle);
        Env.Gl.GetShader(Handle, ShaderParameterName.CompileStatus, out int status);
        if (status != (int) GLEnum.True)
            throw new Exception(Type + " shader failed to compile: " + Env.Gl.GetShaderInfoLog(Handle));
    }

    public void Dispose()
    {
        Env.Gl.DeleteShader(Handle);
    }
}