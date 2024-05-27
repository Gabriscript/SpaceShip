using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;


public enum ObstacleType
{
    Asteroid,

    FlyingSaucer
}
public class ObstacleManager : MonoBehaviour
{
    public GameObject ObstacleAsteroid;
    public int CurrentLevel;
    public GameObject explosionEffects;

    List<GameObject> obstacles = new List<GameObject>();

    public void Reset()
    {
        CurrentLevel = 0;
        while(obstacles.Count > 0)
        {
            GameObject obj = obstacles[0];
            Destroy(obj);
            obstacles.RemoveAt(0);
        }
    }
    public void NextLevel()
    {
        CurrentLevel++;
        for (int i = 0; i < CurrentLevel; i++)
            Spawn(ObstacleType.Asteroid);
    }
    Vector2 GetRandomPosition()
    {
        float orthoHeight = Camera.main.orthographicSize;
        float orthoWidth = Camera.main.orthographicSize * Camera.main.aspect;

        return new Vector3(Random.Range(-orthoWidth, orthoWidth),
                                         Random.Range(-orthoHeight, orthoHeight));
    }
    Vector2 GetRandomDirection()
    {
        return Random.insideUnitCircle.normalized;
    }
    void Spawn(ObstacleType type, Vector2? position = null, Vector2? direction = null,int fragmentdepth = 0)
    {

        switch (type)
        {
            case ObstacleType.Asteroid:
                GameObject newObstacle = Instantiate(ObstacleAsteroid);
                newObstacle.GetComponent<Asteroids>().OnDestruction += Despawn;      // NULL COALESCING OPERATOR ??
                newObstacle.GetComponent<Asteroids>().Init(position ?? GetRandomPosition(), direction ?? GetRandomDirection());
                newObstacle.GetComponent<Asteroids>().FragmentDepth = fragmentdepth;
                obstacles.Add(newObstacle);
                break;

            case ObstacleType.FlyingSaucer:
            default:
                throw new System.NotImplementedException();
        }


    }
    void Despawn(GameObject reference)
    {
        AudioManager.instance.PlayObstacles();
        Instantiate(explosionEffects, reference.transform.position, Quaternion.identity);
        if (reference.GetComponent<Asteroids>() != null)
        {
            int depth = reference.GetComponent<Asteroids>().FragmentDepth;
            if (depth < reference.GetComponent<Asteroids>().MaxFragments)
            {
                depth++;
                Vector2 position = reference.transform.position;
                Vector3 direction = reference.GetComponent<Obstacles>().direction;
                Vector3 leftSideDir = Vector3.Cross(direction, Vector3.forward);
                Vector3 rightSideDir = Vector3.Cross(direction, Vector3.back);

                leftSideDir = Quaternion.Euler(0, 0, Random.Range(-30, 30)) * leftSideDir;
                rightSideDir = Quaternion.Euler(0, 0, Random.Range(-30, 30)) * rightSideDir;

                Spawn(ObstacleType.Asteroid, position, leftSideDir, depth);
                Spawn(ObstacleType.Asteroid, position, rightSideDir, depth);

            }


        }
        
        reference.GetComponent<Obstacles>().OnDestruction -= Despawn;
        obstacles.Remove(reference);
        Destroy(reference);
        GameManager.instance.AddPoints(100);

        if(obstacles.Count == 0)
        {
            GameManager.instance.ChangeState(GameState.NextLevel);
        }
    }
   
}
