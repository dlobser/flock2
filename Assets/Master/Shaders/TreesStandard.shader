Shader "Custom/TreesStandard" {
	Properties {
		_MainTex ("Main", 2D) = "white" {}
		_AOTex ("AO", 2D) = "white" {}
		_Color ("Color",Color)= (1,1,1,1)
		_Channel ("Channel",Color)= (1,1,1,1)
		_Strobe("Strobe",Vector) = (1,1,1,1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _AOTex;

		struct Input {
			float2 uv_MainTex;
			float2 uv_AOTex;
		};

		

			float4 _Color;
			float4 _Channel;
			float4 _Strobe;

		void surf (Input IN, inout SurfaceOutputStandard o) {

		//				fixed4 col = tex2D(_MainTex, i.texcoord);
				fixed4 col = tex2D(_MainTex,IN.uv_MainTex);
//				fixed4 shad = tex2D(_AOTex, i.texcoord);
				fixed4 shad = tex2D(_AOTex, IN.uv_AOTex);
				fixed4 col2 = col*_Channel;
				fixed4 colR = fixed4(col2.r,col2.r,col2.r,1.0);
				fixed4 colG = fixed4(col2.g,col2.g,col2.g,1.0);
				fixed4 colB = fixed4(col2.b,col2.b,col2.b,1.0);
				float strober = max(0.0,min(1.0,sin(IN.uv_MainTex.x*_Strobe.x+_Time.y*_Strobe.y)*_Strobe.z+_Strobe.w));
				fixed4 col3 = (colR+colG+colB)*shad*shad*_Color*strober;
//				UNITY_APPLY_FOG(i.fogCoord, col);
				//UNITY_APPLY_FOG(i.fogCoord, col3);
				//UNITY_OPAQUE_ALPHA(col.a);
				//return col3;
				o.Emission = col3;

			// Albedo comes from a texture tinted by color
			/*fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;*/
		}
		ENDCG
	}
	FallBack "Diffuse"
}
