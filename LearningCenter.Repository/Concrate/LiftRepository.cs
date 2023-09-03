using LearningCenter.Entity.Abstract;
using LearningCenter.Entity.Concrate;
using LearningCenter.Repository.Abstract;
using LearningCenter.WhatIsMinimalApi.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LearningCenter.Repository.Concrate
{
    public class LiftRepository : ILiftRepository
    {
        private readonly LiftDb _liftDb;

        public LiftRepository(LiftDb liftDb)
        {
            _liftDb = liftDb;
        }

        public async Task<IResponseModel> CreateAsync(Lift lift)
        {
            try
            {
                _liftDb.Lifts.Add(lift);
                return await _liftDb.SaveChangesAsync() == 1 ?
                    new ResponseModel { Success = true } :
                    new ResponseModel
                    {
                        Success = false
                    };

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
                var liftData = await GetAsync(x => x.Id == id);
                if (!liftData.Success) return new ResponseModel { Success = false, Message = liftData.Message };
                _liftDb.Lifts.Remove(liftData.Data);
                return await _liftDb.SaveChangesAsync() == 1 ?
                    new ResponseModel { Success = true } :
                    new ResponseModel
                    {
                        Success = false
                    };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IResponseDataModel<Lift>> GetAsync(Expression<Func<Lift, bool>> filter)
        {
            try
            {
#pragma warning disable CS8601 // Possible null reference assignment.
                var data = await _liftDb.Lifts.SingleOrDefaultAsync(filter);
                return data != null ?
                    new ResponseDataModel<Lift>
                    {
                        Success = true,
                        Data = data
                    } :
                    new ResponseDataModel<Lift>
                    {
                        Success = false,
                        Message = "Lift not found"
                    };
#pragma warning restore CS8601 // Possible null reference assignment.
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IResponseDataModel<IEnumerable<Lift>>> GetAllAsync(Expression<Func<Lift, bool>>? filter)
        {
            return new ResponseDataModel<IEnumerable<Lift>>
            {
                Success = true,
                Data = await _liftDb.Lifts.ToListAsync()
            };
        }

        public async Task<IResponseModel> UpdateAsync(Lift lift)
        {
            try
            {
                lift.Name = lift.Name;
                lift.Weight = lift.Weight;
                lift.Reps = lift.Reps;
                _liftDb.Lifts.Update(lift);
                return await _liftDb.SaveChangesAsync() == 1 ?
                  new ResponseModel { Success = true } :
                  new ResponseModel
                  {
                      Success = false
                  };
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
