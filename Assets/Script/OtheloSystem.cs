using OpenCover.Framework.Model;
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
                var sprite = Instantiate(_sprite, new Vector3(1 * x, 1 * y, 2), Quaternion.Euler(90, 0, 0));

                _FieldSpriteState[x, y] = sprite.GetComponent<SpriteSystem>();

                _FieldState[x, y] = SpriteState.None;
            }
        }
        _FieldState[3, 3] = SpriteState.Black;
        _FieldState[3, 4] = SpriteState.White;
        _FieldState[4, 3] = SpriteState.White;
        _FieldState[4, 4] = SpriteState.Black;

    }

    public enum SpriteState�@//��̏��
    {
        None,
        White,
        Black,
    }
    private SpriteState playerTurn = SpriteState.Black;

    int cube_pos_x = 4;
    int cube_pos_y = 4;
    private List<(int, int)> _InfoList = new List<(int, int)>();
    void Update()
    {
        var pos = _cube.transform.localPosition;

        if (Input.GetKeyDown(KeyCode.RightArrow) && cube_pos_x < 8)
        {
            _cube.transform.localPosition = new Vector3(pos.x + 1, pos.y, pos.z);
            cube_pos_x++;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && cube_pos_x > 1)
        {
            _cube.transform.localPosition = new Vector3(pos.x - 1, pos.y, pos.z);
            cube_pos_x--;

        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && cube_pos_y > 1)
        {
            _cube.transform.localPosition = new Vector3(pos.x, pos.y - 1, pos.z);
            cube_pos_y--;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && cube_pos_y < 8)
        {
            _cube.transform.localPosition = new Vector3(pos.x, pos.y + 1, pos.z);
            cube_pos_y++;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bool isPrant = false;
            for (int i = 0; i < 8; i++)
            {
                if (TurnCheck(i)) isPrant = true;
            }


            if (isPrant && 
                _FieldState[(int)_cube.transform.localPosition.x,(int) _cube.transform.localPosition.y] == SpriteState.None)
            {
                foreach (var info in _InfoList)
                {
                    var pos_x = info.Item1;
                    var pos_y = info.Item2;
                    _FieldState[pos_x, pos_y] = playerTurn;�@//���񂾑���̋�������̋�̐F�ɕς���
                }

                _FieldState[(int)_cube.transform.localPosition.x, (int)_cube.transform.localPosition.y] = playerTurn;

                playerTurn = playerTurn == SpriteState.Black ? SpriteState.White : SpriteState.Black;
                _InfoList = new List<(int, int)>(); //_InfoList��������
            }
        }


        for (int x = 0; x < X; x++)
        {
            for (int y = 0; y < X; y++)
            {
                _FieldSpriteState[x, y].SetState(_FieldState[x, y]);
            }
        }
    }

    private bool TurnCheck(int direction)
    {
        bool _turncheck = false;
        var pos_x = _cube.transform.localPosition.x;
        var pos_y = _cube.transform.localPosition.y;
        var yourTurn = playerTurn == SpriteState.Black ? SpriteState.White : SpriteState.Black; //����̃^�[���̐F

        var infoList = new List<(int, int)>(); //�����̋�ɋ��܂ꂽ����̋�̈ʒu���L�^���邽�߂̂��Ղ�

        while (0 <= pos_x && 7 >= pos_x && 0 <= pos_y && 7 >= pos_y) //�t�B�[���h��cube������΂����Ɖ����
        {
            //if (pos_x == 0)
            //{
            //    break;
            //}
            //pos_x--; //1��

            switch (direction)
            {
                case 0: //��
                    if (pos_x == 0) { return _turncheck = false; } // memo : return����ƁATurnCheck�֐����̂��I��������
                    pos_x--;
                    break;
                case 1: //�E
                    if (pos_x == 7) { return _turncheck = false; }
                    pos_x++;
                    break;
                case 2: //��
                    if (pos_y == 0) { return _turncheck = false; }
                    pos_y--;
                    break;
                case 3: //��
                    if (pos_y == 7) { return _turncheck = false; }
                    pos_y++;
                    break;
                case 4: //�E��
                    if (pos_x == 7) { return _turncheck = false; }
                    if (pos_y == 7) { return _turncheck = false; }
                    pos_x++;
                    pos_y++;
                    break;
                case 5: //�E��
                    if (pos_x == 7) { return _turncheck = false; }
                    if (pos_y == 0) { return _turncheck = false; }
                    pos_y--;
                    pos_x++;
                    break;
                case 6: //����
                    if (pos_x == 0) { return _turncheck = false; }
                    if (pos_y == 7) { return _turncheck = false; }
                    pos_y++;
                    pos_x--;
                    break;
                case 7: //����
                    if (pos_x == 0) { return _turncheck = false; }
                    if (pos_y == 0) { return _turncheck = false; }
                    pos_y--;
                    pos_x--;
                    break;
            }


            if (_FieldState[(int)pos_x, (int)pos_y] == yourTurn) //�߂��̂�����̋������
            {
                infoList.Add(((int)pos_x, (int)pos_y)); //��̈ʒu�����L�^���Ă���
            }

            if (infoList.Count == 0/*�����[�v�����ڂ̂Ƃ�*/&& _FieldState[(int)pos_x, (int)pos_y] == playerTurn ||
                _FieldState[(int)pos_x, (int)pos_y] == SpriteState.None) //�߂��̂������̋�A��������None��������false�Ԃ���break
            {
                _turncheck = false;
                break;
            }

            if (infoList.Count > 0 && _FieldState[(int)pos_x, (int)pos_y] == playerTurn)
            {
                _turncheck = true;
                foreach (var info in infoList)
                {
                    _InfoList.Add(info);
                }
                break;
            }

        }
        return _turncheck;
    }
}
