// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)
Shader "Custom/WorldSpaceTexture" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
    }
 
    SubShader {
        Tags { "RenderType"="Opaque" }
 
        CGPROGRAM
        #pragma surface surf Standard
 
        sampler2D _MainTex;
 
        struct Input {
            float3 worldNormal;
            float3 worldPos;
        };
 
        void surf (Input IN, inout SurfaceOutputStandard o) {
 
            if(abs(IN.worldNormal.y) > 0.5)
            {
                o.Albedo = tex2D(_MainTex, IN.worldPos.xz * 0.2);
            }
            else if(abs(IN.worldNormal.x) > 0.5)
            {
                o.Albedo = tex2D(_MainTex, IN.worldPos.yz * 0.2);
            }
            else
            {
                o.Albedo = tex2D(_MainTex, IN.worldPos.xy * 0.2);
            }
 
            o.Emission = o.Albedo;
        }
 
        ENDCG
    }
    FallBack "Diffuse"
}