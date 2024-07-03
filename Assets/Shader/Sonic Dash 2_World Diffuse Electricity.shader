Shader "Sonic Dash 2/World Diffuse Electricity" {
	Properties {
		WaveControl ("Wave Control (Speed x, Size y; Distortion 1, Distortion 2)", Vector) = (650,0.2,67,123)
		_Color ("Colour", Vector) = (1,0.5,0.5,1)
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
}