Shader "Custom/MuroNebbia"
{
    Properties
    {
        _MainTex ("Texture Nebbia (Noise)", 2D) = "white" {}
        [HDR] _Color ("Colore Tinta", Color) = (0,1,1,0.5)
        _Speed ("Velocità Rotazione", Range(-5, 5)) = 0.5
        _Alpha ("Opacità Base", Range(0, 1)) = 0.8
        _FresnelPower ("Intensità Bordi (Fresnel)", Range(0.5, 8.0)) = 3.0
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" "IgnoreProjector"="True" }
        LOD 100
        
        // Disabilita la scrittura nello Z-buffer per la trasparenza corretta
        ZWrite Off
        // Abilita il blending per la trasparenza (Alpha Blending)
        Blend SrcAlpha OneMinusSrcAlpha
        // Disegna entrambi i lati del muro
        Cull Off 

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
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 viewDir : TEXCOORD1;
                float3 normal : NORMAL;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float _Speed;
            float _Alpha;
            float _FresnelPower;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                
                // Calcolo della direzione vista (per l'effetto bordi/fresnel)
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.viewDir = normalize(_WorldSpaceCameraPos.xyz - worldPos);

                // --- LOGICA DI ROTAZIONE ---
                // Calcola seno e coseno basati sul tempo
                float rotation = _Time.y * _Speed;
                float s = sin(rotation);
                float c = cos(rotation);
                
                // Centra le UV (sposta l'origine al centro della texture, 0.5, 0.5)
                float2 center = float2(0.5, 0.5);
                float2 centeredUV = v.uv - center;
                
                // Applica la matrice di rotazione 2D
                float2 rotatedUV;
                rotatedUV.x = centeredUV.x * c - centeredUV.y * s;
                rotatedUV.y = centeredUV.x * s + centeredUV.y * c;
                
                // Riporta le UV alla posizione originale + Tiling
                o.uv = (rotatedUV + center) * _MainTex_ST.xy + _MainTex_ST.zw;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // 1. Campiona la texture (che ora sta girando)
                fixed4 texColor = tex2D(_MainTex, i.uv);
                
                // 2. Calcola l'effetto Fresnel (bordi più visibili)
                // Il dot product tra normale e vista dice "quanto stiamo guardando il muro di fronte"
                float NdotV = saturate(dot(normalize(i.normal), normalize(i.viewDir)));
                // Invertiamo: vogliamo che sia più visibile ai bordi (dove l'angolo è radente)
                float fresnel = pow(1.0 - NdotV, 0.5); // 0.5 ammorbidisce l'effetto base
                
                // 3. Combina Colore
                fixed4 finalColor = texColor * _Color;
                
                // 4. Combina Alpha: Opacità base * Alpha della texture * Effetto Fresnel
                // Usiamo max() per garantire che i bordi siano sempre un po' illuminati
                finalColor.a = _Alpha * texColor.a * (1.0 + fresnel * _FresnelPower);
                
                return finalColor;
            }
            ENDCG
        }
    }
}