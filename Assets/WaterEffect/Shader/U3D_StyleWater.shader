// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "VFX/U3D_StyleWater"
{
	Properties
	{
		_Fresnel_Color("Fresnel_Color", Color) = (0,0,0,0)
		_Speed_2("Speed_2", Vector) = (0.5,0.5,0,0)
		_Speed_1("Speed_1", Vector) = (1,1,0,0)
		_Noise1_Str("Noise1_Str", Range( 0 , 1)) = 0.2
		_Noise2_Tex("Noise2_Tex", 2D) = "white" {}
		_Noise_Tex("Noise_Tex", 2D) = "white" {}
		_Main_Tex("Main_Tex", 2D) = "white" {}
		_Noise_Color("Noise_Color", Color) = (0,0,0,0)
		_Noise2_Color("Noise2_Color", Color) = (0,0,0,0)
		_Bias("Bias", Range( 0 , 1)) = 0
		_VertexNormal("VertexNormal", 2D) = "white" {}
		_Wave("Wave", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Opaque" }
	LOD 100

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend Off
		Cull Back
		ColorMask RGBA
		ZWrite On
		ZTest LEqual
		Offset 0 , 0
		
		
		
		Pass
		{
			Name "Unlit"
			Tags { "LightMode"="ForwardBase" }
			CGPROGRAM

			

			#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
			//only defining to not throw compilation error over Unity 5.5
			#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
			#endif
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"
			#define ASE_NEEDS_FRAG_WORLD_POSITION


			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				float4 ase_texcoord : TEXCOORD0;
				float3 ase_normal : NORMAL;
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 worldPos : TEXCOORD0;
#endif
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
			};

			uniform sampler2D _VertexNormal;
			uniform sampler2D _Noise2_Tex;
			uniform float4 _Noise2_Tex_ST;
			uniform float2 _Speed_2;
			uniform float _Wave;
			uniform sampler2D _Main_Tex;
			uniform float4 _Main_Tex_ST;
			uniform float4 _Noise2_Color;
			uniform float _Bias;
			uniform float4 _Fresnel_Color;
			uniform sampler2D _Noise_Tex;
			uniform float4 _Noise_Tex_ST;
			uniform float2 _Speed_1;
			uniform float4 _Noise_Color;
			uniform float _Noise1_Str;
					float2 voronoihash92( float2 p )
					{
						
						p = float2( dot( p, float2( 127.1, 311.7 ) ), dot( p, float2( 269.5, 183.3 ) ) );
						return frac( sin( p ) *43758.5453);
					}
			
					float voronoi92( float2 v, float time, inout float2 id, float smoothness )
					{
						float2 n = floor( v );
						float2 f = frac( v );
						float F1 = 8.0;
						float F2 = 8.0; float2 mr = 0; float2 mg = 0;
						for ( int j = -1; j <= 1; j++ )
						{
							for ( int i = -1; i <= 1; i++ )
						 	{
						 		float2 g = float2( i, j );
						 		float2 o = voronoihash92( n + g );
								o = ( sin( time + o * 6.2831 ) * 0.5 + 0.5 ); float2 r = g - f + o;
								float d = 0.5 * dot( r, r );
						 		if( d<F1 ) {
						 			F2 = F1;
						 			F1 = d; mg = g; mr = r; id = o;
						 		} else if( d<F2 ) {
						 			F2 = d;
						 		}
						 	}
						}
						return F1;
					}
			

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				float2 uv0_Noise2_Tex = v.ase_texcoord.xy * _Noise2_Tex_ST.xy + _Noise2_Tex_ST.zw;
				float2 temp_output_19_0 = (uv0_Noise2_Tex*1.0 + ( _Time.y * _Speed_2 ));
				float3 normalizeResult95 = normalize( v.ase_normal );
				
				float3 ase_worldNormal = UnityObjectToWorldNormal(v.ase_normal);
				o.ase_texcoord2.xyz = ase_worldNormal;
				
				o.ase_texcoord1.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord1.zw = 0;
				o.ase_texcoord2.w = 0;
				float3 vertexValue = float3(0, 0, 0);
				#if ASE_ABSOLUTE_VERTEX_POS
				vertexValue = v.vertex.xyz;
				#endif
				vertexValue = ( tex2Dlod( _VertexNormal, float4( temp_output_19_0, 0, 0.0) ) * _Wave * float4( normalizeResult95 , 0.0 ) ).rgb;
				#if ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				o.vertex = UnityObjectToClipPos(v.vertex);

#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
#endif
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				fixed4 finalColor;
#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 WorldPosition = i.worldPos;
#endif
				float time92 = _Time.y;
				float2 temp_output_1_0_g1 = i.ase_texcoord1.xy;
				float2 temp_output_11_0_g1 = ( temp_output_1_0_g1 - float2( 0.5,0.5 ) );
				float2 break18_g1 = temp_output_11_0_g1;
				float2 appendResult19_g1 = (float2(break18_g1.y , -break18_g1.x));
				float dotResult12_g1 = dot( temp_output_11_0_g1 , temp_output_11_0_g1 );
				float2 coords92 = ( temp_output_1_0_g1 + ( appendResult19_g1 * ( dotResult12_g1 * float2( 10,10 ) ) ) + float2( 0,0 ) ) * 2.93;
				float2 id92 = 0;
				float voroi92 = voronoi92( coords92, time92,id92, 0 );
				float2 uv_Main_Tex = i.ase_texcoord1.xy * _Main_Tex_ST.xy + _Main_Tex_ST.zw;
				float4 tex2DNode29 = tex2D( _Main_Tex, uv_Main_Tex );
				float2 uv0_Noise2_Tex = i.ase_texcoord1.xy * _Noise2_Tex_ST.xy + _Noise2_Tex_ST.zw;
				float2 temp_output_19_0 = (uv0_Noise2_Tex*1.0 + ( _Time.y * _Speed_2 ));
				float3 ase_worldViewDir = UnityWorldSpaceViewDir(WorldPosition);
				ase_worldViewDir = normalize(ase_worldViewDir);
				float3 ase_worldNormal = i.ase_texcoord2.xyz;
				float fresnelNdotV31 = dot( ase_worldNormal, ase_worldViewDir );
				float fresnelNode31 = ( _Bias + 1.0 * pow( 1.0 - fresnelNdotV31, 2.0 ) );
				float2 uv0_Noise_Tex = i.ase_texcoord1.xy * _Noise_Tex_ST.xy + _Noise_Tex_ST.zw;
				float4 appendResult42 = (float4(( ( voroi92 * 1.0 ) + tex2DNode29 + ( tex2D( _Noise2_Tex, temp_output_19_0 ) * _Noise2_Color ) + ( fresnelNode31 * _Fresnel_Color ) + ( tex2D( _Noise_Tex, (uv0_Noise_Tex*1.0 + ( _Time.y * _Speed_1 )) ) * _Noise_Color * _Noise1_Str ) ).rgb , tex2DNode29.a));
				
				
				finalColor = appendResult42;
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=18100
128;298;1764;576;1314.81;330.5467;1.776164;True;True
Node;AmplifyShaderEditor.Vector2Node;7;-1189.71,81.1882;Inherit;False;Property;_Speed_1;Speed_1;2;0;Create;True;0;0;False;0;False;1,1;0.3,0.3;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;18;-1191.386,270.6962;Inherit;False;Property;_Speed_2;Speed_2;1;0;Create;True;0;0;False;0;False;0.5,0.5;0.4,0.4;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleTimeNode;4;-1204.424,-188.7155;Inherit;True;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;9;-1252.6,-500.837;Inherit;True;0;13;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-930.2225,-31.00828;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;-927.6747,240.6823;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;36;-1225.627,496.8836;Inherit;True;0;20;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScaleAndOffsetNode;12;-599.6051,-92.29709;Inherit;True;3;0;FLOAT2;0,0;False;1;FLOAT;1;False;2;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;35;-617.942,-588.6614;Inherit;False;Property;_Bias;Bias;9;0;Create;True;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;19;-600.5718,218.0633;Inherit;True;3;0;FLOAT2;0,0;False;1;FLOAT;1;False;2;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleTimeNode;94;167.1771,949.3966;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;93;104.5862,787.6289;Inherit;False;Radial Shear;-1;;1;c6dc9fc7fa9b08c4d95138f2ae88b526;0;4;1;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;45;-294.7477,92.60129;Inherit;False;Property;_Noise1_Str;Noise1_Str;3;0;Create;True;0;0;False;0;False;0.2;0.325;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;33;-218.1911,182.8574;Inherit;False;Property;_Noise_Color;Noise_Color;7;0;Create;True;0;0;False;0;False;0,0,0,0;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;32;-192.5695,581.7128;Inherit;False;Property;_Noise2_Color;Noise2_Color;8;0;Create;True;0;0;False;0;False;0,0,0,0;0.07586192,0,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;43;-190.5659,-851.6666;Inherit;False;Property;_Fresnel_Color;Fresnel_Color;0;0;Create;True;0;0;False;0;False;0,0,0,0;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;20;-272.9585,374.0821;Inherit;True;Property;_Noise2_Tex;Noise2_Tex;4;0;Create;True;0;0;False;0;False;-1;3584f2bf4afb5284d91edb6a29126e62;3584f2bf4afb5284d91edb6a29126e62;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;13;-292.9371,-118.9395;Inherit;True;Property;_Noise_Tex;Noise_Tex;5;0;Create;True;0;0;False;0;False;-1;3584f2bf4afb5284d91edb6a29126e62;3584f2bf4afb5284d91edb6a29126e62;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VoronoiNode;92;404.4477,682.4944;Inherit;True;0;0;1;0;1;False;1;False;False;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;2.93;False;3;FLOAT;0;False;2;FLOAT;0;FLOAT2;1
Node;AmplifyShaderEditor.FresnelNode;31;-212.8376,-645.22;Inherit;False;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0.2;False;2;FLOAT;1;False;3;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;34;116.5492,-26.99246;Inherit;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.NormalVertexDataNode;84;952.4371,216.2659;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;96;539.0684,384.5133;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;172.6181,377.6937;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;44;256.0837,-450.2056;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;29;306.1397,-237.3746;Inherit;True;Property;_Main_Tex;Main_Tex;6;0;Create;True;0;0;False;0;False;-1;76d2c2299677d96479a07a25add044ee;76d2c2299677d96479a07a25add044ee;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;82;889.4437,94.01372;Inherit;False;Property;_Wave;Wave;11;0;Create;True;0;0;False;0;False;0;0.03;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;81;1004.983,-134.7504;Inherit;True;Property;_VertexNormal;VertexNormal;10;0;Create;True;0;0;False;0;False;-1;None;7d68a918c0765ff46bd6aa7513519399;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NormalizeNode;95;1223.591,170.335;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;24;654.6671,-177.6338;Inherit;True;5;5;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;83;1395.187,-47.1566;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT3;0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.DynamicAppendNode;42;1066.112,-420.4202;Inherit;True;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;90;1726.583,-270.6735;Float;False;True;-1;2;ASEMaterialInspector;100;1;VFX/U3D_StyleWater;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;True;0;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;True;False;True;0;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Opaque=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;True;1;LightMode=ForwardBase;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;1;True;False;;0
WireConnection;6;0;4;0
WireConnection;6;1;7;0
WireConnection;17;0;4;0
WireConnection;17;1;18;0
WireConnection;12;0;9;0
WireConnection;12;2;6;0
WireConnection;19;0;36;0
WireConnection;19;2;17;0
WireConnection;20;1;19;0
WireConnection;13;1;12;0
WireConnection;92;0;93;0
WireConnection;92;1;94;0
WireConnection;31;1;35;0
WireConnection;34;0;13;0
WireConnection;34;1;33;0
WireConnection;34;2;45;0
WireConnection;96;0;92;0
WireConnection;22;0;20;0
WireConnection;22;1;32;0
WireConnection;44;0;31;0
WireConnection;44;1;43;0
WireConnection;81;1;19;0
WireConnection;95;0;84;0
WireConnection;24;0;96;0
WireConnection;24;1;29;0
WireConnection;24;2;22;0
WireConnection;24;3;44;0
WireConnection;24;4;34;0
WireConnection;83;0;81;0
WireConnection;83;1;82;0
WireConnection;83;2;95;0
WireConnection;42;0;24;0
WireConnection;42;3;29;4
WireConnection;90;0;42;0
WireConnection;90;1;83;0
ASEEND*/
//CHKSM=01EE5AE1C175337E915E67ECBBBFDAF7BC04A3E5