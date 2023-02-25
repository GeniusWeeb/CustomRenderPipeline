#ifndef COMMON_ADDED

#define COMMON_ADDED

#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"



#include "UnityInput.hlsl"




#define UNITY_MATRIX_M unity_ObjectToWorld
#define UNITY_MATRIX_I_M unity_WorldToObject
#define UNITY_MATRIX_V unity_MatrixV
#define UNITY_MATRIX_VP unity_MatrixVP
#define UNITY_MATRIX_P glstate_matrix_projection


//not there in tutorial
#define UNITY_PREV_MATRIX_M unity_Prev_ObjectToWorld
#define UNITY_PREV_MATRIX_I_M unity_PrevInverse_ObjectToWorld
#define UNITY_MATRIX_I_V unity_viewToWorldMatrix


#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/UnityInstancing.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/SpaceTransforms.hlsl"




#endif

