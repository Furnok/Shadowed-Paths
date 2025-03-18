using System;
using UnityEngine;
using System.IO;
using System.Security.Cryptography;
using System.Text;

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
        private static readonly bool FileCrypted = true;
		private static readonly bool HaveSettings = true;

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
            if (!Directory.Exists(SaveDirectory))
            {
                Directory.CreateDirectory(SaveDirectory);
            }

			if (HaveSettings)
			{
				string name = saveSettingsName;

				if (FileAlreadyExist(name))
				{
					LoadFromJson(name, true);
				}
				else
				{
					SaveToJson(name, true);
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
            string filePath = GetFilePath(name);

            return File.Exists(filePath);
        }

        private void SaveToJson(string name, bool isSettings)
        {
            string filePath = GetFilePath(name);

            string decryptedInfoData = "";

            if (isSettings && HaveSettings)
            {
                decryptedInfoData = JsonUtility.ToJson(rsoSettingsSaved.Value);
            }
            else
            {
                decryptedInfoData = JsonUtility.ToJson(rsoContentSaved.Value);
            }


            if (FileCrypted)
            {
                string encryptedJson = Encrypt(decryptedInfoData);
                File.WriteAllText(filePath, encryptedJson);
            }
            else
            {
                File.WriteAllText(filePath, decryptedInfoData);
            }

        }

        private void LoadFromJson(string name, bool isSettings)
        {
            if (FileAlreadyExist(name))
            {
                string filePath = GetFilePath(name);

                string encryptedJson = File.ReadAllText(filePath);

                if (FileCrypted)
                {
                    string decryptedInfoData = Decrypt(encryptedJson);

                    if (isSettings && HaveSettings)
                    {
                        rsoSettingsSaved.Value = JsonUtility.FromJson<SettingsSaved>(decryptedInfoData);
                    }
                    else
                    {
                        rsoContentSaved.Value = JsonUtility.FromJson<ContentSaved>(decryptedInfoData);
                    }
                }
                else
                {
                    if (isSettings)
                    {
                        rsoSettingsSaved.Value = JsonUtility.FromJson<SettingsSaved>(encryptedJson);
                    }
                    else
                    {
                        rsoContentSaved.Value = JsonUtility.FromJson<ContentSaved>(encryptedJson);
                    }
                }
            }
        }

        private void ClearContent(string name)
        {
            if (FileAlreadyExist(name))
            {
                string filePath = GetFilePath(name);

                File.Delete(filePath);
            }
        }    
    }   
}
