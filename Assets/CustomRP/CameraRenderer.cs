using UnityEngine ;

using UnityEngine.Profiling;

using UnityEngine.Rendering ;


// so we can handlle different cameras
//As well as differfent rendering approached per camera as well
//TASK-> Camera should render what it should see
public partial  class CameraRenderer
{
    private ScriptableRenderContext context;
    private Camera camera;
    private CullingResults cullingResult; 
    
    //shader pass unlit
    private static ShaderTagId unlitShaderTagId = new ShaderTagId("SRPDefaultUnlit");

    private string sampleName { get; set; }

    private const string bufferName = "Render Custom Camera";

    #region Entry point
        public void Render(ScriptableRenderContext context, Camera camera , bool useDynamicBatching , bool useGPUInstancing)
        {   
            this.context = context;
            this.camera = camera;
            
            

            PrepareBuffer(); //for multiple camera and camera name
            PrepareForSceneWindow();

            PrepareBuffer();

            if (!Cull()) return;
            
            Setup();
            DrawVisibleGeometry(useDynamicBatching ,useGPUInstancing);
            DrawUnSupportedShaders();
            DrawGizmos();
            Submit();
        }
        
        private void DrawVisibleGeometry(bool useDynamicBatching , bool useGPUInstancing)
        {

            var sortingSetings = new SortingSettings(camera)
            {
                criteria = SortingCriteria.CommonOpaque
                
            };
            var drawingSettings = new DrawingSettings(unlitShaderTagId, sortingSetings)
            {
                
                enableDynamicBatching = useDynamicBatching,
                enableInstancing = useGPUInstancing
                
            };
            
            //what to specifically render 
            var filteringSettings = new FilteringSettings(RenderQueueRange.opaque);

            context.DrawRenderers(cullingResult, ref drawingSettings, ref filteringSettings);
            context.DrawSkybox(  camera);
            sortingSetings.criteria = SortingCriteria.CommonTransparent;
            drawingSettings.sortingSettings = sortingSetings;
            filteringSettings = new FilteringSettings(RenderQueueRange.transparent);
            context.DrawRenderers(cullingResult ,  ref drawingSettings ,ref filteringSettings);
        }





        #endregion
    
    //draw objects
    private CommandBuffer buffer = new CommandBuffer();
    private void Setup()
    {   

        context.SetupCameraProperties(camera); //set at compile time
        CameraClearFlags flags = camera.clearFlags;
        buffer.ClearRenderTarget(flags <= CameraClearFlags.Depth, flags <= CameraClearFlags.Color , flags == CameraClearFlags.Color ? camera.backgroundColor.linear : Color.clear);
        buffer.BeginSample(sampleName); // profiler or frame debugger specific
        ExecuteBuffer();
        

    }


    //info -> this  this is buffered and need to be submitted for execution  in queue like w
    private void Submit()
    {   
        buffer.EndSample(sampleName);
        ExecuteBuffer();
        
        //last is submit for start and end and at last submit
        context.Submit();
    }
    
    //Execute and clear
    private void ExecuteBuffer()
    {
        context.ExecuteCommandBuffer(buffer);
        buffer.Clear();
    }

    //Culling objects and showing rest in cameras point of view
    
    private bool Cull()
    {
        
        //this returns a struc tof object that are visible -> hence we used ref
        if (camera.TryGetCullingParameters(out ScriptableCullingParameters p))
        {
            cullingResult = context.Cull(ref p);
             return true; 
        }

        return false; 
        
        
    }


  

}
