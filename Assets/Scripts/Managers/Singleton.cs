using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feeljoon.FightingGame
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();

                    if (instance == null)
                    {
                        Debug.Log("Type does not exist");
                    }
                }

                return instance;
            }
        }

        protected virtual void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}