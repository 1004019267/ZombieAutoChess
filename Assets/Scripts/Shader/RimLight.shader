﻿Shader "Unlit/RimLight"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}	
		_Color("Main Color",Color)=(1,1,1,1)	
		_RimColor("Rim Color",Color)=(1,1,1,1)
		_RimWidth("Rim Width",float)=0.9
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
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
				float3 normal :NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				fixed3 color : COLOR;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			uniform fixed4 _RimColor;
			float _RimWidth;

			uniform fixed4 _Color;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				
				float3 viewDir=normalize(ObjSpaceViewDir(v.vertex));
				float dotProduct =1-dot(v.normal,viewDir);

				o.color= smoothstep(1-_RimWidth,1.0,dotProduct);
				o.color *=_RimColor;

				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : COLOR
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				col*=_Color;
				col.rgb+=i.color;
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
