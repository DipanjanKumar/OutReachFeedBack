using OutReachDataAccessLayer.DBContext;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;

namespace OutReachDataAccessLayer.Generic_Repository
{
    class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private GenericDBContext db = null;
        private DbSet<T> table = null;
        public GenericRepository()
        {
            this.db = new GenericDBContext();
            this.table = db.Set<T>();
        }
        public GenericRepository(GenericDBContext db)
        {
            this.db = db;
            this.table = db.Set<T>();
        }
        public void Delete(T obj)
        {
            table.Remove(obj);
        }
        public void Insert(T obj)
        {
            table.Add(obj);
        }
        public void Save()
        {
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var errorMessages = dbEx.EntityValidationErrors
                       .SelectMany(x => x.ValidationErrors)
                       .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);                
                var exceptionMessage = string.Concat(dbEx.Message, " The validation errors are: ", fullErrorMessage);                
                throw new DbEntityValidationException(exceptionMessage, dbEx.EntityValidationErrors);               
            }
            catch(DbUpdateException de)
            {
                throw de;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        public IQueryable<T> SelectAll()
        {
            return table.AsQueryable();
        }
        public T SelectByID(object id)
        {
            return table.Find(id);
        }
        public void Update(T obj)
        {
            table.Attach(obj);
            db.Entry(obj).State = EntityState.Modified;
        }
    }
}
