Shader "Unlit/TunnelOfLight"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_MultTex ("Multiply Texture", 2D) = "white" {}
		_Color ("Color", color) = (1,1,1,1)
		_Data ("Data", vector) = (0,0,0,0)
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue" = "Transparent"}
		Blend One One
		ZWrite Off
		ZTest Off
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
				float2 uv3 : TEXCOORD2;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float2 uv2 : TEXCOORD1;
				float2 uv3 : TEXCOORD2;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _MultTex;
			float4 _MultTex_ST;

			float4 _Color;
			float4 _Data;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.uv2 = TRANSFORM_TEX(v.uv, _MainTex);
				o.uv3 = TRANSFORM_TEX(v.uv, _MultTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				float2 nUV = float2(sin(i.uv.y*20-_Time.z)*.1,0);
				fixed4 col3 = tex2D(_MultTex, i.uv3 + float2(_Time.z*_Data.z,_Time.z*_Data.w));
				fixed4 col = tex2D(_MainTex, nUV+col3.xy*.3+i.uv2 + float2(0,_Time.z*_Data.y));
				fixed4 col2 = tex2D(_MainTex, i.uv2*.5 + float2(0,_Time.z*_Data.y*2));
//				fixed4 col3 = tex2D(_MultTex, i.uv3 + float2(_Time.z*_Data.z,_Time.z*_Data.w));
				col+=col2;
				col*=col3;
				float s = pow(sin(i.uv.y*3.1415),_Data.x);
				return col*s*_Color*_Color.a*col.a;
			}
			ENDCG
		}
	}
}
