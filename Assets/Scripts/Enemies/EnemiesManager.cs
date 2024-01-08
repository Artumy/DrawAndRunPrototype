using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemies = new List<GameObject>();

    private List<Vector3> _newEnemiesPosition = new List<Vector3>(); 
    private void OnEnable()
    {
        DrawLine.OnMouseButtonEntered += AddEnemiesPosition;
        DrawLine.OnMouseButtonUped += MouseUp;
        Enemy.Deleted += DeleteEnemy;
    }
    
    private void OnDisable()
    {
        DrawLine.OnMouseButtonEntered -= AddEnemiesPosition;
        DrawLine.OnMouseButtonUped -= MouseUp;
        Enemy.Deleted -= DeleteEnemy;
    }

    private void AddEnemiesPosition(Vector3 pos)
    {
        _newEnemiesPosition.Add(pos);
    }

    private void MouseUp()
    {
        if (_enemies.Count <= _newEnemiesPosition.Count)
        {
            int coefficient = _newEnemiesPosition.Count / _enemies.Count;

            for (int i = 0, j = 0; i < _enemies.Count; i++, j += coefficient)
            {
                _enemies[i].transform.position = new Vector3(_newEnemiesPosition[j].x, 
                    _enemies[i].transform.position.y,
                    _newEnemiesPosition[j].y);
            }
        }
        else if (_newEnemiesPosition.Count != 0)
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                int coefficient = Mathf.Clamp(i, 0, _newEnemiesPosition.Count - 1);
                _enemies[i].transform.position = new Vector3(_newEnemiesPosition[coefficient].x, 
                    _enemies[coefficient].transform.position.y,
                    _newEnemiesPosition[coefficient].y);
            }
        }
        
        _newEnemiesPosition.Clear();
    }

    private void DeleteEnemy(GameObject splineForEnemy)
    {
        _enemies.Remove(splineForEnemy);
    }
    
}
