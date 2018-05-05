using ListWordsToLearn.Common.DB;
using LiteDBCommons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.Factories
{
    public static class RepositoryFactory 
    {
        public static IRepositoryDB<T> GetRepozytory<T>() where T : BaseModel
        {
            return new Repository<T>(Setting.ConnectionSrtingDB);
        }

        public static IRepositoryDB<T> GetRepozytory<T>(string nameCollection) where T : BaseModel
        {
            return new Repository<T>(Setting.ConnectionSrtingDB, nameCollection);
        }
    }
}

