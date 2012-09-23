﻿using System.Security.Permissions;
using System.Runtime.InteropServices;
using System.Security;
using System;
using System.Runtime.ConstrainedExecution;

namespace x360ce.App.Win32
{
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public static class NativeMethods
	{

		// Conversion:
		// Handle - IntPtr
		//  DWORD - UInt32
		// PDWORD - UInt32
		// LPVOID - IntPtr

		#region shell32

		[DllImport("shell32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern Boolean IsUserAnAdmin();


		//[DllImport("coredll")]
		//private static extern bool GetFileVersionInfo(string filename, UInt32 handle, UInt32 len, IntPtr buffer);

		//[DllImport("coredll")]
		//private static extern UInt32 GetFileVersionInfoSize(string filename, out UInt32 handle);

		//[DllImport("coredll")]
		//private static extern bool VerQueryValue(IntPtr buffer, string subblock, out IntPtr blockbuffer, out int len);

		#endregion

		#region ole32

		[DllImport("ole32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.Interface)]
		public static extern object CoGetObject(
		   string pszName,
		   [In] ref BIND_OPTS3 pBindOptions,
		   [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid);

		#endregion

		#region user32

		/// <summary>
		/// Sends the specified message to a window or windows.
		/// </summary>
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

		/// <summary>
		/// Sends the specified message to a window or windows.
		/// </summary>
		/// <param name="hWnd"></param>
		/// <param name="Msg"></param>
		/// <param name="wParam"></param>
		/// <param name="lParam"></param>
		/// <returns></returns>
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

		/// <summary>
		/// Registers the device or type of device for which a window will receive notifications.
		/// </summary>
		/// <param name="hRecipient">A handle to the window or service that will receive device events for the devices specified in the NotificationFilter parameter.</param>
		/// <param name="NotificationFilter">A pointer to a block of data that specifies the type of device for which notifications should be sent.</param>
		/// <param name="Flags">This parameter can be one of the following values.</param>
		/// <returns>If the function succeeds, the return value is a device notification handle. If the function fails, the return value is NULL. To get extended error information, call GetLastError.</returns>
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr RegisterDeviceNotification(IntPtr hRecipient, IntPtr NotificationFilter, uint Flags);

		/// <summary>
		/// Closes the specified device notification handle.
		/// </summary>
		/// <param name="Handle">Device notification handle returned by the RegisterDeviceNotification function.</param>
		/// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero. To get extended error information, call GetLastError.</returns>
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern uint UnregisterDeviceNotification(IntPtr Handle);

		/// <summary>
		/// Sends a message to the specified recipients. The recipients can be applications, installable drivers,
		/// network drivers, system-level device drivers, or any combination of these system components. 
		/// </summary>
		/// <param name="dwFlags">The broadcast option.</param>
		/// <param name="pdwRecipients"></param>
		/// <param name="uiMessage"></param>
		/// <param name="wParam"></param>
		/// <param name="lParam"></param>
		/// <returns></returns>
		[DllImport("user32.dll", EntryPoint = "BroadcastSystemMessageA", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
		public static extern int BroadcastSystemMessage(Int32 dwFlags, ref Int32 pdwRecipients, int uiMessage, int wParam, int lParam);

		[DllImport("user32.dll", EntryPoint = "RegisterWindowMessageA", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
		public static extern int RegisterWindowMessage(String pString);



		#endregion

		#region advapi32

		/// <summary>
		/// Opens the access token associated with a process.
		/// </summary>
		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern Boolean OpenProcessToken(IntPtr ProcessHandle, UInt32 DesiredAccess, out IntPtr TokenHandle);

		/// <summary>
		/// The function opens the access token associated with a process.
		/// </summary>
		[DllImport("advapi32", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool OpenProcessToken(IntPtr hProcess, UInt32 desiredAccess, out SafeTokenHandle hToken);

		/// <summary>
		/// Retrieves a specified type of information about an access token.
		/// </summary>
		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetTokenInformation(
			IntPtr TokenHandle,
			TOKEN_INFORMATION_CLASS TokenInformationClass,
			IntPtr TokenInformation,
			UInt32 TokenInformationLength,
			out UInt32 ReturnLength);

		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetTokenInformation(
			SafeTokenHandle hToken,
			TOKEN_INFORMATION_CLASS tokenInfoClass,
			IntPtr pTokenInfo,
			Int32 tokenInfoLength,
			out Int32 returnLength);

		/// <summary>
		/// The function returns a pointer to a specified subauthority in a 
		/// security identifier (SID). The subauthority value is a relative 
		/// identifier (RID).
		/// </summary>
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr GetSidSubAuthority(IntPtr pSid, UInt32 nSubAuthority);

		#endregion

		#region kernel32

		/// <summary>
		/// Retrieves a pseudo handle for the current process.
		/// </summary>
		[SuppressUnmanagedCodeSecurity, DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr GetCurrentProcess();

		/// <summary>
		/// Retrieves the address of an exported function or variable from the specified dynamic-link library (DLL).
		/// </summary>
		[SuppressUnmanagedCodeSecurity, DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr GetProcAddress(IntPtr hModule, [In, MarshalAs(UnmanagedType.LPStr)] string lpProcName);

		/// <summary>
		/// Closes an open object handle.
		/// </summary>
		[return: MarshalAs(UnmanagedType.Bool)]
		[SuppressUnmanagedCodeSecurity, DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool CloseHandle(IntPtr handle);

		/// <summary>
		/// Loads the specified module into the address space of the calling process.
		/// The specified module may cause other modules to be loaded.
		/// </summary>
		[SuppressUnmanagedCodeSecurity, DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr LoadLibrary(string libFilename);

		/// <summary>
		/// Loads the specified module into the address space of the calling process.
		/// The specified module may cause other modules to be loaded.
		/// </summary>
		[SuppressUnmanagedCodeSecurity, SecurityCritical, DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern SafeLibraryHandle LoadLibraryEx(string libFilename, IntPtr reserved, int flags);

		/// <summary>
		/// Frees the loaded dynamic-link library (DLL) module and, if necessary, decrements its reference count.
		/// </summary>
		[return: MarshalAs(UnmanagedType.Bool)]
		[SuppressUnmanagedCodeSecurity, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		public static extern bool FreeLibrary(IntPtr hModule);

		#endregion

	}

}
