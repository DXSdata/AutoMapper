using System;
using System.Collections.Generic;
using System.Text;

namespace MapperTest
{
    class MyClass1
    {
        public string Var1 { get; set; }
        public string Var2 { get; set; }
        public string Var3 { get; set; }
        public string Var4 { get; set; }
        public string Var5 { get; set; }

        public byte[] BytesEmpty { get; set; } = new byte[0];
        public byte[] BytesNull { get; set; } = null;
    }
}
