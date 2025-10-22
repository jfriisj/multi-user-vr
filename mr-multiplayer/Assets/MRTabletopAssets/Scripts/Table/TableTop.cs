using System;

namespace UnityEngine.XR.Templates.MRTTabletopAssets
{
    public class TableTop : MonoBehaviour
    {
        public static int k_CurrentSeat = -1;

        [SerializeField]
        TableSeat[] m_Seats;
        public TableSeat[] seats => m_Seats;

        [SerializeField]
        float m_SeatDistance = 0.75f;
        public float seatDistance => m_SeatDistance;

        [SerializeField]
        float m_SeatOffset;
        public float seatOffset
        {
            get => m_SeatOffset;
            set => m_SeatOffset = value;
        }

        public Transform GetSeat(int seatIdx)
        {
            if (m_Seats == null || m_Seats.Length == 0)
                return null;
                
            if (seatIdx <= -1)
                return m_Seats[0].seatTransform;

            if (seatIdx >= m_Seats.Length)
                return null;

            return m_Seats[seatIdx].seatTransform;
        }

        void OnValidate()
        {
            if (m_Seats == null)
                return;
                
            foreach (TableSeat seat in m_Seats)
            {
                if (seat.seatTransform != null)
                {
                    seat.seatTransform.localPosition = -seat.seatTransform.forward * m_SeatDistance;
                }
            }
        }
    }

    [Serializable]
    public struct TableSeat
    {
        public Transform seatTransform;
        public int seatID;
    }
}
