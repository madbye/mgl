using System.Drawing;
using System.Numerics;
using MGL;
using MGL.GFX.Common;
using MGL.GFX.Models;
using MGL.GFX.Shaders;
using MGL.GFX.UI.TextRendering;
using MGL.IO;

namespace Example;

class Program
{
    static void Main(string[] args)
    {
        var window = new Window(1280, 720, "MGL Window");
        window.Initialize();

        TextRenderer textRenderer = new(DefaultShaders.GetText());
        textRenderer.Resize(1280,720);
        
        Font font = textRenderer.LoadFont("font.ttf", 48);
        TextStyle textStyle = new TextStyle(font, Color.Green, stroked:true);
        
        Model model = ModelLoader.LoadModel("scene.gltf");
        
        OrthoCamera orthoCamera = new OrthoCamera(new Vector3(10), new Quaternion(), 1280, 720, 10, 0.1f, 1000);
        
        orthoCamera.LookAt(Vector3.Zero);
        
        window.OnResize += (width, height) =>
        {
            orthoCamera.Width = width;
            orthoCamera.Height = height;
            
            textRenderer.Resize(width, height);
        };
        while (!window.ShouldClose())
        {
            window.DoEvents();
            
            RenderCommand.ClearColor(Color.DimGray);
            RenderCommand.ClearDepth();
            
            orthoCamera.SetMatricesToProgram(DefaultShaders.GetUnlit());
            
            model.Draw(Matrix4x4.Identity);
            
            textRenderer.DrawText("Hello World!", textStyle, new Vector2(10));
            
            window.SwapBuffers();
        }
    }
}