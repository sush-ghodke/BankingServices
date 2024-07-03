using AccountModule.Controllers;
using AccountModule.Model;
using AccountModule.ResponseModels;
using AccountModule.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Transaction = AccountModule.Model.Transaction;

namespace AccountModuleUnitTest
{
    public class AccountControllerTest1
    {
        private readonly IAccountService _account;
        private readonly ILogger<AccountController> _logger;

        [Fact]
        public void Test1()
        {
            AccountController account = new(_account, _logger);
            account.Get();
        }
    }
}