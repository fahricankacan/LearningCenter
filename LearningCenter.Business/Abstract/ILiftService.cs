using LearningCenter.Entity.Abstract;
using LearningCenter.Entity.Concrate;
using static LearningCenter.Entity.Concrate.Lift;

namespace LearningCenter.Business.Abstract
{
    public interface ILiftService
    {
        Task<IResponseModel> CreateAsync(Lift lift);
        Task<IResponseDataModel<Lift>> GetAsync(Lift lift);
        Task<IResponseDataModel<IEnumerable<Lift>>> GetAllAsync();
        Task<IResponseModel> UpdateAsync(Lift lift);
        Task<IResponseModel> DeleteAsync(int id);
        Task<IResponseDataModel<Lift>> GetByLiftName(LiftName name);
    }
}
