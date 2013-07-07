using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace WpfCommon {
    public static class Logger {
        private static StringBuilder _sb = new StringBuilder();
        private const bool BATCH_UPDATE = false;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void log(String s) {
            var foo = new StackFrame(1, true);
            string filename = foo.GetFileName();
            if (filename != null) {
                filename = filename.Substring(filename.LastIndexOf('\\') + 1);
                _sb.AppendFormat("\u2022LOG\u2022{0}({1}):{2}:{3}", filename, foo.GetFileLineNumber(), foo.GetMethod().Name, s);
            } else {
                _sb.Append("\u2022LOG\u2022" + s);
            }
            _sb.Append(Environment.NewLine);

            if (BATCH_UPDATE == false) {
                Console.Write(flush());
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void error(String s) {
            var foo = new StackFrame(1, true);
            string filename = foo.GetFileName();
            if (filename != null) {
                filename = filename.Substring(filename.LastIndexOf('\\') + 1);
                _sb.AppendFormat("\u2022ERROR\u2022{0}({1}):{2}:{3}", filename, foo.GetFileLineNumber(), foo.GetMethod().Name, s);
            } else {
                _sb.Append("\u2022LOG\u2022" + s);
            }
            _sb.Append(Environment.NewLine);

            if (BATCH_UPDATE == false) {
                Console.Write(flush());
            }
        }

        public static string flush() {
            String s = _sb.ToString();
            _sb.Clear();
            return s;
        }
    }
}
