[![Build status](https://ci.appveyor.com/api/projects/status/a58ngl716h0oau83/branch/master?svg=true)](https://ci.appveyor.com/project/Sikta/picnicorm-dapper/branch/master)
 

# PicnicOrm
A ORM configuration based library with a Dapper based implementation.


There is an example project in the solution.

Example usage:

``` csharp
public void ConfigureDataBroker()
{
	//Map Address to User
	var userAddressMap = new OneToOneMapping<User, Address>(address => address.Id, user => user.AddressId, 
		(user, address) => user.Address = address);

	//Map Address to Employer & Employer to User
	var employerAddressMap = new OneToOneMapping<Employer, Address>(address => address.Id, 
		employer => employer.AddressId, (employer, address) => employer.Address = address);
	var userEmployerMap = new OneToOneMapping<User, Employer>(employer => employer.Id, user => user.EmployerId,
		(user, employer) => user.Employer = employer);
	userEmployerMap.AddMapping(employerAddressMap);

	//Map Car to user
	var userCarMap = new ManyToManyMapping<User, Car, UserCar>(car => car.Id, 
		userCar => userCar.CarId, 
		userCar => userCar.UserId,
		(user, cars) =>
		{
			foreach (var car in cars)
			{
				user.Cars.Add(car);
				car.Users.Add(user);
			}
		});

	//User Mapping
	var userMap = new ParentMapping<User>(user => user.Id);
	userMap.AddMapping(userAddressMap);
	userMap.AddMapping(userEmployerMap);
	userMap.AddMapping(userCarMap);

	var dataBroker = new SqlDataBroker(@"CONNECTION_STRING", new DapperGridReaderFactory());
	dataBroker.AddMapping(userMap);

	//Add the databroker to your favorite IoC container
}
	
public class UserRepository
{
	public IEnumerable<User> GetUsersBornAfter(DateTime date)
	{
		var parameterList = new List<IDbParameter>();
		parameterList.Add(new DbParameter("BirthDate", DateTime.Parse("06-01-1975"), DbType.Date));

		return this.DataBroker.ExecuteStoredProcedure<User>("dbo.ReadUser", parameterList);
	}
}
```
