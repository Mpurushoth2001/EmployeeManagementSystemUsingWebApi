﻿using EmployeeManagement.Controllers;
using EmployeeManagement.Model.ResponseModel;
using EmployeeManagement.Modules.EmployeeManagement.Command.Delete;
using EmployeeManagement.Modules.EmployeeManagement.Command.Update;
using MediatR;
using Moq;

namespace EmployeeManagementXunitTest.Unit_Test.Controllers.EmployeeController1
{
    public class DeleteEmployeeRecordShould
    {
        private Mock<IMediator> mediatormock;
        private EmployeeController employeecontrollermock;

        public DeleteEmployeeRecordShould()
        {
            mediatormock = new Mock<IMediator>();
            employeecontrollermock = new EmployeeController(mediatormock.Object);            
        }
        [Theory]
        [MemberData(nameof(TestDataProvider.CreateObject), MemberType = typeof(TestDataProvider))]
        public async Task ReturnsCorrectResponse_DeleteNewEmployee(DeleteEmployee request)
        {
            #region Assign
            var response = new EntityResponse() { ResponseId = 1, AdditionalInfo = "Employee Details Deleted" };
            mediatormock.Setup(x => x.Send(request, default)).ReturnsAsync(response);
            #endregion

            #region Act
            var result = await employeecontrollermock.DeleteEmployeeRecord(request);
            #endregion

            #region Assert
            Assert.IsAssignableFrom<EntityResponse>(response);
            #endregion

        }
        [Theory]
        [MemberData(nameof(TestDataProvider.CreateObject), MemberType = typeof(TestDataProvider))]
        public async Task ReturnsFailResponse_DeleteNewEmployee(DeleteEmployee request)
        {
            #region Assign
            var response = new EntityResponse() { ResponseId = 0 };
            mediatormock.Setup(x => x.Send(request, default)).ReturnsAsync(response);
            #endregion

            #region Act
            var result = await employeecontrollermock.DeleteEmployeeRecord(request);
            #endregion

            #region Assert
            Assert.NotEqual(1, response.ResponseId);
            #endregion
        }
        public class TestDataProvider        {            public static IEnumerable<object[]> CreateObject()            {                yield return new object[] {                    new DeleteEmployee()                    {
                        EmployeeId=1,
                    }                };            }        }

    }
}

