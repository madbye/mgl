#version 330 core
                                  
layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec3 aNormal;
layout (location = 2) in vec2 aTextureCoord;

out vec2 frag_texCoords;

uniform mat4 proj;
uniform mat4 view;
uniform mat4 model;

out vec3 FragPos;  
out vec3 Normal;

void main()
{
    Normal = mat3(transpose(inverse(model))) * aNormal;  
    FragPos = vec3(model * vec4(aPosition, 1.0));
    gl_Position = proj * view * model * vec4(aPosition, 1.0);
    frag_texCoords = aTextureCoord;
}