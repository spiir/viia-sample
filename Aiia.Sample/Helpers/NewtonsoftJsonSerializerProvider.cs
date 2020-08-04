using System;
using System.Linq;
using Aiia.Sample.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NodaTime;
using NodaTime.Serialization.JsonNet;

namespace Aiia.Sample.Helpers
{
    internal static class NewtonsoftJsonSerializerProvider
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings().ConfigureResolver(new CamelCasePropertyNamesContractResolver())
                                                                                             .ConfigureForNodaTime(DateTimeZoneProviders.Tzdb)
                                                                                             .ConfigureConverters(typeof(StringEnumConverter))
                                                                                             .WithIsoDateIntervalConverter()
                                                                                             .WithIsoIntervalConverter();

        public static readonly JsonSerializer Instance = JsonSerializer.CreateDefault(Settings);


        public static JsonSerializerSettings Configure(this JsonSerializerSettings settings)
        {
            settings.Converters = Settings.Converters;
            settings.DateParseHandling = Settings.DateParseHandling;
            return settings;
        }

        private static JsonSerializerSettings ConfigureConverters(this JsonSerializerSettings settings, params Type[] converterTypes)
        {
            var instantConverters = settings.Converters.Where(converter => converter is NodaPatternConverter<Instant>).ToList();
            instantConverters.ForEach(converter => settings.Converters.Remove(converter));

            converterTypes.ForEach(converter => settings.Converters.Add(( JsonConverter ) Activator.CreateInstance(converter)));
            return settings;
        }

        private static JsonSerializerSettings ConfigureResolver(this JsonSerializerSettings settings, IContractResolver resolver)
        {
            settings.ContractResolver = resolver;
            return settings;
        }
    }
}
