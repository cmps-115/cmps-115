using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DeepCopy
{
    public static T Copy<T>(T obj)
    {
        object copy = null;
        using (var ms = new MemoryStream())
        {
            var formatter = new BinaryFormatter();
            var ss = new SurrogateSelector();
            var vec2S = new Vector2SerializationSurrogate();
            ss.AddSurrogate(typeof(Vector2), new StreamingContext(StreamingContextStates.All), vec2S);
            formatter.SurrogateSelector = ss;
            formatter.Serialize(ms, obj);
            ms.Position = 0;
            copy = (T)formatter.Deserialize(ms);
            ms.Close();
        }
        return (T)copy;
    }

}

public class Vector2SerializationSurrogate : ISerializationSurrogate
{
    public void GetObjectData(System.Object obj, SerializationInfo info, StreamingContext context)
    {
        Vector2 v2 = (Vector2)obj;
        info.AddValue("x", v2.x);
        info.AddValue("y", v2.y);
    }

    public System.Object SetObjectData(System.Object obj, SerializationInfo info,
                                       StreamingContext context, ISurrogateSelector selector)
    {
        Vector2 v2 = (Vector2)obj;
        v2.x = (float)info.GetValue("x", typeof(float));
        v2.y = (float)info.GetValue("y", typeof(float));
        obj = v2;
        return obj;
    }
}