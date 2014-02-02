Shader "Custom/ProxmityShader" {
Properties {
       //_MainTex ("Base (RGB)", 2D) = "white" {} // Regular object texture 
       _RevealColor ("Reveal Color", Color) = (1,1,1,1)
       _MouseWorldPos ("Mouse World Pos", vector) = (0,0,0,0) // Mouse click location in world space
       _VisibleDistance ("Visibility Distance", float) = 10.0 // How close does mouse world pos have to be to make object visible
       _OutlineWidth ("Outline Width", float) = 3.0 // Used to add an outline around visible area a la Mario Galaxy - http://www.youtube.com/watch?v=91raP59am9U
       _OutlineColour ("Outline Colour", color) = (1.0,1.0,0.0,1.0) // Colour of the outline
    }
    SubShader {
       Tags { "RenderType"="Transparent" "Queue"="Transparent"}
       Pass {
       Blend SrcAlpha OneMinusSrcAlpha
       LOD 200
 
       CGPROGRAM
       #pragma vertex vert
       #pragma fragment frag
 
       // Access the shaderlab properties
       //uniform sampler2D _MainTex;
       uniform float4 _MouseWorldPos;
       uniform float _VisibleDistance;
       uniform fixed4 _RevealColor;
       uniform float _OutlineWidth;
       uniform fixed4 _OutlineColour;
 
       // Input to vertex shader
       struct vertexInput {
            float4 vertex : POSITION;
            float4 texcoord : TEXCOORD0;
         };
       // Input to fragment shader
         struct vertexOutput {
            float4 pos : SV_POSITION;
            float4 position_in_world_space : TEXCOORD0;
            float4 tex : TEXCOORD1;
         };
 
         // VERTEX SHADER
         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output; 
            output.pos =  mul(UNITY_MATRIX_MVP, input.vertex);
            output.position_in_world_space = mul(_Object2World, input.vertex);
            output.tex = input.texcoord;
            return output;
         }
 
      // FRAGMENT SHADER
       float4 frag(vertexOutput input) : COLOR 
       {
         // Calculate distance to player position
            float dist = distance(input.position_in_world_space, _MouseWorldPos);
 
       // Return appropriate colour
            if (dist < _VisibleDistance) {
            	return _RevealColor;
               //return tex2D(_MainTex, float2(input.tex)); // Visible
            }
            else if (dist < _VisibleDistance + _OutlineWidth) {
                return _OutlineColour; // Edge of visible range
            }
            else {
                //float4 tex = tex2D(_MainTex, float2(input.tex)); // Outside visible range
                //tex.a = 0.1;
                float4 tex = _RevealColor;
                tex.a = 0.01;
                return tex;
            }
         }
 
       ENDCG
       }
    } 
	FallBack "Diffuse"
}
