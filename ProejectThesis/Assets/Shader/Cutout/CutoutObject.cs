using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutoutObject : MonoBehaviour
{
    public static int PosID = Shader.PropertyToID("_Positionc");
    public static int SizeID = Shader.PropertyToID("_Sizec");

    public float size = 1.0f;
    public Material WallMaterial;
    public Camera Camera;
    public LayerMask wallMask;



    private void Update()
    {
        var dir = Camera.transform.position - transform.position;
        var ray = new Ray(transform.position, dir.normalized);

        if (Physics.Raycast(ray, 3000, wallMask))
            SetShaderProperties(size);
        else
            SetShaderProperties(0);

        var view =  Camera.WorldToViewportPoint(transform.position);
        WallMaterial.SetVector(PosID, view);

    }
    private void SetShaderProperties(float sizeValue)
    {
        // หา Shader ที่มีชื่อเดียวกันในทุก Material ที่กำลังใช้
        Shader targetShader = Shader.Find(WallMaterial.shader.name);

        // ถ้าเจอ Shader ที่ตรงกัน
        if (targetShader != null)
        {
            // ปรับค่าของ Properties ใน Shader
            foreach (Material material in Resources.FindObjectsOfTypeAll<Material>())
            {
                if (material.shader == targetShader)
                {
                    material.SetFloat(SizeID, sizeValue);

                    var view = Camera.WorldToViewportPoint(transform.position);
                    material.SetVector(PosID, view);
                }
            }
        }
        else
        {
            Debug.LogError("Shader not found.");
        }
    }
}
