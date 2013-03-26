﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeometryConverter.cs" company="Jörg Battermann">
//   Copyright © Jörg Battermann 2011
// </copyright>
// <summary>
//   Defines the GeometryConverter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GeoJSON.Net.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using GeoJSON.Net.Geometry;

    using Newtonsoft.Json;

    /// <summary>
    /// Defines the GeometryObject type. Converts to/from a SimpleGeo 'geometry' field
    /// </summary>
    public class GeometryConverter : JsonConverter
    {
        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter"/> to write to.</param><param name="value">The value.</param><param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is LineString)
            {
                var lineString = value as LineString;
                writer.WriteStartObject();

                // var coords = lineString.Coordinates.Select(x => new[] { x.Longitude, x.Latitude }).ToArray();

                writer.WritePropertyName("type");
                serializer.Serialize(writer, lineString.Type.ToString());

                writer.WritePropertyName("coordinates");
                serializer.Serialize(writer, lineString.Coordinates);

                writer.WriteEndObject();
                return;
            }

            if (value is Point)
            {
                var point = value as Point;
                writer.WriteStartObject();

                var coords = point.Coordinates.Select(x => new[] { x.Longitude, x.Latitude }).First();

                writer.WritePropertyName("type");
                serializer.Serialize(writer, point.Type.ToString());

                writer.WritePropertyName("coordinates");
                serializer.Serialize(writer, coords);


                writer.WriteEndObject();
                return;
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader"/> to read from.</param><param name="objectType">Type of the object.</param><param name="existingValue">The existing value of object being read.</param><param name="serializer">The calling serializer.</param>
        /// <returns>
        /// The object value.
        /// </returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // ToDo: implement
            throw new NotImplementedException();
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            if (objectType.IsInterface)
            {
                return objectType == typeof(IGeometryObject);
            }

            return objectType.GetInterface(typeof(IGeometryObject).Name, true) != null;
        }
    }
}
