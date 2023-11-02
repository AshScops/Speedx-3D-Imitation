Shader "Custom/CubeOutline"
{
    Properties
    {
        _Color("Color", Color) = (1, 1, 1, 1)
        _OutlineColor("Outline Color", Color) = (0, 0, 0, 1)
        _Width("Width", Range(0, 0.5)) = 0.1
    }

        SubShader
    {
        Tags { "Queue" = "Transparent" }
        Pass
        {

            //���Ҫ��ʾ������߿�ȡ����������ע�ͼ���
            //cull off
            //ZWrite off
            blend srcalpha oneminussrcalpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            float4 _Color;
            float4 _OutlineColor;
            float _Width;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                half4 col = half4(0, 0, 0, 0);
                half2 border = half2(_Width, 1.0 - _Width);

                if (i.uv.x < border.x || i.uv.x > border.y || i.uv.y < border.x || i.uv.y > border.y)
                {
                    col = _OutlineColor; // ��Ե����ʹ�������ɫ
                }
                else
                {
                    col = _Color; // �ڲ�����ʹ�������ɫ
                }

                return col;
            }
            ENDCG
        }
    }
}
//���Shader�ܲ��ܸĳɾ������ԽԶ��������ɫԽ���������Խ����������ɫԽ����