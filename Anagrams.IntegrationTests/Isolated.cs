using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Transactions;

namespace Anagrams.IntegrationTests
{
    public class Isolated : Attribute, ITestAction
    {
        private TransactionScope _transaction;
        public ActionTargets Targets => ActionTargets.Test;

        public void AfterTest(ITest test)
        {
            _transaction.Dispose();
        }

        public void BeforeTest(ITest test)
        {
            _transaction = new TransactionScope();
        }
    }
}
