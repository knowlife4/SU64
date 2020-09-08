Shader "Unlit/AngularGradient"
{

    Properties
    {
        [HideInInspector]_MainTex ("Texture", 2D) = "white" {}
        _Color("Color1", Color) = (0,1,1,1)
        _Color2("Color2", Color) = (1,0,1,1)
        _Angle("Angle", range(0, 360)) = 0
        [Toggle]_Flip("Flip gradient direction", float) = 0

        _CenterOffX("Center offset X", range(0, 1.5)) = 0.5
        _CenterOffY("Center offset Y", range(0, 1.5)) = 0.5

        _Blend("Blend", Range(0.01,2)) = 1
        _BlendOffset("Blend offset", Range(0, 1)) = 0
        _Fill("Fill", Range(0, 360)) = 360
        [Toggle]_FillFlip("Flip fill direction", float) = 0
        [Header(Cull mode)]
        [Enum(UnityEngine.Rendering.CullMode)] _CullMode("Cull mode", float) = 2
        [Header(ZTest)]
        [Enum(UnityEngine.Rendering.CompareFunction)] _ZTest("ZTest", float) = 0
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 100
            Blend SrcAlpha OneMinusSrcAlpha
            Cull [_CullMode]
            ZTest [_ZTest]

        Pass
        {

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            #define rad 6.283185307164

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;
            fixed4 _Color2;

            float _CenterOffX;
            float _CenterOffY;

            float _Angle;
            float _Flip;

            float _Blend;
            float _BlendOffset;

            float _Fill;
            float _FillFlip;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //Center point of the gradient
                float2 center = float2(_CenterOffX, _CenterOffY);
                float angle; 

                //Check if we need to flip the angle for anti clockwise rotation
                if (_Flip == 0)
                {
                    //Get the angle from the center to the outside edge. The input for the offset is in degrees to we convert it to radians
                    angle = atan2(i.uv.y - center.y, i.uv.x - center.x) + (radians(_Angle));
                }
                else
                {
                    angle = -atan2(i.uv.y - center.y, i.uv.x - center.x) + (radians(_Angle));
                }
                //Clamp the results to the space of 0° < > 360° instead of -180° < > 180° (as radians)
                angle = (angle + rad) % rad;

                //Normalize the angle (in radians)
                float nAngle = angle / rad;
                //Apply the colour to the selected texture
                fixed4 col = tex2D(_MainTex, i.uv);

                //Lerp the two colours over the angle
                col *= lerp(_Color, _Color2, (nAngle -= _BlendOffset) / _Blend);

                float fillAmount;

                if(_FillFlip == 0)
                {
                    //Get the angle for the cutoff of the gradient
                    fillAmount = atan2(i.uv.y - center.y, i.uv.x - center.x) + (radians(_Angle));
                }
                else
                {
                    fillAmount = -atan2(i.uv.y - center.y, i.uv.x - center.x) + (radians(_Angle));
                }

                //Clamp the results between 0° and 360°
                fillAmount = (fillAmount + rad) % rad;

                //0.0174533 is 1 degree in radians
                float radFill = _Fill *= 0.0174533;

                //If the pixel is outside the fill range set the alpha to 0
                if (fillAmount > _Fill) 
                {
                    col.a = 0;
                }

                return col;
            }
            ENDCG
        }
    }
}