shader_type canvas_item;

uniform bool makeWhite = false;

void fragment() {
	vec4 color = texture(TEXTURE, UV);
    if (makeWhite)
        color.rgb = vec3(1.0);
    COLOR = color;
}