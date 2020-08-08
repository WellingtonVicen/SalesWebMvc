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

        public  async Task<List<Seller>> FindAllAsync()
        {
            return await _contex.Seller.ToListAsync(); // devolver a lista de Sellers do Banco de dados
        }

        public async Task Insert(Seller obj) // insercao no banco de dados
        {

            await _contex.AddAsync(obj);
            await _contex.SaveChangesAsync(); 
        }

        public  async Task<Seller> FindByIdAsync(int id)
        {
            return await _contex.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id); // retorta id e nome do departamento para os detalhes
        }

        public  async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _contex.Seller.FindAsync(id);
                _contex.Seller.Remove(obj);
                await _contex.SaveChangesAsync();
            }
            catch(DbUpdateException e)
            {
                throw new IntegrityException("Can't delete seller because he/she has sales");
            }
        }

        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _contex.Seller.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not Found");
            }
            try
            {
                _contex.Update(obj);
                 await _contex.SaveChangesAsync();
            }
            catch (DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
            
        }
    }
}
