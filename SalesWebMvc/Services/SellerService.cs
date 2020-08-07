using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services
{
    public class SellerService
    {

        private readonly SalesWebMvcContext _contex;

        public SellerService(SalesWebMvcContext context)
        {
            _contex = context;
        }

        public List<Seller> FindAll()
        {
            return _contex.Seller.ToList(); // devolver a lista de Sellers do Banco de dados
        }

        public void Insert(Seller obj) // insercao no banco de dados
        {

            _contex.Add(obj);
            _contex.SaveChanges(); 
        }

        public Seller FindById(int id)
        {
            return _contex.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id); // retorta id e nome do departamento para os detalhes
        }

        public void Remove(int id)
        {
            var obj = _contex.Seller.Find(id);
            _contex.Seller.Remove(obj);
            _contex.SaveChanges();
        }

        public void Update(Seller obj)
        {
            if (!_contex.Seller.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id not Found");
            }
            try
            {
                _contex.Update(obj);
                _contex.SaveChanges();
            }
            catch (DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
            
        }
    }
}
