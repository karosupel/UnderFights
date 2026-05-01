Shader "Custom/Shockwave"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Radius ("Radius", Float) = 0.3
        _Thickness ("Thickness", Float) = 0.05
        _HoleStart ("Hole Start", Float) = -0.5
        _HoleEnd ("Hole End", Float) = 0.5
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

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

            float4 _Color;
            float _Radius;
            float _Thickness;
            float _HoleStart;
            float _HoleEnd;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv - 0.5; // centrowanie UV
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 p = i.uv;

                float dist = length(p);

                // pierścień (shockwave)
                float ring = abs(dist - _Radius) < _Thickness;

                // półokrąg (górna część)
                float half = p.y > 0;

                // kąt
                float angle = atan2(p.y, p.x);

                // dziura
                float hole = (angle > _HoleStart && angle < _HoleEnd);

                if (ring && half && !hole)
                    return _Color;

                return float4(0,0,0,0);
            }
            ENDCG
        }
    }
}