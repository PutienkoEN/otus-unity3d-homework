using System;
using UnityEngine;

namespace Homeworks.SaveLoad.Data
{
    [Serializable]
    public class TransformData
    {
        public float[] Position { get; set; }
        public float[] Rotation { get; set; }
        public float[] Scale { get; set; }

        public static TransformData FromTransform(Transform transform)
        {
            return new TransformData
            {
                Position = Vector3ToArray(transform.position),
                Rotation = Vector3ToArray(transform.rotation.eulerAngles),
                Scale = Vector3ToArray(transform.localScale)
            };
        }

        public Vector3 GetPositionAsVector3()
        {
            return Vector3FromArray(Position);
        }

        public Quaternion GetRotationAsQuaternion()
        {
            return QuaternionFromArray(Rotation);
        }

        public Vector3 GetScaleAsVector3()
        {
            return Vector3FromArray(Scale);
        }

        private static float[] Vector3ToArray(Vector3 gameObjectPosition)
        {
            var position = new float[3];

            position[0] = gameObjectPosition.x;
            position[1] = gameObjectPosition.y;
            position[2] = gameObjectPosition.z;

            return position;
        }

        private static float[] QuaternionToArray(Quaternion gameObjectRotation)
        {
            var rotation = new float[4];

            rotation[0] = gameObjectRotation.x;
            rotation[1] = gameObjectRotation.y;
            rotation[2] = gameObjectRotation.z;
            rotation[3] = gameObjectRotation.w;

            return rotation;
        }

        private static Vector3 Vector3FromArray(float[] position)
        {
            return new Vector3(position[0], position[1], position[2]);
        }

        private static Quaternion QuaternionFromArray(float[] rotation)
        {
            return Quaternion.Euler(rotation[0], rotation[1], rotation[2]);
        }
    }
}