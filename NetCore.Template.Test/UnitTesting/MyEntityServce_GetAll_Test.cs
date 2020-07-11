using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NetCore.Template.Entities;
using NetCore.Template.Repositories;
using NetCore.Template.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace NetCore.Template.Test.UnitTesting
{
    public class MyEntityServce_GetAll_Test : IClassFixture<MyEntityServce_GetAll_Test_Fixture>
    {
        public MyEntityServce_GetAll_Test_Fixture Fixture { get; }
        
        public MyEntityServce_GetAll_Test(MyEntityServce_GetAll_Test_Fixture fixture)
        {
            Fixture = fixture;
        }

        [Fact]
        public void GetAll()
        {
            var result = Fixture.Service.GetAll();

            Assert.True(result.Count == 5);
        }
    }

    public class MyEntityServce_GetAll_Test_Fixture : IDisposable
    {
        public MyEntityService Service { get; private set; }

        public MyEntityServce_GetAll_Test_Fixture()
        {
            var services = new ServiceCollection().AddAutoMapper(typeof(AutoMapperProfile));
            var serviceProvider = services.BuildServiceProvider();

            var mapper = serviceProvider.GetService<IMapper>();

            var entityList = new List<MyEntity>();
            entityList.Add(new MyEntity { Value = "Entity1" });
            entityList.Add(new MyEntity { Value = "Entity2" });
            entityList.Add(new MyEntity { Value = "Entity3" });
            entityList.Add(new MyEntity { Value = "Entity4" });
            entityList.Add(new MyEntity { Value = "Entity5" });

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(x => x.MyEntityRepository.GetAll(It.IsAny<Func<IQueryable<MyEntity>, IIncludableQueryable<MyEntity, object>>>()))
                .Returns(entityList.AsQueryable());

            Service = new MyEntityService(unitOfWorkMock.Object, mapper);
        }

        public void Dispose()
        {
        }
    }
}