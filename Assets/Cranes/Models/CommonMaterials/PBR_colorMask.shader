// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "WarCool/PBR_colorMask"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_Albedo("Albedo", 2D) = "white" {}
		_Dirt("Dirt", Range( 0 , 1)) = 0
		_Mask("Mask", 2D) = "white" {}
		_Color_1("Color_1", Color) = (0.282353,0.4784314,0.7137255,1)
		_Color_2("Color_2", Color) = (0.8039216,0.6156863,0.1215686,1)
		_Color_3("Color_3", Color) = (0.8274511,0.8156863,0.7764707,1)
		_Smouthness("Smouthness", Range( -1 , 1)) = 0
		_Metallic("Metallic", 2D) = "white" {}
		_Normal("Normal", 2D) = "bump" {}
		_Occlusion("Occlusion", 2D) = "white" {}
		_Occlusion_intensity("Occlusion_intensity", Range( 0 , 2)) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Normal;
		uniform float4 _Normal_ST;
		uniform float4 _Color_2;
		uniform sampler2D _Mask;
		uniform float4 _Mask_ST;
		uniform float4 _Color_1;
		uniform float4 _Color_3;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform float _Dirt;
		uniform sampler2D _Metallic;
		uniform float4 _Metallic_ST;
		uniform float _Smouthness;
		uniform sampler2D _Occlusion;
		uniform float4 _Occlusion_ST;
		uniform float _Occlusion_intensity;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Normal = i.uv_texcoord * _Normal_ST.xy + _Normal_ST.zw;
			o.Normal = UnpackNormal( tex2D( _Normal, uv_Normal ) );
			float4 _Color_White = float4(1,1,1,1);
			float2 uv_Mask = i.uv_texcoord * _Mask_ST.xy + _Mask_ST.zw;
			float4 tex2DNode4 = tex2D( _Mask, uv_Mask );
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			float4 tex2DNode13 = tex2D( _Albedo, uv_Albedo );
			float4 temp_output_33_0 = ( tex2DNode13 * tex2DNode13 );
			float4 temp_output_58_0 = ( temp_output_33_0 * temp_output_33_0 );
			o.Albedo = ( ( ( lerp( _Color_White , _Color_2 , tex2DNode4.b ) * lerp( _Color_White , _Color_1 , tex2DNode4.r ) ) * lerp( _Color_White , _Color_3 , tex2DNode4.g ) ) * ( lerp( float4( 0,0,0,0 ) , lerp( tex2DNode13 , float4(0.1544118,0.1544118,0.1544118,1) , tex2DNode13.r ) , _Dirt ) + lerp( tex2DNode13 , ( temp_output_58_0 * temp_output_58_0 ) , _Dirt ) ) ).rgb;
			float2 uv_Metallic = i.uv_texcoord * _Metallic_ST.xy + _Metallic_ST.zw;
			float4 tex2DNode22 = tex2D( _Metallic, uv_Metallic );
			o.Metallic = tex2DNode22.r;
			float4 temp_cast_3 = (tex2DNode22.a).xxxx;
			float4 temp_output_23_0 = lerp( temp_cast_3 , _Color_White , _Smouthness );
			float4 temp_cast_4 = (tex2DNode22.a).xxxx;
			float4 temp_cast_5 = (tex2DNode22.a).xxxx;
			float4 temp_output_41_0 = ( temp_output_23_0 * temp_output_23_0 );
			float4 temp_cast_6 = (tex2DNode22.a).xxxx;
			float4 temp_cast_7 = (tex2DNode22.a).xxxx;
			o.Smoothness = lerp( temp_output_23_0 , ( temp_output_41_0 * temp_output_41_0 ) , _Dirt ).r;
			float2 uv_Occlusion = i.uv_texcoord * _Occlusion_ST.xy + _Occlusion_ST.zw;
			o.Occlusion = lerp( _Color_White , tex2D( _Occlusion, uv_Occlusion ) , _Occlusion_intensity ).r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=10001
7;29;1844;1164;3095.446;1969.529;2.545455;True;True
Node;AmplifyShaderEditor.SamplerNode;13;-1927.309,-1460.964;Float;True;Property;_Albedo;Albedo;0;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;0.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;33;-1387.188,-1245.36;Float;True;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.SamplerNode;4;-1915.877,-1048.335;Float;True;Property;_Mask;Mask;2;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;0.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;40;-1914.073,-1266.109;Float;True;Property;_Dirt;Dirt;1;0;0;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;25;-1577.624,327.6664;Float;True;Property;_Smouthness;Smouthness;6;0;0;-1;1;0;1;FLOAT
Node;AmplifyShaderEditor.ColorNode;2;-1636.268,-646.2347;Float;False;Property;_Color_2;Color_2;4;0;0.8039216,0.6156863,0.1215686,1;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ColorNode;79;-1628.955,-1667.214;Float;False;Constant;_Color_black;Color_black;0;0;0.1544118,0.1544118,0.1544118,1;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ColorNode;6;-1595.403,-295.2619;Float;False;Constant;_Color_White;Color_White;0;0;1,1,1,1;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;22;-1892.892,48.36327;Float;True;Property;_Metallic;Metallic;7;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;0.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ColorNode;1;-1637.533,-817.5063;Float;False;Property;_Color_1;Color_1;3;0;0.282353,0.4784314,0.7137255,1;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.WireNode;45;-1424.619,-850.1391;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;58;-1140.786,-1136.529;Float;True;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.WireNode;44;-1367.018,-890.1751;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.ColorNode;3;-1632.423,-470.8624;Float;False;Property;_Color_3;Color_3;5;0;0.8274511,0.8156863,0.7764707,1;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.LerpOp;75;-1163.719,-1542.11;Float;True;3;0;COLOR;0.0;False;1;COLOR;0;False;2;COLOR;0.0;False;1;COLOR
Node;AmplifyShaderEditor.WireNode;84;-1032.089,-1281.144;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.WireNode;83;-1036.581,-1335.908;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.WireNode;46;-1393.291,-866.3207;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.LerpOp;7;-1235.74,-837.7389;Float;True;3;0;COLOR;0.0,0,0,0;False;1;COLOR;0.0;False;2;FLOAT;0.0;False;1;COLOR
Node;AmplifyShaderEditor.LerpOp;5;-1237.422,-627.6618;Float;True;3;0;COLOR;0.0,0,0,0;False;1;COLOR;0.0;False;2;FLOAT;0.0;False;1;COLOR
Node;AmplifyShaderEditor.LerpOp;23;-1238.932,-20.27269;Float;True;3;0;FLOAT;0,0,0,0;False;1;COLOR;0.0;False;2;FLOAT;0.0;False;1;COLOR
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;80;-875.909,-1042.658;Float;True;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.SamplerNode;27;-1896.338,239.545;Float;True;Property;_Occlusion;Occlusion;9;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;0.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.WireNode;86;-1386.199,-1014.828;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.LerpOp;78;-601.6829,-1317.108;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0.0,0,0,0;False;2;FLOAT;0.0;False;1;COLOR
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-957.5546,-736.505;Float;True;2;0;COLOR;0.0;False;1;COLOR;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.LerpOp;8;-1237.522,-412.5618;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0.0;False;1;COLOR
Node;AmplifyShaderEditor.WireNode;88;-1277.361,165.7641;Float;False;1;0;COLOR;0.0;False;1;COLOR
Node;AmplifyShaderEditor.SamplerNode;21;-1892.846,-142.4006;Float;True;Property;_Normal;Normal;8;0;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.LerpOp;39;-603.954,-1089.765;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0.0,0,0,0;False;2;FLOAT;0.0;False;1;COLOR
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;41;-910.4462,-143.4649;Float;True;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.RangedFloatNode;26;-1248.365,325.4856;Float;True;Property;_Occlusion_intensity;Occlusion_intensity;10;0;1;0;2;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;74;-274.1929,-947.8246;Float;True;2;0;COLOR;0.0,0,0,0;False;1;COLOR;0.0;False;1;COLOR
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-658.4101,-596.0038;Float;True;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.WireNode;51;-438.7757,-626.4514;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.LerpOp;24;-586.1942,207.6714;Float;True;3;0;COLOR;0.0,0,0,0;False;1;COLOR;0.0;False;2;FLOAT;0.0;False;1;COLOR
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;42;-631.1129,-198.2379;Float;True;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.WireNode;90;-1225.873,-136.3829;Float;False;1;0;FLOAT3;0.0;False;1;FLOAT3
Node;AmplifyShaderEditor.WireNode;81;-684.468,175.2751;Float;False;1;0;COLOR;0.0;False;1;COLOR
Node;AmplifyShaderEditor.WireNode;89;-503.5179,-356.8739;Float;False;1;0;FLOAT3;0.0;False;1;FLOAT3
Node;AmplifyShaderEditor.WireNode;87;-775.9636,-243.946;Float;False;1;0;COLOR;0.0;False;1;COLOR
Node;AmplifyShaderEditor.WireNode;53;-36.83694,-32.9134;Float;False;1;0;COLOR;0.0;False;1;COLOR
Node;AmplifyShaderEditor.LerpOp;43;-255.3224,-236.0839;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0.0,0,0,0;False;2;FLOAT;0.0;False;1;COLOR
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-18.48627,-603.1618;Float;True;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;266.9091,-426.082;Float;False;True;2;Float;ASEMaterialInspector;0;Standard;WarCool/PBR_colorMask;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;0;DstColor;Zero;0;OneMinusDstColor;One;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;Relative;0;;-1;-1;-1;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;13;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;33;0;13;0
WireConnection;33;1;13;0
WireConnection;45;0;4;3
WireConnection;58;0;33;0
WireConnection;58;1;33;0
WireConnection;44;0;4;1
WireConnection;75;0;13;0
WireConnection;75;1;79;0
WireConnection;75;2;13;0
WireConnection;84;0;40;0
WireConnection;83;0;40;0
WireConnection;46;0;4;2
WireConnection;7;0;6;0
WireConnection;7;1;2;0
WireConnection;7;2;45;0
WireConnection;5;0;6;0
WireConnection;5;1;1;0
WireConnection;5;2;44;0
WireConnection;23;0;22;4
WireConnection;23;1;6;0
WireConnection;23;2;25;0
WireConnection;80;0;58;0
WireConnection;80;1;58;0
WireConnection;86;0;40;0
WireConnection;78;1;75;0
WireConnection;78;2;83;0
WireConnection;9;0;7;0
WireConnection;9;1;5;0
WireConnection;8;0;6;0
WireConnection;8;1;3;0
WireConnection;8;2;46;0
WireConnection;88;0;6;0
WireConnection;39;0;13;0
WireConnection;39;1;80;0
WireConnection;39;2;84;0
WireConnection;41;0;23;0
WireConnection;41;1;23;0
WireConnection;74;0;78;0
WireConnection;74;1;39;0
WireConnection;10;0;9;0
WireConnection;10;1;8;0
WireConnection;51;0;86;0
WireConnection;24;0;88;0
WireConnection;24;1;27;0
WireConnection;24;2;26;0
WireConnection;42;0;41;0
WireConnection;42;1;41;0
WireConnection;90;0;21;0
WireConnection;81;0;23;0
WireConnection;89;0;90;0
WireConnection;87;0;22;0
WireConnection;53;0;24;0
WireConnection;43;0;81;0
WireConnection;43;1;42;0
WireConnection;43;2;51;0
WireConnection;11;0;10;0
WireConnection;11;1;74;0
WireConnection;0;0;11;0
WireConnection;0;1;89;0
WireConnection;0;3;87;0
WireConnection;0;4;43;0
WireConnection;0;5;53;0
ASEEND*/
//CHKSM=2894DBDA949BCFABECF0DE339473D9F961DBA87D