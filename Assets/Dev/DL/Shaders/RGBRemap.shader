Shader "Holojam/RGBRemap"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_ColorR ("Color:R",Color) = (0,0,0,0)
		_ColorG ("Color:G",Color) = (0,0,0,0)
		_ColorB ("Color:B",Color) = (0,0,0,0)
		_ColorMult("Color Multiply",Color) = (1,1,1,1)
		_InHueShift("In Hue Shift",Float) = 0
		_OutHueShift("Out Hue Shift",Float) = 0
		_Saturation("Saturation",Float) =1
		_InSpeed("In Hue Speed",Float) =0
		_OutSpeed("Out Hue Speed",Float) =0
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue" = "Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off
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
			float4 _ColorR;
			float4 _ColorG;
			float4 _ColorB;
			float4 _ColorMult;
			float _InHueShift;
			float _OutHueShift;
			float _Saturation;
			float _InSpeed;
			float _OutSpeed;

			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

			float3 rgbToHsv(float3 _RGB)
            {
                float3 HSV = 0;
 
                float minChannel, maxChannel;
 
                maxChannel = max(_RGB.x, _RGB.y);
                minChannel = min(_RGB.x, _RGB.y);
 
                maxChannel = max(_RGB.z, maxChannel);
                minChannel = min(_RGB.z, minChannel);
 
                HSV.z = maxChannel;
 
                float delta = maxChannel - minChannel;             //Delta RGB value
 
                //if ( delta > 0 )
                //{                    // If gray, leave H  S at zero
                    HSV.y = delta / HSV.z;
                    float3 delRGB = (HSV.zzz - _RGB + 3*delta) / (6*delta);
                    if ( _RGB.x == HSV.z ) HSV.x = delRGB.z - delRGB.y;
                    if ( _RGB.y == HSV.z ) HSV.x = ( 1.0 / 3.0 ) + delRGB.x - delRGB.z;
                    if ( _RGB.z == HSV.z ) HSV.x = ( 2.0 / 3.0 ) + delRGB.y - delRGB.x;
                //}
 
                return (HSV);
            }
 
            float3 hsvToRgb(float3 HSV)
            {
                float var_h = HSV.x * 6;
                //float var_i = floor(var_h);   // Or ... var_i = floor( var_h )
                float var_1 = HSV.z * ( 1.0 - HSV.y );
                float var_2 = HSV.z * ( 1.0 - HSV.y * (var_h-floor( var_h )));
                float var_3 = HSV.z * ( 1.0 - HSV.y * (1-(var_h-floor( var_h ))));
 
                float3 RGB = float3(HSV.z, var_1, var_2);
 
                if (var_h < 5)  { RGB = float3(var_3, var_1, HSV.z); }
                if (var_h < 4)  { RGB = float3(var_1, var_2, HSV.z); }
                if (var_h < 3)  { RGB = float3(var_1, HSV.z, var_3); }
                if (var_h < 2)  { RGB = float3(var_2, HSV.z, var_1); }
                if (var_h < 1)  { RGB = float3(HSV.z, var_3, var_1); }
 
                return (RGB);
            }
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);

				float3 hsv = rgbToHsv(col.xyz);
                hsv.x += _InHueShift + _Time.x * _InSpeed;
 				hsv.x = frac(hsv.x);
                fixed4 inShifted = fixed4( half3(hsvToRgb(hsv) ), col.a);

				fixed4 colR = _ColorR * inShifted.x;
				fixed4 colG = _ColorG * inShifted.y;
				fixed4 colB = _ColorB * inShifted.z;
				fixed4 col2 = colR+colG+colB;

				hsv = rgbToHsv(col2.xyz);
                hsv.x += _OutHueShift + _Time.x * _OutSpeed;
 				hsv.x = frac(hsv.x);
 				hsv.y*=_Saturation;
                fixed4 outShifted = fixed4( half3(hsvToRgb(hsv) ), col.a)*_ColorMult;

				outShifted.a = col.a;

				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, outShifted);
				return outShifted;
			}
			ENDCG
		}
	}
}
