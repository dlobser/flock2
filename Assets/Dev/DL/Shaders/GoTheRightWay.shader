Shader "Unlit/GoTheRightWay"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_ColorTex ("Color Texture", 2D) = "white" {}
		_Color("Color",Color) = (1,1,1,1)
		_Data("data",Float) = (0,0,0,0)
	}
	SubShader
	{
		Tags { "RenderType"="Transparent""Queue"="Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
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
			sampler2D _ColorTex;
			float4 _ColorTex_ST;
			float4 _Data;
			float4 _Color;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv + float2(0,_Data.x*_Time.x));
				fixed4 col2 = tex2D(_ColorTex, i.uv + float2(0,_Data.x*_Time.x));
				col2*=_Color;
				float ramp = max(0.0,min(1.0,((i.uv.y-_Data.y)*_Data.z)));
				col2.a = col.r * _Data.w;
				col2.a *= ramp*_Color.a;
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col2;
			}
			ENDCG
		}
	}
}
