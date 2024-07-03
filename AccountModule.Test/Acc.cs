using AccountModule.ApiResponse;
using AccountModule.Model;
using AccountModule.ResponseModels;
using AccountModule.Service;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Moq;
using AccountModule.Controllers;
using Microsoft.Extensions.Logging;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection.Metadata;
using AccountModule;
using Serilog;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;



namespace AccountModule.Test
{
    public class Acc
    {
        private readonly IAccountService _account;
        private readonly ILogger<AccountController> _logger;
        public Mock<IAccountService> mock = new Mock<IAccountService>();


        [Fact]
        public async Task AccountController_GetDetails_Data()
        {

            var loggerMock = new Mock<ILogger<AccountController>>();
            var accountServiceMock = new Mock<IAccountService>();
            var controller = new AccountController(accountServiceMock.Object, loggerMock.Object);


            var accountDetailList = new List<AccountDetailResponse>() {
              new AccountDetailResponse

            {
                        AccountId = 1,
                        AccountTypeId = 1,
                        CurrentBalance = 35000,
                        Status = 1,
                        OpenDate = DateTime.Now,
                        ClosedDate = DateTime.Now,
                        AccountTypeName = "Saving",
                        UserId = 16,
                        UserName = "Pooja Sharma"
                    },
              new AccountDetailResponse
              {
                        AccountId = 2,
                        AccountTypeId = 2,
                        CurrentBalance = 35000,
                        Status = 2,
                        OpenDate = DateTime.Now,
                        ClosedDate = DateTime.Now,
                        AccountTypeName = "Saving",
                        UserId = 16,
                        UserName = "Pooja Sharma"
                    },

                    };
            accountServiceMock.Setup(service => service.GetAccountDetailList()).ReturnsAsync(accountDetailList);

            var result = await controller.Get();

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            var response = okResult.Value as ApiResponse<dynamic>;
            Assert.NotNull(response);
            Assert.Equal(Constants.Success, response.Status);

        }



        [Fact]
        public async Task AccountController_GetDetails()
        {
            var loggerMock = new Mock<ILogger<AccountController>>();
            var accountServiceMock = new Mock<IAccountService>();
            var controller = new AccountController(accountServiceMock.Object, loggerMock.Object);


            var result = await controller.Get();

            var okResult = result as OkObjectResult;
            Assert.Null(okResult);
        }



        [Fact]
        public async void AccountController_Get_DetailsById()
        {

            // AccountController controller = new AccountController(_account, _logger);
            var loggerMock = new Mock<ILogger<AccountController>>();
            var accountServiceMock = new Mock<IAccountService>();
            var controller = new AccountController(accountServiceMock.Object, loggerMock.Object);

            // Assert.NotNull(0);
            var accountDetails = new AccountDetailResponse
            {
                AccountId = 1,
                AccountTypeId = 1,
                CurrentBalance = 35000,
                Status = 1,
                OpenDate = DateTime.Now,
                ClosedDate = DateTime.Now,
                AccountTypeName = "Saving",
                UserId = 16,
                UserName = "Pooja Sharma"

            };

            //var result = await controller.GetAccountDetailById(accountDetails);
            long accountId = 1;
            accountServiceMock.Setup(service => service.GetAccountDetailById(accountId)).ReturnsAsync(accountDetails);

            var result = await controller.GetAccountDetailByid(accountId);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            var response = okResult.Value as ApiResponse<dynamic>;
            Assert.NotNull(response);
            Assert.Equal(Constants.Success, response.Status);

        }



        [Fact]
        public async Task AccountController_AddDetails()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<AccountController>>();
            var accountServiceMock = new Mock<IAccountService>();
            var controller = new AccountController(accountServiceMock.Object, loggerMock.Object);
            var accountDetails = new AccountDetails
            {
                AccountId = 0,
                AccountTypeId = 1,
                UserId = 1

            };
            var result = await controller.AddAccountDetail(accountDetails);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;

            var response = okResult.Value as ApiResponse<dynamic>;
            Assert.Equal(Constants.Success, response.Status);
        }


        [Fact]
        public async Task AccountController_AddAccountDetailError()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<AccountController>>();
            var accountServiceMock = new Mock<IAccountService>();
            var controller = new AccountController(accountServiceMock.Object, loggerMock.Object);
            var accountDetails = new AccountDetails
            {
                AccountId = 0,
                UserId = 0,
                AccountTypeId = 0,

            };


            // Act
            var result = await controller.AddAccountDetail(accountDetails);

            // Assert

            var BadResult = result as BadRequestObjectResult;
            Assert.Null(BadResult);



        }


        [Fact]
        public async Task AccountController_AddAccountDetailELseCondition()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<AccountController>>();
            var accountServiceMock = new Mock<IAccountService>();
            var controller = new AccountController(accountServiceMock.Object, loggerMock.Object);
            var accountDetails = new AccountDetails
            {
                AccountId = 1,
                UserId = 2,
                AccountTypeId = 2,
            };


            // Act
            var result = await controller.AddAccountDetail(accountDetails);

            // Assert


            var BadResult = result as BadRequestObjectResult;
            Assert.Null(BadResult);


        }

        [Fact]
        public async Task AccountController_UpdateAccountDetail()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<AccountController>>();
            var accountServiceMock = new Mock<IAccountService>();
            var controller = new AccountController(accountServiceMock.Object, loggerMock.Object);
            var accountDetails = new AccountDetails
            {
                AccountId = 1,
                UserId = 2,
                AccountTypeId = 2

            };
            var result = await controller.UpdateAccountDetail(accountDetails);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var apiResponse = Assert.IsType<ApiResponse<dynamic>>(okResult.Value);
            Assert.Equal(Constants.Success, apiResponse.Status);
            Assert.Equal(Constants.Update, apiResponse.Message);
            Assert.Equal(accountDetails, apiResponse.Data);

        }
        [Fact]
        public async Task UpdateAccountDetail_InvalidAccountDetails_ReturnsNotFound()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<AccountController>>();
            var accountServiceMock = new Mock<IAccountService>();
            var controller = new AccountController(accountServiceMock.Object, loggerMock.Object);
            var accountDetails = new AccountDetails
            {
                AccountId = 0,
                UserId = 0,
                AccountTypeId = 2

            };

            // Act
            var result = await controller.UpdateAccountDetail(accountDetails);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var apiResponse = Assert.IsType<ApiResponse<object>>(notFoundResult.Value);
            Assert.Equal(Constants.Badrequest, apiResponse.Status);
            Assert.Equal(Constants.Failure, apiResponse.Message);
        }
        [Fact]
        public async Task UpdateAccountDetail_ExceptionThrown_ReturnsNull()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<AccountController>>();
            var accountServiceMock = new Mock<IAccountService>();
            var controller = new AccountController(accountServiceMock.Object, loggerMock.Object);
            var accountDetails = new AccountDetails
            {
                AccountId = 1,
                UserId = 1,
                AccountTypeId = 2

            };


            // Assert

            var result = await controller.UpdateAccountDetail(accountDetails);

            // Assert

            var BadResult = result as BadRequestObjectResult;
            Assert.Null(BadResult);


        }
        [Fact]
        public async Task TransactionList_WithTransactionData_ReturnsOk()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<AccountController>>();
            var accountServiceMock = new Mock<IAccountService>();
            var controller = new AccountController(accountServiceMock.Object, loggerMock.Object);


            var transactions = new List<TransactionResponse>()
            {
                  new TransactionResponse
                  {

                      TransactionId = 1,
                      AccountId= 1,
                      Amount = 100,
                      TransactionTypeId = 1,
                      TransactionDate = DateTime.Now,
                      TransactionModeId = 1

                  }

            };
            accountServiceMock.Setup(service => service.GetTransactionList()).ReturnsAsync(transactions);

            var result = await controller.TransactionList();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var apiResponse = Assert.IsType<ApiResponse<dynamic>>(okResult.Value);
            Assert.Equal(Constants.Success, apiResponse.Status);
            Assert.Equal(Constants.Get, apiResponse.Message);
            Assert.Equal(transactions, apiResponse.Data);
        }
        [Fact]
        public async Task TransactionList_WithEmptyTransactionList_ReturnsNotFound()
        {
            var loggerMock = new Mock<ILogger<AccountController>>();
            var accountServiceMock = new Mock<IAccountService>();
            var controller = new AccountController(accountServiceMock.Object, loggerMock.Object);

            var transactions = new List<TransactionResponse>()
            {
                  new TransactionResponse
                  {

                      TransactionId = 0,
                      AccountId= 1,
                      Amount = 100,
                      TransactionTypeId = 1,
                      TransactionDate = DateTime.Now,
                      TransactionModeId = 1

                  }
            };
            long transactionId = 0;
            var result = await controller.GetTransaction(transactionId);

            var okResult = result as OkObjectResult;
            Assert.Null(okResult);
        }


        [Fact]
        public async Task GetTransaction_WithValidTransactionId_ReturnsOk()
        {
            var loggerMock = new Mock<ILogger<AccountController>>();
            var accountServiceMock = new Mock<IAccountService>();
            var controller = new AccountController(accountServiceMock.Object, loggerMock.Object);

            long TransactionId = 1;

            var transactions = new TransactionResponse();

            transactions.TransactionId = TransactionId;
            transactions.AccountId = 1;
            transactions.Amount = 100;
            transactions.TransactionTypeId = 1;
            transactions.TransactionDate = DateTime.Now;
            transactions.TransactionModeId = 1;

            accountServiceMock.Setup(service => service.GetTransactionById(TransactionId)).ReturnsAsync(transactions);

            var result = await controller.GetTransaction(TransactionId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var apiResponse = Assert.IsType<ApiResponse<dynamic>>(okResult.Value);
            Assert.Equal(Constants.Success, apiResponse.Status);
            Assert.Equal(Constants.Get, apiResponse.Message);
            Assert.Equal(transactions, apiResponse.Data);
        }

        [Fact]
        public async Task GetTransaction_WithInvalidTransactionId_ReturnsNotFound()
        {
            var loggerMock = new Mock<ILogger<AccountController>>();
            var accountServiceMock = new Mock<IAccountService>();
            var controller = new AccountController(accountServiceMock.Object, loggerMock.Object);

            long InValidTransactionId = 1;


            //var transactions = new TransactionResponse();
            //{
            //    TransactionId = InValidTransactionId,
            //    AccountId = 1,
            //    Amount = 100,
            //    TransactionTypeId = 1,
            //    TransactionDate = DateTime.Now,
            //    TransactionModeId = 1
            //};

            accountServiceMock.Setup(service => service.GetTransactionById(InValidTransactionId)).ReturnsAsync(() => null);

            var result = await controller.GetTransaction(InValidTransactionId);

            var okResult = result as OkObjectResult;
            Assert.Null(okResult);

        }

        [Fact]
        public async Task AddTransaction_ValidTransaction_ReturnsSuccess()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<AccountController>>();
            var accountServiceMock = new Mock<IAccountService>();
            var controller = new AccountController(accountServiceMock.Object, loggerMock.Object);

            var validTransaction = new Model.Transaction
            {
                TransactionId = 0,
                AccountId = 1
            };

            accountServiceMock.Setup(service => service.AddTransaction(validTransaction)).ReturnsAsync("Success");

            // accountServiceMock.Setup(service => service.AddTransaction(TransactionId)).ReturnsAsync(transactions);
            // var result = await controller.AddTransaction(transactions);

            // Act
            var result = await controller.AddTransaction(validTransaction) as ObjectResult;


            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);


        }

        [Fact]
        public async Task AddTransaction_InValidTransaction()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<AccountController>>();
            var accountServiceMock = new Mock<IAccountService>();
            var controller = new AccountController(accountServiceMock.Object, loggerMock.Object);

            var validTransaction = new Model.Transaction
            {
                TransactionId = 1,
                AccountId = 1
            };

            accountServiceMock.Setup(service => service.AddTransaction(validTransaction)).ReturnsAsync("Success");

            var result = await controller.AddTransaction(validTransaction) as ObjectResult;


            var BadResult = result as BadRequestObjectResult;
            Assert.Null(BadResult);

        }

        [Fact]
        public async Task UpdateTransaction_ValidTransaction_ReturnsSuccess()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<AccountController>>();
            var accountServiceMock = new Mock<IAccountService>();
            var controller = new AccountController(accountServiceMock.Object, loggerMock.Object);
            var validTransaction = new Transaction
            {
                TransactionId = 1,
                AccountId = 1
            };

            //Act
            var result = await controller.UpdateTransaction(validTransaction);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var apiResponse = Assert.IsType<ApiResponse<dynamic>>(okResult.Value);

            //Assert
            Assert.Equal(Constants.Success, apiResponse.Status);
            Assert.Equal(Constants.Update, apiResponse.Message);
            Assert.Equal(validTransaction, apiResponse.Data);

        }
        [Fact]
        public async Task UpdateTransaction_InvalidTransaction_ReturnsNotFound()
        {
            var loggerMock = new Mock<ILogger<AccountController>>();
            var accountServiceMock = new Mock<IAccountService>();
            var controller = new AccountController(accountServiceMock.Object, loggerMock.Object);

            var invalidTransaction = new Transaction 
            { 
                TransactionId = 0, 
                AccountId = 0 
            };


            var result = await controller.UpdateTransaction(invalidTransaction);
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var apiResponse = Assert.IsType<ApiResponse<object>>(notFoundResult.Value);
            //Assert.Equal(Constants.Badrequest, apiResponse.Status);
            Assert.Equal("Please provide the valid TransactionId",apiResponse.Message);



        }

    }
}

  


//private List<AccountDetailResponse> GetAccountsData()
//{
//    List<AccountDetailResponse> productsData = new List<AccountDetailResponse>
//    {
//        new AccountDetailResponse
//        {
//            AccountId = 1,
//            AccountTypeId = 1,
//            CurrentBalance = 35000,
//            Status = 1,
//            OpenDate = DateTime.Now,
//            ClosedDate = DateTime.Now,
//            AccountTypeName = "Saving",
//            UserId = 16,
//            UserName = "Pooja Sharma"
//        },
//    };
//    return productsData;

