using System;
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
    }

    [Serializable]
    public struct Consequence
    {
        public int id;
        public string text;
    }
}