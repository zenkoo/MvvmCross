// MvxSwissBindingTest.cs

// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
//
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

using MvvmCross.Binding.Parse.Binding;
using MvvmCross.Binding.Parse.Binding.Swiss;
using MvvmCross.Platform.Logging;
using MvvmCross.Platform.Platform;
using Xunit;

namespace MvvmCross.Binding.Test.Parse.Binding.Swiss
{
    
    public class MvxSwissBindingTest
        : MvxBaseSwissBindingTest<MvxSwissBindingParser>
    {
        [Fact]
        public void TestFunctionalValueConverterBinding()
        {
            var text = "Target ConvertThis(Foo)";
            var expected = new MvxSerializableBindingSpecification()
            {
                {
                    "Target",
                    new MvxSerializableBindingDescription()
                    {
                            Converter = "ConvertThis",
                            Function = "Single",
                            Sources = new MvxSerializableBindingDescription[]
                                {
                                    new MvxSerializableBindingDescription()
                                        {
                                            Path = "Foo",
                                        },
                                }
                    }
                }
            };
            MvxTestLog.Instance.Trace("Testing: {0}", text);
            PerformTest(text, expected);
        }

        [Fact]
        public void TestFunctionalValueConverterWithParameterBinding()
        {
            var text = "Target ConvertThis(Foo, 12)";
            var expected = new MvxSerializableBindingSpecification()
            {
                {
                    "Target",
                    new MvxSerializableBindingDescription()
                    {
                            Converter = "ConvertThis",
                            Function = "Single",
                            Sources = new MvxSerializableBindingDescription[]
                                {
                                    new MvxSerializableBindingDescription()
                                        {
                                            Path = "Foo",
                                        },
                                },
                            ConverterParameter = 12
                    }
                }
            };
            MvxTestLog.Instance.Trace("Testing: {0}", text);
            PerformTest(text, expected);
        }

        [Fact]
        public void TestFunctionalValueConverterWithParameterBinding2()
        {
            var text = "Target ConvertThis(Foo, 12.45)";
            var expected = new MvxSerializableBindingSpecification()
            {
                {
                    "Target",
                    new MvxSerializableBindingDescription()
                    {
                            Converter = "ConvertThis",
                            Function = "Single",
                            Sources = new MvxSerializableBindingDescription[]
                                {
                                    new MvxSerializableBindingDescription()
                                        {
                                            Path = "Foo",
                                        },
                                },
                            ConverterParameter = 12.45
                    }
                }
            };
            MvxTestLog.Instance.Trace("Testing: {0}", text);
            PerformTest(text, expected);
        }

        [Fact]
        public void TestFunctionalValueConverterWithParameterBinding3()
        {
            var text = "Target ConvertThis(Foo, true)";
            var expected = new MvxSerializableBindingSpecification()
            {
                {
                    "Target",
                    new MvxSerializableBindingDescription()
                    {
                            Converter = "ConvertThis",
                            Function = "Single",
                            Sources = new MvxSerializableBindingDescription[]
                                {
                                    new MvxSerializableBindingDescription()
                                        {
                                            Path = "Foo",
                                        },
                                },
                            ConverterParameter = true
                    }
                }
            };
            MvxTestLog.Instance.Trace("Testing: {0}", text);
            PerformTest(text, expected);
        }

        [Fact]
        public void TestFunctionalValueConverterWithParameterBinding4()
        {
            var text = "Target ConvertThis(Foo, 'Hello World')";
            var expected = new MvxSerializableBindingSpecification()
            {
                {
                    "Target",
                    new MvxSerializableBindingDescription()
                    {
                            Converter = "ConvertThis",
                            Function = "Single",
                            Sources = new MvxSerializableBindingDescription[]
                                {
                                    new MvxSerializableBindingDescription()
                                        {
                                            Path = "Foo",
                                        },
                                },
                            ConverterParameter = "Hello World"
                    }
                }
            };
            MvxTestLog.Instance.Trace("Testing: {0}", text);
            PerformTest(text, expected);
        }

        [Fact]
        public void TestFunctionalValueConverterWithNullInTheName()
        {
            var text = "Target NullThis(Foo, 'Hello World')";
            var expected = new MvxSerializableBindingSpecification()
            {
                {
                    "Target",
                    new MvxSerializableBindingDescription()
                    {
                            Converter = "NullThis",
                            Function = "Single",
                            Sources = new MvxSerializableBindingDescription[]
                                {
                                    new MvxSerializableBindingDescription()
                                        {
                                            Path = "Foo",
                                        },
                                },
                            ConverterParameter = "Hello World"
                    }
                }
            };
            MvxTestLog.Instance.Trace("Testing: {0}", text);
            PerformTest(text, expected);
        }
    }
}
