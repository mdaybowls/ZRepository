﻿using System.Collections.Generic;
using System.Data.Entity;
using Moq;

namespace ZRepository.Testing
{
    public class MockedDbContext<T> : Mock<T> where T : DbContext
    {
        public Dictionary<string, object> Tables
        {
            get { return _tables ?? (_tables = new Dictionary<string, object>()); }
        }

        private Dictionary<string, object> _tables;
    }
}
