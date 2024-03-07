using Scheduling;
namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private PCB? pCB1;
        private PCB? pCB2;
        private PCB? pCB3;

        [TestInitialize]
        public void TestInitialize()
        {
            pCB1 = new PCB
            {
                ProcessName = "Process1",
                ProcessPriority = 1,
                ProcessState = ProcessStateType.Running
            };

            pCB2 = new PCB
            {
                ProcessName = "Process2",
                ProcessPriority = 2,
                ProcessState = ProcessStateType.Running
            };

            pCB3 = new PCB
            {
                ProcessName = "Process3",
                ProcessPriority = 3,
                ProcessState = ProcessStateType.Running
            };
        }

        [TestMethod]
        public void ToString_Test()
        {
            //Act 
            string result1 = pCB1.ToString();
            string result2 = pCB2.ToString();
            string result3 = pCB3.ToString();

            // Assert
            string expectedString1 = "Process1(1)";
            string expectedString2 = "Process2(2)";
            string expectedString3 = "Process3(3)";

            Assert.AreEqual(expectedString1, result1);
            Assert.AreEqual(expectedString2, result2);
            Assert.AreEqual(expectedString3, result3);
        }
    }
}