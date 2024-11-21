Shader "Custom/Outline Mask" {
    Properties {
        [Enum(UnityEngine.Rendering.CompareFunction)] _ZTest("ZTest", Float) = 3 // Equal
    }
    
    SubShader {
        Tags {
            "Queue" = "Transparent"
            "RenderType" = "Transparent"
        }
        
        Pass {
            Name "Mask"
            Cull Off
            ZTest [_ZTest]
            ZWrite Off
            ColorMask 0
            
            Stencil {
                Ref 1
                Pass Replace
            }
        }
    }
}