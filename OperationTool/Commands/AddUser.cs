﻿using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using GoldMine.DataModel;
using GoldMine.ServerBase;
using System;

namespace GoldMine.OperationTool.Commands
{
    public class AddUser : Command
    {
        public override bool IsRunnable(Options args)
        {
            return args.AddUser;
        }

        public override ConsoleKey InvokeKey
        {
            get
            {
                return ConsoleKey.A;
            }
        }

        public override void Run()
        {
            var req = new UpdateItemRequest()
            {
                TableName = "counters",
                UpdateExpression = "ADD id :incr",
                ReturnValues = new ReturnValue("UPDATED_NEW")
            };
            req.Key.Add("table_name", new AttributeValue("user"));
            req.ExpressionAttributeValues.Add(":incr", new AttributeValue() { N = "1" });
            req.UpdateExpression = "ADD id :incr";

            var resp = DynamoDBClient.Instance.UpdateItem(req);
            short newId = short.Parse(resp.Attributes["id"].N);

            using (var ctx = new DynamoDBContext(DynamoDBClient.Instance))
            {
                User user = new User()
                {
                    id = newId,
                    accesskey = Guid.NewGuid()
                };
                ctx.Save(user);
            }
            Console.WriteLine("User [" + newId + "] has been added");

            if (Program.IsInteractive)
                Program.ToMainMenu();
        }
    }
}