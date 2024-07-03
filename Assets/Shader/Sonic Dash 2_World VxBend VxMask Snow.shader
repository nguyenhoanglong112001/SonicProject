Shader "Sonic Dash 2/World VxBend VxMask Snow" {
	Properties {
		_Diffuse ("Base (RGB)", 2D) = "white" {}
		_Snow ("Distance", Float) = 3
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = 1;
		}
		ENDCG
	}
	Fallback "Sonic Dash 2/World VxBend Simple Optimised Snow"
}