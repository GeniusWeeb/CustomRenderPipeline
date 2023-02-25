using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu( menuName = "CustomRendering/RenderPipelineAsset")]
public class CustomRenderPipelineAsset : RenderPipelineAsset
{

    [SerializeField] private bool  useDynamicBatching =true ,  useGPUInstancing = true , useSRPBatcher = true ;
    
    protected override RenderPipeline CreatePipeline()
    {

        return new CustomRenderPipeline(useDynamicBatching , useGPUInstancing , useSRPBatcher);
    }


  
}
