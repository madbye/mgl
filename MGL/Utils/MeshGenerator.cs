using System;
using System.Collections.Generic;
using MGL.GFX;
using MGL.GFX.VertexAttributes;

namespace MGL.Utils;

public static class MeshGenerator
{
    
    public static VAO GenQuad()
    {
        VertexAttributePointer pos = new(0, 3, 0); 
        VertexAttributePointer tex = new(2, 2, 3);
        float[] vertices =
        {
            1f, -1f, 0f, 1, 0,
            1f, 1f, 0f, 1, 1,
            -1f, 1f, 0f, 0, 1,
            -1f,  -1f, 0f, 0, 0
        };
        int[] indices =
        {
            0, 1, 3,
            1, 2, 3
        };
        return new(vertices, indices, new[]{pos, tex});
    }
    
    public static VAO GenCube()
    {
        VertexAttributePointer pos = new(0, 3, 0);
        VertexAttributePointer normal = new(1, 3, 3);
        VertexAttributePointer tex = new(2, 2, 6);

        float[] vertices =
        {
            -0.5f, -0.5f,  0.5f,  0, 0, 1,  0, 0,
            0.5f, -0.5f,  0.5f,  0, 0, 1,  1, 0,
            0.5f,  0.5f,  0.5f,  0, 0, 1,  1, 1,
            -0.5f,  0.5f,  0.5f,  0, 0, 1,  0, 1,
    
            -0.5f, -0.5f, -0.5f,  0, 0, -1,  1, 0,
            0.5f, -0.5f, -0.5f,  0, 0, -1,  0, 0,
            0.5f,  0.5f, -0.5f,  0, 0, -1,  0, 1,
            -0.5f,  0.5f, -0.5f,  0, 0, -1,  1, 1,
    
            -0.5f,  0.5f, -0.5f,  0, 1, 0,  0, 1,
            0.5f,  0.5f, -0.5f,  0, 1, 0,  1, 1,
            0.5f,  0.5f,  0.5f,  0, 1, 0,  1, 0,
            -0.5f,  0.5f,  0.5f,  0, 1, 0,  0, 0,

            -0.5f, -0.5f, -0.5f,  0, -1, 0,  0, 0,
            0.5f, -0.5f, -0.5f,  0, -1, 0,  1, 0,
            0.5f, -0.5f,  0.5f,  0, -1, 0,  1, 1,
            -0.5f, -0.5f,  0.5f,  0, -1, 0,  0, 1,

            0.5f, -0.5f, -0.5f,  1, 0, 0,  1, 0,
            0.5f, -0.5f,  0.5f,  1, 0, 0,  0, 0,
            0.5f,  0.5f,  0.5f,  1, 0, 0,  0, 1,
            0.5f,  0.5f, -0.5f,  1, 0, 0,  1, 1,

            -0.5f, -0.5f, -0.5f, -1, 0, 0,  0, 0,
            -0.5f, -0.5f,  0.5f, -1, 0, 0,  1, 0,
            -0.5f,  0.5f,  0.5f, -1, 0, 0,  1, 1,
            -0.5f,  0.5f, -0.5f, -1, 0, 0,  0, 1
        };

        int[] indices =
        {
            0, 1, 2,  2, 3, 0,    
            4, 5, 6,  6, 7, 4,    
            8, 9, 10, 10, 11, 8,  
            12, 13, 14, 14, 15, 12,
            16, 17, 18, 18, 19, 16,
            20, 21, 22, 22, 23, 20 
        };

        return new(vertices, indices, new[] { pos, normal, tex });
    }

    public static VAO GenUVSphere(uint stackCount, uint sectorCount)
    {
        var vertices = new List<float>();
        
        for (int i = 0; i <= stackCount; ++i)
        {
            float stackAngle = MathF.PI / 2 - i * (MathF.PI / stackCount); 
            float xy = MathF.Cos(stackAngle);
            float z = MathF.Sin(stackAngle);

            for (int j = 0; j <= sectorCount; ++j)
            {
                float sectorAngle = j * (2 * MathF.PI / sectorCount); 
                
                float x = xy * MathF.Cos(sectorAngle);
                float y = xy * MathF.Sin(sectorAngle);
                
                float nx = x;
                float ny = y;
                float nz = z;
                
                float u = (float)j / sectorCount;
                float v = (float)i / stackCount;

                vertices.AddRange(new float[] { x, y, z, nx, ny, nz, u, v });
            }
        }
        var indices = new List<int>();
        
        for (uint i = 0; i < stackCount; ++i) 
        {
            uint k1 = i * (sectorCount + 1);     
            uint k2 = k1 + (sectorCount + 1);   

            for (int j = 0; j < sectorCount; ++j, ++k1, ++k2)
            {
                if (i != 0)
                {
                    indices.Add((int)k1);
                    indices.Add((int)k2);
                    indices.Add((int)k1 + 1);
                }
                
                if (i != (stackCount - 1))
                { 
                    indices.Add((int)k1 + 1);
                    indices.Add((int)k2);
                    indices.Add((int)k2 + 1);
                }
            }
        }
        
        VertexAttributePointer pos = new(0, 3, 0);
        VertexAttributePointer norm = new(1, 3, 3);
        VertexAttributePointer tex = new(2, 2, 6);

        return new(vertices.ToArray(), indices.ToArray(), new[] { pos,norm, tex });
    }

    public static VAO GenPlane(float size = 1, ushort textureTiling = 1)
    {
        VertexAttributePointer pos = new(0, 3, 0);
        VertexAttributePointer normal = new(1, 3, 3);
        VertexAttributePointer tex = new(2, 2, 6);
        
        float[] vertices =
        {
            size/2, 0, size/2, 0, 1, 0, -textureTiling,0,
            size/2, 0, -size/2, 0, 1, 0, -textureTiling,-textureTiling,
            -size/2, 0, -size/2, 0, 1, 0, 0,-textureTiling,
            -size/2, 0, size/2, 0, 1, 0, 0,0,
        };
        int[] indices =
        {
            0, 1, 3,
            1, 2, 3
        };
        return new(vertices, indices, new[] { pos, normal, tex });
    }
}