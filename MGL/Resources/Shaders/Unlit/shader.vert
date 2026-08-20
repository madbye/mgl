#version 330 core
                                  
layout (location = 0) in vec3 aPosition;
layout (location = 2) in vec2 aTextureCoord;

out vec2 frag_texCoords;

uniform mat4 proj;
uniform mat4 view;
uniform mat4 model;

void main()
{
    gl_Position = proj * view * model * vec4(aPosition, 1.0);
    frag_texCoords = aTextureCoord;
}