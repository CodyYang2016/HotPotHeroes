sampler2D TextureSampler : register(s0);
float2 screenSize;           // Screen size in pixels
float2 linkPosition;         // Link's position in screen space (pixels)
float visibilityRadius;      // Radius in pixels

float4 MainPS(float2 texCoord : TEXCOORD) : SV_Target
{
    float2 pixelPos = texCoord * screenSize;
    float2 offset = pixelPos - linkPosition;

    float distanceToLink = length(offset);

    float4 color = tex2D(TextureSampler, texCoord);

	// if (distanceToLink < 5.0f)
	// 	return float4(1, 0, 0, 1); // red pixel at center of light
		
    if (distanceToLink < visibilityRadius)
    {
        return color;
    }
    else
    {
        float darknessFactor = saturate((distanceToLink - visibilityRadius) / 160.0f);
        color.rgb *= (1.0f - darknessFactor);
        return color;
		//return float4(1, 0, 0, 1);
    }
}

technique DarknessEffect
{
    pass P0
    {
        PixelShader = compile ps_3_0 MainPS();
    }
}