Shader "Sonic Dash 2/World Diffuse Enerbeam" {
	Properties {
		_MainTex ("Particle Texture", 2D) = "white" {}
		WaveControl ("Wave Control (Speed x, Size y; Distortion 1, Distortion 2)", Vector) = (650,0.2,67,123)
		enerbeam_color ("enerbeam colour", Vector) = (1,0.5,0.5,1)
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
}