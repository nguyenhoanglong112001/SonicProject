Shader "Sonic Dash 2/Character VxBend Reflect" {
	Properties {
		_Diffuse ("Base (RGB)", 2D) = "white" {}
		_RimP ("Rim Power", Float) = 3
		_RimI ("Rim Int", Float) = 0.5
		_reflection_cubemap ("_reflection_cubemap", Cube) = "skybox" {}
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
	Fallback "Sonic Dash 2/World VxBend Simple Optimised"
}