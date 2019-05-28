using MailSender.lib.Data.Linq2Sql;
using MailSender.lib.Services.InMemory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace MailSender.lib.Tests.Services.InMemory
{
	[TestClass]
	public class DataInMemoryTests
	{
		public DataInMemoryTests()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		#region Additional test attributes
		//
		// You can use the following additional attributes as you write your tests:
		//
		// Use ClassInitialize to run code before running the first test in the class
		// [ClassInitialize()]
		// public static void MyClassInitialize(TestContext testContext) { }
		//
		// Use ClassCleanup to run code after all tests in a class have run
		// [ClassCleanup()]
		// public static void MyClassCleanup() { }
		//
		// Use TestInitialize to run code before running each test 
		// [TestInitialize()]
		// public void MyTestInitialize() { }
		//
		// Use TestCleanup to run code after each test has run
		// [TestCleanup()]
		// public void MyTestCleanup() { }
		//
		#endregion

		[TestMethod]
		public void Add_Method_AddNewItemInService()
		{
			// AAA - подход
			// Arange

			const string expectedName = "Test Recipient";
			const string expectedEmail = "Test email";
			var newRecipient = new Recipient
			{
				Name = expectedName,
				Email = expectedEmail
			};

			var service = new RecipientsDataInMemory();

			// Act
			var actualId = service.Add(newRecipient);

			// Assert
			Assert.AreEqual(newRecipient.Id, actualId);
			Assert.IsTrue(service.GetAll().Contains(newRecipient));

			if (service.GetById(newRecipient.Id) != newRecipient)
				throw new AssertFailedException("В сервисе под указанным идентификатором хранится неверная сущность");
		}

		[TestMethod]
		public void GetById_ReturnCorrectItem()
		{
			const string expected_name = "Test Recipient";
			const string expected_email = "Test email";
			var new_recipient = new Recipient
			{
				Name = expected_name,
				Email = expected_email
			};

			var service = new RecipientsDataInMemory();

			var max_id = service.GetAll().Max(recipient => recipient.Id);

			var actual_id = service.Add(new_recipient);

			var expected_id = max_id + 1;

			//Assert.AreEqual(expected_id, actual_id);
			//Assert.AreEqual(expected_id, new_recipient.Id);

			var actual_recipient = service.GetById(expected_id);

			Assert.AreEqual(new_recipient, actual_recipient);
		}

		[TestMethod, Timeout(150), ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void GetById_Thrown_ArgumentOutOfRangeException_OnNegativeId()
		{
			const int id = -5;

			var service = new RecipientsDataInMemory();

			service.GetById(id);
		}
	}
}
