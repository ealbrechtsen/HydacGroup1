using ModelPersistence;
using ModelPersistence.Model;
using ModelPersistence.Persistence;
namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            EmployeeRepository employeeRepository = new EmployeeRepository();
            Employee employee1 = new Employee("lars","peter","202020","123456789123456","email@gmail.com","aoadiasdoij");
            //Act
            employeeRepository.Add(employee1);
            //Assert
            Assert.AreEqual(1, employeeRepository.Employees.Count);
        }
    }
}