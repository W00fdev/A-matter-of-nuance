using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "New Scroll", menuName = "Scroll Data", order = 52)]
    public class ScrollData : ScriptableObject
    {
        public string description;
        public Variant[] variants;
    }

    [Serializable]
    public struct Variant
    {
        public string tooltip;
        public Consequence consequence;
        public List<Value> values;
    }

    [Serializable]
    public struct Value
    {
        public ResourceWinType resourceType;
        public float deltaValue;
    }

    [Serializable]
    public struct Consequence
    {
        public int id;
        public string text;
        public bool hasToCreateVassal;
        public bool hasToLoseVassal;
    }
}