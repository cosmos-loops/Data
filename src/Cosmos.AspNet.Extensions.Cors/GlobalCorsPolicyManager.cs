﻿using System;
using System.Collections.Generic;

namespace Cosmos.AspNet.Extensions
{
    internal static class GlobalCorsPolicyManager
    {
        private static readonly Dictionary<string, CorsPolicy> _policyMap;
        private static string _defaultPolicyName;

        static GlobalCorsPolicyManager()
        {
            _policyMap = new Dictionary<string, CorsPolicy>();
        }

        /// <summary>
        /// 由 CorsCoreHelper 负责初始化
        /// </summary>
        /// <param name="options"></param>
        public static void SetPolicyMap(CorsOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var map = options.GetPolicyMap();
            _defaultPolicyName = options.DefaultPolicyName;

            foreach (var key in map.Keys)
            {
                var builder = new CorsPolicyBuilder(map[key]);
                _policyMap.Add(key, builder.Build());
            }

            if (!IsContainsPolicy(_defaultPolicyName))
            {
                _policyMap.Add(_defaultPolicyName, CorsPolicy.DefaultCorsPolicy);
            }
        }

        public static string DefaultPolicyName => _defaultPolicyName;

        public static bool IsContainsPolicy(string name) => _policyMap.ContainsKey(name);

        public static CorsPolicy GetPolicy(string name) => IsContainsPolicy(name) ? _policyMap[name] : null;

        public static CorsPolicy GetDefaultPolicy() => IsContainsPolicy(_defaultPolicyName) ? _policyMap[_defaultPolicyName] : CorsPolicy.DefaultCorsPolicy;
    }
}
