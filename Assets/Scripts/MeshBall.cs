using System;
using System.Numerics;
using UnityEngine;
using Matrix4x4 = UnityEngine.Matrix4x4;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;
using Vector4 = UnityEngine.Vector4;

namespace DefaultNamespace
{
    public class MeshBall : MonoBehaviour
    {
        private static int baseColorId = Shader.PropertyToID("_BaseColor");

        [SerializeField] private Mesh mesh = default;
        [SerializeField] private Material material = default;


        private Matrix4x4[] matrices = new Matrix4x4[1023];
        private Vector4[] baseColors = new Vector4[1023];

        private MaterialPropertyBlock block;


        private void Awake()
        {
            for (int i = 0; i < matrices.Length; i++)
            {
                matrices[i] = Matrix4x4.TRS(Random.insideUnitSphere, Quaternion.identity, Vector3.one);
                baseColors[i] = new Vector4(Random.value, Random.value, Random.value, 1f);
            }
        }


        private void Update()
        {
            if (block == null)
            {   block = new MaterialPropertyBlock();
                 block.SetVectorArray(baseColorId, baseColors);
            }
            Graphics.DrawMeshInstanced(mesh ,0 ,material,matrices,1023,block);
        }
    }
}