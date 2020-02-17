using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.SqliteClient;
using UnityEngine.Networking;
using System;
using System.IO;
using System.Data;



public class database : MonoBehaviour
{
    public static int damage;
    public static int Hp;
    public static int jumpcnt;
    public static int gold;
    private void Awake()
    {
        StartCoroutine(DBCreate());
    }

    private void Start()
    {
        DBConnectionCheck();
        DataBaseRead("Select * From userinfo");
    }

    IEnumerator DBCreate()
    {
        string filepath = string.Empty;
        if(Application.platform == RuntimePlatform.Android) //안드로이드
        {
            filepath = Application.persistentDataPath + "/livegameDB.db";
            if(!File.Exists(filepath))
            {
                UnityWebRequest unityWebRequest = UnityWebRequest.Get("jar:file://" + Application.dataPath + "!/assets/livegameDB.db");
                unityWebRequest.downloadedBytes.ToString();
                yield return unityWebRequest.SendWebRequest().isDone;
                File.WriteAllBytes(filepath, unityWebRequest.downloadHandler.data);

            }
        }
        else //기타 플랫폼(PC..)
        {
            filepath = Application.dataPath + "/livegameDB.db";
            if(!File.Exists(filepath))
            {
                File.Copy(Application.streamingAssetsPath + "/livegameDB.db", filepath);
            }
        }
    }

    public string GetDBFilePath() //파일 경로 얻기
    {
        string str = string.Empty;
        if(Application.platform == RuntimePlatform.Android)
        {
            str = "URI=file:" + Application.persistentDataPath + "/livegameDB.db";
        }
        else
        {
            str = "URI=file:" + Application.dataPath + "/livegameDB.db";
        }
        return str;
    }

    public void DBConnectionCheck() //db 연결확인
    {
        try
        {
            IDbConnection dbConnection = new SqliteConnection(GetDBFilePath());
            dbConnection.Open();

            if(dbConnection.State == ConnectionState.Open)
            {
                Debug.Log("연결 성공");
            }
            else
            {
                Debug.Log("연결 실패");
            }
        }
        catch(Exception e)
        {
            Debug.Log("에러");
        }
    }

    public void DataBaseRead(string query)
    {
        IDbConnection dbConnection = new SqliteConnection(GetDBFilePath());
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = query;
        IDataReader dataReader = dbCommand.ExecuteReader();
        while(dataReader.Read())
        {
            // 첫씬에서 DB값을 읽어와 공격력, 체력, 대쉬가능 횟수, 골드를 읽어옴
            damage = dataReader.GetInt32(0);
            Hp = dataReader.GetInt32(1);
            jumpcnt = dataReader.GetInt32(2);
            gold = dataReader.GetInt32(3);            
        }
        dataReader.Dispose(); //생성순서와 반대로 닫음
        dataReader = null;
        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;
    }

    public void DatabaseUpdate(string column, string value)
    {
        string query = "Update userinfo Set "+column+ "=" +value;
        Debug.Log(query);
        IDbConnection dbConnection = new SqliteConnection(GetDBFilePath());
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();

        dbCommand.CommandText = query;
        dbCommand.ExecuteNonQuery();
        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;
    }
}
