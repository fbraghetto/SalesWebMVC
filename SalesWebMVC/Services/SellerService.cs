using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller s)
        {

            _context.Add(s);
            _context.SaveChanges();
        }

        public Seller FindByID(int id)
        {
            return _context.Seller.Include(d => d.Department).FirstOrDefault(s => s.Id == id);
        }

        public void Remove(int id)
        {
            var s = _context.Seller.Find(id);
            _context.Seller.Remove(s);
            _context.SaveChanges();
        }


        public void Update(Seller obj)
        {
            if (!_context.Seller.Any(x => x.Id == obj.Id))
                throw new NotFoundExceptions("Id not found");

            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
