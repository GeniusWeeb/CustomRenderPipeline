using System;
using UnityEngine;

    [DisallowMultipleComponent]
    
    //to provide colors basically
    public class PerObjectMaterialProperties : MonoBehaviour
    {
        
        //this generate and stores the property of custom shader called "_BaseColor" into static .
        private static int baseColorID = Shader.PropertyToID("_BaseColor");
        private static int cutOffdId = Shader.PropertyToID("_CutOff");
        
        [SerializeField] private Color baseColor = Color.white;
        [SerializeField][Range(0f,1f)] private float cutOff = 0.5f;

        private static MaterialPropertyBlock block;

        private void Awake()
        {
            OnValidate();
        }

        
        //gpu instancing for this
        
        //single draw call -> multiple objects with same mesh
        private void OnValidate()
        {
            if (block == null)
                block = new MaterialPropertyBlock();
            
            block.SetColor(baseColorID , baseColor );
            block.SetFloat(cutOffdId , cutOff);
            GetComponent<Renderer>().SetPropertyBlock(block);
            Debug.Log(baseColorID);
            
        }
        
        
        
        
        }
    
