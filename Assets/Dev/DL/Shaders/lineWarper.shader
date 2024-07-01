// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/lineWarper"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Pos ("position",Vector) = (0,0,0,0)
		_Data ("X:Dist, Y:Mult, Z:Freq, W:Speed", Vector ) = (0,0,0,0)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		Cull Off
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"
			#include "noiseSimplex.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
//				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				float4 pos : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Pos;
			float4 _Data;

			float4 LerpU( float4 a, float4 b, float t ){
			     return t*b + ((1-t)*a);
			}

			v2f vert (appdata v)
			{
				v2f o;
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				float sm = _Data.z;

				float4 noise = float4(snoise(v.vertex*sm+float3(_Data.w*_Time.x,v.uv.x,1)),snoise(sm*v.vertex+float3(1,_Data.w*_Time.x,v.uv.x)),snoise(sm*v.vertex+float3(v.uv.x,1,_Data.w*_Time.x)),0)*v.uv.x*2;

				float dist = max(0,((distance(_Pos,v.vertex)-1.5)*-1));
				float mult = _Data.x*dist;
				float4 sub = ((_Pos*mult) - v.vertex ) * v.uv.x ;

		
				float4 offset = ( (v.vertex * float4(v.uv.x*mult,v.uv.x*mult,v.uv.x*mult,0)  - sub ) );// * v.uv.x  ;

				o.vertex = UnityObjectToClipPos((v.vertex + offset) + noise*_Data.y );

				o.pos = float4(v.uv.x,v.uv.x,v.uv.x,0);

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				// apply fog
//				UNITY_APPLY_FOG(i.fogCoord, col);
				return col*col.a;//fixed4(i.uv.x,i.uv.y,0.0,1.0);
			}
			ENDCG
		}
	}
}
