using System.Collections.Generic;
using UnityEngine;

namespace TurtleGamesStudio
{
    public class PositionConverterXY : IPositionConverter
    {
        public Vector3 ConvertPosition(Vector2 positionXY)
        {
            return positionXY;
        }

        public List<Vector3> ConvertPositions(IEnumerable<Vector2> positionXY)
        {
            List<Vector3> positions = new List<Vector3>();

            foreach (var position in positionXY)
                positions.Add(ConvertPosition(position));

            return positions;
        }

        public Quaternion ConvertRotation(Quaternion rotation)
        {
            return rotation;
        }

        public Vector3 ConvertScale(Vector2 scaleXY)
        {
            return scaleXY;
        }
    }
}
