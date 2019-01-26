﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using MrCMS.Helpers;
using MrCMS.Messages;

namespace MrCMS.Services
{
    public class MessageTemplateParser : IMessageTemplateParser
    {
        private static readonly MethodInfo GetMessageTemplateMethod = typeof(MessageTemplateParser)
            .GetMethodExt("GetAllTokens");

        private readonly IServiceProvider _serviceProvider;

        public MessageTemplateParser(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public string Parse<T>(string template, T instance)
        {
            var stringBuilder = new StringBuilder(template);

            ApplyTypeSpecificTokens(instance, stringBuilder);

            ApplySystemWideTokens(stringBuilder);

            return stringBuilder.ToString();
        }

        public string Parse(string template)
        {
            var stringBuilder = new StringBuilder(template);

            ApplySystemWideTokens(stringBuilder);

            return stringBuilder.ToString();
        }

        private void ApplySystemWideTokens(StringBuilder stringBuilder)
        {
            var providers = _serviceProvider.GetServices<ITokenProvider>();
            foreach (var token in providers.SelectMany(provider => provider.Tokens))
            {
                stringBuilder.Replace("{" + token.Key + "}", token.Value());
            }
        }

        private void ApplyTypeSpecificTokens<T>(T instance, StringBuilder stringBuilder)
        {
            IEnumerable<ITokenProvider<T>> tokenProviders = _serviceProvider.GetServices<ITokenProvider<T>>();

            foreach (var token in tokenProviders.SelectMany(tokenProvider => tokenProvider.Tokens))
            {
                stringBuilder.Replace("{" + token.Key + "}", token.Value(instance));
            }
        }

        public List<string> GetAllTokens<T>()
        {
            IEnumerable<ITokenProvider<T>> tokenProviders = _serviceProvider.GetServices<ITokenProvider<T>>();
            return tokenProviders.SelectMany(provider => provider.Tokens.Select(pair => pair.Key)).ToList();
        }

        public List<string> GetAllTokens(MessageTemplate template)
        {
            var tokens = new List<string>();
            tokens.AddRange(GetAllTokens(template.ModelType));
            tokens.AddRange(GetAllStandardTokens());
            return tokens;
        }

        private List<string> GetAllTokens(Type type)
        {
            if (type == null)
                return new List<string>();
            return GetMessageTemplateMethod.MakeGenericMethod(type).Invoke(this, new object[] { }) as List<string>;
        }

        public List<string> GetAllStandardTokens()
        {
            IEnumerable<ITokenProvider> tokenProviders = _serviceProvider.GetServices<ITokenProvider>();
            return tokenProviders.SelectMany(provider => provider.Tokens.Select(pair => pair.Key)).ToList();
        }
    }
}