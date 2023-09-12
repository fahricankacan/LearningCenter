// See https://aka.ms/new-console-template for more information

using LearningCenter.CreateFakeDataWithBogus.Concrate;

Console.WriteLine("Hello, World!");



var fakeDataManager = new FakeDataManager();
var data = fakeDataManager.GenrateBankAccounts();
Console.ReadLine();

