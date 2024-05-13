using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtheloSystem : MonoBehaviour
{
    public GameObject _sprite;
    public GameObject _cube;
    SpriteState[,] _FieldState = new SpriteState[X, Y];
    SpriteSystem[,] _FieldSpriteState = new SpriteSystem[X, Y];
    const int X = 8; const int Y = 8;
    void Start()
    {
        for (int x = 0; x < X; x++)
        {
            for (int y = 0; y < X; y++)
            {
                var sprite = Instantiate(_sprite, new Vector3(1 * x, 1 * y, 1), Quaternion.Euler(90, 0, 0));

                _FieldSpriteState[x, y] = sprite.GetComponent<SpriteSystem>();

                _FieldState[x, y] = SpriteState.None;
            }
        }
        _FieldState[3, 3] = SpriteState.Black;
        _FieldState[3, 4] = SpriteState.White;
        _FieldState[4, 3] = SpriteState.White;
        _FieldState[4, 4] = SpriteState.Black;

    }

    public enum SpriteState
    {
        None,
        White,
        Black,
    }
    private SpriteState playerTurn = SpriteState.Black;

    int cube_pos_x = 3;
    int cube_pos_y = 3;
    void Update()
    {
        var pos = _cube.transform.localPosition;

        if (Input.GetKeyDown(KeyCode.RightArrow)) _cube.transform.localPosition = new Vector3(pos.x + 1, pos.y, pos.z);
        if (Input.GetKeyDown(KeyCode.LeftArrow)) _cube.transform.localPosition = new Vector3(pos.x - 1, pos.y, pos.z);
        if (Input.GetKeyDown(KeyCode.DownArrow)) _cube.transform.localPosition = new Vector3(pos.x, pos.y - 1, pos.z);
        if (Input.GetKeyDown(KeyCode.UpArrow)) _cube.transform.localPosition = new Vector3(pos.x, pos.y + 1, pos.z);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _FieldState[(int)_cube.transform.localPosition.x, (int)_cube.transform.localPosition.y] = playerTurn;
        }


        for (int x = 0; x < X; x++)
        {
            for (int y = 0; y < X; y++)
            {
                _FieldSpriteState[x, y].SetState(_FieldState[x, y]);
            }
        }
    }
}
