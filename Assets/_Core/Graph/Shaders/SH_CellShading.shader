Shader "Sorangon Shader Library/Custom Surfaces/Cell Shading"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _LightDir("Light Direction", Vector) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float4 _LightDir;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            float GetLightValue(float3 normal) {
                return round(saturate(dot(normalize(_LightDir), normal)));
            }

            float4 CalculateLighting(float3 surfaceNormal){
                float light = GetLightValue(surfaceNormal);
                float4 finalLight = UNITY_LIGHTMODEL_AMBIENT + light;

                return finalLight;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                col *= _Color;

                return col * CalculateLighting(i.normal);
            }
            ENDCG        
        }

        Pass
        {
            Name "SHADOW_PASS"
            Tags{"LightMode" = "ShadowCaster"}
            Fog{Mode Off}
            ZWrite On ZTest LEqual Cull Off
            Offset 1, 1

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_shadowcaster
            #pragma fragmentoption ARB_precision_hint_fastest
            #include "UnityCG.cginc"

            struct v2f{
                V2F_SHADOW_CASTER;
            };

            v2f vert(appdata_base v){
                v2f o;
                TRANSFER_SHADOW_CASTER(o);
                return o;
            }

            float4 frag(v2f i) : COLOR{
                SHADOW_CASTER_FRAGMENT(i);
            }

            ENDCG
        }

    }

    Fallback "Diffuse"
}
