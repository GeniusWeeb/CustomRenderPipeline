using UnityEditor;
using UnityEngine ;
using UnityEngine.Profiling;
using UnityEngine.Rendering ;


// so we can handlle different cameras
//As well as differfent rendering approached per camera as well
//TASK-> Camera should render what it should see
public partial  class CameraRenderer
{

    private partial void DrawUnSupportedShaders();
    private partial void PrepareBuffer();
    private partial void DrawGizmos();



    private partial void PrepareForSceneWindow(); 

    #if UNITY_EDITOR

    

    private partial void PrepareBuffer()
    {
        Profiler.BeginSample("Editor  Only");
        buffer.name = sampleName =  camera.name;
        Profiler.EndSample();
    }

    private static ShaderTagId[] legacyShaderTagIds =
        {
            new ShaderTagId("Always"),
            new ShaderTagId("ForwardBase"),
            new ShaderTagId("PrepassBase"),
            new ShaderTagId("Vertex"),
            new ShaderTagId("VertexLMRGBM"),
            new ShaderTagId("VertexLM"),

            
            
        };

        private static Material errorMaterial;



        
        //render ui using out custom RP
        private partial void PrepareForSceneWindow()
        {
            if(camera.cameraType == CameraType.SceneView)
                    ScriptableRenderContext.EmitWorldGeometryForSceneView(camera);
            
            
        }



        private  partial void DrawUnSupportedShaders()
            {

                if (errorMaterial == null)
                {
                    errorMaterial = new Material(Shader.Find("Hidden/InternalErrorShader"));            
                }

                
                //shaders come from draw
                //render order comees from filter I guess

                
                //check the cull for objects w these shaer ids and then tag them as unsupported


                var drawingSetting = new DrawingSettings(legacyShaderTagIds[0], new SortingSettings(camera))
                {
                        overrideMaterial  = errorMaterial
                };
                var filterSetting = FilteringSettings.defaultValue;

            
                for (int i = 1; i < legacyShaderTagIds.Length; i++)
                {
                    drawingSetting.SetShaderPassName(i , legacyShaderTagIds[i]);
                }

                context.DrawRenderers(cullingResult , ref drawingSetting , ref filterSetting);
            }

        private partial void DrawGizmos()
        {

            
            
            //chekc if gizmoz can be drawm
            if (UnityEditor.Handles.ShouldRenderGizmos())

            //chekc if gizmoz can be drawm
            if (Handles.ShouldRenderGizmos())

            {
                context.DrawGizmos(camera ,GizmoSubset.PreImageEffects);
                context.DrawGizmos(camera ,GizmoSubset.PostImageEffects);
                
                
            }
            
        }


#endif

  

}
