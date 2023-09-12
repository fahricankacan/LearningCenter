using Entity.CreateFakeDataWithBogus;

namespace LearningCenter.CreateFakeDataWithBogus.Abstract
{
    public interface IFakeDataService
    {
        public IEnumerable<Employe> GeneratePersonData();
        public IEnumerable<Cat> GenrateCatData();
        public IEnumerable<BankAccount> GenrateBankAccounts();
    }
}
