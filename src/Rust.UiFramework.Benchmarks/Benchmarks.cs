﻿
using System.Text;
using BenchmarkDotNet.Attributes;
using Oxide.Ext.UiFramework.Builder.UI;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Game.Rust.Cui;

namespace Rust.UiFramework.Benchmarks
{
    [MemoryDiagnoser]
    public class Benchmarks
    {
        private const int Iterations = 100;
        private readonly List<string> _oxideMins = new List<string>();
        private readonly List<string> _oxideMaxs = new List<string>();
        private readonly List<UiPosition> _frameworkPos = new List<UiPosition>();
        private readonly Random _random = new Random();
        public readonly byte[] Buffer = new byte[1024 * 1024];
        private CuiElementContainer _oxideContainer;
        private string _oxideJson;
        private UiBuilder _builder;
        //private UiBuilder _randomBuilder;
        private JsonFrameworkWriter _writer;
        //private JsonFrameworkWriter _randomWriter;

        [GlobalSetup]
        public void Setup()
        {
            for (int i = 0; i < Iterations; i++)
            {
                float xMin = (float)_random.NextDouble();
                float xMax = (float)_random.NextDouble();
                float yMin = (float)_random.NextDouble();
                float yMax = (float)_random.NextDouble();
                _oxideMins.Add($"{xMin} {yMin}");
                _oxideMaxs.Add($"{xMax} {yMax}");
                _frameworkPos.Add(new UiPosition(xMin, yMin, xMax, yMax));
            }
            
            _oxideContainer = GetOxideContainer();
            _oxideJson = _oxideContainer.ToJson();
            _builder = GetFrameworkBuilder();
            //_randomBuilder = GetRandomPositionBuilder();
            _writer = _builder.CreateWriter();
            //_randomWriter = _randomBuilder.CreateWriter();
        }

        [Benchmark]
        public CuiElementContainer Oxide_CreateContainer()
        {
            return GetOxideContainer();
        }
        
        [Benchmark]
        public UiBuilder UiFramework_CreateContainer()
        {
            UiBuilder builder = GetFrameworkBuilder();
            builder.Dispose();
            return builder;
        }
        
        [Benchmark]
        public string Oxide_CreateJson()
        {
            return _oxideContainer.ToJson();
        }
        
        [Benchmark]
        public JsonFrameworkWriter UiFramework_CreateJson()
        {
            JsonFrameworkWriter writer = _builder.CreateWriter();
            writer.Dispose();
            return writer;
        }
        
        [Benchmark]
        public byte[] Oxide_EncodeJson()
        {
            return Encoding.UTF8.GetBytes(_oxideJson);
        }
        
        [Benchmark]
        public int UiFramework_EncodeJson()
        {
            int count = _writer.WriteTo(Buffer);
            return count;
        }
        
        [Benchmark]
        public byte[] Oxide_Full()
        {
            var builder = GetOxideContainer();
            string json = builder.ToJson();
            return Encoding.UTF8.GetBytes(json);
        }
        
        [Benchmark(Baseline = true)]
        public int UiFramework_Full()
        {
            UiBuilder builder = GetFrameworkBuilder();
            int count = builder.WriteBuffer(Buffer);
            builder.Dispose();
            return count;
        }

        private CuiElementContainer GetOxideContainer()
        {
            CuiElementContainer container = new CuiElementContainer();
            for (int i = 0; i < Iterations; i++)
            {
                container.Add(new CuiPanel
                {
                    Image =
                    {
                        Color = "1.0 1.0 1.0 1.0"
                    },
                    RectTransform =
                    {
                        AnchorMin = _oxideMins[i],
                        AnchorMax = _oxideMaxs[i]
                    }
                });
            }

            return container;
        }

        private UiBuilder GetFrameworkBuilder()
        {
            UiBuilder builder = UiBuilder.Create(UiPosition.Full, UiColor.Clear, "123");
            builder.EnsureCapacity(Iterations);
            for (int i = 0; i < Iterations - 1; i++)
            {
                builder.Panel(builder.Root, _frameworkPos[i], UiColor.Black);
            }

            return builder;
        }
    }
}