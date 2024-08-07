using System.Collections.Generic;
using UnityEngine;

namespace TurtleGamesStudio
{
    public class PositionConverterXZ : IPositionConverter
    {
        public Vector3 ConvertPosition(Vector2 positionXY)
        {
            return new Vector3(positionXY.x, 0f, positionXY.y);
        }

        public Quaternion ConvertRotation(Quaternion rotation)
        {
            return Quaternion.Euler(0, rotation.eulerAngles.z, 0);
        }

        public Vector3 ConvertScale(Vector2 scaleXY)
        {
            return new Vector3(scaleXY.x, 1f, scaleXY.y);
        }

        public List<Vector3> ConvertPositions(IEnumerable<Vector2> positionXY)
        {
            List<Vector3> positions = new List<Vector3>();

            foreach (var position in positionXY)
            {
                positions.Add(ConvertPosition(position));
            }

            return positions;
        }
    }
}
