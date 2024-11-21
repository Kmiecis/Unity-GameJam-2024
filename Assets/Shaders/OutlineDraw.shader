﻿Shader "Custom/Outline Draw" {
    Properties {
        [Enum(UnityEngine.Rendering.CompareFunction)] _ZTest("ZTest", Float) = 2 // Less

        _Color("Color", Color) = (1, 1, 1, 1)
        _Width("Width", Range(0, 20)) = 5
    }

    SubShader {
        Tags {
            "Queue" = "Transparent+1"
            "RenderType" = "Transparent"
            "DisableBatching" = "True"
        }

        Pass {
            Name "Draw"
            Cull Off
            ZTest [_ZTest]
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            ColorMask RGB
            
            Stencil {
                Ref 1
                Comp NotEqual
            }
        
            CGPROGRAM
            #include "UnityCG.cginc"

            #pragma vertex vert
            #pragma fragment frag

            struct appdata {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 texcoord3 : TEXCOORD3;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f {
                float4 position : SV_POSITION;
                fixed4 color : COLOR;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            uniform fixed4 _Color;
            uniform float _Width;

            v2f vert(appdata input) {
                v2f output;
            
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);
                
                float3 normal = input.texcoord3.w > 0.0 ? input.normal : input.texcoord3;
                float3 viewNormal = normalize(mul((float3x3)UNITY_MATRIX_IT_MV, normal));
                float3 viewPosition = UnityObjectToViewPos(input.vertex);

                output.position = UnityViewToClipPos(viewPosition + viewNormal * -viewPosition.z * _Width / (min(_ScreenParams.x, _ScreenParams.y)));
                output.color = _Color;
            
                return output;
            }
        
            fixed4 frag(v2f input) : SV_Target {
                return input.color;
            }
            ENDCG
        }
    }
}