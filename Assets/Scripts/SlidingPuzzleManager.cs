using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingPuzzleManager : MonoBehaviour
{
    //reference game board
    [SerializeField] private Transform gameTransform;
    //reference game piece
    [SerializeField] private Transform piecePrefab;

    //the last piece needs to be empty/non present for the puzzle, record it here
    private int emptyLocation;

    //size x size grid of pieces
    private int size;

    //creating game setup with size x size pieces
    private void CreateGamePieces(float gapThickness)
    {
        //width of each tile
        float width = 1/(float)size; //so each piece will be one third of a unit
            
        //to iterate over the size x size 2D array we create a grid
        for (int row = 0; row < size; row++) //go through each row
        {
            for (int col = 0; col < size; col++) //go through each column
            {
                //create the pieces in center position
                Transform piece = Instantiate(piecePrefab, gameTransform);
                //place the piece with center being the anchor
                piece.localPosition = new Vector3(-1 + (2 * width * col) + width, +1 - (2 * width * row) - width, 0);
                //scale the piece, substracting the gap to have space between pieces
                piece.localScale = ((2 *width) - gapThickness) * Vector3.one;
                //name the piece so we can check if theyre all aligned later
                piece.name = $"{(row * size) + col}";
                //empty space bottom right
                if ((row == size - 1) && (col == size - 1))
                {
                    emptyLocation = (size * size) - 1;
                    piece.gameObject.SetActive(false);
                }
            }

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //size x size grid
        size = 3;
        //CreateGamePieces(0.01f); //the parameter is thickness between pieces
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
