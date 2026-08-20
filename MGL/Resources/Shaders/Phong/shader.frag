#version 330 core
                                    
in vec2 frag_texCoords;
out vec4 out_color;

uniform sampler2D uTexture;

uniform vec3 lightPos;
uniform vec3 lightColor = vec3(1, 1, 1);

uniform vec3 viewPos;

uniform bool shaded = true;

uniform float specularStrength;
uniform float ambientStrength;

in vec3 FragPos;  
in vec3 Normal;  

struct Light {
    vec3 direction;
  
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};

void main()
{  
    if(shaded){
        vec3 norm = normalize(Normal);
        vec3 lightDir = normalize(lightPos - FragPos);

        float diff = max(dot(norm, lightDir), 0.0);
        vec3 diffuse = diff * lightColor;

        vec3 ambient = ambientStrength * lightColor;

        vec3 viewDir = normalize(viewPos - FragPos);
        vec3 reflectDir = reflect(-lightDir, norm);


        float spec = pow(max(dot(viewDir, reflectDir), 0.0), 16);
        vec3 specular = specularStrength * spec * vec3(1,1,1);

        vec3 result = ambient + diffuse + specular;
        
        out_color =  vec4(result, 1.0) * texture(uTexture, frag_texCoords);
    }
    else{
        out_color = texture(uTexture, frag_texCoords);
    }
}