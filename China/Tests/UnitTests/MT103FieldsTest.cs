using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using China.ICBC;
using China.ICBC.SWIFT;
using China.ICBC.SWIFT.Fields;
using China.ICBC.SWIFT.Fields.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.UnitTests
{
    [TestClass]
    public class MT103FieldsTest
    {
        public const string CONNECTION_STRING = @"data source=ZEUS;initial catalog=Snova_table_СССБ;integrated security=True;multipleactiveresultsets=True;App=EntityFramework";
        //public const string CONNECTION_STRING = @"data source=APOLON;initial catalog=Snova_table_СССБ;integrated security=True;multipleactiveresultsets=True;App=EntityFramework";

        [TestMethod]
        public void TransactionNumberTest()
        {
            try
            {
                var field = new TransactionNumber();

                string result, message;
                Assert.IsTrue(field.Check(out result, out message));
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [TestMethod]
        public void BankOperationCodeTest()
        {
            try
            {
                var field = new BankOperationCode();

                string result, message;
                Assert.IsTrue(field.Check(out result, out message));
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [TestMethod]
        public void AmountTest()
        {
            try
            {
                var field = new Amount(100M);

                string result, message;
                Assert.IsTrue(field.Check(out result, out message));
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [TestMethod]
        public void SenderTest()
        {
            try
            {
                var field = new Sender(
                            "Дмитриева Светлана Сергеевна",
                            new PassportData(series: "9003", number: "623901", country: Enums.Country.RUS),
                            new AddressData(building: "173", street: "ул. Ардонская", city: "г. Владикавказ", country: "RUS")
                        );

                string result, message;
                Assert.IsTrue(field.Check(out result, out message));
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [TestMethod]
        public void HeadSwiftCodeTest()
        {
            try
            {
                var field = new HeadSwiftCode();

                string result, message;
                Assert.IsTrue(field.Check(out result, out message));
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [TestMethod]
        public void CardBranchSwiftCodeTest()
        {
            try
            {
                var field = new CardBranchSwiftCode();

                string result, message;
                Assert.IsTrue(field.Check(out result, out message));
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [TestMethod]
        public void CashBranchSwiftCodeTest()
        {
            try
            {
                var field = new CashBranchSwiftCode(ProvincialBranch.GetAllProvincialBranch(CONNECTION_STRING).First());

                string result, message;
                Assert.IsTrue(field.Check(out result, out message));
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }


        [TestMethod]
        public void CardBeneficiaryTest()
        {
            try
            {
                var field = new CardBeneficiary(
                    accountNumber: "1234567890123456",
                    name1: new NameWithHieroglyph("name1", "1000", string.Empty),
                    name2: new NameWithHieroglyph("name2", "2000", string.Empty),
                    name3: new NameWithHieroglyph("name3", "3000", string.Empty),
                    name4: new NameWithHieroglyph("name4", "4000", string.Empty),
                    address: new AddressData("11", "Улица такая-то", "Иваново", "RUS")
                );

                string result, message;
                Assert.IsTrue(field.Check(out result, out message));
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [TestMethod]
        public void CashBeneficiaryTest()
        {
            try
            {
                var field = new CashBeneficiary(
                    name1: new NameWithHieroglyph("name1", "1000", string.Empty),
                    name2: new NameWithHieroglyph("name2", "2000", string.Empty),
                    name3: new NameWithHieroglyph("name3", "3000", string.Empty),
                    name4: new NameWithHieroglyph("name4", "4000", string.Empty),
                    address: new AddressData("11", "Улица такая-то", "Иваново", "RUS"),
                    passport: new PassportData("", "123456789012345", Enums.Country.CHN)
                );

                string result, message;
                Assert.IsTrue(field.Check(out result, out message));
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [TestMethod]
        public void CardPaymentInformationTest()
        {
            try
            {
                var field = new CardPaymentInformation();

                string result, message;
                Assert.IsTrue(field.Check(out result, out message));
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [TestMethod]
        public void CashPaymentInformationTest()
        {
            try
            {
                var field = new CashPaymentInformation();

                string result, message;
                Assert.IsTrue(field.Check(out result, out message));
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [TestMethod]
        public void ExpensesDetailTest()
        {
            try
            {
                var field = new ExpensesDetail();

                string result, message;
                Assert.IsTrue(field.Check(out result, out message));
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [TestMethod]
        public void CardSenderToReceiverInformationTest()
        {
            try
            {
                var field = new CardSenderToReceiverInformation(
                    beneficiaryPhone: new PhoneData(Enums.CountryPhoneCode.CHN, "123456789"));

                string result, message;
                Assert.IsTrue(field.Check(out result, out message));
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [TestMethod]
        public void CashSenderToReceiverInformationTest()
        {
            try
            {
                var field = new CashSenderToReceiverInformation(
                    beneficiaryPhone: new PhoneData(Enums.CountryPhoneCode.CHN, "123456789"),
                    provincialBranch: ProvincialBranch.GetAllProvincialBranch(CONNECTION_STRING).First());

                string result, message;
                Assert.IsTrue(field.Check(out result, out message));
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [TestMethod]
        public void GetProvincialBranchTest()
        {
            try
            {
                var result = ProvincialBranch.GetProvincialBranch("CHONGQING MUNICIPAL BRANCH", CONNECTION_STRING);
                Assert.IsTrue(!string.IsNullOrEmpty(result.Swift.Code));
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }
    }
}
