Shader "Holojam/RGBRemapSimpleTransparentMultiTex"
{
	Properties
	{
		_Tex1 ("Texture", 2D) = "white" {}
		_Tex2 ("Texture", 2D) = "white" {}
		_Tex3 ("Texture", 2D) = "white" {}
		_Tex4 ("Texture", 2D) = "white" {}
		_Tex5 ("Texture", 2D) = "white" {}
		_Tex6 ("Texture", 2D) = "white" {}
		_Tex7 ("Texture", 2D) = "white" {}
		_Tex8 ("Texture", 2D) = "white" {}
		_ColorOffset("Color Offset", Vector) = (1,1,1,1)
		_TextureFade ("Texture Fade", Float) = 0
		_ColorMult("Color Multiply",Color) = (1,1,1,1)
		_ColorAdd("Color Add",Color) = (1,1,1,1)
	}
	SubShader
	{
		Tags {"RenderType"="Transparent" "Queue" = "Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
//			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float2 uv2 : TEXCOORD1;
				float2 uv3 : TEXCOORD2;
				float2 uv4 : TEXCOORD3;
				float4 ColorR : COLOR;


			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float2 uv2 : TEXCOORD1;
				float2 uv3 : TEXCOORD2;
				float2 uv4 : TEXCOORD3;
				float4 ColorR : COLOR;

//				UNITY_FOG_COORDS(3)
				float4 vertex : SV_POSITION;
			};

			sampler2D _Tex1;
			sampler2D _Tex2;
			sampler2D _Tex3;
			sampler2D _Tex4;
			sampler2D _Tex5;
			sampler2D _Tex6;
			sampler2D _Tex7;
			sampler2D _Tex8;
			float _TextureFade;
			float4 _Tex1_ST;
			float4 _ColorMult;
			float4 _ColorAdd;
			float4 _ColorOffset;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.uv = TRANSFORM_TEX(v.uv, _Tex1);
				o.uv2 = v.uv2;
				o.uv3 = v.uv3;
				o.uv4 = v.uv4;
				o.ColorR = v.ColorR;
//				o.ColorG = mul(UNITY_MATRIX_IT_MV, v.ColorG);
//				o.ColorB = v.ColorB;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
//				UNITY_TRANSFER_FOG(o,o.vertex);
//				float d = length(mul (UNITY_MATRIX_MV,v.vertex));
				return o;
			}

			float r(float i){
				return (1+i)*.5;
			}


			fixed4 frag (v2f i) : SV_Target
			{
				float3 ColorR = fixed3(i.uv3.x,i.uv3.y,i.uv4.x);
				float3 ColorG = fixed3(i.uv4.y,i.ColorR.r,i.ColorR.g);
				float3 ColorB = fixed3(i.ColorR.b,i.ColorR.a,i.uv2.y);
				float which = i.uv2.x;
//				float speed = i.uv2.x - which;
				// sample the texture
//				fixed4 col = lerp(tex2D(_MainTex, i.uv),tex2D(_SecondTex, i.uv),_TextureFade);

				float4 CR1 = tex2D(_Tex1, i.uv);
				float4 CR2 = tex2D(_Tex2, i.uv);
				float4 CR3 = tex2D(_Tex3, i.uv);
				float4 CR4 = tex2D(_Tex4, i.uv);
				float4 CR5 = tex2D(_Tex5, i.uv);
				float4 CR6 = tex2D(_Tex6, i.uv);
				float4 CR7 = tex2D(_Tex7, i.uv);
				float4 CR8 = tex2D(_Tex8, i.uv);

				float f = which;

				float4 col = 
							   lerp(CR1,
							   lerp(CR2,
							   lerp(CR3,
							   lerp(CR4, 
							   lerp(CR5, 
							   lerp(CR6, 
							   lerp(CR7, 
							   lerp(CR8, 
							   CR1, 
							   		clamp(f-8, 0.0, 1.0)),
							   		clamp(f-7, 0.0, 1.0)),
							   		clamp(f-6, 0.0, 1.0)),
							   		clamp(f-5, 0.0, 1.0)),
							   		clamp(f-4, 0.0, 1.0)),
							   		clamp(f-3, 0.0, 1.0)),
							   		clamp(f-2, 0.0, 1.0)),
							   		clamp(f-1, 0.0, 1.0));

                float4 off = _ColorOffset * _ColorOffset.a * _Time.y;// * speed*10;

                float4 inShifted = fixed4(
	                r(sin(col.r+off.x))*col.r,
	                r(sin(col.g+off.y))*col.g,
	                r(sin(col.b+off.z))*col.b,
	                1.0);

				float3 colR = lerp(ColorR,ColorG , inShifted.x)*col.x;
				float3 colG = lerp(ColorG,ColorB , inShifted.y)*col.y;
				float3 colB = lerp(ColorB,ColorR , inShifted.z)*col.z;
				float3 col2 = (colR+colG+colB) * _ColorMult * _ColorMult.a*4;

				fixed4 col3 = fixed4(col2.x,col2.y,col2.z,col.a)+_ColorAdd;
//				 apply fog
//				UNITY_APPLY_FOG(i.fogCoord, col2);
				return col3;
			}
			ENDCG
		}
	}
}
