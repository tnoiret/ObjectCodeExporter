using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectLiteralVisualizer;
using ObjectLiteralVisualizer.Test.EntitySource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectCodeExporter.Tests
{
    [TestClass()]
    public class ObjectCodeExporterTests
    {
        [TestMethod()]
        public void TranslateEmptyEntity()
        {
            string need = $"EmptyEntity emptyEntityTranslated0 = new EmptyEntity();{Environment.NewLine}";
            EmptyEntity ee = new EmptyEntity();

            ObjectCodeExporter olt = new ObjectCodeExporter();
            Assert.AreEqual(need, olt.Translate(ee));
        }


        [TestMethod()]
        public void TranslateSimpleEntity()
        {
            string need = $"Customer customerTranslated0 = new Customer();{Environment.NewLine}customerTranslated0.Id = new Guid(\"21947c10-0d93-4377-8587-bfe5d9638022\");{Environment.NewLine}customerTranslated0.FirstName = \"Thomas\";{Environment.NewLine}customerTranslated0.LastName = \"L\'asticot\";{Environment.NewLine}";
            Customer c = new Customer()
            {
                FirstName = "Thomas",
                LastName = "L'asticot",
                Id = new Guid("21947c10-0d93-4377-8587-bfe5d9638022")
            };

            ObjectCodeExporter olt = new ObjectCodeExporter();
            Assert.AreEqual(need, olt.Translate(c));
        }

        [TestMethod()]
        public void TranslateAllSimpleTypes()
        {
            string need = $"EntityWithAllBaseTypes entityWithAllBaseTypesTranslated0 = new EntityWithAllBaseTypes();{Environment.NewLine}entityWithAllBaseTypesTranslated0.Mp1 = null;{Environment.NewLine}entityWithAllBaseTypesTranslated0.Mp2 = \"Test Lol c\'est cool j\'ai même mis un \"\";{Environment.NewLine}entityWithAllBaseTypesTranslated0.Mp3 = 42;{Environment.NewLine}entityWithAllBaseTypesTranslated0.Mp4 = true;{Environment.NewLine}entityWithAllBaseTypesTranslated0.Mp5 = -2;{Environment.NewLine}entityWithAllBaseTypesTranslated0.Mp6 = 0x42;{Environment.NewLine}entityWithAllBaseTypesTranslated0.Mp7 = 25;{Environment.NewLine}entityWithAllBaseTypesTranslated0.Mp8 = 3;{Environment.NewLine}entityWithAllBaseTypesTranslated0.Mp9 = 0x43;{Environment.NewLine}entityWithAllBaseTypesTranslated0.Mp10 = 25.6d;{Environment.NewLine}entityWithAllBaseTypesTranslated0.Mp11 = 26.5m;{Environment.NewLine}entityWithAllBaseTypesTranslated0.Mp12 = 62.5f;{Environment.NewLine}entityWithAllBaseTypesTranslated0.Mp13 = -1265465654L;{Environment.NewLine}entityWithAllBaseTypesTranslated0.Mp14 = 156456465L;{Environment.NewLine}entityWithAllBaseTypesTranslated0.Mp15 = new DateTime(2017, 5, 27, 14, 32, 25, 122);{Environment.NewLine}entityWithAllBaseTypesTranslated0.Mp16 = new Guid(\"51326947-38ac-4300-a02c-ede7a9698a16\");{Environment.NewLine}";
            EntityWithAllBaseTypes ewabt = new EntityWithAllBaseTypes();
            ObjectCodeExporter olt = new ObjectCodeExporter();
            Assert.AreEqual(need, olt.Translate(ewabt));

            EntityWithChildEntity entityWithChildEntityTranslated = new EntityWithChildEntity();
            ChildEntity childEntityTranslated = new ChildEntity();
            entityWithChildEntityTranslated.MyProperty = childEntityTranslated;
            childEntityTranslated.MyProperty = 10;
        }

        [TestMethod()]
        public void TranslateEntityWithChildEntity()
        {
            string need = $"EntityWithChildEntity entityWithChildEntityTranslated0 = new EntityWithChildEntity();{Environment.NewLine}ChildEntity childEntityTranslated1 = new ChildEntity();{Environment.NewLine}childEntityTranslated1.MyProperty = 10;{Environment.NewLine}entityWithChildEntityTranslated0.MyProperty = childEntityTranslated1;{Environment.NewLine}";
            EntityWithChildEntity ewce = new EntityWithChildEntity();
            ObjectCodeExporter olt = new ObjectCodeExporter();
            Assert.AreEqual(need, olt.Translate(ewce));
        }

        [TestMethod()]
        public void TransltateEntityWithTwoLevelChildEntity()
        {
            string need = $"EntityWithTwoLevelChildEntity entityWithTwoLevelChildEntityTranslated0 = new EntityWithTwoLevelChildEntity();{Environment.NewLine}ChildEntityLevel1 childEntityLevel1Translated1 = new ChildEntityLevel1();{Environment.NewLine}childEntityLevel1Translated1.MyProperty = 10;{Environment.NewLine}ChildEntityLevel2 childEntityLevel2Translated2 = new ChildEntityLevel2();{Environment.NewLine}childEntityLevel2Translated2.MyProperty = 10;{Environment.NewLine}childEntityLevel1Translated1.MyProperty2 = childEntityLevel2Translated2;{Environment.NewLine}entityWithTwoLevelChildEntityTranslated0.MyProperty = childEntityLevel1Translated1;{Environment.NewLine}";
            EntityWithTwoLevelChildEntity ewtlce = new EntityWithTwoLevelChildEntity();
            ObjectCodeExporter olt = new ObjectCodeExporter();
            Assert.AreEqual(need, olt.Translate(ewtlce));
        }

        [TestMethod()]
        public void TranslateEntityWithListOfInt()
        {
            string need = $"EntityWithListOfInt entityWithListOfIntTranslated0 = new EntityWithListOfInt();{Environment.NewLine}entityWithListOfIntTranslated0.MyInt = 10;{Environment.NewLine}entityWithListOfIntTranslated0.MyIntList.Add(11);{Environment.NewLine}entityWithListOfIntTranslated0.MyIntList.Add(12);{Environment.NewLine}entityWithListOfIntTranslated0.MyIntList.Add(13);{Environment.NewLine}";
            EntityWithListOfInt ewloi = new EntityWithListOfInt();
            ewloi.MyInt = 10;
            ewloi.MyIntList.Add(11);
            ewloi.MyIntList.Add(12);
            ewloi.MyIntList.Add(13);

            ObjectCodeExporter olt = new ObjectCodeExporter();
            Assert.AreEqual(need, olt.Translate(ewloi));
        }

        [TestMethod()]
        public void TranslateEntityWithListOfChild()
        {
            string need = $"EntityWithListOfChild entityWithListOfChildTranslated0 = new EntityWithListOfChild();{Environment.NewLine}Child childTranslated1 = new Child();{Environment.NewLine}childTranslated1.MyInt = 11;{Environment.NewLine}entityWithListOfChildTranslated0.Childs.Add(childTranslated1);{Environment.NewLine}Child childTranslated2 = new Child();{Environment.NewLine}childTranslated2.MyInt = 12;{Environment.NewLine}entityWithListOfChildTranslated0.Childs.Add(childTranslated2);{Environment.NewLine}Child childTranslated3 = new Child();{Environment.NewLine}childTranslated3.MyInt = 13;{Environment.NewLine}entityWithListOfChildTranslated0.Childs.Add(childTranslated3);{Environment.NewLine}entityWithListOfChildTranslated0.MyIntToo = 10;{Environment.NewLine}";
            EntityWithListOfChild entityWithListOfChildTranslated0 = new EntityWithListOfChild();
            Child childTranslated1 = new Child();
            childTranslated1.MyInt = 11;
            entityWithListOfChildTranslated0.Childs.Add(childTranslated1);
            Child childTranslated2 = new Child();
            childTranslated2.MyInt = 12;
            entityWithListOfChildTranslated0.Childs.Add(childTranslated2);
            Child childTranslated3 = new Child();
            childTranslated3.MyInt = 13;
            entityWithListOfChildTranslated0.Childs.Add(childTranslated3);
            entityWithListOfChildTranslated0.MyIntToo = 10;

            ObjectCodeExporter olt = new ObjectCodeExporter();
            Assert.AreEqual(need, olt.Translate(entityWithListOfChildTranslated0));
        }

        [TestMethod()]
        public void TranslateEntityWithListOfChildWithListOfOtherChild()
        {
            string need = $"EntityWithListOfChildWithListOfOtherChild entityWithListOfChildWithListOfOtherChildTranslated0 = new EntityWithListOfChildWithListOfOtherChild();{Environment.NewLine}ChildWithListOfOtherChild childWithListOfOtherChildTranslated1 = new ChildWithListOfOtherChild();{Environment.NewLine}childWithListOfOtherChildTranslated1.MyInt = 11;{Environment.NewLine}OtherChild otherChildTranslated2 = new OtherChild();{Environment.NewLine}otherChildTranslated2.MyIntAgain = 42;{Environment.NewLine}childWithListOfOtherChildTranslated1.OtherChilds.Add(otherChildTranslated2);{Environment.NewLine}entityWithListOfChildWithListOfOtherChildTranslated0.Childs.Add(childWithListOfOtherChildTranslated1);{Environment.NewLine}entityWithListOfChildWithListOfOtherChildTranslated0.MyIntToo = 10;{Environment.NewLine}";
            EntityWithListOfChildWithListOfOtherChild entityWithListOfChildWithListOfOtherChildTranslated0 = new EntityWithListOfChildWithListOfOtherChild();
            ChildWithListOfOtherChild childWithListOfOtherChildTranslated1 = new ChildWithListOfOtherChild();
            childWithListOfOtherChildTranslated1.MyInt = 11;
            OtherChild otherChildTranslated2 = new OtherChild();
            otherChildTranslated2.MyIntAgain = 42;
            childWithListOfOtherChildTranslated1.OtherChilds.Add(otherChildTranslated2);
            entityWithListOfChildWithListOfOtherChildTranslated0.Childs.Add(childWithListOfOtherChildTranslated1);
            entityWithListOfChildWithListOfOtherChildTranslated0.MyIntToo = 10;

            ObjectCodeExporter olt = new ObjectCodeExporter();
            Assert.AreEqual(need, olt.Translate(entityWithListOfChildWithListOfOtherChildTranslated0));
        }

        [TestMethod()]
        public void TranslateEntityWithDictionary()
        {
            string need = $"EntityWithDictionary entityWithDictionaryTranslated0 = new EntityWithDictionary();{Environment.NewLine}entityWithDictionaryTranslated0.Dico.Add(\"Penny\", \"Leonard\");{Environment.NewLine}entityWithDictionaryTranslated0.Dico.Add(\"Sheldon\", \"Soft kitty\");{Environment.NewLine}";
            EntityWithDictionary entityWithDictionary0 = new EntityWithDictionary();
            entityWithDictionary0.Dico.Add("Penny", "Leonard");
            entityWithDictionary0.Dico.Add("Sheldon", "Soft kitty");

            ObjectCodeExporter olt = new ObjectCodeExporter();
            Assert.AreEqual(need, olt.Translate(entityWithDictionary0));
        }

        [TestMethod()]
        public void TranslateCollection()
        {
            string need = $"List<Customer> listCustomerTranslated0 = new List<Customer>();{Environment.NewLine}Customer customerTranslated1 = new Customer();{Environment.NewLine}customerTranslated1.Id = new Guid(\"00000000-0000-0000-0000-000000000000\");{Environment.NewLine}customerTranslated1.FirstName = null;{Environment.NewLine}customerTranslated1.LastName = null;{Environment.NewLine}listCustomerTranslated0.Add(customerTranslated1);{Environment.NewLine}Customer customerTranslated2 = new Customer();{Environment.NewLine}customerTranslated2.Id = new Guid(\"00000000-0000-0000-0000-000000000000\");{Environment.NewLine}customerTranslated2.FirstName = null;{Environment.NewLine}customerTranslated2.LastName = null;{Environment.NewLine}listCustomerTranslated0.Add(customerTranslated2);{Environment.NewLine}";
            List<Customer> test = new List<Customer>();
            test.Add(new Customer());
            test.Add(new Customer());

            ObjectCodeExporter olt = new ObjectCodeExporter();
            Assert.AreEqual(need, olt.Translate(test));
        }
    }
}