Shader "Sonic Dash 2/World VxBend Glass" {
	Properties {
		_Color ("Main Color", Vector) = (1,1,1,1)
		_Cubemap ("_Cubemap", Cube) = "black" {}
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