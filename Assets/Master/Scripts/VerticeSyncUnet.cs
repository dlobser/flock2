//#if ENABLE_UNET
using System;
using UnityEngine;
using System.Collections.Generic;

namespace UnityEngine.Networking
{
    [AddComponentMenu("Network/VerticeSyncUnet")]
    //[NetworkSettings(channel=2)]

    public class VerticeSyncUnet : NetworkBehaviour
    {
        public class PointInfo
        {
            public Transform m_bone;
            public Vector3 m_TargetSyncPosition;
            public Quaternion m_TargetSyncRotation3D;
        }

        const short PointTransformMessage = 200;

        // [SerializeField]
        // Transform m_Target;

        [SerializeField]
        Transform[] m_points;


        [SerializeField]
        float m_SendInterval = 25f;
        [SerializeField]
        NetworkTransform.AxisSyncMode m_SyncRotationAxis = NetworkTransform.AxisSyncMode.AxisXYZ;
        [SerializeField]
        NetworkTransform.CompressionSyncMode m_RotationSyncCompression = NetworkTransform.CompressionSyncMode.None;

        [SerializeField]
        float m_InterpolateRotation = 0.5f;
        [SerializeField]
        float m_InterpolateMovement = 0.5f;

        [SerializeField]
        int m_SyncLevel = 0;

        [SerializeField]
        int m_NetworkChannel = Channels.DefaultUnreliable;

        // movement smoothing
        public PointInfo[] mPointInfos;


        float m_LastClientSyncTime; // last time client received a sync from server
        float m_LastClientSendTime; // last time client send a sync to server

        const float k_LocalRotationThreshold = 0.00001f;

        public int numPoints;
        public int binarySize;


        NetworkWriter m_LocalTransformWriter;

        // settings
       // public Transform target { get { return m_Target; } set { m_Target = value; OnValidate(); } }
        public float sendInterval { get { return m_SendInterval; } set { m_SendInterval = value; } }
        public NetworkTransform.AxisSyncMode syncRotationAxis { get { return m_SyncRotationAxis; } set { m_SyncRotationAxis = value; } }
        public NetworkTransform.CompressionSyncMode rotationSyncCompression { get { return m_RotationSyncCompression; } set { m_RotationSyncCompression = value; } }
        public float interpolateRotation { get { return m_InterpolateRotation; } set { m_InterpolateRotation = value; } }
        public float interpolateMovement { get { return m_InterpolateMovement; } set { m_InterpolateMovement = value; } }
        public int syncLevel { get { return m_SyncLevel; } set { m_SyncLevel = value; } }

        // runtime data
        public float lastSyncTime { get { return m_LastClientSyncTime; } }

        void OnValidate()
        {
            if (m_SendInterval < 0)
            {
                m_SendInterval = 0;
            }

            if (m_SyncRotationAxis < NetworkTransform.AxisSyncMode.None || m_SyncRotationAxis > NetworkTransform.AxisSyncMode.AxisXYZ)
            {
                m_SyncRotationAxis = NetworkTransform.AxisSyncMode.None;
            }

            if (interpolateRotation < 0)
            {
                interpolateRotation = 0.01f;
            }
            if (interpolateRotation > 1.0f)
            {
                interpolateRotation = 1.0f;
            }

            if (interpolateMovement < 0)
            {
                interpolateMovement = 0.01f;
            }

            if (interpolateMovement > 1.0f)
            {
                interpolateMovement = 1.0f;
            }
        }



        void Awake()
        {
           
            if (m_points.Length == 0)
                Debug.LogError("need more points");

            mPointInfos = new PointInfo[m_points.Length];
            for (int i = 0; i < m_points.Length; i++)
            {
                mPointInfos[i] = new PointInfo();
                mPointInfos[i].m_bone = m_points[i];
            }

            // cache these to avoid per-frame allocations.
            if (localPlayerAuthority)
            {
                m_LocalTransformWriter = new NetworkWriter();
            }
            numPoints = m_points.Length;

            NetworkServer.RegisterHandler(PointTransformMessage, HandlePoints);
        }

        public override bool OnSerialize(NetworkWriter writer, bool initialState)
        {
            if (initialState)
            {
                // dont send in initial data. size is likely too large for default channel
                return true;
            }

            if (syncVarDirtyBits == 0)
            {
                writer.WritePackedUInt32(0);
                return false;
            }

            // dirty bits
            writer.WritePackedUInt32(1);

            SerializeModeTransform(writer);
            return true;
        }

        void SerializeModeTransform(NetworkWriter writer)
        {
            int start = writer.Position;
            foreach (var bone in m_points)
            {
                // position
                writer.Write(bone.position);

                // rotation
                if (m_SyncRotationAxis != NetworkTransform.AxisSyncMode.None)
                {
                    NetworkTransform.SerializeRotation3D(writer, bone.rotation, syncRotationAxis, rotationSyncCompression);
                }
            }

            int sz = writer.Position - start;
            if (sz > 1400 && binarySize == 0)
            {
                // this is only generated once.
                Debug.LogWarning("NetworkSkeleton binary serialization size is very large:" + sz + ". Consider reducing the number of levels being synchronized.");
            }
            binarySize = sz;
        }


        public override void OnDeserialize(NetworkReader reader, bool initialState)
        {
            if (initialState)
                return;

            if (isServer && NetworkServer.localClientActive)
                return;

            if (reader.ReadPackedUInt32() == 0)
                return;

            UnserializeModeTransform(reader, initialState);

            m_LastClientSyncTime = Time.time;
        }

        void UnserializeModeTransform(NetworkReader reader, bool initialState)
        {
            if (hasAuthority)
            {
                // this component must read the data that the server wrote, even if it ignores it.
                // otherwise the NetworkReader stream will still contain that data for the next component.

                for (int i = 0; i < m_points.Length; i++)
                {
                    // position
                    reader.ReadVector3();

                    if (syncRotationAxis != NetworkTransform.AxisSyncMode.None)
                    {
                        NetworkTransform.UnserializeRotation3D(reader, syncRotationAxis, rotationSyncCompression);
                    }
                }
                return;
            }

            for (int i = 0; i < m_points.Length; i++)
            {
                var pointInfo = mPointInfos[i];

                // position
                pointInfo.m_TargetSyncPosition = reader.ReadVector3();

                // rotation
                if (syncRotationAxis != NetworkTransform.AxisSyncMode.None)
                {
                    var rot = NetworkTransform.UnserializeRotation3D(reader, syncRotationAxis, rotationSyncCompression);
                    pointInfo.m_TargetSyncRotation3D = rot;
                }
            }
        }

        void FixedUpdate()
        {
            if (isServer)
            {
                FixedUpdateServer();
            }
            if (isClient)
            {
                FixedUpdateClient();
            }
        }

        void FixedUpdateServer()
        {
            if (syncVarDirtyBits != 0)
                return;

            // dont run if network isn't active
            if (!NetworkServer.active)
                return;

            // dont run if we haven't been spawned yet
            if (!isServer)
                return;

            // dont' auto-dirty if no send interval
            if (GetNetworkSendInterval() == 0)
                return;

            // This will cause transform to be sent
             SetDirtyBit(1);

            //Debug.Log("server");


        }

        void FixedUpdateClient()
        {
            // dont run if we haven't received any sync data
            if (m_LastClientSyncTime == 0)
                return;

            // dont run if network isn't active
            if (!NetworkServer.active && !NetworkClient.active)
                return;

            // dont run if we haven't been spawned yet
            if (!isServer && !isClient)
                return;

            // dont run if not expecting continuous updates
            if (GetNetworkSendInterval() == 0)
                return;

            // dont run this if this client has authority over this player object
            if (hasAuthority)
                return;



                // interpolate on client
                for (int i = 0; i < m_points.Length; i++)
            {
                var point = m_points[i];
                var pointinfo = mPointInfos[i];

                point.position = Vector3.Lerp(point.position, pointinfo.m_TargetSyncPosition, m_InterpolateMovement);
                point.rotation = Quaternion.Slerp(point.rotation, pointinfo.m_TargetSyncRotation3D, m_InterpolateRotation);
            }

     
        }


        // --------------------- local transform sync  ------------------------

        void Update()
        {
            if (!hasAuthority)
                return;

            if (!localPlayerAuthority)
                return;

            if (NetworkServer.active)
                return;

            if (Time.time - m_LastClientSendTime > GetNetworkSendInterval())
            {
                SendTransform();
                m_LastClientSendTime = Time.time;
            }
        }

        bool HasMoved()
        {
            //TODO - idle animation make this useless?
            return true;
        }

        [Client]
        void SendTransform()
        {
            if (!HasMoved() || ClientScene.readyConnection == null)
            {
                return;
            }

            m_LocalTransformWriter.StartMessage(PointTransformMessage);
            m_LocalTransformWriter.Write(netId);
            SerializeModeTransform(m_LocalTransformWriter);

            m_LocalTransformWriter.FinishMessage();

            ClientScene.readyConnection.SendWriter(m_LocalTransformWriter, GetNetworkChannel());
        }

        static internal void HandlePoints(NetworkMessage netMsg)
        {
            NetworkInstanceId netId = netMsg.reader.ReadNetworkId();

            GameObject foundObj = NetworkServer.FindLocalObject(netId);
            if (foundObj == null)
            {
                if (LogFilter.logError) { Debug.LogError("NetworPoints no gameObject"); }
                return;
            }

            VerticeSyncUnet sk = foundObj.GetComponent<VerticeSyncUnet>();
            if (sk == null)
            {
                if (LogFilter.logError) { Debug.LogError("NetworkSkeleton null target"); }
                return;
            }

            if (!netMsg.conn.clientOwnedObjects.Contains(netId))
            {
                if (LogFilter.logWarn) { Debug.LogWarning("NetworkSkeleton netId:" + netId + " is not for a valid player"); }
                return;
            }

            sk.UnserializeModeTransform(netMsg.reader, false);
            sk.m_LastClientSyncTime = Time.time;
        }

        public override int GetNetworkChannel()
        {
            return m_NetworkChannel;
        }
        public override float GetNetworkSendInterval()
        {
            return m_SendInterval;
        }
    }
}