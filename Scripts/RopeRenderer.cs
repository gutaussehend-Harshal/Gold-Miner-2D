using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeRenderer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField] private Transform startPosition;
    private float line_Width = 0.05f;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = line_Width;
        lineRenderer.enabled = false;
    }
    public void RenderLine(Vector3 endPosition, bool enableRender)
    {
        if (enableRender)
        {
            if (!lineRenderer.enabled)
            {
                lineRenderer.enabled = true;
            }
            lineRenderer.positionCount = 2;
        }
        else
        {
            lineRenderer.positionCount = 0;
            if (lineRenderer.enabled)
            {
                lineRenderer.enabled = false;
            }
        }
        if (lineRenderer.enabled)
        {
            Vector3 position = startPosition.position;
            position.z = -10f;
            startPosition.position = position;

            position = endPosition;
            position.z = 0f;
            endPosition = position;

            lineRenderer.SetPosition(0, startPosition.position);
            lineRenderer.SetPosition(1, endPosition);
        }
    }
}
