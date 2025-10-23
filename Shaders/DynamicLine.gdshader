shader_type canvas_item;
uniform sampler2D SCREEN_TEXTURE : hint_screen_texture, filter_linear_mipmap;
uniform vec4 line_color : source_color = vec4(0, 0, 0, 1.0);
uniform float threshold = 0.2;
uniform bool invert_on_dark = true;

void fragment() {
    vec4 screen_col = textureLod(SCREEN_TEXTURE, SCREEN_UV, 0.0);
    float brightness = dot(screen_col.rgb, vec3(0.299, 0.587, 0.114)); //Brightness equation

    vec4 final_color = line_color;
    if (invert_on_dark && brightness < threshold) {
        final_color.rgb = vec3(1.0) - line_color.rgb;
    }

    COLOR = final_color;
}