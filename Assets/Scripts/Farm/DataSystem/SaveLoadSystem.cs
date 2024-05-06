using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable] public class GameData{
    public string Name;
    public string CurrentLevelName;
    public PlayerData PlayerData;
    public List<PotData> PotDatas;
    public List<LandData> LandDatas;
}

public interface ISaveable
{
    SerializableGuid Id {get; set;}
}

public interface IBind<TData> where TData : ISaveable{
    SerializableGuid Id {get; set;}
    void Bind(TData data);
}


public class SaveLoadSystem : PersistentSingleton<SaveLoadSystem>
{

   [SerializeField] public GameData gameData;
   public Crops crops;
   public FarmerTerrain farmerTerrain;

    IDataService dataService;

    protected override void Awake()
    {
        base.Awake();
        dataService = new FileDataService(new JsonSerializer());
    }

    void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    

    void OnDisable()=> SceneManager.sceneLoaded -= OnSceneLoaded;
    

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Menu") return;
        LoadListData();
        Bind<Farmer,PlayerData>(gameData.PlayerData);
        Bind<Pot,PotData>(gameData.PotDatas);
        Bind<Land,LandData>(gameData.LandDatas);
    }

    void LoadListData()
    {
        if(gameData.PotDatas.Count>0)crops.loadPot(gameData.PotDatas);  
        else crops.newPot(new UnityEngine.Vector3(0,0,0),5);  

        if(gameData.LandDatas.Count>0)farmerTerrain.loadLand(gameData.LandDatas);  
        else farmerTerrain.newLand(new UnityEngine.Vector3(4,0,4),5);  
    }


    void Bind<T, TData>(TData data) where T : MonoBehaviour, IBind<TData> where TData : ISaveable, new() 
    {
        var entity = FindObjectsByType<T>(FindObjectsSortMode.None).FirstOrDefault();
        if (entity != null) {
            if (data == null) {
                data = new TData { Id = entity.Id };
            }
            entity.Bind(data);
        }
    }

    void Bind<T, TData>(List<TData> datas) where T: MonoBehaviour, IBind<TData> where TData : ISaveable, new() 
    {
        var entities = FindObjectsByType<T>(FindObjectsSortMode.None);
        
        foreach(var entity in entities) {
            var data = datas.FirstOrDefault(d=> d.Id == entity.Id);
            if (data == null) {
                data = new TData { Id = entity.Id };
                datas.Add(data); 
            }
            entity.Bind(data);
        }
    }

    public void NewGame()
    {
        gameData = new GameData{
            Name = "New Game",
            CurrentLevelName = "Demo",
            PlayerData = new PlayerData
            {
                PlayerPosition = new UnityEngine.Vector3(0,0,0),
                PlayerRotation = new UnityEngine.Quaternion(0,0,0,0),
                ToolEquipted = "Hand",
                HandItem = SerializableGuid.Empty
            },
            PotDatas = new List<PotData>(),
            LandDatas = new List<LandData>()

        };
        SceneManager.LoadScene(gameData.CurrentLevelName);
    }

    public void SaveGame()
    {
        dataService.Save(gameData);
    }

    public void LoadGame(string gameName)
    {
        gameData = dataService.Load(gameName);

        if (String.IsNullOrWhiteSpace(gameData.CurrentLevelName))
        {
            gameData.CurrentLevelName = "Demo";
        }

        SceneManager.LoadScene(gameData.CurrentLevelName);
    }

    public void DeleteGame(string gameName)
    {
        dataService.Delete(gameName);
    }

    public void ReloadGame()
    {
        LoadGame(gameData.Name);
    }
}

