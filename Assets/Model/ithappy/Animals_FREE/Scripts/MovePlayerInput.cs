using UnityEngine;

namespace ithappy.Animals_FREE
{
    [RequireComponent(typeof(CreatureMover))]
    public class MovePlayerInput : MonoBehaviour
    {
        [Header("AI Wander Settings")]
        [SerializeField] private float changeDirectionTime = 3f;
        [SerializeField] private float idleChance = 0.3f;

        private CreatureMover m_Mover;

        private Vector2 m_Axis;
        private bool m_IsRun;
        private bool m_IsJump;

        private Vector3 m_Target;

        private float timer;

        private void Awake()
        {
            m_Mover = GetComponent<CreatureMover>();
        }

        private void Update()
        {
            GatherAI();
            SetInput();
        }

        // ================= AI LOGIC =================
        public void GatherAI()
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                // random nghỉ hoặc đi
                if (Random.value < idleChance)
                {
                    m_Axis = Vector2.zero; // đứng yên
                }
                else
                {
                    Vector2 randomDir = new Vector2(
                        Random.Range(-1f, 1f),
                        Random.Range(-1f, 1f)
                    ).normalized;

                    m_Axis = randomDir;
                }

                timer = changeDirectionTime + Random.Range(-1f, 1f);
            }

            // AI không chạy nhanh cố định
            m_IsRun = Random.value > 0.7f;

            // không nhảy random (có thể bật nếu muốn)
            m_IsJump = false;

            // target đơn giản = hướng nhìn
            m_Target = transform.position + new Vector3(m_Axis.x, 0, m_Axis.y);
        }

        public void SetInput()
        {
            if (m_Mover != null)
            {
                m_Mover.SetInput(in m_Axis, in m_Target, in m_IsRun, m_IsJump);
            }
        }
    }
}