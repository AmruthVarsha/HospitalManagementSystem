using HospitalManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Domain.Interfaces
{
    public interface IRepository<T>
    {
        public void Add(T entity);
        public List<T> GetAll();
        public T GetById(string id);
        public void Update(T entity);
        public void Delete(T entity);
    }
}
