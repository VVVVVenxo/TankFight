using UnityEngine;

public class RandomHeight : MonoBehaviour
{
    public GameObject cube;  // 存放要随机生成的预制体
    public int maxCubes = 10;  // 最大生成物体的数量
    private float t1, t2;    // 控制生成间隔时间的变量
    private float x, z;      // 随机位置的坐标变量
    public float spawnInterval = 3f; // 生成物体的间隔时间

    void Start()
    {
        t1 = 0; // 游戏开始的时间
    }

    void Update()
    {
        t2 = Time.time; // 游戏进行到的时间
        if (t2 - t1 >= spawnInterval && GameObject.FindGameObjectsWithTag("HealthPowerUp").Length < maxCubes)
        {
            x = Random.Range(-35f, 35f); // 指定 x 轴方向上的范围
            z = Random.Range(-35f, 35f); // 指定 z 轴方向上的范围
            Instantiate(cube, new Vector3(x, 0.5f, z), Quaternion.identity); // 随机生成物体（预制体，生成的位置，方向）。
            t1 = t2; // 每生成一次，将 t1 设置为 t2
        }
    }
}
