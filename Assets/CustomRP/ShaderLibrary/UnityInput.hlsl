#ifndef CUSTOM_INPUT_ADDED
#define CUSTOM_INPUT_ADDED




CBUFFER_START(UnityPerDraw)
float4x4 unity_ObjectToWorld;
float4 unity_LODFade;
float4x4 unity_WorldToObject;

real4 unity_WorldTransformParams;
CBUFFER_END


float4x4  unity_viewToWorldMatrix;
float4x4 unity_Prev_ObjectToWorld;
float4x4 unity_PrevInverse_ObjectToWorld;
float4x4 unity_MatrixVP;
float4x4 unity_MatrixV;
float4x4 glstate_matrix_projection;


//camere
#endif
