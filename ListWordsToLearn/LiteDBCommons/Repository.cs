using ListWordsToLearn.Common.DB;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LiteDBCommons
{
    public class Repository<T> : IRepositoryDB<T> where T : BaseModel
    {
        private LiteCollection<T> tab;
        private LiteDatabase db;

        public Repository(string pathBd)
        {
            db = new LiteDatabase(pathBd);
            tab = db.GetCollection<T>();
        }

        public Repository(string pathBd, string nameCollection)
        {
            db = new LiteDatabase(pathBd);
            tab = db.GetCollection<T>(nameCollection);
        }

        public bool DropCollection(string name)
        {
            try
            {
                db.DropCollection(name);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<T> GetAll()
        {
            return tab.FindAll();
        }

        public T GetById(int id)
        {
            return tab.Find(e => e.ID == id).FirstOrDefault();
        }

        public IEnumerable<T> GetList(Expression<Func<T, bool>> predicate)
        {
            return tab.Find(predicate);
        }

        public bool Insert(T entity)
        {
            try
            {
                tab.Insert(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Remove(T entity)
        {
            try
            {
                tab.Delete(e => e.ID == entity.ID);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(T entity)
        {
            try
            {
                tab.Update(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
