#ifdef GL_ES
    #define LOWP lowp
    precision mediump float;
#else
    #define LOWP
#endif

// Uniforms
uniform sampler2D TextureSampler;

// Varyings
varying vec4 v_color;
varying vec2 v_texCoords;

void main()
{
    // 1. Sample the texture (this gives us the alpha mask of the font)
    vec4 texColor = texture2D(TextureSampler, v_texCoords);
        
        // Умножаем цвет текстуры на цвет вершины (v_color).
        // Это окрасит шрифт в красный, сохранив форму символов.
        gl_FragColor = texColor * v_color;
}