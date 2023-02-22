using UnityEngine ; 
using UnityEngine.Rendering ; 
 
public class CustomRenderPipeline : RenderPipeline
{
    private CameraRenderer renderer = new CameraRenderer(); 
        
        protected override void Render(ScriptableRenderContext context, Camera[] cameras)
        {
            foreach (Camera cam in cameras)
            {
                renderer.Render(context , cam);
            }
        }
        
    



    }
