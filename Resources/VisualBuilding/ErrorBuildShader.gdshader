shader_type spatial;

uniform vec4 base_color : source_color = vec4(1.0, 0.0, 0.0, 0.3); // green fill
uniform vec4 border_color : source_color = vec4(0.7, 0.2, 0.2, 7.0); // dark green border
uniform float border_thickness = 0.03;

void fragment() {
    vec2 uv = fract(UV); // Normalize to 0–1, even if mesh UVs go beyond it

    // Determine if we are in the border region
    float border_mask = 
        step(uv.x, border_thickness) +                       // left
        step(uv.y, border_thickness) +                       // bottom
        step(1.0 - uv.x, border_thickness) +                 // right
        step(1.0 - uv.y, border_thickness);                  // top

    border_mask = clamp(border_mask, 0.0, 1.0);

    vec4 color = mix(base_color, border_color, border_mask);
    ALBEDO = color.rgb;
    ALPHA = color.a;
}
