// **************************************************************************
// File:  sharedmemoryserver.cs
// Created:  11/30/2009 Copyright (c) 2009
//
// Description: The Shared Memory Server is responsible for creating (if one
// does not exist) or opening a Shared Memory file.
//
// **************************************************************************
#define TRACE_SM_ACCESS

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DELTA_Form
{
    public class SharedMemServer : IDisposable
    {
        //public const UInt16 MAPSIZE = 16384;
        public const UInt16 MAPSIZE = UInt16.MaxValue;

        // Handle to the shared memory
        private IntPtr hMem = new IntPtr(0);

        // Pointer to mapped shared memory
        public unsafe byte* pMem = null;

        //private string msg = "";

        // Name based on product name for shared objects.
        public string memName;

        ~SharedMemServer()
        {
            Close();
        }

        // *************************************************************************************
        public unsafe bool Open(string strName)
        {
            // Close file if already open
            if (IsOpen())
            {
                Close();
            }
            memName = strName;
            memName += "_sm";

            Debug.Assert(hMem == IntPtr.Zero);
            Debug.Assert(pMem == null);

            // Try to open existing file
            if (!_Open())
            {
                // Create if none exist
                if (!_Create())
                {
                    return false;
                }
            }

            Debug.Assert(!(hMem == IntPtr.Zero));

            // Map the transport memory
            // Obtain a read/write map for the entire file
            pMem = (byte*)MapViewOfFile(hMem, FileRights.ReadWrite, 0, 0, 0);

            if (pMem == null)
            {
                Trace.WriteLine
                  ("Attempt to map shared memory failed: " + Marshal.GetLastWin32Error());
                Close();
                return false;
            }

            Trace.WriteLine("Shared memory endpoint created\n");
            return true;
        } // Open (string strName)

        // *************************************************************************************
        public unsafe void Close()
        {
            if (pMem != null)
            {
                UnmapViewOfFile((System.IntPtr)pMem);
                pMem = null;
            }

            // Close the transport mechanism
            if (hMem != IntPtr.Zero)
            {
                CloseHandle(hMem);
                hMem = IntPtr.Zero;
            }
        } // Close()

        // *************************************************************************************
        // Returns true if Open() was successfully called.
        public bool IsOpen()
        {
            return !(hMem == IntPtr.Zero);
        }

        // *************************************************************************************
        private bool _Create()
        {
            string strName = "";

            int nAttempt = 0;

            do
            {
                // First try the global namespace
                if (nAttempt == 0)
                    strName = "Global\\" + memName.ToString();

                // Next the the local namespace
                else if (nAttempt == 1)
                    strName = "Local\\" + memName.ToString();

                hMem = CreateFileMapping(NoFileHandle, 0,
                                                FileProtection.ReadWrite,
                                                0, MAPSIZE, strName);
                /* 2018/03/27 Unknown Error !!!
				if (hMem == IntPtr.Zero)
					throw new Exception
					  ("Open/create error: " + Marshal.GetLastWin32Error());
                      */
            } while ((hMem == IntPtr.Zero) && ++nAttempt < 2);

            if (hMem == IntPtr.Zero)
            {
                Trace.WriteLine("Failed to create shared memory object!");
                return false;
            }

            Trace.WriteLine("Shared memory object created as " + strName);
            return true;
        } // _Create()

        // *************************************************************************************
        private bool _Open()
        {
            int nSession = -2;
            string strName = "";

            do
            {
                // Try the global table ( is a service or at least running with admin rights)
                if (nSession == -2)
                    strName = "Global\\" + memName.ToString();

                // Try the local session (runtime is in the same user session)
                else if (nSession == -1)
                    strName = "Local\\" + memName.ToString();

                // Try session-specific (admin and runtime are in different local user sessions)
                else
                    strName = "Session\\" + nSession.ToString() + memName.ToString();

                hMem = OpenFileMapping(FileRights.ReadWrite, false, strName);
            } while ((hMem == IntPtr.Zero) && nSession++ < 255);

            if (hMem != IntPtr.Zero)
            {
                Trace.WriteLine("Shared memory opened as {0}", strName);
                return true;
            }

            return false;
        } // _Open()

        // *************************************************************************************
        private enum FileProtection : uint      // constants from winnt.h
        {
            ReadOnly = 2,
            ReadWrite = 4
        }

        // *************************************************************************************
        private enum FileRights : uint          // constants from WinBASE.h
        {
            Read = 4,
            Write = 2,
            ReadWrite = Read + Write
        }

        private static readonly IntPtr NoFileHandle = new IntPtr(-1);

        // *************************************************************************************
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr CreateFileMapping(IntPtr hFile,
                                                int lpAttributes,
                                                FileProtection flProtect,
                                                uint dwMaximumSizeHigh,
                                                uint dwMaximumSizeLow,
                                                string lpName);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr OpenFileMapping(FileRights dwDesiredAccess,
                                              bool bInheritHandle,
                                              string lpName);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr MapViewOfFile(IntPtr hFileMappingObject,
                                            FileRights dwDesiredAccess,
                                            uint dwFileOffsetHigh,
                                            uint dwFileOffsetLow,
                                            uint dwNumberOfBytesToMap);

        [DllImport("Kernel32.dll")]
        private static extern bool UnmapViewOfFile(IntPtr map);

        [DllImport("kernel32.dll")]
        private static extern int CloseHandle(IntPtr hObject);

        // *************************************************************************************
        public unsafe IntPtr Root
        {
            get
            {
                return ((System.IntPtr)pMem);
            }
        }

        // *************************************************************************************
        public unsafe void Dispose()
        {
            if (pMem != null)
            {
                UnmapViewOfFile((System.IntPtr)pMem);
            }

            if (hMem != IntPtr.Zero)
            {
                CloseHandle(hMem);
            }
            pMem = null;
            hMem = IntPtr.Zero;
        } // Dispose()
    } // class SharedMemServer : IDisposable
} // namespace CidaRefImplCsharp