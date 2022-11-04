using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SaveData
{
    public float transform_X;
    public float transform_Y;
    public float transform_Z;
    public bool active;
    public bool camera;
    public string sceneName;
    public List<SlotData> item_data;
}

public class SaveManager : MonoBehaviour
{
    private PlayerMove playerMove;
    private QuestInventory questInven;
    private CameraManager cameraManager;
    private Bed bed;

    public List<Slot> itemList;
    public SaveData saveData;

    private Vector3 vector;

    [SerializeField] private GameObject save_obj;
    private bool isSave = false;

    public void SaveActive()
    {
        isSave = !isSave;
        save_obj.SetActive(isSave);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            SaveActive();
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            IsSave();
        }

        if (Input.GetKeyDown(KeyCode.F11))
        {
            IsLoad();
        }
    }

    public void IsSave()
    {
        playerMove = FindObjectOfType<PlayerMove>();
        questInven = FindObjectOfType<QuestInventory>();

        saveData.transform_X = playerMove.transform.position.x;
        saveData.transform_Y = playerMove.transform.position.y;
        saveData.transform_Z = playerMove.transform.position.z;
        saveData.active = false;
        saveData.camera = true;


        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/saveFile.dat");

        bf.Serialize(file, saveData);
        file.Close();

        Debug.Log(Application.dataPath + "의 위치에 저장했습니다.");
    }

    public void IsLoad()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.dataPath + "/saveFile.dat", FileMode.Open);

        if(file != null && file.Length > 0)
        {
            saveData = (SaveData)bf.Deserialize(file);
        }

        playerMove = FindObjectOfType<PlayerMove>();
        questInven = FindObjectOfType<QuestInventory>();
        cameraManager = FindObjectOfType<CameraManager>();
        bed = FindObjectOfType<Bed>();

        cameraManager.isCamera = true;
        
        if (bed != null)
            bed.isActive = saveData.active;
        playerMove.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        playerMove.isMove = true;
        if (bed != null)
            bed.isBed = false;

        vector.Set(saveData.transform_X, saveData.transform_Y, saveData.transform_Z);
        playerMove.transform.position = vector;
        Debug.Log("플레이어 위치를 로드했습니다." + vector);


        file.Close();

    }
}
