
using MemoryPack;
using MessagePack;
using NewDatasourceTest;
using System.Diagnostics;
using System.Net.Http.Json;

var client = new HttpClient();
var sw = new Stopwatch();
sw.Start();
sw.Stop();
for (int k = 0; k < 2; k++)
{

    int step = 20;
    sw.Restart();
    for (int i = 0; i < step; i++)
    {
        var restapi = await client.GetFromJsonAsync<List<Yubin>>(@"https://musewiki.net/YubinAPI/restapi");
        Console.WriteLine(restapi!.Count);
    }
    sw.Stop();

    TimeSpan ts = sw.Elapsed;
    Console.WriteLine($"{ts.Hours}時間{ts.Minutes}分{ts.Seconds}秒{ts.Milliseconds}ミリ秒{ts.Nanoseconds}ns");

    sw.Restart();
    for (int i = 0; i < step; i++)
    {
        var messagePack = await client.GetByteArrayAsync(@"https://musewiki.net/YubinAPI/messagepack");
        var result = MessagePackSerializer.Deserialize<List<Yubin>>(messagePack);
        Console.WriteLine(result.Count);
    }
    sw.Stop();

    ts = sw.Elapsed;
    Console.WriteLine($"{ts.Hours}時間{ts.Minutes}分{ts.Seconds}秒{ts.Milliseconds}ミリ秒{ts.Nanoseconds}ns");

    sw.Restart();
    for (int i = 0; i < step; i++)
    {
        var memoryPack = await client.GetByteArrayAsync(@"https://musewiki.net/YubinAPI/memorypack");
        var result = MemoryPackSerializer.Deserialize<List<Yubin>>(memoryPack);
        Console.WriteLine(result!.Count);
    }
    sw.Stop();

    ts = sw.Elapsed;
    Console.WriteLine($"{ts.Hours}時間{ts.Minutes}分{ts.Seconds}秒{ts.Milliseconds}ミリ秒{ts.Nanoseconds}ns");

}
Console.ReadLine();

client.Dispose();