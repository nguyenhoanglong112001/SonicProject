Shader "Sonic Dash 2/Water_Surface_2" {
	Properties {
		_Cube ("Reflection Cubemap", Cube) = "_Skybox" {}
		_BumpMap ("Normalmap", 2D) = "bump" {}
		_LightMap ("Lightmap (RGB)", 2D) = "white" {}
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
}