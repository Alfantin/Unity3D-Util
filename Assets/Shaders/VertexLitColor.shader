Shader "Mobile/VertexLitColored" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
	}
	SubShader {
		
		Pass{
			
			Material{
				Diffuse [_Color]
				Ambient [_Color]
			}
			
			Lighting On
			Fog { Mode Off }
		}

	} 
	FallBack "VertexLit", 2
}