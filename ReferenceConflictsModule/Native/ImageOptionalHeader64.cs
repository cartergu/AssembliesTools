﻿using System.Runtime.InteropServices;

namespace ReferenceConflictsModule.Native
{
    [StructLayout(LayoutKind.Explicit)]
    internal struct ImageOptionalHeader64
    {
        [FieldOffset(0)]
        public ushort Magic;
        [FieldOffset(224)]
        public ImageDataDirectory DataDirectory;
    }
}