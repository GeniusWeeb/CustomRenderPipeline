#ifndef CUSTOM_UNLIT_PASS_INCLUDED

#define CUSTOM_UNLIT_PASS_INCLUDED

    #include "../ShaderLibrary/Common.hlsl"


//macros 
TEXTURE2D(_BaseMap);
SAMPLER(sampler_BaseMap);


UNITY_INSTANCING_BUFFER_START(UnityPerMaterial)
UNITY_DEFINE_INSTANCED_PROP(float4 , _BaseMap_ST) //offset and tiling PER INSTANCE
UNITY_DEFINE_INSTANCED_PROP(float4 , _BaseColor)
UNITY_DEFINE_INSTANCED_PROP(float , _CutOff)
UNITY_INSTANCING_BUFFER_END(UnityPerMaterial)

//vertex  function thinks we are passing a struct params for accessing vertex index
// that is being rendered eg for gpu instancing we need index data
struct Attributes
{
    float2 baseUV : TEXCOORD0;  // texture coordinates
    float3 positionOS : POSITION ; //we need position from this specific transform ;
    UNITY_VERTEX_INPUT_INSTANCE_ID // OBJECT INDEX
    
};
struct Varyings  // since data can vary for fragments of the same triangle
{
    float2 baseUV : VAR_BASE_UV; // tex frag corrd
    float4 positionCS : SV_POSITION ;// for index
    UNITY_VERTEX_INPUT_INSTANCE_ID 
};

//both struct above are the same thing essentially and do the same functionality



Varyings UnlitPassVertex (Attributes input){//this input is already filled with input
    
    Varyings output ;  // creating object of varyings
    float4 baseST =  UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial ,_BaseMap_ST);
    output.baseUV = input.baseUV * baseST.xy + baseST.zw;
    UNITY_SETUP_INSTANCE_ID(input);//extracts index/position and makes it global so macros can access it
    UNITY_TRANSFER_INSTANCE_ID(input , output); // copies input structure to the output structure in VERTEX SHADER
    float3 positionWS = TransformObjectToWorld(input.positionOS);
    output.positionCS = TransformWorldToHClip(positionWS); //same thing as above but getting clip space
    //storing index and position in a diff struct for the fragment data instead of returning it directly
    return output ; 
        }
float4 UnlitPassFragment (Varyings input) : SV_TARGET {
    UNITY_SETUP_INSTANCE_ID(input);
    float4 baseMap  = SAMPLE_TEXTURE2D(_BaseMap , sampler_BaseMap ,input.baseUV);
    float4 baseColor = UNITY_ACCESS_INSTANCED_PROP( UnityPerMaterial,_BaseColor);
    float4 base  = baseMap * baseColor;
    #if defined(_CLIPPING)
    clip(base.a -( UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial ,_CutOff) ));
    #endif
    return base ; 
}


#endif
