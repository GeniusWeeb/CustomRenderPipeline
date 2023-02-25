using UnityEngine ; 
using UnityEngine.Rendering ; 
 
public class CustomRenderPipeline : RenderPipeline
{
    bool useDynamicBatching ,  useGPUInstancing; 
    private CameraRenderer renderer = new CameraRenderer(); 
        
        protected override void Render(ScriptableRenderContext context, Camera[] cameras)
        {
            foreach (Camera cam in cameras)
            {
                renderer.Render(context , cam, useDynamicBatching , useGPUInstancing);
            }
        }


        public CustomRenderPipeline(bool useDynamicBatching, bool useGPUInstancing , bool useSRPBatcher)
        {

            this.useDynamicBatching = useDynamicBatching;
            this.useGPUInstancing = useGPUInstancing;
             GraphicsSettings.useScriptableRenderPipelineBatching = useSRPBatcher; 
        }





}
