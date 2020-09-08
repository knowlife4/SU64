// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

Shader "Custom/JellyShader" {
Properties {
_Color ("Color", Color) = (1,1,1,1)
_MainTex ("Albedo (RGB)", 2D) = "white" {}
_BumpMap ("Bumpmap", 2D) = "bump" {}
_NormalStrength ("Strength", Float) = 0.1
_Glossiness ("Smoothness", Range(0,1)) = 0.5
_Metallic ("Metallic", Range(0,1)) = 0.0
_Amplitude ("Amplitude", Float) = 0.1
_Frequency ("Frequency", Float) = 0.1
_ScrollXSpeed("X", Range(0,10)) = 2
_ScrollYSpeed("Y", Range(0,10)) = 3
}
SubShader {
Tags { "RenderType"="Opaque" }
LOD 200

CGPROGRAM
// Physically based Standard lighting model, and enable shadows on all light types
#pragma surface surf Standard fullforwardshadows addshadow vertex:vert

// Use shader model 3.0 target, to get nicer looking lighting
#pragma target 3.0

sampler2D _MainTex;
sampler2D _BumpMap;

struct Input {
float2 uv_MainTex;
float2 uv_BumpMap;
};

struct appdata {
        float4 vertex : POSITION;
        float4 tangent : TANGENT;
        float3 normal : NORMAL;
        float2 texcoord : TEXCOORD0;
        float2 texcoord1 : TEXCOORD1;
        float2 texcoord2 : TEXCOORD2;
};


half _Glossiness;
half _Metallic;
fixed4 _Color;
half _Frequency = 10;
half _Amplitude = 0.1;
half _NormalStrength = 0.1;
fixed _ScrollXSpeed;
fixed _ScrollYSpeed;

// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
// #pragma instancing_options assumeuniformscaling
UNITY_INSTANCING_BUFFER_START(Props)
// put more per-instance properties here
UNITY_INSTANCING_BUFFER_END(Props)

void vert (inout appdata v) {
v.vertex.xyz += v.normal * sin(v.vertex.x * _Frequency + _Time.y) * (_Amplitude/100);
}

void surf (Input IN, inout SurfaceOutputStandard o) {
// Albedo comes from a texture tinted by color
fixed2 scrolledUV = IN.uv_MainTex;
fixed xScrollValue = _ScrollXSpeed * _Time;
fixed yScrollValue = _ScrollYSpeed * _Time;
scrolledUV += fixed2(xScrollValue, yScrollValue);
half4 c = tex2D(_MainTex, scrolledUV) * _Color;
o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
o.Albedo += c.rbg;
// Metallic and smoothness come from slider variables
o.Metallic = _Metallic;
o.Smoothness = _Glossiness;
o.Alpha = c.a;
fixed3 normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap)); 
normal.z = normal.z * _NormalStrength; 
o.Normal = normalize(normal); 
}
ENDCG
}
FallBack "Diffuse"
}