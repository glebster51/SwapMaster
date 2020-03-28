// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "g51/Bird"
{
	Properties
	{
		_MaskX("MaskX", Float) = 0
		_SizeMask("SizeMask", Float) = 0
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_X_Force("X_Force", Float) = 0.5
		_Z_Force("Z_Force", Float) = 0.5
		_Speed("Speed", Float) = 5
		_Y_Force("Y_Force", Float) = 0.5
		_RotatewElipse("RotatewElipse", Float) = -0.5
		_TimeOffsetByMask("TimeOffsetByMask", Float) = -0.5
		[Toggle]_MaskTest("MaskTest", Float) = 0
		_FallOffset("FallOffset", Vector) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
		};

		uniform float _Speed;
		uniform float _MaskX;
		uniform float _SizeMask;
		uniform float _TimeOffsetByMask;
		uniform float _X_Force;
		uniform float _Y_Force;
		uniform float _RotatewElipse;
		uniform float _Z_Force;
		uniform float3 _FallOffset;
		uniform float _MaskTest;
		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float mulTime16 = _Time.y * _Speed;
			float3 ase_vertex3Pos = v.vertex.xyz;
			float temp_output_10_0 = max( ( abs( ase_vertex3Pos.x ) + _MaskX ) , 0.0 );
			float WingsMask18 = ( ( temp_output_10_0 * temp_output_10_0 ) * _SizeMask );
			float3 objToWorld160 = mul( unity_ObjectToWorld, float4( float3( 0,0,0 ), 1 ) ).xyz;
			float3 objToWorld169 = mul( unity_ObjectToWorld, float4( ase_vertex3Pos, 1 ) ).xyz;
			float Sin81 = sin( ( ( ( mulTime16 - ( WingsMask18 * _TimeOffsetByMask ) ) + ( ( objToWorld160.y + objToWorld169.y ) * 0.1 ) ) * UNITY_PI ) );
			float3 normalizeResult45 = normalize( float3(0,1,0) );
			float3 objToWorld36 = mul( unity_ObjectToWorld, float4( float3( 0,0,1 ), 1 ) ).xyz;
			float3 objToWorld46 = mul( unity_ObjectToWorld, float4( float3( 0,0,0 ), 1 ) ).xyz;
			float3 normalizeResult44 = normalize( ( objToWorld36 - objToWorld46 ) );
			float dotResult38 = dot( normalizeResult45 , normalizeResult44 );
			float clampResult70 = clamp( dotResult38 , 0.0 , 1.0 );
			float temp_output_124_0 = ( 1.0 - clampResult70 );
			float angleX97 = ( 1.0 - ( temp_output_124_0 * temp_output_124_0 ) );
			float SinMinusOne119 = sin( ( ( ( ( mulTime16 - ( WingsMask18 * _TimeOffsetByMask ) ) + ( ( objToWorld160.y + objToWorld169.y ) * 0.1 ) ) * UNITY_PI ) + ( _RotatewElipse * UNITY_PI ) ) );
			float3 appendResult94 = (float3(( ( Sin81 * Sin81 ) * ( angleX97 * -0.5 ) * _X_Force * sign( ase_vertex3Pos.x ) ) , ( Sin81 * _Y_Force * angleX97 ) , ( SinMinusOne119 * _Z_Force * angleX97 )));
			float3 MoveUp139 = appendResult94;
			float3 appendResult152 = (float3(( sign( ase_vertex3Pos.x ) * _FallOffset.x ) , _FallOffset.y , _FallOffset.z));
			float3 FallPose165 = appendResult152;
			float RawAngleX146 = dotResult38;
			float clampResult150 = clamp( ( RawAngleX146 * -1.0 ) , 0.0 , 1.0 );
			float3 lerpResult141 = lerp( MoveUp139 , FallPose165 , clampResult150);
			v.vertex.xyz += ( lerpResult141 * WingsMask18 );
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float temp_output_10_0 = max( ( abs( ase_vertex3Pos.x ) + _MaskX ) , 0.0 );
			float WingsMask18 = ( ( temp_output_10_0 * temp_output_10_0 ) * _SizeMask );
			float4 temp_cast_0 = (WingsMask18).xxxx;
			o.Albedo = lerp(tex2D( _TextureSample0, uv_TextureSample0 ),temp_cast_0,_MaskTest).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17000
0;6;1920;1013;3706.528;-299.8464;1;True;False
Node;AmplifyShaderEditor.CommentaryNode;143;-2917.473,-636.02;Float;False;1460.413;430.901;;9;10;134;3;8;59;136;135;18;2;Маска Сдвига;1,1,1,1;0;0
Node;AmplifyShaderEditor.PosVertexDataNode;2;-2893.474,-545.0198;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.AbsOpNode;3;-2689.472,-594.0198;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-2875.747,-396.1187;Float;False;Property;_MaskX;MaskX;0;0;Create;True;0;0;False;0;0;0.25;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;134;-2481.965,-429.695;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMaxOpNode;10;-2244.456,-432.037;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;136;-2042.963,-347.695;Float;False;Property;_SizeMask;SizeMask;1;0;Create;True;0;0;False;0;0;0.3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;145;-2918.591,624.2661;Float;False;1722.589;513.8001;;13;36;46;97;126;125;124;146;70;38;37;45;44;47;Угол крена;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;59;-2086.129,-559.1218;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;135;-1865.964,-502.6952;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TransformPositionNode;36;-2845.03,804.1047;Float;False;Object;World;False;Fast;True;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.TransformPositionNode;46;-2851.106,951.1592;Float;False;Object;World;False;Fast;True;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.PosVertexDataNode;168;-2849.842,457.2901;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;47;-2620.616,879.0663;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;18;-1657.059,-506.1036;Float;True;WingsMask;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;144;-2908.26,-104.2701;Float;False;1716.456;584.3052;;18;163;160;162;119;81;122;120;17;121;60;127;131;16;130;129;137;82;170;Синусойды;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector3Node;37;-2635.731,723.4436;Float;False;Constant;_Vector1;Vector 1;2;0;Create;True;0;0;False;0;0,1,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.TransformPositionNode;160;-2847.272,303.3665;Float;False;Object;World;False;Fast;True;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;82;-2821.813,-6.941635;Float;False;Property;_Speed;Speed;5;0;Create;True;0;0;False;0;5;3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;129;-2794.223,121.9836;Float;False;18;WingsMask;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NormalizeNode;44;-2431.404,861.6173;Float;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;137;-2858.262,225.5823;Float;False;Property;_TimeOffsetByMask;TimeOffsetByMask;8;0;Create;True;0;0;False;0;-0.5;0.05;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NormalizeNode;45;-2426.125,776.2167;Float;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TransformPositionNode;169;-2682.842,448.2901;Float;False;Object;World;False;Fast;True;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleTimeNode;16;-2607.96,-54.27011;Float;False;1;0;FLOAT;10;False;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;38;-2267.473,802.9372;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;170;-2478.842,358.2901;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;130;-2566.225,76.98363;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0.05;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;131;-2407.226,-14.01653;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;163;-2394.276,199.3667;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;70;-2094.082,802.5451;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;127;-2137.871,177.7323;Float;False;Property;_RotatewElipse;RotatewElipse;7;0;Create;True;0;0;False;0;-0.5;-0.35;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;124;-1933.532,804.7666;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;162;-2261.276,-20.63333;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PiNode;121;-1902.42,150.7365;Float;False;1;0;FLOAT;-0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.PiNode;60;-2145.962,-26.62422;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;125;-1770.132,791.3952;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;126;-1632.544,793.5809;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;120;-1681.421,80.73643;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;17;-1711.601,-31.12051;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;81;-1561.674,-43.2301;Float;False;Sin;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;142;-1156.832,11.75443;Float;False;1150.018;1136.679;;18;95;102;107;112;99;111;108;101;114;116;113;84;98;83;115;5;94;139;Формирование вектора сдвига при восходе;1,1,1,1;0;0
Node;AmplifyShaderEditor.SinOpNode;122;-1533.707,84.33583;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;97;-1479.603,788.4061;Float;False;angleX;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;164;-1122.692,-502.7257;Float;False;941.4774;387.5192;;6;165;152;154;159;155;153;Формирование вектора сдвига при падении;1,1,1,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode;95;-1074.285,68.93762;Float;False;81;Sin;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;119;-1414.421,49.73642;Float;False;SinMinusOne;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;102;-1075.893,146.8826;Float;False;97;angleX;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;153;-1072.693,-452.7255;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PosVertexDataNode;107;-1106.832,255.5608;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;99;-864.7895,254.9291;Float;False;Property;_X_Force;X_Force;3;0;Create;True;0;0;False;0;0.5;0.8;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;84;-871.2554,582.2742;Float;False;Property;_Y_Force;Y_Force;6;0;Create;True;0;0;False;0;0.5;0.56;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SignOpNode;108;-836.8325,335.5608;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;111;-845.1265,160.3584;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;-0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;114;-868.4865,934.0547;Float;False;Property;_Z_Force;Z_Force;4;0;Create;True;0;0;False;0;0.5;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;98;-895.5326,660.8575;Float;False;97;angleX;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;112;-840.9555,61.75441;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;159;-1084.692,-308.725;Float;False;Property;_FallOffset;FallOffset;10;0;Create;True;0;0;False;0;0,0,0;-0.35,0.07,-0.7;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.GetLocalVarNode;116;-890.6166,1032.433;Float;False;97;angleX;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SignOpNode;154;-863.6925,-405.7254;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;83;-888.0005,505.8847;Float;False;81;Sin;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;113;-902.1565,836.2911;Float;False;119;SinMinusOne;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;146;-2097.314,938.3147;Float;False;RawAngleX;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-670.7995,568.1663;Float;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;101;-628.5565,212.0457;Float;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;155;-704.6924,-350.7251;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;115;-672.0185,925.0604;Float;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;167;128.412,11.46244;Float;False;985.7842;367.8679;;8;150;149;148;166;140;141;19;64;Смешивание и умножение на маску;1,1,1,1;0;0
Node;AmplifyShaderEditor.DynamicAppendNode;94;-427.3153,539.9728;Float;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DynamicAppendNode;152;-535.6924,-291.7249;Float;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;148;178.412,218.3305;Float;False;146;RawAngleX;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;139;-230.8144,543.0107;Float;False;MoveUp;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;165;-383.7885,-294.6116;Float;False;FallPose;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;149;359.4117,222.3306;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;-1;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;140;479.6711,65.46254;Float;False;139;MoveUp;1;0;OBJECT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;166;478.5922,141.9757;Float;False;165;FallPose;1;0;OBJECT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ClampOpNode;150;500.4113,220.3306;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;19;719.3822,244.4976;Float;False;18;WingsMask;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;141;742.5445,106.5021;Float;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;20;561.348,-180.55;Float;False;18;WingsMask;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;53;443.889,-382.7029;Float;True;Property;_TextureSample0;Texture Sample 0;2;0;Create;True;0;0;False;0;None;c5ec7c18725735e48bcd59f42d0deb0c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ToggleSwitchNode;133;807.4609,-298.7331;Float;False;Property;_MaskTest;MaskTest;9;0;Create;True;0;0;False;0;0;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;64;952.1957,152.1483;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1333.764,-279.2641;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;g51/Bird;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;3;0;2;1
WireConnection;134;0;3;0
WireConnection;134;1;8;0
WireConnection;10;0;134;0
WireConnection;59;0;10;0
WireConnection;59;1;10;0
WireConnection;135;0;59;0
WireConnection;135;1;136;0
WireConnection;47;0;36;0
WireConnection;47;1;46;0
WireConnection;18;0;135;0
WireConnection;44;0;47;0
WireConnection;45;0;37;0
WireConnection;169;0;168;0
WireConnection;16;0;82;0
WireConnection;38;0;45;0
WireConnection;38;1;44;0
WireConnection;170;0;160;2
WireConnection;170;1;169;2
WireConnection;130;0;129;0
WireConnection;130;1;137;0
WireConnection;131;0;16;0
WireConnection;131;1;130;0
WireConnection;163;0;170;0
WireConnection;70;0;38;0
WireConnection;124;0;70;0
WireConnection;162;0;131;0
WireConnection;162;1;163;0
WireConnection;121;0;127;0
WireConnection;60;0;162;0
WireConnection;125;0;124;0
WireConnection;125;1;124;0
WireConnection;126;0;125;0
WireConnection;120;0;60;0
WireConnection;120;1;121;0
WireConnection;17;0;60;0
WireConnection;81;0;17;0
WireConnection;122;0;120;0
WireConnection;97;0;126;0
WireConnection;119;0;122;0
WireConnection;108;0;107;1
WireConnection;111;0;102;0
WireConnection;112;0;95;0
WireConnection;112;1;95;0
WireConnection;154;0;153;1
WireConnection;146;0;38;0
WireConnection;5;0;83;0
WireConnection;5;1;84;0
WireConnection;5;2;98;0
WireConnection;101;0;112;0
WireConnection;101;1;111;0
WireConnection;101;2;99;0
WireConnection;101;3;108;0
WireConnection;155;0;154;0
WireConnection;155;1;159;1
WireConnection;115;0;113;0
WireConnection;115;1;114;0
WireConnection;115;2;116;0
WireConnection;94;0;101;0
WireConnection;94;1;5;0
WireConnection;94;2;115;0
WireConnection;152;0;155;0
WireConnection;152;1;159;2
WireConnection;152;2;159;3
WireConnection;139;0;94;0
WireConnection;165;0;152;0
WireConnection;149;0;148;0
WireConnection;150;0;149;0
WireConnection;141;0;140;0
WireConnection;141;1;166;0
WireConnection;141;2;150;0
WireConnection;133;0;53;0
WireConnection;133;1;20;0
WireConnection;64;0;141;0
WireConnection;64;1;19;0
WireConnection;0;0;133;0
WireConnection;0;11;64;0
ASEEND*/
//CHKSM=EA078823C8262B1A779D4A198257DA8EC03D7F03