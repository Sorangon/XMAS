// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Custom/Sprite/Outline"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _OutlineWidth ("Outline Width", Float) = 0.2
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        [HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
        [PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
        [PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0
    }

    SubShader
    {

        //Right pass
        Pass{
            CGPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            #pragma target 2.0

            struct appdata {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct varying {
                float4 vertex : SV_POSITION;
                float2 texcoord : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _OutlineColor;
            float _OutlineWidth;

            varying Vert(appdata v){
                varying o;
                v.vertex.xy += fixed2(1.0, 0.0) * _OutlineWidth;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord;
                return o;
            }

            float4 Frag(varying i) : SV_TARGET{
                float texMask = tex2D(_MainTex, i.texcoord).a;

                if(texMask <= 0){
                    discard;
                }

                return _OutlineColor;
            }

        ENDCG
        }

        //Left Pass
        Pass{
            CGPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            #pragma target 2.0

            struct appdata {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct varying {
                float4 vertex : SV_POSITION;
                float2 texcoord : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _OutlineColor;
            float _OutlineWidth;

            varying Vert(appdata v){
                varying o;
                v.vertex.xy += fixed2(-1.0, 0.0) * _OutlineWidth;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord;
                return o;
            }

            float4 Frag(varying i) : SV_TARGET{
                float texMask = tex2D(_MainTex, i.texcoord).a;

                if(texMask <= 0){
                    discard;
                }

                return _OutlineColor;
            }

        ENDCG
        }

        //Down Pass
        Pass{
            CGPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            #pragma target 2.0

            struct appdata {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct varying {
                float4 vertex : SV_POSITION;
                float2 texcoord : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _OutlineColor;
            float _OutlineWidth;

            varying Vert(appdata v){
                varying o;
                v.vertex.xy += fixed2(0.0, -1.0) * _OutlineWidth;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord;
                return o;
            }

            float4 Frag(varying i) : SV_TARGET{
                float texMask = tex2D(_MainTex, i.texcoord).a;

                if(texMask <= 0){
                    discard;
                }

                return _OutlineColor;
            }

        ENDCG
        }

        //Up Pass
        Pass{
            CGPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            #pragma target 2.0

            struct appdata {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct varying {
                float4 vertex : SV_POSITION;
                float2 texcoord : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _OutlineColor;
            float _OutlineWidth;

            varying Vert(appdata v){
                varying o;
                v.vertex.xy += fixed2(0.0, 1.0) * _OutlineWidth;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord;
                return o;
            }

            float4 Frag(varying i) : SV_TARGET{
                float texMask = tex2D(_MainTex, i.texcoord).a;

                if(texMask <= 0){
                    discard;
                }

                return _OutlineColor;
            }

        ENDCG
        }

        //Base Sprite shader pass here

        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

        Pass
        {
        CGPROGRAM
            #pragma vertex SpriteVert
            #pragma fragment SpriteFrag
            #pragma target 2.0
            #pragma multi_compile_instancing
            #pragma multi_compile _ PIXELSNAP_ON
            #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
            #include "UnitySprites.cginc"
        ENDCG
        }
    }
}
