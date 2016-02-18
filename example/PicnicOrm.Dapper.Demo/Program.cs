using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;

using Dapper;

using PicnicOrm.Ado.Factories;
using PicnicOrm.Dapper.Demo.Factories;
using PicnicOrm.Dapper.Demo.Models;
using PicnicOrm.Dapper.Factories;
using PicnicOrm.Data;
using PicnicOrm.Mapping;

namespace PicnicOrm.Dapper.Demo
{
    class Program
    {
        #region Private Methods

        static void Main(string[] args)
        {
            var dataBroker = new DapperDataBroker(@"Server=(localdb)\ProjectsV12;Database=DemoDatabase;Integrated security=True", new DapperGridReaderFactory());
            Func<IGridReader, IEnumerable<User>> mapping = (gridReader) =>
                {
                    var users = gridReader.Read<User>();
                    var userAddresses = gridReader.Read<Address>().ToDictionary(address => address.Id);
                    var employers = gridReader.Read<Employer>().ToDictionary(employer => employer.Id);
                    var employerAddresses = gridReader.Read<Address>().ToDictionary(address => address.Id);
                    var userCars = gridReader.Read<UserCar>().GroupBy(userCar => userCar.UserId, userCar => userCar.CarId).ToDictionary(grouping => grouping.Key, grouping => grouping.ToList());
                    var cars = gridReader.Read<Car>().ToDictionary(car => car.Id);

                    foreach (var employer in employers.Values)
                    {
                        employer.Address = employerAddresses[employer.AddressId];
                    }

                    foreach (var user in users)
                    {
                        user.Address = userAddresses[user.AddressId];
                        user.Employer = employers[user.EmployerId];

                        foreach (var carId in userCars[user.Id])
                        {
                            user.Cars.Add(cars[carId]);
                        }
                    }

                    return users;
                };

            var userList = dataBroker.ExecuteStoredProcedure<User>("dbo.ReadUser", mapping);

            var test = userList;
        }

        //static void Main(string[] args)
        //{
        //    IEnumerable<User> users = null;

        //    using (var connection = new SqlConnection(@"Server=(localdb)\ProjectsV12;Database=DemoDatabase;Integrated security=True"))
        //    {
        //        using (var multi = connection.QueryMultiple("dbo.ReadUser", commandType: CommandType.StoredProcedure))
        //        {
        //            users = multi.Read<User>();
        //            var userAddresses = multi.Read<Address>().ToDictionary(address => address.Id);
        //            var employers = multi.Read<Employer>().ToDictionary(employer => employer.Id);
        //            var employerAddresses = multi.Read<Address>().ToDictionary(address => address.Id);
        //            var userCars = multi.Read<UserCar>().GroupBy(userCar => userCar.UserId, userCar => userCar.CarId).ToDictionary(grouping => grouping.Key, grouping => grouping.ToList());
        //            var cars = multi.Read<Car>().ToDictionary(car => car.Id);

        //            foreach (var employer in employers.Values)
        //            {
        //                employer.Address = employerAddresses[employer.AddressId];
        //            }

        //            foreach (var user in users)
        //            {
        //                user.Address = userAddresses[user.AddressId];
        //                user.Employer = employers[user.EmployerId];

        //                foreach (var carId in userCars[user.Id])
        //                {
        //                    user.Cars.Add(cars[carId]);
        //                }
        //            }
        //        }
        //    }

        //    var test = users;
        //}

        //static void Main(string[] args)
        //{
        //    //Map Address to User
        //    var userAddressMap = new OneToOneMapping<User, Address>(address => address.Id, user => user.AddressId, (user, address) => user.Address = address);

        //    //Map Address to Employer & Employer to User
        //    var employerAddressMap = new OneToOneMapping<Employer, Address>(address => address.Id, employer => employer.AddressId, (employer, address) => employer.Address = address);
        //    var userEmployerMap = new OneToOneMapping<User, Employer>(employer => employer.Id, user => user.EmployerId, (user, employer) => user.Employer = employer);
        //    userEmployerMap.AddMapping(employerAddressMap);

        //    //Map Car to user
        //    var userCarMap = new ManyToManyMapping<User, Car, UserCar>(car => car.Id, userCar => userCar.CarId, userCar => userCar.UserId, (user, cars) => user.Cars = cars.ToList());
        //        (user, cars) =>
        //        {
        //            foreach (var car in cars)
        //            {
        //                user.Cars.Add(car);
        //                car.Users.Add(user);
        //            }
        //        });

        //    //User Mapping
        //    var userMap = new ParentMapping<User>(user => user.Id);
        //    userMap.AddMapping(userAddressMap);
        //    userMap.AddMapping(userEmployerMap);
        //    userMap.AddMapping(userCarMap);

        //    //var dataBroker = new SqlDataBroker(@"Server=(localdb)\ProjectsV12;Database=DemoDatabase;Integrated security=True", new DapperGridReaderFactory());
        //    var gridReaderFactory = new SqlGridReaderFactory();
        //    gridReaderFactory.AddEntityFactory(new AddressFactory());
        //    gridReaderFactory.AddEntityFactory(new CarFactory());
        //    gridReaderFactory.AddEntityFactory(new EmployerFactory());
        //    gridReaderFactory.AddEntityFactory(new UserFactory());
        //    gridReaderFactory.AddEntityFactory(new UserCarFactory());

        //    var dataBroker = new SqlDataBroker(@"Server=(localdb)\ProjectsV12;Database=DemoDatabase;Integrated security=True", gridReaderFactory);
        //    dataBroker.AddMapping(userMap);

        //    var totalTime = 0L;
        //    for (int i = 0; i < 100; i++)
        //    {
        //        Stopwatch watch = new Stopwatch();

        //        watch.Start();
        //        //var parameterList = new List<IDbParameter>();
        //        //parameterList.Add(new DbParameter("BirthDate", DateTime.Parse("06-01-1975"), DbType.Date));
        //        var users = dataBroker.ExecuteStoredProcedure<User>("dbo.ReadUser");
        //        watch.Stop();

        //        totalTime += watch.ElapsedMilliseconds;
        //    }

        //    var test = totalTime;
        //}

        #endregion
    }
}