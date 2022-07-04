Shader "Hidden/GrayFxShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PathTex ("PathTex", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _PathTex;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 pathCol = tex2D(_PathTex, i.uv);
                // just invert the colors
                if(pathCol.r == 0 && pathCol.g == 0 && pathCol.b == 0)
                {
                    col.rgb = (col.r + col.b + col.g) * 0.3;
                }
                return col;
            }
            ENDCG
        }
    }
}
