using AutoMapper;
using GeekShoping.ProductAPI.Data.ValueObjects;
using GeekShoping.ProductAPI.Model;
using GeekShoping.ProductAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShoping.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MySQLContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(MySQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductVO>> FindAll()
        {
            var products = await _context.Products.ToListAsync();
            return _mapper.Map<List<ProductVO>>(products);
        }

        public async Task<ProductVO> FindById(long id)
        {
            var product = await _context.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<ProductVO>(product);
        }

        public async Task<ProductVO> Create(ProductVO product)
        {
            var productReturn = _mapper.Map<Product>(product);
            _context.Products.Add(productReturn);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductVO>(productReturn);
        }

        public async Task<ProductVO> Update(ProductVO product)
        {
            var productReturn = _mapper.Map<Product>(product);
            _context.Products.Update(productReturn);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductVO>(productReturn);
        }

        public async Task<bool> DeleteById(long id)
        {
            try
            {
                var product = await _context.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
                if(product is null)
                {
                    return false;
                } else
                {
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
