using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMvcContext _contex;

        public DepartmentService(SalesWebMvcContext context)
        {
            _contex = context;
        }

        public List<Department> FindAll()
        {
            return _contex.Department.OrderBy(x => x.Name).ToList();  // retorna nomes dos departamentos em ordem  crescente
        }
    }
}
