using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Sirenix.Serialization;
using Sirenix.OdinInspector;

public class PlayerController : SerializedMonoBehaviour
{
    #region Variable Decarations    
    #region //BASIC MOVEMENT
    private Vector3 originTile;
    private Vector3 destinationTile;
    private Vector3 newPosition;
    private Vector3 lastPosition;

    private Vector3 currentPositionOnGrid;
    private Vector3 desiredPositionOnGrid;

    private GameObject touchedObject;

    private Animator anim;
    Rigidbody2D rb;

    private float movementCooldown = 0.3f;
    private float moveTime = 0.3f;

    public static float playerMovementSpeed;

    [SerializeField]
    public static bool playerHasMoved = false;
    private bool movementIsCoolingDown = false;
    private bool blinkIsCoolingdown = false;
    #endregion

    #region //HIGHLIGHTING TILES & MOVE OBJECT ABILITY
    public Tile highlightTile;
    public Tilemap highlightTilemap;

    private Vector3 mousePosition;
    private Vector3Int previous;

    private Vector3 objectPosition;
    private Vector3 targetObjectPosition;
    private Vector3 facingDirection;

    private GameObject targetedObject;

    private Collider2D boxColl;

    public bool playerIsSelectingTile = false;

    #endregion

    #region //TOPPLE TREE ABIL
    public static bool rotateright = false;
    public static bool rotateleft = false;
    public static bool rotatedown = false;
    public static bool rotateup = false;
    #endregion

    #region //COLLISIONS

    public SpriteRenderer sr;
    private Color[] colors = { Color.yellow, Color.red };

    private bool hitEnemy = false;
    private bool hitTrap = false;
    private bool hitStaticEnvironment = false;

    #endregion

    #region //WORLD SWITCHING
    bool playerIsSpirit = false;
    bool playerIsReal = true;
    private bool playerIsSwtichingWorlds = false;
    #endregion
    #endregion

    #region Monobehavior Callbacks
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        boxColl = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ConvertPlayerMovementInputs();

        TileSwitch();

        //PlayerHit();

        SwitchWorlds();
    }

    private void FixedUpdate()
    {
        Vector3 playerLastPosition = Vector3.zero;
        playerMovementSpeed = (((transform.position - playerLastPosition).magnitude) / Time.deltaTime);
        playerLastPosition = transform.position;
    }

    // Use this for player abilities
    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerIsSwtichingWorlds = true;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            boxColl.enabled = false;
            playerHasMoved = false;
            playerIsSelectingTile = true;
        }
    }
    #endregion

    #region Player Functions

    #region BASIC MOVEMENT ON A GRID
    private void ConvertPlayerMovementInputs()
    {
        //Movement Overrides all other functions
        if (playerHasMoved || movementIsCoolingDown) return;

        int horizontal = 0;
        int vertical = 0;

        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");

        anim.SetFloat("Horizontal", horizontal);
        anim.SetFloat("Vertical", vertical);

        if (horizontal != 0) vertical = 0;

        if (horizontal != 0 || vertical != 0)
        {
            playerHasMoved = true;
            //microMovementCooldown(movementCooldown);
            CalculateMovement(horizontal, vertical);
        }
    }

    private void CalculateMovement(int xDirection, int yDirection)
    {
        currentPositionOnGrid = highlightTilemap.WorldToCell(transform.position);

        desiredPositionOnGrid = highlightTilemap.WorldToCell(new Vector3(currentPositionOnGrid.x + xDirection, currentPositionOnGrid.y + yDirection, 0));

        desiredPositionOnGrid = new Vector3(desiredPositionOnGrid.x + 0.5f, desiredPositionOnGrid.y + 0.75f, 0);
        
        StartCoroutine(MoveToCell(desiredPositionOnGrid));
    }

    private IEnumerator MoveToCell(Vector3 destinationPosition)
    {
        float sqrRemainingDistanceToDestination = (transform.position - destinationPosition).sqrMagnitude;
        float inverseMoveTime = 1 / moveTime;

        while (sqrRemainingDistanceToDestination > float.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, destinationPosition, inverseMoveTime * Time.unscaledDeltaTime);
            sqrRemainingDistanceToDestination = (transform.position - destinationPosition).sqrMagnitude;

            yield return null;
        }

        playerHasMoved = false;
    }

    private IEnumerator microMovementCooldown(float cooldown)
    {
        movementIsCoolingDown = true;
        while (cooldown > 0f)
        {
            cooldown -= Time.unscaledDeltaTime;
            yield return null;
        }

        movementIsCoolingDown = false;
    }
    #endregion

    #region TILE HIGHLIGHT AND MOVE

    private void TileSwitch()
    {
        //Get mousePosition and floor it to align with grid            
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = ExtensionMethods.getFlooredWorldPosition(mousePosition);

        //Get mousePosition in Grivd coord
        Vector3Int currentMousePositionInGrid = highlightTilemap.WorldToCell(mousePosition);

        ////Debug.Log("My mouse grid position is " + currentMousePositionInGrid);

        if (playerIsSelectingTile == true)
        {

            // if the position has changed
            if (currentMousePositionInGrid != previous)
            {
                // set the new tile
                highlightTilemap.SetTile(currentMousePositionInGrid, highlightTile);

                // erase previous
                highlightTilemap.SetTile(previous, null);

                // save the new position for next frame
                previous = currentMousePositionInGrid;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            playerIsSelectingTile = false;

            RaycastHit2D mouseCollisionRay = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.zero);

            if (mouseCollisionRay.collider != null)
            {
                targetedObject = mouseCollisionRay.collider.gameObject;
                //Debug.Log("I have hit an object called " + targetedObject.name);

                targetedObject.AddComponent<MoveObject>();
            }

            if (playerIsSelectingTile == false)
            {
                highlightTilemap.SetTile(currentMousePositionInGrid, null);
            }
        }
    }

    #endregion

    #region RAYCAST CHECK COLLISION
    #endregion

    #region COLLISIONS

    void OnTriggerEnter2D(Collider2D other)
    {
        #region HitCalculations

        if (other.tag == "Enemy")
        {
            hitEnemy = true;
            //Debug.Log("I touched the " + touchedObject);
        }

        if (other.tag == "Trap")
        {
            hitTrap = true;
        }

        if (other.tag == "StaticEnv")
        {
            hitStaticEnvironment = true;
        }

        #endregion

        #region Topple Tree Ability
        if (other.tag == "CollumColliderW")
        {
            rotateright = true;
            rotateleft = false;
            rotateup = false;
            rotatedown = false;
            //Debug.Log("colliding");
        }

        else if (other.tag == "CollumColliderE")
        {
            rotateleft = true;
            rotateright = false;
            rotateup = false;
            rotatedown = false;
            //Debug.Log("colliding");
        }

        else if (other.tag == "CollumColliderN")
        {
            rotatedown = true;
            rotateleft = false;
            rotateright = false;
            rotateup = false;
            //Debug.Log("colliding");
        }

        else if (other.tag == "CollumColliderS")
        {
            rotateup = true;
            rotateleft = false;
            rotateright = false;
            rotatedown = false;
            //Debug.Log("colliding");
        }

        #endregion
    }

    private void PlayerHit()
    {
        sr.color = Color.Lerp(sr.color, Color.white, Time.deltaTime / 0.5f); //FLASH - lerps from white to red over 1 second

        if (hitEnemy == true)
        {
            //Debug.Log("I have hit an enemy");
            sr.color = new Color(2, 0, 0, 255);
            hitEnemy = false;
        }

        if (hitTrap == true)
        {
            //Debug.Log("I have hit a trap");
            sr.color = new Color(2, 0, 0, 255);
            hitTrap = false;
        }

        if (hitStaticEnvironment == true)
        {
            //Debug.Log("I have hit the environment");
            sr.color = new Color(2, 0, 0, 255);
            hitStaticEnvironment = false;
        }

    }

    private void SwitchWorlds()
    {
        //if(playerCanSwitch)
        if (playerIsSwtichingWorlds == true && playerIsReal == true)
        {
            transform.position = new Vector3(transform.position.x + 92, transform.position.y);
            playerIsSwtichingWorlds = false;
            playerIsReal = false;
            playerIsSpirit = true;
        }

        else if (playerIsSwtichingWorlds == true && playerIsSpirit == true)
        {
            transform.position = new Vector3(transform.position.x - 92, transform.position.y);
            playerIsSwtichingWorlds = false;
            playerIsSpirit = false;
            playerIsReal = true;
        }
    }
    #endregion
    #endregion
}