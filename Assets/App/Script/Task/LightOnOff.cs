using UnityEngine;

public class LightOnOff : MonoBehaviour
{
    [SerializeField] private Light light; // Varibale untuk mnyimpan component light yang akan dihidup/matikan
    [SerializeField] private bool isOn = false; // Untuk mengecek apakah lampu sedang manyala atau tidak

    void Start()
    {
        light = GetComponent<Light>(); // Menyimpan component Light
    }

    public void Toggle() // switch fuction
    {
        isOn = !isOn; // mengubah value varible sesuai kondisi terakhir
        light.enabled = isOn; // jika sedang hidup maka akan dimatikan begitu pula sebaliknya
    }
}
