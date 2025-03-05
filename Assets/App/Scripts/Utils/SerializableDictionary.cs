using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDictionary<TKey, TValue> : ISerializationCallbackReceiver
{
    [SerializeField] private List<TKey> keys = new List<TKey>();
    [SerializeField] private List<TValue> values = new List<TValue>();

    private Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();

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
        dictionary = new Dictionary<TKey, TValue>();

        for (int i = 0; i < Math.Min(keys.Count, values.Count); i++)
        {
            if (!dictionary.ContainsKey(keys[i]))
            {
                dictionary.Add(keys[i], values[i]);
            }
        }
    }
}
