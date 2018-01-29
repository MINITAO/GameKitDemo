Shader "Unlit/TextureScroll"
{
	Properties
	{
		_MainTex ("Main Texture", 2D) = "white" {}
		[Enum(UnityEngine.Rendering.BlendMode)] _BlendSrc("Src Factor",float)=5
		[Enum(UnityEngine.Rendering.BlendMode)] _BlendDst("Dst Factor",float)=10
		[Enum(UnityEngine.Rendering.CullMode)] _CullMode("Cull Mode",float)=0
		_ScrollSpeedX ("Scroll Speed X" ,Float) = 0
		_ScrollSpeedY ("Scroll Speed Y" ,Float) = 0
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue"="Transparent"  }

		Cull [_CullMode]
		Lighting Off
		ZWrite Off
		Blend [_BlendSrc] [_BlendDst]

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float4 color    : COLOR;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				fixed4 color    : COLOR;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			float _ScrollSpeedX;
			float _ScrollSpeedY;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				o.color = v.color;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float2 uv = i.uv - float2(_Time.y*_ScrollSpeedX,_Time.y*_ScrollSpeedY);
				// sample the texture
				fixed4 col = tex2D(_MainTex, uv)*i.color;

				return col;
			}
			ENDCG
		}
	}
}
