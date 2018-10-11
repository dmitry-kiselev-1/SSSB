using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using China.ICBC.SWIFT;
using China.ICBC.SWIFT.Fields;
using China.ICBC.SWIFT.Fields.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.UnitTests
{
    [TestClass]
    [Synchronization]
    public class MT103MessagesTest : ContextBoundObject
    {
        public const string OUTPUT_DIRECTORY = @"\\ftp\China\IN\test";
        //public const string CONNECTION_STRING = @"data source=ZEUS;initial catalog=Snova_table_СССБ;integrated security=True;multipleactiveresultsets=True;App=EntityFramework";
        public const string CONNECTION_STRING = @"data source=APOLON;initial catalog=Snova_table_СССБ;integrated security=True;multipleactiveresultsets=True;App=EntityFramework";

        [TestMethod]
        public void MessageATest()
        {
            try
            {
                var message = GetNewA();
                string result = message.ToSwift();
                message.Save(CONNECTION_STRING);    
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [TestMethod]
        public void MessageBTest()
        {
            try
            {
                var message = GetNewB();
                string result = message.ToSwift();
                message.Save(CONNECTION_STRING);
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [TestMethod]
        public void MessageCTest()
        {
            try
            {
                var message = GetNewC();
                string result = message.ToSwift();
                message.Save(CONNECTION_STRING);
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [TestMethod]
        public void DeserializeTest()
        {
            try
            {
                var message = GetNewA();

                MemoryStream memoryStream = new MemoryStream();
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, message);

                memoryStream.Position = 0;

                byte[] bites = memoryStream.GetBuffer();

                var newObject = binaryFormatter.Deserialize(new MemoryStream(bites)); //(memoryStream);

                Assert.IsTrue(newObject.GetType() == message.GetType());
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [TestMethod]
        public void TimeCheckLoadTest()
        {
            try
            {
                var timer = System.Diagnostics.Stopwatch.StartNew();

                List<Task> taskCol = new List<Task>();

                for (int i = 0; i < 100; i++)
                {
                    GetNewA().Save(CONNECTION_STRING);
                    GetNewB().Save(CONNECTION_STRING);
                }

                timer.Stop();

                Trace.WriteLine(string.Empty);
                Trace.WriteLine("Обработка заняла времени: " + timer.Elapsed);
                Trace.WriteLine(string.Empty);

                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [TestMethod]
        public void SendTestMany()
        {
            try
            {
                //for (int i = 0; i < 100; i++)
                //{
                    //Thread.Sleep(new TimeSpan(0, 0, 0));
                    MT103.Send(CONNECTION_STRING, new DirectoryInfo(OUTPUT_DIRECTORY), 1000, 1);
                //}

                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [TestMethod]
        public void SendTestSingle()
        {
            try
            {
                MT103.Send(CONNECTION_STRING, new DirectoryInfo(OUTPUT_DIRECTORY), "ССС140118000001");
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        private MessageA GetNewA()
        {
            return new MessageA
                    (
                        new Amount(100.51M),
                        new Sender(
                            "SenderFirstName SenderSecondName SenderThirdName",
                            new PassportData(series: "9003", number: "623901", country: Enums.Country.RUS),
                            new AddressData(building: "1а кв.11", street: "ул. 7-я Парковая", city: "г. Москва", country: "RUS")
                        ),
                        new CardBeneficiary(
                            accountNumber:  "1234567890123456",
                            name1:          new NameWithHieroglyph("BeneficiaryName1"),
                            name2:          new NameWithHieroglyph("BeneficiaryName2"),
                            name3:          new NameWithHieroglyph("BeneficiaryName3"),
                            name4:          new NameWithHieroglyph("BeneficiaryName4"),
                            address:        new AddressData(building: "111", street: "Xidan", city: "Beijing", country: "CHN")
                         ),
                        new CardSenderToReceiverInformation(
                            beneficiaryPhone: new PhoneData(Enums.CountryPhoneCode.CHN, "123456789012")
                            )
                    );    
        }

        private MessageB GetNewB()
        {
            return new MessageB
                    (
                        new Amount(100.51M),
                        new Sender(
                            "SenderFirstName SenderSecondName SenderThirdName",
                            new PassportData(series: "9003", number: "623901", country: Enums.Country.RUS),
                            new AddressData(building: "1а кв.11", street: "ул. 7-я Парковая", city: "г. Москва", country: "RUS")
                        ),
                        new CashBranchSwiftCode(ProvincialBranch.GetAllProvincialBranch(CONNECTION_STRING).First()),
                        new CashBeneficiary(
                            name1:      new NameWithHieroglyph("BeneficiaryName1"),
                            name2:      new NameWithHieroglyph("BeneficiaryName2"),
                            name3:      new NameWithHieroglyph("BeneficiaryName3"),
                            name4:      new NameWithHieroglyph("BeneficiaryName4"),
                            address:    new AddressData(building: "111", street: "Xidan", city: "Beijing", country: "CHN"),
                            passport:   new PassportData("", "123456789012345678", Enums.Country.CHN)),
                        new CashSenderToReceiverInformation(
                            beneficiaryPhone: new PhoneData(Enums.CountryPhoneCode.CHN, "1234567890"),
                            //provincialBranch: new ProvincialBranch("ProvincialBranchName", "ProvincialBranchAdderess", new SwiftCode("1234567890")))
                            provincialBranch: ProvincialBranch.GetAllProvincialBranch(CONNECTION_STRING).First())
                    );
        }

        private MessageC GetNewC()
        {
            return new MessageC
                    (
                        new Amount(100.51M),
                        new Sender(
                            "SenderFirstName SenderSecondName SenderThirdName",
                            new PassportData(series: "9003", number: "623901", country: Enums.Country.RUS),
                            new AddressData(building: "1а кв.11", street: "ул. 7-я Парковая", city: "г. Москва", country: "RUS")
                        ),
                        new CardBeneficiary(
                            accountNumber:  "1234567890123456",
                            name1:          new NameWithHieroglyph("BeneficiaryName1", "1000", ""),
                            name2:          new NameWithHieroglyph("BeneficiaryName2", "2000", ""),
                            name3:          new NameWithHieroglyph("BeneficiaryName3", "3000", ""),
                            name4:          new NameWithHieroglyph("BeneficiaryName3", "4000", ""),
                            address:        new AddressData(building: "111", street: "Xidan", city: "Beijing", country: "CHN")
                         ),
                        new CardRateSenderToReceiverInformation(
                            beneficiaryPhone: new PhoneData(Enums.CountryPhoneCode.CHN, "123456789012"),
                            rate: 6.1274M,
                            amount: 100.51M)
                    );
        }

    }
}
