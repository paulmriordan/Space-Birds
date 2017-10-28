Shader "Sprites/Dissolve"
{
    Properties {
        [PerRendererData] _MainTex ("Main texture", 2D) = "white" {}
        _DissolveTex ("Dissolve texture", 2D) = "gray" {}
        _DissolveProgress ("Dissolve Progress", Range(0.0, 1.00)) = 0.
    }
 
    SubShader {
 
        Tags { "Queue"="Transparent" }
		
		Cull Off
		Lighting Off
		ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass {
           
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
 
            #include "UnityCG.cginc"
 
            struct v2f {
                float4 vertex : SV_POSITION;
                float2 texcoord : TEXCOORD0;
            };
 
            v2f vert(appdata_base v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord;
                return o;
            }
 
            sampler2D _MainTex;
            sampler2D _DissolveTex;
            float _DissolveProgress;
 
            fixed4 frag(v2f i) : SV_Target {
                float4 c = tex2D(_MainTex, i.texcoord);
                float val = tex2D(_DissolveTex, i.texcoord).r;
 
                c.a *= step(_DissolveProgress, val);
                return c;
            }
            ENDCG
        }
    }
}