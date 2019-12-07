using UnityEngine;

namespace myGame
{
    public class Move : MonoBehaviour
    {

        private float moveSpeed = 20;
        private int movingState;
        private Vector3 middle;
        private Vector3 target;

        void Update()
        {
            if (movingState == 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, middle, moveSpeed * Time.deltaTime);
                if (transform.position == middle)
                {
                    movingState = 2;
                }
            }
            else if (movingState == 2)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
                if (transform.position == target)
                {
                    movingState = 0;
                }
            }
        }
        public void setDestination(Vector3 t)
        {
            target = t;
            middle = t;
            if (t.y == transform.position.y)
            {
                movingState = 2;
            }
            else if (t.y < transform.position.y)
            {
                middle.y = transform.position.y;
            }
            else
            {
                middle.x = transform.position.x;
            }
            movingState = 1;
        }
    }
}

