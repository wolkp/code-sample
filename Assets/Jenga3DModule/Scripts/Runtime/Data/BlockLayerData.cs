using UnityEngine;

public class BlockLayerData 
{
    public Vector3 CenterPoint { get; private set; }

    public BlockLayerData(Vector3 centerPoint)
    {
        CenterPoint = centerPoint;
    }
}