Shader "Unlit/outlineInteractable"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_mixAmount("Blending between texture and color",Range(0,1)) = 0
		_Range("Range", float) = 1
		_OutlineColor("Outline Color" , Color) = (1,1,1,1)
	}
		SubShader
		{
			LOD 200
			//Tags { "RenderType" = "Opaque" }



			Pass//outline pass seen through objects
		{
			Tags{ "Queue" = "Transparent" "RenderType" = "Opaque" }
			//ZTest Always
			Cull front
			ZWrite Off
			Lighting Off
			//Blend One Zero
			//Blend SrcAlpha OneMinusSrcAlpha // Traditional transparency
			//Blend One OneMinusSrcAlpha // Premultiplied transparency
			//Blend One One // Additive
			//Blend OneMinusDstColor One // Soft Additive
			//Blend DstColor Zero // Multiplicative
			//Blend DstColor SrcColor // 2x Multiplicative
			CGPROGRAM
	#pragma vertex vert
	#pragma fragment frag
	#pragma target 3.0 

	#include "UnityCG.cginc"


			struct appdata
		{
			float4 vertex : POSITION;
			float3 normal : NORMAL;
			float2 uv : TEXCOORD0;
		};

		struct v2f
		{
			float4 vertex : SV_POSITION;
			float2 uv : TEXCOORD0;
		};

		//sampler2D _MainTex;
		float4 _MainTex_ST;
		float _Range;
		float4 _OutlineColor;
		float _mixAmount;
		sampler2D _MainTex;

		v2f vert(appdata v)
		{
			v2f o;
			o.uv = TRANSFORM_TEX(v.uv, _MainTex);//send uv
			float3 val = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
			float2 off = TransformViewToProjection(val.xy);//ignore all but xy
			o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
			o.vertex.xy += _Range * clamp(sin(_Time.w) + 1,1,2)*  off;// Removed o.vertex.z * for ortho


			return o;
		}

		fixed4 frag(v2f i) : SV_Target
		{
			fixed4 col = _OutlineColor;
			return col;
		}
			ENDCG
		}
			Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
			LOD 100

			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha

			Pass{
			CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma target 2.0
#pragma multi_compile_fog

#include "UnityCG.cginc"

			struct appdata_t {
			float4 vertex : POSITION;
			float2 texcoord : TEXCOORD0;
		};

		struct v2f {
			float4 vertex : SV_POSITION;
			half2 texcoord : TEXCOORD0;
			UNITY_FOG_COORDS(1)
		};

		sampler2D _MainTex;
		float4 _MainTex_ST;
		half4 _Color;

		v2f vert(appdata_t v)
		{
			v2f o;
			o.vertex = UnityObjectToClipPos(v.vertex);
			o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
			UNITY_TRANSFER_FOG(o,o.vertex);
			return o;
		}

		fixed4 frag(v2f i) : SV_Target
		{
			fixed4 col = tex2D(_MainTex, i.texcoord) * _Color;
		UNITY_APPLY_FOG(i.fogCoord, col);
		return col;
		}
			ENDCG
		}

		}
}
