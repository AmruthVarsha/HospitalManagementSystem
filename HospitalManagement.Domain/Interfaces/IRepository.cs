using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Domain.Interfaces
{
    public interface IRepository<T>
    {
        public void Add();
        public void GetAll();
        public void GetById();
        public void Update();
        public void Delete();
    }
}
