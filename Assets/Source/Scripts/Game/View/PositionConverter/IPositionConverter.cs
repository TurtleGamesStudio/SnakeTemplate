using System.Collections.Generic;
using UnityEngine;

namespace TurtleGamesStudio
{
    public interface IPositionConverter
    {
        Vector3 ConvertPosition(Vector2 positionXY);
        Vector3 ConvertScale(Vector2 scaleXY);
        Quaternion ConvertRotation(Quaternion rotation);
        List<Vector3> ConvertPositions(IEnumerable<Vector2> positionXY);
    }
}
