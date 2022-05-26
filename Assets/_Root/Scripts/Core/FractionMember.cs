using System;
using System.Collections.Generic;
using System.Linq;
using _Root.Scripts.Abstractions;
using UnityEngine;

namespace _Root.Scripts.Core
{
    public class FractionMember : MonoBehaviour, IFractionMember
    {
        
        public int FractionID
        {
            get => _fractionID;
            set
            {
                _fractionID = value;
                Register();
            }
        }

        private static Dictionary<int, List<int>> _membersCount = new Dictionary<int, List<int>>();
        [SerializeField] private int _fractionID;
        public static int FractionCount
        {
            get
            {
                lock (_membersCount)
                {
                    return _membersCount.Keys.First();
                }
            }
        }

        public static int GetWinner()
        {
            lock (_membersCount)
            {
                return _membersCount.Keys.First();
            }
        }

        private void Awake()
        {
            if (FractionID != 0)
            {
                Register();
            }
        }

        private void OnDestroy()
        {
            Unregister();
        }

        private void Register()
        {
            lock (_membersCount)
            {
                if (!_membersCount.ContainsKey(FractionID))
                {
                    _membersCount.Add(FractionID, new List<int>());
                }

                if (!_membersCount[FractionID].Contains(GetInstanceID()))
                {
                    _membersCount[FractionID].Add(GetInstanceID());
                }
            }
        }

        private void Unregister()
        {
            lock (_membersCount)
            {
                if (_membersCount[FractionID].Contains(GetInstanceID()))
                {
                    _membersCount[FractionID].Remove(GetInstanceID());
                }

                if (_membersCount[FractionID].Count == 0)
                {
                    _membersCount.Remove(FractionID);
                }
            }
        }
    }
}