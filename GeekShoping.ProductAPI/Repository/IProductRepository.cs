using GeekShoping.ProductAPI.Data.ValueObjects;

namespace GeekShoping.ProductAPI.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductVO>> FindAll();
        Task<ProductVO> FindById(long id);

        Task<ProductVO> Create(ProductVO product);
        Task<ProductVO> Update(ProductVO product);
        Task<bool> DeleteById(long id);
    }
}
