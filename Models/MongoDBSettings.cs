﻿using System;
namespace TodoList.Models
{
	public class MongoDBSettings
	{
        public string ConnectionURI { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string CollectionName { get; set; } = null!;
    }
}

