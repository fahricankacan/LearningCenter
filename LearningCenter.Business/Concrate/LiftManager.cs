using LearningCenter.Business.Abstract;
using LearningCenter.Entity.Abstract;
using LearningCenter.Entity.Concrate;
using LearningCenter.Repository.Abstract;
using static LearningCenter.Entity.Concrate.Lift;

namespace LearningCenter.Business.Concrate
{
    public class LiftManager : ILiftService
    {
        private readonly ILiftRepository _liftRepository;

        public LiftManager(ILiftRepository liftRepository)
        {
            _liftRepository = liftRepository;
        }

        public async Task<IResponseModel> CreateAsync(Lift lift)
        {
            try
            {
                var result = await _liftRepository.CreateAsync(lift);
                return result.Success.Equals(true) ?
                    new ResponseModel { Success = true } :
                    new ResponseModel { Success = false };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IResponseModel> DeleteAsync(int id)
        {
            try
            {
                var liftResult = await _liftRepository.GetAllAsync(null);
                if (!liftResult.Success) return new ResponseModel { Success = false, Message = liftResult.Message };

                var result = await _liftRepository.DeleteAsync(id);
                return result.Success.Equals(true) ?
                    new ResponseModel { Success = true } :
                    new ResponseModel { Success = false };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IResponseDataModel<Lift>> GetAsync(Lift lift)
        {
            try
            {
                var result = await _liftRepository.GetAsync(x => x.Id == lift.Id);
                return result.Success.Equals(true) ?
                    new ResponseDataModel<Lift> { Success = true, Data = result.Data } :
                    new ResponseDataModel<Lift> { Success = false };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IResponseDataModel<IEnumerable<Lift>>> GetAllAsync()
        {
            try
            {
                var result = await _liftRepository.GetAllAsync();
                return result.Success.Equals(true) ?
                    new ResponseDataModel<IEnumerable<Lift>> { Success = true, Data = result.Data } :
                    new ResponseDataModel<IEnumerable<Lift>> { Success = false };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IResponseModel> UpdateAsync(Lift lift)
        {
            try
            {
                var result = await _liftRepository.UpdateAsync(lift);
                return result.Success.Equals(true) ?
                    new ResponseModel { Success = true } :
                    new ResponseModel { Success = false };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IResponseDataModel<Lift>> GetByLiftName(LiftName name)
        {
            try
            {
                var result = await _liftRepository.GetAsync(x => x.Name == name);

                return result.Success.Equals(true) ?
                    new ResponseDataModel<Lift> { Success = true, Data = result.Data } :
                    new ResponseDataModel<Lift> { Success = false };
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
