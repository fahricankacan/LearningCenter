using Bogus;
using Entity.CreateFakeDataWithBogus;
using LearningCenter.CreateFakeDataWithBogus.Abstract;

namespace LearningCenter.CreateFakeDataWithBogus.Concrate
{
    public class FakeDataManager : IFakeDataService
    {
        public IEnumerable<Employe> GeneratePersonData()
        {

            decimal[] salaries = { 1000, 2000, 3000, 5000, 10000 };

            var persons = new Faker<Employe>()
                .RuleFor(x => x.Name, f => f.Name.FirstName())
                .RuleFor(x => x.Surname, f => f.Name.LastName())
                .RuleFor(x => x.Adress, f => f.Address.FullAddress())
                .RuleFor(x => x.Email, f => f.Person.Email)
                .RuleFor(x => x.Birhday, f => f.Person.DateOfBirth)
                .RuleFor(x => x.Salary, f => f.PickRandom(salaries))
                .Generate(100);

            return persons;
        }
        public IEnumerable<Cat> GenrateCatData()
        {
            string[] catNames = { "Boncuk,Luna,Oliver " };

            return new Faker<Cat>().Rules((f, x) =>
            {
                string catName = f.PickRandom(catNames);
                x.Name = catName;
            }).Generate(100);
        }

        public IEnumerable<BankAccount> GenrateBankAccounts()
        {
            return new Faker<BankAccount>().Rules((f, x) =>
            {
                x.Name = f.Finance.AccountName();
                x.Iban = f.Finance.Iban();
                x.Debt = f.Finance.Random.UInt(100, 100000);
                x.Balance = f.Finance.Random.UInt(100, 100000);
                x.IsLocked = f.Random.Bool();
                DateTime cardDatetime = f.Date.Future(yearsToGoForward: 17, refDate: DateTime.Now);
                x.CreditCard = new CreditCard
                {
                    CreditCardExpireDay = f.Date.Future(yearsToGoForward: 17, refDate: DateTime.Now).Day,
                    CreditCardExpireYear = cardDatetime.Year,
                    Number = f.Finance.CreditCardNumber(),
                    Ccv = f.Finance.CreditCardCvv(),
                };

                if (x.Balance - x.Debt < 0 || x.IsLocked)
                {
                    x.ToTakeOutaLoan = false;
                }
                else
                {
                    x.ToTakeOutaLoan = true;

                }
            }).Generate(100);
        }
    }
}
