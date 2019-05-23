using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BuildingObject : MonoBehaviour
    {
        const float SCREEN_OFFSET = 0.5f;
       /* [HideInInspector]
        public ObstacleObject obstaclePoolObject;*/
        [HideInInspector]
        private Camera mainCamera;
        [SerializeField]
        private Vector2 startPosition;
        [HideInInspector]
        private Rigidbody2D rigibody2D;
        //     m_Rigidbody2D.MovePosition(m_Rigidbody2D.position + m_Velocity);
        /*    public void Awake()
            {
                rigibody2D = GetComponent<Rigidbody2D>();
                mainCamera = Object.FindObjectOfType<Camera>();
                //   mainCamera = FindObjectOfType<Camera>();
            }
            public void ReturnToPool()
            {
                obstaclePoolObject.ReturnToPool();
            }
            public void Move()
            {
            }
            void FixedUpdate()
            {
                Vector3 screenPoint = mainCamera.WorldToViewportPoint(transform.position);
                if (screenPoint.x < -SCREEN_OFFSET)
                {
                    obstaclePoolObject.ReturnToPool();
                    return;
                }
                rigibody2D.MovePosition(rigibody2D.position + new Vector2(-Game.Instance.Hero.SpeedBehaviour.Speed - 0.06f, 0));
            }
            public Vector2 StartPosition
            {
                get
                {
                    return startPosition;
                }
                set { startPosition = value; }
            }
            private void OnTriggerEnter2D(Collider2D collision)
            {
                if (collision.gameObject.tag == "Hero")
                {
                    Game.Instance.State = State.PreGameOver;
                }

            }   */
    }
}

