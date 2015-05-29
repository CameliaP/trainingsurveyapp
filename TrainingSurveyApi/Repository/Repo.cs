using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using efAcademy.Context;
using efAcademy.Entities;
using efAcademy.Entities.Web;
using System.Data;
using System.Data.Entity;
using System.Net.Http;
using System.Web.Http.Routing;

namespace TrainingSurveyApi.Repository
{
    [Serializable]
    public class UserInputException : Exception
    {
        public UserInputException() { }
        public UserInputException(string message) : base(message) { }
        public UserInputException(string message, Exception inner) : base(message, inner) { }
        protected UserInputException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
    [Serializable]
    public class StoreQueryException : Exception
    {
        public StoreQueryException() { }
        public StoreQueryException(string message) : base(message) { }
        public StoreQueryException(string message, Exception inner) : base(message, inner) { }
        protected StoreQueryException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
    [Serializable]
    public class StoreException : Exception
    {
        public StoreException() { }
        public StoreException(string message) : base(message) { }
        public StoreException(string message, Exception inner) : base(message, inner) { }
        protected StoreException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
    public interface IGet<T>
    {
        IEnumerable<T> GETEmployeesIndex(int top, int skip);   
        IEnumerable<T> OfString(string phrase);
        IEnumerable<T> OfInteger(int param);
        //this would all get the singleton instance given the primary key
        T ofStringPK(string pk);
        T OfIntPK(int pk);

    }
    public interface IPost<T>
    {
        void Add(T newItem);
    }
    public interface IPut<T>
    {
         void Update(T toUpdate);
    }
    public interface IDelete<T>
    {
         void Delete(T toDelete);
    }
    public class Repo<T>
    {
        private academyContext db;
        protected ICollection<T> result;
        protected T singleResult;

        //derivitives need not access the request
        protected HttpRequestMessage request;
        protected UrlHelper urlHelper;

        public HttpRequestMessage Request
        {
            
            set
            {
                //request is writeonly
                request = value;
                urlHelper = new UrlHelper(request);//setting the url helper
            }
        }


        protected academyContext CurrentContext{ 
            get{
                if (db.Database.Connection.State==ConnectionState.Broken || 
                    db.Database.Connection.State== ConnectionState.Closed)
                {
                    db = new academyContext();//reopening the context whe broken
                }
                return db;     
        } }
        public Repo() {
            try
            {
                this.db = new academyContext();
                
            }
            catch (Exception x)
            {
                throw new StoreException("Failed to load dbcontext", x);
            }
        }
    }

    
}