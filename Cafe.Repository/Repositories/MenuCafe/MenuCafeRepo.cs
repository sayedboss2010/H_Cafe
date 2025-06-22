using AutoMapper;
using Cafe.EF.Models;
using Cafe.VM.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Cafe.VM.ViewModels.MenuCafeVM;

namespace Cafe.Repository.Repositories.MenuCafe
{
    public class MenuCafeRepo
    {
        private readonly IMapper _mapper;

        public MenuCafeRepo(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IList<Category> list()
        {
            using var dbContext = new ErpDbContext();
            var categories = (from c in dbContext.ItemCategories
                              select new Category
                              {
                                  Id = c.CategoryID,
                                  Name = c.NameAr,
                                  Subcategories = (from i in dbContext.Items
                                                   where i.CategoryID == c.CategoryID
                                                   select new Subcategory
                                                   {
                                                       Id = i.ItemID,
                                                       Name = i.NameAr,
                                                       Price = i.Price,
                                                   }).ToList()
                              }).ToList();
            return _mapper.Map<List<Category>>(categories);
        }
    }
}
