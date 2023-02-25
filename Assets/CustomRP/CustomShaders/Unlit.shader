Shader "Custom RP/Unlit" 
{
    Properties { //MATERIAL LINKS
          
        _BaseMap("Texture",2D) = "white" {}      
        _BaseColor("Color", color) = (1,1,1,1)
        _CutOff("Alpha Cutoff" , Range(0.0,1.1)) = 0.5
        
        
        //transparency
    [Enum(UnityEngine.Rendering.BlendMode)] _SrcBlend("Src Blend" , float) = 1   //drawn recent
    [Enum(UnityEngine.Rendering.BlendMode)] _DstBlend("Dst Blend", float) = 0   //what was drawn before
    [Enum(off , 0 , on , 1)] _ZWrite("Z Write",float) =1     
    [Toggle(_CLIPPING)]_Clipping("Alpha Clipping", float) = 0      
        
        
            
    }//Defining shader properties
    
    
SubShader
    {
        Pass{
            
                Blend [_SrcBlend][_DstBlend]
                ZWrite [_ZWrite]
                HLSLPROGRAM
                #pragma shader_feature _CLIPPING
                #pragma  multi_compile_instancing //directive for GPU instancing
                #pragma vertex UnlitPassVertex
                #pragma fragment UnlitPassFragment
                
                #include "UnlitPass.hlsl"
            
            ENDHLSL   
            
            }

    }    
    
 }   
    
    
    
    