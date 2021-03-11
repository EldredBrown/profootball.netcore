﻿using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Decorators;

namespace EldredBrown.ProFootball.NETCore.Services
{
    public class NullGameStrategy : ProcessGameStrategyBase
    {
        private static NullGameStrategy _instance;

        private NullGameStrategy()
            : base(null)
        {
        }

        public static NullGameStrategy Instance
        {
            get
            {
                if (_instance is null)
                {
                    _instance = new NullGameStrategy();
                }

                return _instance;
            }
        }

        // Do nothing methods, for this is an implementation of the Null Object Pattern.
        public override async Task ProcessGame(IGameDecorator gameDecorator)
        {}
    }
}