using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class S_LoadSaveData : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, S_SaveName] private string saveSettingsName;
    [SerializeField, S_SaveName] private string saveAchievementsName;

    [Header("Input")]
    [SerializeField] private RSE_LoadData rseLoadData;
    [SerializeField] private RSE_LoadTempData rseLoadTempData;
    [SerializeField] private RSE_SaveData rseSaveData;
    [SerializeField] private RSE_DeleteData rseDeleteData;

    [Header("Output")]
    [SerializeField] private RSE_LoadAchievements rseLoadAchievements;
    [SerializeField] private RSO_SettingsSaved rsoSettingsSaved;
    [SerializeField] private SSO_Achievements ssoAchievements;
    [SerializeField] private RSO_Achievements rsoAchievements;
    [SerializeField] private RSO_AchievementsSave rsoAchievementsSave;
    [SerializeField] private RSO_ContentSaved rsoContentSaved;
    [SerializeField] private RSE_DataTemp rseDataTemp;
    [SerializeField] private RSE_DataUI rseDataUI;
    [SerializeField] private RSE_LoadSettings rseLoadSettings;

    private static readonly string EncryptionKey = "ajekoBnPxI9jGbnYCOyvE9alNy9mM/Kw";
    private static readonly string SaveDirectory = Path.Combine(Directory.GetParent(Application.dataPath).FullName, "Saves");
    private static readonly bool fileCrypted = false;

    private void Awake()
    {
        rsoSettingsSaved.Value = new();
        rsoContentSaved.Value = new();
        rsoAchievements.Value = new();
        rsoAchievementsSave.Value = new();
    }

    private void OnEnable()
    {
        rseSaveData.action += SaveToJson;
        rseLoadData.action += LoadFromJson;
        rseLoadTempData.action += LoadTempFromJson;
        rseDeleteData.action += DeleteData;
    }

    private void OnDisable()
    {
        rseSaveData.action -= SaveToJson;
        rseLoadData.action -= LoadFromJson;
        rseLoadTempData.action -= LoadTempFromJson;
        rseDeleteData.action -= DeleteData;

        rsoSettingsSaved.Value = null;
        rsoContentSaved.Value = null;
        rsoAchievements.Value = null;
        rsoAchievementsSave.Value = null;
    }

    private void Start()
    {
        Directory.CreateDirectory(SaveDirectory);

        if (saveSettingsName != null)
        {
            if (FileAlreadyExist(saveSettingsName))
            {
                LoadFromJson(saveSettingsName, true, false);
            }
            else
            {
                SaveToJson(saveSettingsName, true, false);
            }
        }

        if (saveAchievementsName != null)
        {
            if (FileAlreadyExist(saveAchievementsName))
            {
                LoadFromJson(saveAchievementsName, false, true);
            }
            else
            {
                rseLoadAchievements.Call();
            }
        }
    }

    private static string Encrypt(string plainText)
    {
        using Aes aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(EncryptionKey);
        aes.GenerateIV();

        using MemoryStream memoryStream = new();
        memoryStream.Write(aes.IV, 0, aes.IV.Length);

        using (CryptoStream cryptoStream = new(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
        using (StreamWriter writer = new(cryptoStream))
            writer.Write(plainText);

        return Convert.ToBase64String(memoryStream.ToArray());
    }

    private static string Decrypt(string cipherText)
    {
        byte[] buffer = Convert.FromBase64String(cipherText);

        using Aes aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(EncryptionKey);
        aes.IV = buffer[..(aes.BlockSize / 8)];

        using MemoryStream memoryStream = new(buffer[(aes.BlockSize / 8)..]);
        using CryptoStream cryptoStream = new(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read);
        using StreamReader reader = new(cryptoStream);

        return reader.ReadToEnd();
    }

    private string GetFilePath(string name)
    {
        return Path.Combine(SaveDirectory, $"{name}.json");
    }

    private bool FileAlreadyExist(string name)
    {
        return File.Exists(GetFilePath(name));
    }

    private void SaveToJson(string name, bool isSetting, bool isAchievement)
    {
        if (name == null) return;

        string filePath = GetFilePath(name);

        string dataToSave = "";

        if (isSetting)
        {
            dataToSave = JsonUtility.ToJson(rsoSettingsSaved.Value);

            StartCoroutine(S_Utils.Delay(0.1f, () => rseLoadSettings.Call()));
        }
        else if (isAchievement)
        {
            dataToSave = JsonUtility.ToJson(rsoAchievementsSave.Value);
        }
        else
        {
            dataToSave = JsonUtility.ToJson(rsoContentSaved.Value);
        }

        File.WriteAllText(filePath, fileCrypted ? Encrypt(dataToSave) : dataToSave);

        rseDataUI.Call(name);
    }

    private void LoadFromJson(string name, bool isSettings, bool isAchievement)
    {
        if (!FileAlreadyExist(name)) return;

        string filePath = GetFilePath(name);
        string encryptedJson = File.ReadAllText(filePath);

        if (fileCrypted)
        {
            encryptedJson = Decrypt(encryptedJson);
        }

        if (isSettings)
        {
            rsoSettingsSaved.Value = JsonUtility.FromJson<S_SettingsSaved>(encryptedJson);

            StartCoroutine(S_Utils.Delay(0.1f, () => rseLoadSettings.Call()));
        }
        else if (isAchievement)
        {
            rsoAchievementsSave.Value = JsonUtility.FromJson<S_AchievementsSaved>(encryptedJson);

            for (int i = 0; i < ssoAchievements.Value.Count; i++)
            {
                rsoAchievements.Value.Add(ssoAchievements.Value[i].Clone());
                rsoAchievements.Value[i].unlocked = rsoAchievementsSave.Value.listAchievements[i].unlocked;
            }
        }
        else
        {
            rsoContentSaved.Value = JsonUtility.FromJson<S_ContentSaved>(encryptedJson);
        }
    }

    private void LoadTempFromJson(string name)
    {
        if (!FileAlreadyExist(name)) return;

        string filePath = GetFilePath(name);
        string encryptedJson = File.ReadAllText(filePath);

        if (fileCrypted)
        {
            encryptedJson = Decrypt(encryptedJson);
        }

        rseDataTemp.Call(JsonUtility.FromJson<S_ContentSaved>(encryptedJson));
    }

    private void DeleteData(string name)
    {
        if (FileAlreadyExist(name))
        {
            string filePath = GetFilePath(name);

            File.Delete(filePath);

            rseDataUI.Call(name);
        }
    }
}
