// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/StandardOccluded"
{
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BumpMap ("Bumpmap", 2D) = "bump" {}
        _OccludedColor("Occluded Color", Color) = (1,1,1,1)
    }
    SubShader {
   
        Pass
        {
            Tags { "Queue"="Geometry+1" }
            ZTest Greater
            ZWrite Off
 
            CGPROGRAM
            #pragma vertex vert            
            #pragma fragment frag
            #pragma fragmentoption ARB_precision_hint_fastest
 
            half4 _OccludedColor;
 
            float4 vert(float4 pos : POSITION) : SV_POSITION
            {
                float4 viewPos = UnityObjectToClipPos(pos);
                return viewPos;
            }
 
                half4 frag(float4 pos : SV_POSITION) : COLOR
            {
                return _OccludedColor;
            }
 
            ENDCG
        }
 
        Tags { "RenderType"="Opaque" "Queue"="Geometry+1"}
        LOD 200
        ZWrite On
        ZTest LEqual
       
        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Lambert fullforwardshadows
 
        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0
 
        sampler2D _MainTex;
        sampler2D _BumpMap;

        struct Input {
            float2 uv_MainTex;
            float2 uv_BumpMap;
        };
 
        fixed4 _Color;
 
        void surf (Input IN, inout SurfaceOutput o) {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
            o.Albedo = c.rgb;
        }
        ENDCG
    }
    FallBack "Diffuse"
}