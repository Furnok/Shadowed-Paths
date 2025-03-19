using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDictionary<TKey, TValue> : ISerializationCallbackReceiver
{
    [SerializeField] private List<TKey> keys = new();
    [SerializeField] private List<TValue> values = new();

    private readonly Dictionary<TKey, TValue> dictionary = new();

    public Dictionary<TKey, TValue> Dictionary => dictionary;

    // Called before Saving (Convert Dictionary to Lists)
    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();

        foreach (var kvp in dictionary)
        {
            keys.Add(kvp.Key);
            values.Add(kvp.Value);
        }
    }

    // Called after Loading (Convert Lists back to Dictionary)
    public void OnAfterDeserialize()
    {
        dictionary.Clear();

        for (int i = 0; i < Mathf.Min(keys.Count, values.Count); i++)
        {
            if (!dictionary.ContainsKey(keys[i]))
            {
                dictionary.Add(keys[i], values[i]);
            } 
        }
    }
}
