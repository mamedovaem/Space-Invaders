using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    // Start is called before the first frame update
    public BaseBlock BlockPrefab;
    private BaseBlock[,] _blocks;
    private const int BASE_LENGTH = 4;
    private const int BASE_WIDTH = 4;

    void Start()
    {
        _blocks = new BaseBlock[BASE_LENGTH, BASE_WIDTH];
        for (int i = 0; i < BASE_LENGTH; i++)
        {
            for(int j = 0; j < BASE_WIDTH; j++)
            {
                _blocks[i, j] = Instantiate(BlockPrefab);
                _blocks[i, j].transform.parent = this.transform;
                _blocks[i, j].transform.localPosition = new Vector3(0.5F * i, 0.2F * j, -1);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
