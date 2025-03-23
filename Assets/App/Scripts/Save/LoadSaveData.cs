using System;
using UnityEngine;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class SaveConfig
{
    public static readonly int SaveMax = 5;
    public static bool saveActived = true;
    public static readonly bool HaveSettings = true;
    public static readonly bool FileCrypted = true;
}

namespace BT.Save
{
    public class LoadSaveData : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField, SaveName] private string saveSettingsName;

        [Header("Input")]
        [SerializeField] private RSE_LoadData rseLoad;
        [SerializeField] private RSE_SaveData rseSave;
        [SerializeField] private RSE_ClearData rseClear;

        [Header("Output")]
        [SerializeField] private RSO_SettingsSaved rsoSettingsSaved;
        [SerializeField] private RSO_ContentSaved rsoContentSaved;
        
        private static readonly string EncryptionKey = "ajekoBnPxI9jGbnYCOyvE9alNy9mM/Kw";
        private static readonly string SaveDirectory = Path.Combine(Directory.GetParent(Application.dataPath).FullName, "Saves");
        

        private void OnEnable()
        {
            rseSave.action += SaveToJson;
            rseLoad.action += LoadFromJson;
            rseClear.action += ClearContent;
        }

        private void OnDisable()
        {
            rseSave.action -= SaveToJson;
            rseLoad.action -= LoadFromJson;
            rseClear.action -= ClearContent;
        }

        private void Start()
        {
            if (SaveConfig.SaveMax <= 0 && !SaveConfig.HaveSettings)
            {
                SaveConfig.saveActived = false;
            }

            if (SaveConfig.saveActived)
            {
                if (!Directory.Exists(SaveDirectory))
                {
                    Directory.CreateDirectory(SaveDirectory);
                }

                if (SaveConfig.HaveSettings)
                {
                    if (FileAlreadyExist(saveSettingsName))
                    {
                        LoadFromJson(saveSettingsName, true);
                    }
                    else
                    {
                        SaveToJson(saveSettingsName, true);
                    }
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

        private void SaveToJson(string name, bool isSettings)
        {
            if (SaveConfig.saveActived)
            {
                string filePath = GetFilePath(name);

                string dataToSave = "";

                if (isSettings && SaveConfig.HaveSettings)
                {
                    dataToSave = JsonUtility.ToJson(rsoSettingsSaved.Value);
                }
                else
                {
                    if (SaveConfig.SaveMax > 0)
                    {
                        dataToSave = JsonUtility.ToJson(rsoContentSaved.Value);
                    }
                    else
                    {
                        dataToSave = JsonUtility.ToJson(rsoSettingsSaved.Value);
                    }
                }

                File.WriteAllText(filePath, SaveConfig.FileCrypted ? Encrypt(dataToSave) : dataToSave);
            }
        }

        private void LoadFromJson(string name, bool isSettings)
        {
            if (SaveConfig.saveActived)
            {
                if (!FileAlreadyExist(name)) return;

                string filePath = GetFilePath(name);
                string encryptedJson = File.ReadAllText(filePath);

                if (SaveConfig.FileCrypted)
                {
                    encryptedJson = Decrypt(encryptedJson);
                }

                if (isSettings && SaveConfig.HaveSettings)
                {
                    rsoSettingsSaved.Value = JsonUtility.FromJson<SettingsSaved>(encryptedJson);
                }
                else
                {
                    if (SaveConfig.SaveMax > 0)
                    {
                        rsoContentSaved.Value = JsonUtility.FromJson<ContentSaved>(encryptedJson);
                    }
                    else
                    {
                        rsoSettingsSaved.Value = JsonUtility.FromJson<SettingsSaved>(encryptedJson);
                    }
                }
            }
        }

        private void ClearContent(string name)
        {
            if (SaveConfig.saveActived)
            {
                if (FileAlreadyExist(name))
                {
                    string filePath = GetFilePath(name);

                    File.Delete(filePath);
                }
            }
        }    
    }   
}
