Shader "Holojam/RGBRemapSimple"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Occlusion ("Occlusion", 2D) = "white" {}
		_ColorR ("Color:R",Color) = (0,0,0,0)
		_ColorG ("Color:G",Color) = (0,0,0,0)
		_ColorB ("Color:B",Color) = (0,0,0,0)
		_ColorR2 ("Color:R",Color) = (0,0,0,0)
		_ColorG2 ("Color:G",Color) = (0,0,0,0)
		_ColorB2 ("Color:B",Color) = (0,0,0,0)
		_ColorMult("Color Multiply",Color) = (1,1,1,1)
		_ColorAdd("Color Add",Color) = (1,1,1,1)
		_InHueShift("In Hue Shift",Vector) = (0,0,0,0)
		_InSpeed("In Hue Speed",Vector) = (0,0,0,0)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }//"RenderType"="Transparent" "Queue" = "Transparent" }
//		Cull 
//		Blend SrcAlpha OneMinusSrcAlpha
//		ZWrite Off
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
				float4 color :COLOR;
				float2 uv : TEXCOORD0;
				float2 uv2 : TEXCOORD1;

			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float2 uv2 : TEXCOORD1;
				float4 color: COLOR;
				UNITY_FOG_COORDS(2)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _Occlusion;
			float4 _Occlusion_ST;
			float4 _ColorR;
			float4 _ColorG;
			float4 _ColorB;
			float4 _ColorR2;
			float4 _ColorG2;
			float4 _ColorB2;
			float4 _ColorMult;
			float4 _ColorAdd;
			float4 _InHueShift;
			float4 _InSpeed;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.uv2 = TRANSFORM_TEX(v.uv, _Occlusion);
				UNITY_TRANSFER_FOG(o,o.vertex);
				o.color = v.color;
//				float d = length(mul (UNITY_MATRIX_MV,v.vertex));
				return o;
			}

			float r(float i){
				return (1+i)*.5;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 occ = tex2D(_Occlusion, i.uv2 );
				fixed4 col = tex2D(_MainTex, i.uv+ occ.xy*.2);

                float4 off = _InHueShift + _Time.y * _InSpeed * _InSpeed.a;

                fixed4 inShifted = fixed4(
	                r(sin(col.x+off.x))*col.r,
	                r(sin(col.y+off.y))*col.g,
	                r(sin(col.z+off.z))*col.b,
	                1.0);

				fixed4 colR = lerp(_ColorR,_ColorR2 , inShifted.x)*col.x;
				fixed4 colG = lerp(_ColorG,_ColorG2 , inShifted.y)*col.y;
				fixed4 colB = lerp(_ColorB,_ColorB2 , inShifted.z)*col.z;
				fixed4 col2 = (_ColorAdd+colR+colG+colB) * _ColorMult * i.color * occ;

//				col2 = fixed4(.5,.5,.5,1.0);
				col2.a = col.a;
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col2);
				return col2;
			}
			ENDCG
		}
	}
}
