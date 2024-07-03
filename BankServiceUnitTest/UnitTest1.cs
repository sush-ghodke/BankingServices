using AccountModule.Controllers;
using AccountModule.Model;
using AccountModule.ResponseModels;
using AccountModule.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Transaction = AccountModule.Model.Transaction;

namespace BankServiceUnitTest
{
    public class UnitTest1
    {
        private readonly IAccountService _account;
        private readonly ILogger<AccountController> _logger;
       
        [Fact]
        public void Test1()
        {
            AccountController account = new (_account, _logger);
            account.Get();
        }
        [Fact]
        public void GetAccountDetailByid()
        {

            AccountController account = new(_account, _logger);
            account.GetAccountDetailByid(5);
        }
        [Fact]
        public void AddAccountDetail()
        {
            AccountDetails accountDetails =new AccountDetails();
            AccountController account = new(_account, _logger);
            account.AddAccountDetail(accountDetails);
        }
        [Fact]
        public void UpdateAccountDetail()
        {
            AccountDetails accountDetails = new AccountDetails();
            AccountController account = new(_account, _logger);
            account.UpdateAccountDetail(accountDetails);
        }
        [Fact]
        public void GetTransactionList()
        {
            AccountDetails accountDetails = new AccountDetails();
            AccountController account = new(_account, _logger);
            account.TransactionList();
        }
        [Fact]
        public void GetTransaction()
        {
            AccountDetails accountDetails = new AccountDetails();
            AccountController account = new(_account, _logger);
            account.GetTransaction(4);
        }
        [Fact]
        public void AddTransaction()
        {
            Transaction transaction = new Transaction();
            AccountController account = new(_account, _logger);
            account.AddTransaction(transaction);
        }
        [Fact]
        public void UpdateTransaction()
        {
            Transaction transaction = new Transaction();
            AccountController account = new(_account, _logger);
            account.UpdateTransaction(transaction);
        }
    }
}