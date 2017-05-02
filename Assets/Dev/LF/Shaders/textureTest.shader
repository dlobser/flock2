// Unlit shader. Simplest possible textured shader.
// - no lighting
// - no lightmap support
// - no per-material color

Shader "Unlit/TextureTest" {
Properties {
	_MainTex ("Main", 2D) = "white" {}
	_AOTex ("AO", 2D) = "white" {}
	_Color ("Color",Color)= (1,1,1,1)
	_Channel ("Channel",Color)= (1,1,1,1)
	_Strobe("Strobe",Vector) = (1,1,1,1)

}

SubShader {
	Tags { "RenderType"="Opaque" }
	LOD 100
	
	Pass {  
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata_t {
				float4 vertex : POSITION;
//				float2 texcoord : TEXCOORD0;
				float2 uv : TEXCOORD0;
				float2 uv2 : TEXCOORD1;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
//				half2 texcoord : TEXCOORD0;
				float2 uv : TEXCOORD0;
				float2 uv2 : TEXCOORD1;
				UNITY_FOG_COORDS(2)
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _AOTex;
			float4 _AOTex_ST;
			float4 _Color;
			float4 _Channel;
			float4 _Strobe;
			
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
//				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.uv2 = TRANSFORM_TEX(v.uv, _AOTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
//				fixed4 col = tex2D(_MainTex, i.texcoord);
				fixed4 col = tex2D(_MainTex, i.uv);
//				fixed4 shad = tex2D(_AOTex, i.texcoord);
				fixed4 shad = tex2D(_AOTex, i.uv2);
				fixed4 col2 = col*_Channel;
				fixed4 colR = fixed4(col2.r,col2.r,col2.r,1.0);
				fixed4 colG = fixed4(col2.g,col2.g,col2.g,1.0);
				fixed4 colB = fixed4(col2.b,col2.b,col2.b,1.0);
				float strober = max(0.0,min(1.0,sin(i.uv.x*_Strobe.x+_Time.y*_Strobe.y)*_Strobe.z+_Strobe.w));
				fixed4 col3 = (colR+colG+colB)*shad*shad*_Color*strober;
//				UNITY_APPLY_FOG(i.fogCoord, col);
				UNITY_APPLY_FOG(i.fogCoord, col3);
				UNITY_OPAQUE_ALPHA(col.a);
				return col3;
			}
		ENDCG
	}
}

}
