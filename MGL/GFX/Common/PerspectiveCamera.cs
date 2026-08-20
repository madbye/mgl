using System;
using System.Numerics;
using MGL.GFX.Shaders;

namespace MGL.GFX.Common;

public class PerspectiveCamera
{
    public Vector3 Position { get; set; }
    public Quaternion Rotation { get; set; }
    public float FOV { get; set; }
    public uint Width { get; set; }
    public uint Height { get; set; }
    public float NearPlaneDistance { get; set; }
    public float FarPlaneDistance { get; set; }
    
    public PerspectiveCamera(Vector3 position, Quaternion rotation, uint width, uint height, float fov, float near, float far)
    {
        Position = position;
        Rotation = rotation;
        Width = width;
        Height = height;
        FOV = fov;
        NearPlaneDistance = near;
        FarPlaneDistance = far;
    }
    
    public Matrix4x4 GetProjectionMatrix()
    {
        return Matrix4x4.CreatePerspectiveFieldOfView(FOV * (MathF.PI / 180f), (float)Width/Height, NearPlaneDistance, FarPlaneDistance);
    }
    
    public Matrix4x4 GetViewMatrix()
    {
        if (Matrix4x4.Invert(Matrix4x4.CreateFromQuaternion(Rotation) * Matrix4x4.CreateTranslation(Position), out Matrix4x4 viewMat))
        {
            return viewMat;
        }
        return Matrix4x4.Identity;
    }
    public void LookAt(Vector3 target, Vector3 up = default)
    {
        if (up == default) up = Vector3.UnitY;
        
        Matrix4x4 viewMatrix = Matrix4x4.CreateLookAt(Position, target, up);
        if (Matrix4x4.Invert(viewMatrix, out Matrix4x4 invertedMat))
        {
            Matrix4x4.Decompose(invertedMat, out _, out Quaternion rotation, out _);
            Rotation = rotation;
        }
    }

    public void SetMatricesToProgram(ShaderProgram program)
    {
        program.SetUniform("view", GetViewMatrix());
        program.SetUniform("proj", GetProjectionMatrix());
    }
}