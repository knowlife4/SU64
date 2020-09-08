Shader "Custom/DiffuseAlpha" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _MaskTex("Opacity (A)", 2D) = "white" {}
        _Cutoff ("Alpha cutoff (For shadows?!)", Range(0,1)) = 0.5
        _BaseScale ("Base Tiling", Vector) = (1,1,1,0)
    }
    SubShader {
        Tags { "Queue"="AlphaTest+50" "IgnoreProjector"="True" "RenderType"="TransparentCutout" }
        LOD 200
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
       
        CGPROGRAM
        #pragma surface surf Lambert fullforwardshadows keepalpha addshadow
        #pragma target 3.0
        fixed3 _BaseScale;
        sampler2D _MainTex;
        sampler2D _MaskTex;
       
        struct Input {
            float2 uv_MainTex;
            float2 uv_MaskTex;
            float3 worldPos;
	        float3 worldNormal;
        };
       
        fixed4 _Color;
       
        void surf (Input IN, inout SurfaceOutput o) {
            fixed4 texXY = tex2D(_MainTex, IN.worldPos.xy * _BaseScale.z);// IN.uv_MainTex);
	        fixed4 texXZ = tex2D(_MainTex, IN.worldPos.xz * _BaseScale.y);// IN.uv_MainTex);
	        fixed4 texYZ = tex2D(_MainTex, IN.worldPos.yz * _BaseScale.x);// IN.uv_MainTex);
	        fixed3 mask = fixed3(
	            	dot (IN.worldNormal, fixed3(0,0,1)),
                    dot (IN.worldNormal, fixed3(0,1,0)),
                    dot (IN.worldNormal, fixed3(1,0,0)));
	
	        fixed4 tex = 
	            	texXY * abs(mask.x) +
            		texXZ * abs(mask.y) +
            		texYZ * abs(mask.z);
        	fixed4 c = tex * _Color;
            	o.Albedo = c.rgb;
            half opacity = tex2D(_MaskTex, IN.uv_MaskTex).a;
            o.Alpha = opacity;
        }
        ENDCG
    }
Fallback "Legacy Shaders/Diffuse"
}

