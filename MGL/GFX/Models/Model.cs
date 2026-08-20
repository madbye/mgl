using System.Numerics;
using MGL.GFX.Models;

namespace MGL.GFX.Models;

public class Model
{
    public ModelNode Root { get; set; }
    public Material[] Materials { get; set; }

    public Model(ModelNode root, Material[] materials)
    {
        Root = root;
        Materials = materials;
    }

    public void Draw(Matrix4x4 transform)
    {
        Root.Draw(transform, Materials);
    }
}