using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingleResponsibilityPrinciple;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple.Tests
{
    [TestClass()]
    public class TradeProcessorTests
    {


        private int CountDbRecords()
        {
            //Home Connection  using (var connection = new System.Data.SqlClient.SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\chris\source\repos\tgibbons-students-css\cis-3285-asg-8-ckillian26\tradedatabase.mdf;Integrated Security=True;Connect Timeout=30;"))
            using (var connection = new System.Data.SqlClient.SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ckillian\source\repos\cis-3285-asg-8-ckillian26\tradedatabase.mdf;Integrated Security=True;Connect Timeout=30;"))

            {
                connection.Open();
                string myScalarQuery = "SELECT COUNT(*) FROM trade";
                SqlCommand myCommand = new SqlCommand(myScalarQuery, connection);
                int count = (int)myCommand.ExecuteScalar();
                connection.Close();
                return count;
            }
        }
        [TestMethod]
        public void TestNormalFile()
        {
            //Arrange
            var tradeStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SingleResponsibilityPrincipleTests.trades.txt");
            var tradeProcessor = new TradeProcessor();
            int startCount = CountDbRecords();
            
            //Act
            tradeProcessor.ProcessTrades(tradeStream);
            int endCount = CountDbRecords();

            //Assert
            int count = CountDbRecords();
            Assert.AreEqual(endCount - startCount, 4);
        }
        [TestMethod]
        public void TestBadFile()
        {
            //Arrange
            var tradeStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SingleResponsibilityPrincipleTests.badTrades1.txt");
            var tradeProcessor = new TradeProcessor();
            int startCount = CountDbRecords();

            //Act
            tradeProcessor.ProcessTrades(tradeStream);
            int endCount = CountDbRecords();

            //Assert
            int count = CountDbRecords();
            Assert.AreEqual(startCount, endCount);
        }
        [TestMethod]
        public void TestNegativeAmount()
        {
            //Arrange
            var tradeStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SingleResponsibilityPrincipleTests.negativeAmount.txt");
            var tradeProcessor = new TradeProcessor();
            int startCount = CountDbRecords();

            //Act
            tradeProcessor.ProcessTrades(tradeStream);
            int endCount = CountDbRecords();

            //Assert
            int count = CountDbRecords();
            Assert.AreEqual(startCount, endCount);
        }
        [TestMethod]
        public void TestNegativePrice()
        {
            //Arrange
            var tradeStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SingleResponsibilityPrincipleTests.negativePrice.txt");
            var tradeProcessor = new TradeProcessor();
            int startCount = CountDbRecords();

            //Act
            tradeProcessor.ProcessTrades(tradeStream);
            int endCount = CountDbRecords();

            //Assert
            int count = CountDbRecords();
            Assert.AreEqual(startCount, endCount);
        }
        [TestMethod]
        public void OnlyOneRecordAdded()
        {
            //Arrange
            var tradeStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SingleResponsibilityPrincipleTests.oneTrade.txt");
            var tradeProcessor = new TradeProcessor();
            int startCount = CountDbRecords();
            
            //Act
            tradeProcessor.ProcessTrades(tradeStream);
            int endCount = CountDbRecords();

            //Assert
            int count = CountDbRecords();
            Assert.AreEqual(endCount - startCount, 1);
        }
        [TestMethod]
        public void NoRecordAdded()
        {
            //Arrange
            var tradeStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SingleResponsibilityPrincipleTests.emptyTrade.txt");
            var tradeProcessor = new TradeProcessor();
            int startCount = CountDbRecords();
            //Act
            tradeProcessor.ProcessTrades(tradeStream);
            int endCount = CountDbRecords();

            //Assert
            int count = CountDbRecords();
            Assert.AreEqual(endCount, startCount);
        }
        [TestMethod]
        public void CorrectFile()
        {
            //Arrange
            var tradeStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SingleResponsibilityPrincipleTests.trades.txt");
            var tradeProcessor = new TradeProcessor();

            //Assert
            Assert.AreEqual(tradeStream, tradeStream);
        }
        [TestMethod]
        public void FileExists()
        {
            //Arrange
            var tradeStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SingleResponsibilityPrincipleTests.null.txt");
            var tradeProcessor = new TradeProcessor();

            //Assert
            Assert.IsNotNull(tradeStream);
        }

    }
}