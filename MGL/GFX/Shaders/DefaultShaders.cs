using System.Reflection;

namespace MGL.GFX.Shaders;

public static class DefaultShaders
{
    private static ShaderProgram? _phong;
    private static ShaderProgram? _unlit;
    private static ShaderProgram? _text;
    private static ShaderProgram? _blit;
    private static ShaderProgram? _pbr;

    public static ShaderProgram GetPhong()
    {
        _phong ??= LoadProgram("MGL.Resources.Shaders.Phong.shader.frag",
            "MGL.Resources.Shaders.Phong.shader.vert");

        return _phong;
    }

    public static ShaderProgram GetUnlit()
    {
        _unlit ??= LoadProgram("MGL.Resources.Shaders.Unlit.shader.frag",
            "MGL.Resources.Shaders.Unlit.shader.vert");

        return _unlit;
    }
    public static ShaderProgram GetText()
    {
        _text ??= LoadProgram("MGL.Resources.Shaders.Text.shader.frag",
            "MGL.Resources.Shaders.Text.shader.vert");
        
        return _text;
    }
    public static ShaderProgram GetBlit()
    {
        _blit ??= LoadProgram("MGL.Resources.Shaders.Blit.shader.frag",
            "MGL.Resources.Shaders.Blit.shader.vert");
        
        return _blit;
    }
    public static ShaderProgram GetPBR()
    {
        _pbr ??= LoadProgram("MGL.Resources.Shaders.PBR.shader.frag",
            "MGL.Resources.Shaders.PBR.shader.vert");
        
        return _pbr;
    }
    private static ShaderProgram LoadProgram(string fragment, string vertex)
    {
        Shader frag;
        Shader vert;
        using (var reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(fragment)))
        {
            frag = new Shader(reader.ReadToEnd(), ShaderType.Fragment);
        }
        using (var reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(vertex)))
        {
            vert = new Shader(reader.ReadToEnd(), ShaderType.Vertex);
        }
        frag.Compile();
        vert.Compile();
        return new (new[]{frag, vert});
    }

}