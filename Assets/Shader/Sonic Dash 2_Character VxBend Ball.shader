Shader "Sonic Dash 2/Character VxBend Ball" {
	Properties {
		_Diffuse ("Base (RGB)", 2D) = "white" {}
		_RimP ("Rim Power", Float) = 3
		_RimI ("Rim Int", Float) = 0.5
		_SpecPower ("Specular Power", Float) = 3
		_SpecInt ("Specular Int", Float) = 0.5
		_Color ("Specular Colour", Vector) = (1,0.5,0.5,1)
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = _Color.rgb;
			o.Alpha = _Color.a;
		}
		ENDCG
	}
	Fallback "Sonic Dash 2/World VxBend Simple Optimised"
}