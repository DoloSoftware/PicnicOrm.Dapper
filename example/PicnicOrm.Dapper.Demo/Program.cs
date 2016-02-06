using System.Linq;

using PicnicOrm.Dapper.Demo.Models;
using PicnicOrm.Dapper.Mapping;

namespace PicnicOrm.Dapper.Demo
{
    class Program
    {
        #region Private Methods

        static void Main(string[] args)
        {
            //Map Address to User
            var userAddressMap = new OneToOneMapping<User, Address>(address => address.Id, user => user.AddressId, (user, address) => user.Address = address);

            //Map Address to Employer & Employer to User
            var employerAddressMap = new OneToOneMapping<Employer, Address>(address => address.Id, employer => employer.AddressId, (employer, address) => employer.Address = address);
            var userEmployerMap = new OneToOneMapping<User, Employer>(employer => employer.Id, user => user.EmployerId, (user, employer) => user.Employer = employer);
            userEmployerMap.AddMapping(employerAddressMap);

            //Map Car to user
            var userCarMap = new ManyToManyMapping<User, Car, UserCar>(car => car.Id, userCar => userCar.CarId, userCar => userCar.UserId, (user, cars) => user.Cars = cars.ToList());

            //User Mapping
            var userMap = new ParentMapping<User>(user => user.Id);
            userMap.AddMapping(userAddressMap);
            userMap.AddMapping(userEmployerMap);
            userMap.AddMapping(userCarMap);

            var dataBroker = new SqlDataBroker(@"Server=(localdb)\ProjectsV12;Database=DemoDatabase;Integrated security=True");
            dataBroker.AddMapping((int)ConfigType.User, userMap);

            var users = dataBroker.ExecuteStoredProcedure<User>("EXEC dbo.ReadUser", (int) ConfigType.User);

            var test = users;
        }

        #endregion
    }

    public enum ConfigType
    {
        User
    }
}