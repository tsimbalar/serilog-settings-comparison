#r ".\TestDummies.dll"
using TestDummies;

LoggerConfiguration
    .WriteTo.DummyWithComplexParams(
        poco: new Poco()
        {
            StringProperty = "myString",
            IntProperty = 42,
            Nested = new SubPoco()
            {
                SubProperty = "Sub"
            }
        },
        intArray: new[] { 2, 4, 6 },
        stringArray: new[] { "one", "two", "three" },
        objArray: new SubPoco[]
            {
                new SubPoco()
                {
                    SubProperty = "Sub1"
                },
                new SubPoco()
                {
                    SubProperty = "Sub2"
                }
            }
    );
