using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingPuzzleManager : MonoBehaviour
{
    //reference game board
    [SerializeField] private Transform gameTransform;
    //reference game piece
    [SerializeField] private Transform piecePrefab;

    //keeping track of where they are to track them around
    private List<Transform> pieces;

    //the last piece needs to be empty/non present for the puzzle, record it here
    private int emptyLocation;

    //size x size grid of pieces
    private int size;

    //creating game setup with size x size pieces
    private void CreateGamePieces(float gapThickness)
    {
        //width of each tile
        float width = 1 / (float)size; //so each piece will be one third of a unit

        //to iterate over the size x size 2D array we create a grid
        for (int row = 0; row < size; row++) //go through each row
        {
            for (int col = 0; col < size; col++) //go through each column
            {
                //create the pieces in center position
                Transform piece = Instantiate(piecePrefab, gameTransform);
                //add to the list to identify what piece we clicked
                pieces.Add(piece);
                //place the piece with center being the anchor
                piece.localPosition = new Vector3(-1 + (2 * width * col) + width, +1 - (2 * width * row) - width, 0);
                //scale the piece, substracting the gap to have space between pieces
                piece.localScale = ((2 * width) - gapThickness) * Vector3.one;
                //name the piece so we can check if theyre all aligned later
                piece.name = $"{(row * size) + col}";
                //empty space bottom right
                if ((row == size - 1) && (col == size - 1))
                {
                    emptyLocation = (size * size) - 1;
                    piece.gameObject.SetActive(false);
                }
                else
                {
                    //map the uv coords appropriately from 0 to 1
                    float gap = gapThickness / 2;
                    Mesh mesh = piece.GetComponent<MeshFilter>().mesh;
                    Vector2[] uv = new Vector2[4];
                    // UV coord order: (0, 1), (1, 1), (0, 0), (1, 0)
                    uv[0] = new Vector2((width * col) + gap, 1 - ((width * (row + 1)) - gap));
                    uv[1] = new Vector2((width * (col + 1)) - gap, 1 - ((width * (row + 1)) - gap));
                    uv[2] = new Vector2((width * col) + gap, 1 - ((width * row) + gap));
                    uv[3] = new Vector2((width * (col + 1)) - gap, 1 - ((width * row) + gap));
                    // Assign our new UVs to the mesh.
                    mesh.uv = uv;
                }
            }

        }
    }

    private bool SwapIfValid(int i, int offset, int colCheck)
    {
        //when on rightmost column we dont wanna look left and vice versa
        //because on the screen there would be the edge of the puzzle
        //but in the list it would be the leftmost one up a row
        if (((i % size) != colCheck) && ((i + offset) == emptyLocation))
        {
            //valid, swap pieces in game state
            (pieces[i], pieces[i + offset]) = (pieces[i + offset], pieces[i]);
            //swap transforms
            (pieces[i].localPosition, pieces[i + offset].localPosition) = (pieces[i + offset].localPosition, pieces[i].localPosition);
            //update empty location
            emptyLocation = i;
            return true;
        }
        return false;
    }
    // Start is called before the first frame update
    private void Shuffle()
    {
        int count = 0;
        int last = 0;
        int inversionsLeft = (100); //it needs to be even to be solvable
        while (inversionsLeft != 0)
        {
            // Pick a random location.
            int rnd = Random.Range(0, size * size);
            // Only thing we forbid is undoing the last move.
            if (rnd == last) { continue; }
            last = emptyLocation;
            // Try surrounding spaces looking for valid move.
            if (SwapIfValid(rnd, -size, size))
            {
                count++;
            }
            else if (SwapIfValid(rnd, +size, size))
            {
                count++;
            }
            else if (SwapIfValid(rnd, -1, 0))
            {
                count++;
            }
            else if (SwapIfValid(rnd, +1, size - 1))
            {
                count++;
            }
            inversionsLeft--;
        }
    }
    // We name the pieces in order so we can use this to check completion.
    private bool CheckCompletion()
    {
        for (int i = 0; i < pieces.Count; i++)
        {
            if (pieces[i].name != $"{i}")
            {
                return false;
            }
        }
        return true;
    }
    void Start()
    {
        //init. list
        pieces = new List<Transform>();
        //size x size grid
        size = 3;
        CreateGamePieces(0.01f); //the parameter is thickness between pieces
        //shuffle
        Shuffle();
    }

    // Update is called once per frame
    void Update()
    {
        //check
        if (CheckCompletion())
        {
            Debug.Log("DONE");
        }
        //time to detect mouse clicks!
        //on click send out ray to see if we clicked a piece
        if (Input.GetMouseButtonDown(0)) //zero indicates left mouse click
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                //Debug.Log("TRUE");
                //iterate over array to see which one we hit
                for (int i = 0; i < pieces.Count; i++)
                {
                    if (pieces[i] == hit.transform)
                    {
                        //check every direction around it to see if valid move
                        //break out on success if not keep trying
                        if (SwapIfValid(i, -size, size)) { break; }
                        if (SwapIfValid(i, +size, size)) { break; }
                        if (SwapIfValid(i, -1, 0)) { break; }
                        if (SwapIfValid(i, +1, size - 1)) { break; }
                    }
                }

            }

        }
    }
}
