Shader "Custom/TwoScrolling" {
Properties {
    _Color ("Main Color", Color) = (1,1,1,1)
    _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
    _speed ("Speed", Float) = 0.2
}

Category {
    Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="Transparent"}
    LOD 100

    Cull Off Lighting Off ZWrite Off
    Blend SrcAlpha OneMinusSrcAlpha

    SubShader {
        Pass {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag

            #include "UnityCG.cginc"

            uniform sampler2D _MainTex;
            float _speed;
            fixed4 _Color;

            float4 frag(v2f_img i) : COLOR {

                i.uv.x += _Time*_speed;

                fixed4 tex = tex2D(_MainTex, i.uv) * _Color;

                tex.a = _Color.a;

                return tex;
            }
            ENDCG
        }
    }
}
}