using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Vector3 targetPosition;
    private bool isMoving = false;
    public GameObject flagPrefab;
    private GameObject currentFlag;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Action
    public enum CharacterAction { Idle, Walk, Mining, Fishing, Attacking }
    public CharacterAction currentAction = CharacterAction.Idle;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        targetPosition = transform.position;
    }

    void Update()
    {
        HandleMovement();

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPos.z = 0;
            targetPosition = clickPos;
            isMoving = true;

            // Tạm thời cho là "Walk", sau đó kiểm tra vật thể ở đó để đổi thành Mining/Fishing/...
            currentAction = CharacterAction.Walk;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPos.z = 0;
            targetPosition = clickPos;
            isMoving = true;

            currentAction = CharacterAction.Walk;

            // Spawn flag
            if (currentFlag != null)
                Destroy(currentFlag);

            currentFlag = Instantiate(flagPrefab, clickPos, Quaternion.identity);
        }
        if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
        {
            isMoving = false;

            if (currentFlag != null)
                Destroy(currentFlag);

          
}

    }

    void HandleMovement()
    {
        if (isMoving)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            Vector3 direction = targetPosition - transform.position;
            if (direction.x != 0)
                spriteRenderer.flipX = direction.x < 0;

            animator.Play("RunStrip");

            if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
            {
                isMoving = false;

                // Kiểm tra vật thể ở gần → đổi hành động
                GameObject nearest = FindNearbyObject();
                if (nearest != null)
                {
                    string tag = nearest.tag;

                    switch (tag)
                    {
                        case "Rock":
                            currentAction = CharacterAction.Mining;
                            animator.Play("Mining");
                            break;
                        case "Water":
                            currentAction = CharacterAction.Fishing;
                            animator.Play("WaitingFish");
                            break;
                        case "Enemy":
                            currentAction = CharacterAction.Attacking;
                            animator.Play("AttackStrip");
                            HealthEnemy enemyHealth = nearest.GetComponent<HealthEnemy>();
                            if (enemyHealth != null)
                            {
                                enemyHealth.TakeDamage(20); // damage tùy bạn chỉnh
                            }
                            break;
                         
                        default:
                            currentAction = CharacterAction.Idle;
                            animator.Play("Idle");
                            break;
                    }
                }
                else
                {
                    currentAction = CharacterAction.Idle;
                    animator.Play("Idle");
                }
            }
        }
    }

    GameObject FindNearbyObject()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Rock") || hit.CompareTag("Water") || hit.CompareTag("Enemy"))
                return hit.gameObject;
        }
        return null;
    }
}
