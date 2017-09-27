Shader "Rubin/rays"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_SecondTex ("Texture", 2D) = "white" {}

		_Color ("Color",color) = (1,1,1,1)
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue" = "Transparent"}
		ZWrite off
		Blend One One
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _SecondTex;
			float4 _SecondTex_ST;
			float4 _Color;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;//TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, TRANSFORM_TEX(i.uv, _MainTex));
				fixed4 col2 = tex2D(_SecondTex, TRANSFORM_TEX(i.uv, _SecondTex) + float2(col.r,col.g)*.01);
				col*=((pow(1-i.uv.y,3)))* _Color;
				//col.a = 1-(pow(i.uv.y,1))*col.r*_Color.a;
				// apply fog
//				UNITY_APPLY_FOG(i.fogCoord, col);
				return col*col2*_Color.a*15;
			}
			ENDCG
		}
	}
}
